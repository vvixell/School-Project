using System;

namespace SchoolProject
{
    public static class Shades
    {
        public static char ShadeLight = '░';
        public static char ShadeMedium = '▒';
        public static char ShadeDark = '▓';
        public static char ShadeFull = '█';
        public static char[] ShadesList = { '█', '▓', '▒', '░', ' ' };
    }

    public static class Box
    {
        public static char Corner = '╬';
        public static char Horizontal = '═';
        public static char Vertical = '║';
        public static char Left = '╠';
        public static char Right = '╣';
    }

    public static class OtherAscii
    {
        public static char SkillCheckBoxLeft = '>';
        public static char SkillCheckBoxRight = '<';
        public static string[] SelectionStrings =
        {
            @" ► ",
            @" ◄ "
        };
        public static string[] YouDied =
{
            "╬══════════════════════════════════════════════════════════╬",
            "║ ▓██   ██▓ ▒█████   █    ██    ▓█████▄  ██▓▓█████ ▓█████▄ ║",
            "║  ▒██  ██▒▒██▒  ██▒ ██  ▓██▒   ▒██▀ ██▌▓██▒▓█   ▀ ▒██▀ ██▌║",
            "║   ▒██ ██░▒██░  ██▒▓██  ▒██░   ░██   █▌▒██▒▒███   ░██   █▌║",
            "║   ░ ▐██▓░▒██   ██░▓▓█  ░██░   ░▓█▄   ▌░██░▒▓█  ▄ ░▓█▄   ▌║",
            "║   ░ ██▒▓░░ ████▓▒░▒▒█████▓    ░▒████▓ ░██░░▒████▒░▒████▓ ║",
            "╬════██▒▒▒═░═▒░▒░▒░═░▒▓▒═▒═▒═════▒▒▓══▒═░▓══░░═▒░═░═▒▒▓══▒═╬",
            "   ▓██ ░▒░   ░ ▒ ▒░ ░░▒░ ░ ░     ░ ▒  ▒  ▒ ░ ░ ░  ░ ░ ▒  ▒  ",
            "   ▒ ▒ ░░  ░ ░ ░ ▒   ░░░ ░ ░     ░ ░  ░  ▒ ░   ░    ░ ░  ░  ",
            "   ░ ░         ░ ░     ░           ░     ░     ░  ░   ░     ",
            "   ░ ░                           ░                  ░       "
        };

        public static string[] TheEnd =
        {
            "████████╗██╗  ██╗███████╗    ███████╗███╗   ██╗██████╗ ",
            "╚══██╔══╝██║  ██║██╔════╝    ██╔════╝████╗  ██║██╔══██╗",
            "   ██║   ███████║█████╗      █████╗  ██╔██╗ ██║██║  ██║",
            "   ██║   ██╔══██║██╔══╝      ██╔══╝  ██║╚██╗██║██║  ██║",
            "   ██║   ██║  ██║███████╗    ███████╗██║ ╚████║██████╔╝",
            "   ╚═╝   ╚═╝  ╚═╝╚══════╝    ╚══════╝╚═╝  ╚═══╝╚═════╝ "
        };
    }

    public static class CampMap
    {
        public static string[] CampMapArray =
        {
            @"         ╔═══════╗                                                  ",
            @"       ╔═╝╣Store╠╚═╗                          ╔╦═══╦╗               ",
            @"       ╚╬═════════╬╝                       ╔══╩╝(3)╚╩══╗            ",
            @"        ║(1)Combat║                       ╔╣Marketplace╠╗           ",
            @"        ║  Store  ║                      ═╩╬═══════════╬╩═          ",
            @"        ╚═════════╝                        ║           ║            ",
            @"             # *#                          ║           ║            ",
            @"              #*#*##                      ╔╩╦══╦═══╦══╦╩╗           ",
            @"                ##* ##                    ║ ║  ║   ║  ║ ║           ",
            @"                  ### ╬══════════╬  ##    ╚═╩══╩═══╩══╩═╝           ",
            @"                      ║(2)Healing║ ##*###       ###                 ",
            @"                      ║ Campfire ║ # ####*####****#                 ",
            @"                      ╬══════════╬       ##**####                   ",
            @"                         * ##                           ╬           ",
            @"             ╔╗         ####                         ╬ ╬╬╬          ",
            @"            ╔╩╬╗        ###                         ╬╬╬╬╬╬   ╬      ",
            @"           ╔╩╦╩╩╗      #*#         ┌───┬───┐      ╬ ╬╬╬ ║ ╬ ╬╬╬     ",
            @"          ═╬═╩══╬═    #**## #      │(5)└─► │  ╬  ╬╬╬ ║ ##╬╬╬╬╬╬     ",
            @"        ╔══╣ ╔╗ ║      #*##**##    │Forrest│ ╬╬╬ ╬╬╬  *~ ╬╬╬ ║ ╬    ",
            @"    ╔══╦╩═╦╣ ╚╝ ║      *##  ##**   └───┬───┘ ╬╬╬  ║ *~*   ║ ╬ ╬╬╬   ",
            @"   ═╬══╩══╩╩════╣     #*#     ####     │    ╬ ║ ╬   #~  ╬  ╬╬╬╬╬╬   ",
            @"    ║(4)Town ╔╗ ╠  ##**#        **#    │   ╬╬╬ ╬╬╬ *## ╬╬╬ ╬╬╬ ║    ",
            @"    ║  Hall  ╚╝ ╬╗##*#            ####     ╬╬╬ ╬╬╬ ~#  ╬╬╬  ║       ",
            @"    ╬═══════════╬╩╗                 # #     ║   ║  ##~* ║           "
        };

        public static string[] CampFire =
        {
@"╦ ╦┌─┐┌─┐┬  ┬┌┐┌┌─┐  ╔═╗┌─┐┌┬┐┌─┐┌─┐┬┬─┐┌─┐",
@"╠═╣├┤ ├─┤│  │││││ ┬  ║  ├─┤│││├─┘├┤ │├┬┘├┤ ",
@"╩ ╩└─┘┴ ┴┴─┘┴┘└┘└─┘  ╚═╝┴ ┴┴ ┴┴  └  ┴┴└─└─┘",
@"                 .     *                   ",
@"                  - *%#*                   ",
@"                  ** =.=                   ",
@"               : *%+- -*  =                ",
@"             **=+*: %  =-%*                ",
@"              ===   =  : =#                ",
@"              .  - %.% %% %%               ",
@"          :.+= %@@*@ @@@@%#%-*%            ",
@"       .*-*-%%@##%+@*%**%+@@@-:%@%*        ",
@"      *@@@%%.@@@@@@@%@@@@@@   =-:@*        ",
@"       :%+%    %- @@ %%    : .@@@@%%*      ",
@"      :*%%@@@@:@=#   %:*%@@@@@@@#.         ",
@"        ***:= %%@@@@%*%@@@%*               "
        };

        public static string[] RouletteTable =
        {
            "          ╔════╦════╦════╗          ",
            "          ║    ║    ║    ║          ",
            "     ╔════╬════╩════╩════╬════╗     ",
            "     ║    ║              ║    ║     ",
            "╔════╬════╝              ╚════╬════╗",
            "║    ║                        ║    ║",
            "╠════╣                        ╠════╣",
            "║    ║                        ║    ║",
            "╚════╬════╗              ╔════╬════╝",
            "     ║    ║              ║    ║     ",
            "     ╚════╬════╦════╦════╬════╝     ",
            "          ║    ║    ║    ║          ",
            "          ╚════╩════╩════╝           "
        };

        public static void DrawMap()
        {
            int CurrentCursorPos = 4;
            Console.SetCursorPosition(0, CurrentCursorPos);
            CurrentCursorPos++;

            Console.Write($"╬═{new string('═', CampMapArray[0].Length)}═╬");
            for (int i = 0; i < CampMapArray.Length; i++)
            {
                Console.SetCursorPosition(0, CurrentCursorPos + i);
                Console.Write($"║ {CampMapArray[i]} ║");
            }
            Console.SetCursorPosition(0, CurrentCursorPos + CampMapArray.Length);
            Console.Write($"╬═{new string('═', CampMapArray[0].Length)}═╬");
        }

        public static void SelectPlace(int place)
        { 
            DrawMap();
            Console.ForegroundColor = ConsoleColor.Red;
            switch (place)
            {
                case 1:
                    for (int i = 7; i <= 9; i++)
                    {
                        Console.SetCursorPosition(5, i);
                        Console.Write(OtherAscii.SelectionStrings[0]);
                    }

                    for (int i = 7; i <= 9; i++)
                    {
                        Console.SetCursorPosition(23, i);
                        Console.Write(OtherAscii.SelectionStrings[1]);
                    }
                    break;
                case 2:
                    for (int i = 15; i <= 16; i++)
                    {
                        Console.SetCursorPosition(20, i);
                        Console.Write(OtherAscii.SelectionStrings[0]);
                    }

                    for (int i = 15; i <= 16; i++)
                    {
                        Console.SetCursorPosition(37, i);
                        Console.Write(OtherAscii.SelectionStrings[1]);
                    }
                    break;
                case 3:
                    for (int i = 9; i <= 11; i++)
                    {
                        Console.SetCursorPosition(39, i);
                        Console.Write(OtherAscii.SelectionStrings[0]);
                    }

                    for (int i = 9; i <= 11; i++)
                    {
                        Console.SetCursorPosition(61, i);
                        Console.Write(OtherAscii.SelectionStrings[1]);
                    }

                    break;
                case 4:
                    for (int i = 22; i <= 25; i++)
                    {
                        Console.SetCursorPosition(1, i);
                        Console.Write(OtherAscii.SelectionStrings[0]);
                    }

                    for (int i = 22; i <= 25; i++)
                    {
                        Console.SetCursorPosition(21, i);
                        Console.Write(OtherAscii.SelectionStrings[1]);
                    }
                    break;
                case 5:

                    for (int i = 21; i <= 24; i++)
                    {
                        Console.SetCursorPosition(33, i);
                        Console.Write(OtherAscii.SelectionStrings[0]);
                    }

                    for (int i = 21; i <= 24; i++)
                    {
                        Console.SetCursorPosition(47, i);
                        Console.Write(OtherAscii.SelectionStrings[1]);
                    }
                    break;
            }

            Console.ResetColor();
        }
    }

    public static class ForestAscii 
    {
        public static string[] SignAscii =
        {
            @"  __________________________________________________________  ",
            @" //--------------------------------------------------------\\ ",
            @"//                                                          \\",
            @"|| ► Back to Camp ◄                 ╬═════════════════╬     ||",
            @"||                                  ║ Use ▲ & ▼ Arrow ║     ||",
            @"|| Shadowed Plateau                 ║  Keys to move.  ║     ||",
            @"||                                  ║ Press Enter to  ║     ||",
            @"|| Carlsbad Caverns (Boss)          ║  select choice. ║     ||",
            @"||                                  ╬═════════════════╬     ||",
            @"|| Mammoth Cave [Locked]                                    ||",
            @"||                                                          ||",
            @"|| Luray Caverns [Locked]                                   ||",
            @"\\__________________________||  ||__________________________//",
            @" \--------------------------\\  //--------------------------/ ",
            @"                             ||||                             ",
            @"                             ||||                             "
        };
    }
    public static class CharactersAscii
    {
        public static string[] PlayerAscii =
        {
            @"  O  ",
            @" /|\ ",
            @"/ | \",
            @" / \ ",
            @"/   \"
        };

        public static string[][] MonsterAscii =
        {
            new string[]{
                @" ⌐[]¬ ",
                @"\_||_/",
                @"\_{}_/",
                @" /  \ ",
                @" |  | "
            },
            new string[]{
                @" [Ø] ",
                @"/-|-\",
                @"| | |",
                @" / \ ",
                @" | | "
            },
            new string[]{
                @"  Ä  ",
                @"|[#]|",
                @"+ V +",
                @" / \ ",
                @" | | "
            },

            new string[]{
                @"  (Θ) ",
                @"/╩{#}╩\",
                @"╫ |_| ╫",
                @" // \\ ",
                @" ║   ║ "
            }
        };

    }
}