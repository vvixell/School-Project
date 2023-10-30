using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject
{
    static class HealingCampfire
    {
        public static void Enter()
        {
            Game.player.Location = "Campfire";
            Game.player.DrawStats();
            for (int i = 0; i < CampMap.CampFire.Length; i++)
            {
                Console.WriteLine(CampMap.CampFire[i]);
            }
            Console.WriteLine();

            int CursorTop = Console.CursorTop;
            while (true)
            {
                Dialogue.RollInDialougue(Dialogue.Characters.Campfire, "Campfire-Question", 10, 1000);
                Dialogue.RollInDialougue(Dialogue.Characters.Campfire, "Campfire-Options", 10, 1000);

                ConsoleKey Input = Util.GetInput(false, ConsoleKey.D1, ConsoleKey.D2);

                if (Input == ConsoleKey.D2) break;

                Game.Wait(1000);
                Util.ClearFromLine(CursorTop);
                SkillCheck HealthSkillCheck = new SkillCheck(Game.rand.Next(3,7), Game.rand.Next(0,4));

                if (HealthSkillCheck.Passed)
                {
                    int HealedHealth = (int)(0.1 * Game.player.stats.MaxHealth);
                    Dialogue.RollInDialougue(Dialogue.Characters.Campfire, "Campfire-Passed", 10, 200, HealedHealth.ToString());
                    Game.player.stats.Heal(HealedHealth);
                    Game.player.DrawStats(Console.CursorTop);
                }
                else
                {
                    Dialogue.RollInDialougue(Dialogue.Characters.Campfire, "Campfire-Fail", 10, 200);
                }
                Game.Wait(1000);
                Util.ClearFromLine(CursorTop);
            }


            Game.Wait(500);
            Game.Camp();
        }
    }
}
