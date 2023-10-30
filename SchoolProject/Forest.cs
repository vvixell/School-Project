using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject
{
    static class Forest
    {
        public static string[] Options = { "Back to camp", "Shadowed Plateau", "Carlsbad Caverns (Boss)", "Mammoth Cave [Locked]", "Luray Caverns [Locked]" };

        public static void Enter()
        {
            Game.player.Location = "Forest";
            Game.player.DrawStats();

            for (int i = 0; i < ForestAscii.SignAscii.Length; i++)
            {
                Console.WriteLine(ForestAscii.SignAscii[i]);
            }

            int Selected = 0;
            //Mammoth Cave (Boss)
            //Luray Caverns (Boss)
            while (true)
            {
                ConsoleKey input = Util.GetInput(true, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Enter);
                int lastSelected = Selected;

                if (input == ConsoleKey.Enter)
                {
                    if (Selected == 0 || Selected == 1 || Selected == 2)
                        break;
                    else if (Selected == 3 && Cave1.Completed)
                        break;
                    else if (Selected == 4 && Cave2.Completed)
                        break;
                }

                switch (input)
                {
                    case ConsoleKey.UpArrow:
                        Selected = Util.Wrap(Selected - 1, 5);
                        break;
                    case ConsoleKey.DownArrow:
                        Selected = Util.Wrap(Selected + 1, 5);
                        break;
                }

                Console.SetCursorPosition(3, 7 + 2 * lastSelected);
                Console.Write($"{Options[lastSelected]}    ");

                Console.SetCursorPosition(3, 7 + 2 * Selected);
                Console.Write($"► {Options[Selected]} ◄");
            }
            Console.SetCursorPosition(0, 4 + ForestAscii.SignAscii.Length);
            switch (Selected)
            {
                case 1:
                    ShadowedPlateau.Enter();
                    break;
                case 2:
                    Cave1.Enter();
                    break;
                case 3:
                    if (Cave1.Completed) Cave2.Enter();
                    break;
                case 4:
                    if (Cave1.Completed) Cave3.Enter();
                    break;
            }

            Game.Camp();
        }
    }

    static class Cave1 //Carlsbad Caverns
    {
        public static bool Completed = false;

        public static void Enter()
        {
            Util.WipeScreen();
            Game.player.Location = "Carlsbad Caverns";
            Game.player.DrawStats();

            if (Completed)
                Dialogue.RollInDialougue(Dialogue.Characters.Narrator, "CaveCompleted", 10);
            else
            {
                for (int i = 1; i <= 5; i++)
                {
                    Dialogue.RollInDialougue(Dialogue.Characters.Narrator, $"Cave1-{i}", 10, 1500);
                }

                ConsoleKey Input = Dialogue.AskQuestionWithOptions(Dialogue.Characters.Narrator, "YN", new ConsoleKey[] { ConsoleKey.Y, ConsoleKey.N });

                if (Input == ConsoleKey.Y)
                {
                    Game.Wait(1000);
                    Util.WipeScreen();

                    Monster Boss = new Monster(210, 50, true, true);
                    Boss.stats.Name = "Carlsbad Caverns Boss";
                    Battle battle = new Battle(Game.player, Boss);

                    Completed = true;
                    Forest.Options[2] = "Carlsbad Caverns (Completed)";
                    Forest.Options[3] = "Mammoth Cave (Boss)";
                }
            }

            Game.Wait(1000);
            Util.WipeScreen();
        }
    }

    static class Cave2 //Mammoth Cave
    {
        public static bool Completed = false;

        public static void Enter()
        {
            Util.WipeScreen();
            Game.player.Location = "Mammoth Cave";
            Game.player.DrawStats();

            if (Completed)
                Dialogue.RollInDialougue(Dialogue.Characters.Narrator, "CaveCompleted", 10);
            else
            {
                for (int i = 1; i <= 5; i++)
                {
                    Dialogue.RollInDialougue(Dialogue.Characters.Narrator, $"Cave2-{i}", 10, 1500);
                }

                ConsoleKey Input = Dialogue.AskQuestionWithOptions(Dialogue.Characters.Narrator, "YN", new ConsoleKey[] { ConsoleKey.Y, ConsoleKey.N });

                if (Input == ConsoleKey.Y)
                {
                    Game.Wait(1000);
                    Util.WipeScreen();

                    Monster Boss = new Monster(470, 100, true, true);
                    Boss.stats.Name = "Mammoth Cave Boss";
                    Battle battle = new Battle(Game.player, Boss);

                    Completed = true;

                    Forest.Options[3] = "Mammoth Cave (Completed)";
                    Forest.Options[4] = "Luray Caverns (Boss)";
                }
            }

            Game.Wait(1000);
            Util.WipeScreen();
        }
    }

    static class Cave3 //Luray Caverns
    {
        public static bool Completed = false;

        public static void Enter()
        {
            Util.WipeScreen();
            Game.player.Location = "Luray Caverns";
            Game.player.DrawStats();

            if (Completed)
                Dialogue.RollInDialougue(Dialogue.Characters.Narrator, "CaveCompleted", 10);
            else
            {
                for (int i = 1; i <= 5; i++)
                {
                    Dialogue.RollInDialougue(Dialogue.Characters.Narrator, $"Cave3-{i}", 10, 1500);
                }

                ConsoleKey Input = Dialogue.AskQuestionWithOptions(Dialogue.Characters.Narrator, "YN", new ConsoleKey[] { ConsoleKey.Y, ConsoleKey.N });

                if (Input == ConsoleKey.Y)
                {
                    Game.Wait(1000);
                    Util.WipeScreen();

                    Monster Boss = new Monster(625, 150, true, true);
                    Boss.stats.Name = "Luray Caverns Boss";
                    Battle battle = new Battle(Game.player, Boss);

                    Completed = true;
                    Forest.Options[4] = "Luray Caverns (Completed)";
                }
            }

            Game.Wait(1000);
            Util.WipeScreen();
        }
    }

    static class ShadowedPlateau
    {
        static bool CompletedIntro = false;

        public static void Enter()
        {
            Util.WipeScreen();
            Game.player.Location = "Shadowed Plateau";
            Game.player.DrawStats();

            if(!CompletedIntro)
            {
                for (int i = 1; i <= 3; i++)
                {
                    Dialogue.RollInDialougue(Dialogue.Characters.Narrator, $"Plateau-Intro-{i}", 10, 1000);
                }

                CompletedIntro = true;
            }
            Dialogue.RollInDialougue(Dialogue.Characters.Narrator, $"Plateau-Intro-4", 10, 1000);
            ConsoleKey Input = Dialogue.AskQuestionWithOptions(Dialogue.Characters.Narrator, "YN", new ConsoleKey[] { ConsoleKey.Y, ConsoleKey.N });
            Game.Wait(500);
            if (Input == ConsoleKey.Y)
            {
                while (true)
                {
                    Monster monster = new Monster(
                        (int)(Game.player.stats.MaxHealth * (Game.rand.Next(50, 100) / 100f)),
                        (int)(Game.player.stats.Strength * (Game.rand.Next(60, 120) / 100f)));

                    new Battle(Game.player, monster); 

                    Dialogue.RollInDialougue(Dialogue.Characters.Narrator, $"Plateau-Question", 10, 1000);
                    Input = Dialogue.AskQuestionWithOptions(Dialogue.Characters.Narrator, "YN", new ConsoleKey[] { ConsoleKey.Y, ConsoleKey.N });
                    if (Input == ConsoleKey.N) break;
                }
            }

            Game.Wait(1000);
            Util.WipeScreen();
        }
    }
}
