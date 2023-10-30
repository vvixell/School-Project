using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SchoolProject
{
    class Battle
    {
        public Player player;
        public Monster monster;
        int TurnOrder; //0 - Player First , 1 - Monster First

        public Battle(Player _player, Monster _monster, int Order = 0)
        {
            player = _player;
            monster = _monster;
            TurnOrder = Order;

            DrawBattle();
        }

        public int PStatsX, PStatsY;
        public int MStatsX, MStatsY;

        void DrawBattle()
        {
            //Draw Player
            Util.WipeScreen();
            int startLine = 3;
            Console.SetCursorPosition(10, 1);
            Console.Write("[You]");
            for (int i = 0; i < 5; i++)
            {
                Console.SetCursorPosition(10, startLine + i);
                Console.Write(CharactersAscii.PlayerAscii[i]);
            }

            //Draw Monster
            string [] MonsterAscii = CharactersAscii.MonsterAscii[(int)monster.type];
            int NameStartX = 30 + (int)(MonsterAscii[0].Length / 2) - (int)(monster.stats.Name.Length / 2);
            Console.SetCursorPosition(NameStartX, 1);
            Console.Write($"[{monster.stats.Name}]");
            for (int i = 0; i < 5; i++)
            {
                Console.SetCursorPosition(30, startLine + i);
                Console.Write(MonsterAscii[i]);
            }

            PStatsX = 6;
            PStatsY = 9;
            MStatsX = 26;
            MStatsY = 9;

            Console.SetCursorPosition(PStatsX, PStatsY - 1);
            Console.Write($"╔═══════════╗");
            Console.SetCursorPosition(PStatsX, PStatsY);
            Console.Write($"║ ♥    ‼    ║");
            Console.SetCursorPosition(PStatsX, PStatsY + 1);
            Console.Write($"╚═══════════╝");

            Console.SetCursorPosition(MStatsX, MStatsY - 1);
            Console.Write($"╔═══════════╗");
            Console.SetCursorPosition(MStatsX, MStatsY);
            Console.Write($"║ ♥    ‼    ║");
            Console.SetCursorPosition(MStatsX, MStatsY + 1);
            Console.Write($"╚═══════════╝");

            UpdateStats();
            Console.SetCursorPosition(0, 12);
            Console.WriteLine("__________________________________________________");

            Turns();
        }

        void UpdateStats() // CHANGE THIS TO UPDATE THE WHOLE STRING NOT JUST HOW LONG THE HEALTH STRING IS
        {
            int CurrentLine = Console.CursorTop;
            Console.SetCursorPosition(PStatsX, PStatsY);
            Console.Write($"║ ♥    ‼    ║");
            Console.SetCursorPosition(PStatsX + 6 - player.stats.Health.ToString().Length, PStatsY);
            Console.Write(player.stats.Health);
            Console.SetCursorPosition(PStatsX + 11 - player.stats.Strength.ToString().Length, PStatsY);
            Console.Write(player.stats.Strength);

            Console.SetCursorPosition(MStatsX, MStatsY);
            Console.Write($"║ ♥    ‼    ║");
            Console.SetCursorPosition(MStatsX + 6 - monster.stats.Health.ToString().Length, MStatsY);
            Console.Write(monster.stats.Health);
            Console.SetCursorPosition(MStatsX + 11 - monster.stats.Strength.ToString().Length, MStatsY);
            Console.Write(monster.stats.Strength);

            Console.SetCursorPosition(0, CurrentLine);
        }
       
        void Turns()
        {
            while(true)
            {
                if (TurnOrder == 0)
                    PlayerAttack();
                else
                    MonsterAttack();
                UpdateStats();
                
                if(monster.stats.Health <= 0)
                {
                    Util.ClearFromLine(14);
                    Dialogue.RollInDialougue(Dialogue.Characters.Battle, "Battle-Win", 10, 1000, monster.stats.Name);
                    monster.Die();
                    Game.Wait(1500);
                    break;
                }
                if (player.stats.Health <= 0)
                {
                    Game.Wait(1500);
                    Util.WipeScreen();
                    player.Die($"Lost in battle to a {monster.stats.Name}");
                }

                TurnOrder = (TurnOrder + 1) % 2;
            }

            Game.Wait(1000);
            Util.WipeScreen();
        }

        void MonsterAttack()
        {
            Console.SetCursorPosition(0, 14);
            int Damage = (int)((Game.rand.Next(75, 126) / 100f) * monster.stats.Strength);
            if (reflect)
            {
                Damage = (int)(Damage * Util.Clamp((float)player.stats.Strength / monster.stats.Strength,0,1));
                Dialogue.RollInDialougue(Dialogue.Characters.Battle, "Battle-Reflect", 1, 500, Damage.ToString());
                monster.stats.TakeDamage(Damage);
                player.stats.Heal((int)(Damage * player.stats.LeechPercent / 100f));
            }
            else if (dodge)
            {
                int Amount = (int)(Game.rand.Next(75, 126) / 100f * player.stats.Strength);
                Dialogue.RollInDialougue(Dialogue.Characters.Battle, "Battle-Heal", 1, 500, Amount.ToString());
                player.stats.Heal(Amount);
            }
            else
            {
                Dialogue.RollInDialougue(Dialogue.Characters.Battle, "Battle-Monster-Attack", 1, 500, Damage.ToString());
                player.stats.TakeDamage(Damage);
            }
            reflect = false;
            dodge = false;
            Game.Wait(2000);
            int Top = Console.CursorTop;
            for (int i = 14; i <= Top; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', 100));
            }
        }

        bool reflect = false, dodge = false;

        void PlayerAttack()
        {
            Console.SetCursorPosition(0, 14);
            Dialogue.RollInDialougue(Dialogue.Characters.Battle, "Battle-Question", 1, 500);
            ConsoleKey Choice = Dialogue.AskQuestionWithOptions(Dialogue.Characters.Battle, "Battle-Question-Options", new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3 });
            Game.Wait(1000);
            switch (Choice)
            {
                case ConsoleKey.D1: //Attack
                    SkillCheck AttackSkillCheck = new SkillCheck(2, 3, SkillCheck.SkillCheckType.Reaction);
                    if (AttackSkillCheck.Passed)
                    {
                        int Damage = (int)(Game.rand.Next(75, 126) / 100f * player.stats.Strength);
                        Dialogue.RollInDialougue(Dialogue.Characters.Battle, "Battle-Attack", 1, 500, Damage.ToString());
                        monster.stats.TakeDamage(Damage);
                        player.stats.Heal((int)(Damage * player.stats.LeechPercent / 100f));
                    }
                    else
                    {
                        Dialogue.RollInDialougue(Dialogue.Characters.Battle, "Battle-Attack-Fail", 1, 500);
                    }
                        
                    break;
                case ConsoleKey.D2: //Heal
                    SkillCheck HealSkillCheck = new SkillCheck(3, 2, SkillCheck.SkillCheckType.Reaction);
                    if (HealSkillCheck.Passed)
                    {
                        dodge = true;
                    }
                    else
                    {
                        Dialogue.RollInDialougue(Dialogue.Characters.Battle, "Battle-Heal-Fail", 1, 500);
                    }
                    break;
                case ConsoleKey.D3: //Reflect
                    SkillCheck ReflectSkillCheck = new SkillCheck(10 , 1, SkillCheck.SkillCheckType.Reaction, true);
                    if (ReflectSkillCheck.Passed)
                    {
                        reflect = true;
                    }
                    else
                    {
                        Dialogue.RollInDialougue(Dialogue.Characters.Battle, "Battle-Reflect-Fail", 1, 500);
                    }
                    break;
            }
            Game.Wait(2000);
            int Top = Console.CursorTop;
            for (int i = 14; i <= Top; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', 100));
            }
        }
    }
}
