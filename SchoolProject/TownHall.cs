using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject
{
    static class TownHall
    {
        public static void Enter()
        {
            Game.player.DrawStats();
            
            Dialogue.Characters c = Dialogue.Characters.Chief;
            Dialogue.RollInDialougue(c, "TownHall-Welcome", 10, 500, Game.player.stats.Name);

            while(true)
            {
                Dialogue.RollInDialougue(c, "TownHall-Question", 10);

                ConsoleKey Input = Dialogue.AskQuestionWithOptions(c, "TownHall-Question-Options", new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3 });

                if (Input == ConsoleKey.D3) break;

                switch (Input)
                {
                    case ConsoleKey.D1:
                        for (int i = 1; i <= 3; i++)
                            Dialogue.RollInDialougue(c, $"TownHall-HelpStart{i}", 10);
                        Game.WaitForKeyPress();
                        break;
                    case ConsoleKey.D2:
                        Dialogue.RollInDialougue(c, "TownHallStats", 10, 500,
                            Game.player.stats.MaxHealth.ToString(),
                            Game.player.stats.Strength.ToString(),
                            Game.player.stats.LeechPercent.ToString(),
                            Game.player.MonstersKilled.ToString());
                        Game.WaitForKeyPress();
                        break;
                }

                Util.WipeScreen(4);
            }

            Game.Wait(500);
            Game.Camp();
        }
    }
}
