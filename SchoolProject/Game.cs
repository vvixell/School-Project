using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SchoolProject
{
    public static class Game
    {
        public static Random rand;
        public static CancellationTokenSource cts;

        public static Player player;

        static void Main(string[] args)
        {
            Start();
        }

        static void Start()
        {
            rand = new Random();
            cts = new CancellationTokenSource();
            player = new Player();
            Thread ListenThread = new Thread(Util.ListenForKeyPress);
            ListenThread.Start();

            Backstory();
            Intro();
            Console.ReadKey();
        }
        public static void Backstory()
        {
            for (int i = 1; i <= 7; i++)
            {
                Dialogue.RollInDialougue(Dialogue.Characters.Narrator, $"BackStory{i}", 10, 2000);
            }

            WaitForKeyPress();
            Util.WipeScreen();
        }

        static void Intro()
        {
            Dialogue.RollInDialougue(Dialogue.Characters.Chief, "Intro-Welcome", 10);
            Dialogue.RollInDialougue(Dialogue.Characters.Chief, "Intro-Name", 10);
            player.stats.Name = Console.ReadLine();
            Console.WriteLine();
            Dialogue.RollInDialougue(Dialogue.Characters.Chief, "Intro-Name2", 10, 1000, player.stats.Name);
            Dialogue.RollInDialougue(Dialogue.Characters.Chief, "Intro-Question", 10);
            ConsoleKey Choice = Dialogue.AskQuestionWithOptions(Dialogue.Characters.Chief, "Intro-Question-Options", new ConsoleKey[] { ConsoleKey.Y, ConsoleKey.N });
            switch (Choice)
            {
                case ConsoleKey.Y:
                    CampIntro();
                    break;
                case ConsoleKey.N:
                    Dialogue.RollInDialougue(Dialogue.Characters.Chief, "Intro-Question-N", 10);
                    Choice = Dialogue.AskQuestionWithOptions(Dialogue.Characters.Chief, "Intro-Question-Options", new ConsoleKey[] { ConsoleKey.Y, ConsoleKey.N });
                    switch (Choice)
                    {
                        case ConsoleKey.Y:
                            Dialogue.RollInDialougue(Dialogue.Characters.Chief, "Intro-Question-N2", 10);
                            CampDecline();
                            break;
                        case ConsoleKey.N:
                            CampIntro();
                            break;
                    }
                    break;
            }
        }

        static void CampIntro()
        {
            WaitForKeyPress();
            Util.WipeScreen();

            for (int i = 1; i <= 6; i++)
            {
                Dialogue.RollInDialougue(Dialogue.Characters.Chief, "CampIntro" + i, 10);
            }

            WaitForKeyPress();
            
            Camp();
        }

        public static void Camp()
        {
            Util.WipeScreen();

            player.Location = "Camp";

            player.DrawStats();

            int CurrentPlace = 0;

            ConsoleKey Input = ConsoleKey.NoName;

            while (Input != ConsoleKey.Enter)
            {
                switch (Input)
                {
                    case ConsoleKey.LeftArrow:
                        CurrentPlace = Util.TrueMod((CurrentPlace - 1), 5);
                        break;
                    case ConsoleKey.RightArrow:
                        CurrentPlace = Util.TrueMod((CurrentPlace + 1), 5);
                        break;
                }
                CampMap.SelectPlace(CurrentPlace + 1);
                Console.SetCursorPosition(5 + CampMap.CampMapArray[0].Length, 7);
                Console.Write("Where would you like to go?");
                Console.SetCursorPosition(5 + CampMap.CampMapArray[0].Length, 8);
                Console.Write("Use Left and Right arrow keys to navigate");
                Console.SetCursorPosition(5 + CampMap.CampMapArray[0].Length, 10);
                Console.Write("{Press Enter to select}");
                Input = Util.GetInput(false, ConsoleKey.Enter, ConsoleKey.LeftArrow, ConsoleKey.RightArrow);
            }
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Wait(100);
            Util.WipeScreen();

            switch (CurrentPlace)
            {
                case 0: //Combat Shop
                    CombatShop.Enter();
                    break;
                case 1: //Healing Campfire
                    HealingCampfire.Enter();
                    break;
                case 2: //Marketplace
                    Marketplace.Enter();
                    break;
                case 3: //Town Hall
                    TownHall.Enter();
                    break;
                case 4: //Forest
                    Forest.Enter();
                    break;
            }
            //Navigate to the chosen place
        }

        static void CampDecline()
        {
            WaitForKeyPress();
            Util.WipeScreen();

            for (int i = 1; i <= 4; i++)
                Dialogue.RollInDialougue(Dialogue.Characters.Narrator, $"WanderOff{i}", 10, 2000);
            Dialogue.WriteDialogue(Dialogue.Characters.Narrator, "WanderOff-Attack1");
            SkillCheck AttackSkillCheck = new SkillCheck(2, 5, SkillCheck.SkillCheckType.Reaction);
            if(AttackSkillCheck.Passed)
            {
                Dialogue.RollInDialougue(Dialogue.Characters.Narrator, "WanderOff-AttackBlock", 10, 2000);
                WaitForKeyPress();
                Util.WipeScreen();

                Monster BushMonster = new Monster(100, 25, false);

                new Battle(player, BushMonster);

                for (int i = 1; i <= 3; i++)
                {
                    Dialogue.RollInDialougue(Dialogue.Characters.Narrator, "WanderOff-Win"+i, 10, 2000);
                }
                End();
            }
            else
            {
                Dialogue.RollInDialougue(Dialogue.Characters.Narrator, "WanderOff-AttackFail1", 10, 2000);
                Dialogue.RollInDialougue(Dialogue.Characters.Narrator, "WanderOff-AttackFail2", 10, 2000);
                Wait(2000);
                Dialogue.WriteDialogue(Dialogue.Characters.Narrator, "WanderOff-AttackFail3");
                Wait(3000);
                Util.WipeScreen();
                player.Die();
            }
        }

        public static void Wait(int ms)
        {
            bool JustWait = false;
            if(Util.CTSDisposed)
            {
                cts = new CancellationTokenSource();
                Util.CTSDisposed = false;
                JustWait = true;
            }
            while (!cts.Token.IsCancellationRequested)
            {
                cts.Token.WaitHandle.WaitOne(ms);
                break;
            }

            if (JustWait)
            {
                Util.CTSDisposed = true;
                cts.Cancel();
                cts.Dispose();
            }
            Util.ClearInputBuffer();
        }

        public static void WaitForKeyPress()
        {
            Thread.Sleep(1000);
            Console.WriteLine("{Press Any Key}");
            Console.ReadKey(true);
            Util.ClearInputBuffer();
        }

        public static void End()
        {
            WaitForKeyPress();
            Environment.Exit(0);
        }

        public static void Complete()
        {
            Util.WipeScreen();

            for (int i = 1; i <= 3; i++)
            {
                Dialogue.RollInDialougue(Dialogue.Characters.Chief, $"End{i}", 10);
            }

            for (int i = 0; i < OtherAscii.TheEnd.Length; i++)
            {
                Console.WriteLine(OtherAscii.TheEnd[i]);
            }

            WaitForKeyPress();
            Environment.Exit(0);
        }
    }
}
