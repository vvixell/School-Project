using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SchoolProject
{
    static class Marketplace
    {
        static Dialogue.Characters c = Dialogue.Characters.Marketplace;

        public static void Enter()
        {
            Game.player.Location = "Marketplace";
            Game.player.DrawStats();

            Dialogue.RollInDialougue(c, "Welcome", 10, 1000, Game.player.stats.Name);

            while(true)
            {
                Dialogue.RollInDialougue(c, "Options-Question", 10);
                ConsoleKey Input = Dialogue.AskQuestionWithOptions(c, "Options", new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3 });

                if (Input == ConsoleKey.D3) break;

                switch (Input)
                {
                    case ConsoleKey.D1: //Sell Items
                        SellItems();
                        break;
                    case ConsoleKey.D2: //Gamble
                        Gamble();
                        break;
                }
                Util.ClearFromLine(4);
            }

            Dialogue.RollInDialougue(c, "Goodbye", 10);
            Game.Wait(500);
            Game.Camp();
        }

        public static void SellItems()
        {
            if(Game.player.Inventory.Count > 0)
            {
                Game.Wait(250);
                Util.ClearFromLine(4);
                Dialogue.RollInDialougue(c, "ListItems", 10);

                int StartListLine = Console.CursorTop;

                for (int i = 0; i < Game.player.Inventory.Count; i++)
                {
                    Item item = Game.player.Inventory[i].Item;
                    int Amount = Game.player.Inventory[i].Amount;

                    Console.SetCursorPosition(0,StartListLine + i);
                    Console.Write($"{item.Name} - x{Amount} - ${item.Value}");
                }
                Console.SetCursorPosition(0, StartListLine + Game.player.Inventory.Count + 1);
                Console.Write("Go Back");

                int Selected = 0;

                SelectItem(0, 0, StartListLine);

                while (true)
                {
                    ConsoleKey Input = Util.GetInput(true ,ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Enter);

                    switch (Input)
                    {
                        case ConsoleKey.UpArrow:
                            SelectItem(Selected, (int)Util.Clamp(Selected - 1, 0, Game.player.Inventory.Count), StartListLine);
                            Selected = (int)Util.Clamp(Selected - 1, 0, Game.player.Inventory.Count);
                            break;
                        case ConsoleKey.DownArrow:
                            SelectItem(Selected, (int)Util.Clamp(Selected + 1, 0, Game.player.Inventory.Count), StartListLine);
                            Selected = (int)Util.Clamp(Selected + 1, 0, Game.player.Inventory.Count);
                            break;
                        case ConsoleKey.Enter:
                            if (Selected < Game.player.Inventory.Count)
                            {
                                Game.Wait(500);
                                Util.ClearFromLine(4);

                                InventoryItem SelectedItem = Game.player.Inventory[Selected];
                                Dialogue.RollInDialougue(c, "BuyConfirm", 10, 250, SelectedItem.Item.Name, SelectedItem.Item.Value.ToString());
                                ConsoleKey Answer = Dialogue.AskQuestionWithOptions(c, "ConfirmYN", new ConsoleKey[] { ConsoleKey.Y, ConsoleKey.N });

                                if (Answer == ConsoleKey.Y)
                                {
                                    Game.player.Inventory[Selected].Modify(-1);
                                    Game.player.Money += Game.player.Inventory[Selected].Item.Value;
                                    if (Game.player.Inventory[Selected].Amount == 0)
                                        Game.player.Inventory.Remove(Game.player.Inventory[Selected]);

                                    Dialogue.RollInDialougue(c, "BuyThanks", 10); 
                                    Game.player.DrawStats(Console.CursorTop);
                                    Game.Wait(500);
                                    Selected = 0;
                                }

                                SellItems();
                            }
                            break;
                    }
                    if (Selected >= Game.player.Inventory.Count && Input == ConsoleKey.Enter) break;
                }
            }
            else
                Dialogue.RollInDialougue(c, "NoItems", 10);

            Game.Wait(250);
        }

        public static void Gamble()
        {
            Game.Wait(250);
            Util.WipeScreen(4);

            Dialogue.RollInDialougue(c, "Gamble", 10, 250);
            ConsoleKey Input = Dialogue.AskQuestionWithOptions(c, "GameOptions", new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3 });

            switch (Input)
            {
                case ConsoleKey.D1:
                    Roulette();
                    break;
            }
        }

        static int[][] SlotPositions = new int[][] {
            new int[] { 17,1 },
            new int[] { 22,1 },
            new int[] { 27,3 },
            new int[] { 32,5 },
            new int[] { 32,7 },
            new int[] { 27,9 },
            new int[] { 22,11 },
            new int[] { 17,11 },
            new int[] { 12,11 },
            new int[] { 7,9 },
            new int[] { 2,7 },
            new int[] { 2,5 },
            new int[] { 7,3 },
            new int[] { 12,1 }
        };

        static int[] SlotNumbers = { 0, 8, 2, 5, 11, 1, 9, 0, 7, 4, 1, 3, 12, 6 };

        public static void Roulette()
        {
            Game.Wait(250);
            Util.WipeScreen(4);

            bool Playing = true;
            while (Playing)
            {
                Dialogue.RollInDialougue(c, "GambleAmount", 10, 250);

                int GambleAmount = -1;

                while (true)
                {
                    string AmountInput = Console.ReadLine();
                    Console.WriteLine();

                    try
                    {
                        GambleAmount = int.Parse(AmountInput);
                        if (GambleAmount <= Game.player.Money)
                        {
                            break;
                        }
                    }
                    catch (Exception)
                    {

                    }

                    Dialogue.RollInDialougue(c, "InvalidAmount", 10);

                    Game.Wait(1000);
                    Util.WipeScreen(6);
                }

                if(GambleAmount == 0)
                {
                    break;
                }

                Game.player.Money -= GambleAmount;
                Util.WipeScreen(4);
                Game.player.DrawStats();

                int RouletteStartPosition = Console.CursorTop;
                for (int i = 0; i < CampMap.RouletteTable.Length; i++)
                    Console.WriteLine(CampMap.RouletteTable[i]);
                Console.WriteLine();


                int EndCursorPosition = Console.CursorTop;

                for (int i = 0; i < SlotPositions.Length; i++)
                {
                    Console.SetCursorPosition(SlotPositions[i][0] - 1, SlotPositions[i][1] + RouletteStartPosition);
                    if (SlotNumbers[i] == 00)
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                    else if (i % 2 == 0)
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                    else
                        Console.BackgroundColor = ConsoleColor.Black;

                    Console.Write(" ");
                    Console.Write(SlotNumbers[i].ToString("00"));
                    Console.Write(" ");
                }

                Console.ResetColor();
                Console.SetCursorPosition(0, EndCursorPosition + 1);

                Dialogue.RollInDialougue(c, "RouletteBet", 10, 250);
                ConsoleKey Input = Dialogue.AskQuestionWithOptions(c, "RouletteBetOptions", new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2 } );

                Game.Wait(250);
                Util.WipeScreen(EndCursorPosition + 1);

                if(Input == ConsoleKey.D1) //Colour
                {
                    Dialogue.RollInDialougue(c, "ColourBet", 10, 250);
                    Input = Dialogue.AskQuestionWithOptions(c, "ColourBetOptions", new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2 });

                    Game.Wait(500);
                    Util.WipeScreen(EndCursorPosition + 1);
                    bool Black = Input == ConsoleKey.D1;
                    int Roll = SpinRouletteWheel(RouletteStartPosition, EndCursorPosition);
                    

                    if (Roll % 2 != 0 && Roll != 0 && Roll != 7) // Black
                    {
                        if (Black)
                        {
                            Dialogue.RollInDialougue(c, "GambleWin", 10, 250, "2x" );
                            Game.player.Money += 2 * GambleAmount;
                        }
                        else
                            Dialogue.RollInDialougue(c, "GambleLose", 10, 250);
                    }
                    else if(Roll % 2 == 0 && Roll != 0 && Roll != 7) // Red
                    {
                        if (!Black)
                        {
                            Dialogue.RollInDialougue(c, "GambleWin", 10, 250, "2x");
                            Game.player.Money += 2 * GambleAmount;
                        }
                        else
                            Dialogue.RollInDialougue(c, "GambleLose", 10, 250);
                    }
                    else
                    {
                        Dialogue.RollInDialougue(c, "GambleLose", 10, 250);
                    }
                }
                else //Number
                {
                    Dialogue.RollInDialougue(c, "NumberBet", 10, 250);
                    Input = Dialogue.AskQuestionWithOptions(c, "NumberBetOptions", new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3});

                    Game.Wait(500);
                    Util.WipeScreen(EndCursorPosition + 1);
                    if (Input != ConsoleKey.D3)
                    {
                        int Roll = SpinRouletteWheel(RouletteStartPosition, EndCursorPosition);
                        

                        bool Even = Input == ConsoleKey.D1;

                        if (SlotNumbers[Roll] % 2 == 0) // Even
                        {
                            if (Even)
                            {
                                Dialogue.RollInDialougue(c, "GambleWin", 10, 250, "2x");
                                Game.player.Money += 2 * GambleAmount;
                            }
                            else
                                Dialogue.RollInDialougue(c, "GambleLose", 10, 250);
                        }
                        else // Odd
                        {
                            if (!Even)
                            {
                                Dialogue.RollInDialougue(c, "GambleWin", 10, 250, "2x");
                                Game.player.Money += 2 * GambleAmount;
                            }
                            else
                                Dialogue.RollInDialougue(c, "GambleLose", 10, 250);
                        }
                    }
                    else
                    {
                        Dialogue.RollInDialougue(c, "CertainNumber", 10, 250);
                        int Number = -1;
                        while (true)
                        {
                            int Line = Console.CursorTop;
                            string NumberInput = Console.ReadLine();

                            try
                            {
                                Number = int.Parse(NumberInput);
                                if (Number >= 0 && Number <= 12)
                                    break;
                            }
                            catch (Exception)
                            {

                            }

                            Console.SetCursorPosition(0, Line);
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, Line);
                        }

                        Game.Wait(500);
                        Util.WipeScreen(EndCursorPosition + 1);
                        int Roll = SpinRouletteWheel(RouletteStartPosition, EndCursorPosition);
                        
                        if (SlotNumbers[Roll] == Number)
                        {
                            Dialogue.RollInDialougue(c, "GambleWin", 10, 250, "10x");
                            Game.player.Money += 10 * GambleAmount;
                        }
                        else
                            Dialogue.RollInDialougue(c, "GambleLose", 10, 250);
                    }
                }

                Game.player.DrawStats(Console.CursorTop);
                Game.Wait(2000);
                Util.WipeScreen(4);
            }
        }

        static int SpinRouletteWheel(int StartLine, int EndLine)
        {
            int Index = 0;
            int LastIndex = 0;
            int Speed = 1000;
            for (int s = 10; s > 0; s-=2)
            {
                int Rolls = Game.rand.Next(s, s*2);
                for (int r = 0; r < Rolls; r++)
                {
                    if (SlotNumbers[LastIndex] == 0)
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                    else if (LastIndex % 2 == 0)
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                    else
                        Console.BackgroundColor = ConsoleColor.Black;

                    Console.SetCursorPosition(SlotPositions[LastIndex][0] - 1, SlotPositions[LastIndex][1] + StartLine);
                    Console.Write(" ");
                    Console.Write(SlotNumbers[LastIndex].ToString("00"));
                    Console.Write(" ");

                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.SetCursorPosition(SlotPositions[Index][0] - 1, SlotPositions[Index][1] + StartLine);
                    Console.Write(" ");
                    Console.Write(SlotNumbers[Index].ToString("00"));
                    Console.Write(" ");

                    LastIndex = Index;
                    Index = Util.Wrap(Index + 1, 13);
                    Thread.Sleep(1000 / Speed);
                }
                Speed /= 3;
            }
            Console.ResetColor();
            Console.SetCursorPosition(0, EndLine);
            return LastIndex;
        }

        public static void Slots()
        {

        }

        public static void SelectItem(int Index, int NextIndex, int StartLine)
        {
            if (Index >= Game.player.Inventory.Count)
            {
                Console.SetCursorPosition(0, StartLine + Index + 1);
                Console.Write("Go Back      ");
            }
            else
            {
                var Item = Game.player.Inventory[Index];
                Console.SetCursorPosition(0, StartLine + Index);
                Console.Write($"{Item.Item.Name} - x{Item.Amount} - ${Item.Item.Value}      ");
            }


            if (NextIndex >= Game.player.Inventory.Count)
            {
                Console.SetCursorPosition(0, StartLine + NextIndex + 1);
                Console.Write("  ► Go Back ◄");
            }
            else
            {
                var Item = Game.player.Inventory[NextIndex];
                Console.SetCursorPosition(0, StartLine + NextIndex);
                Console.Write($"  ► {Item.Item.Name} - x{Item.Amount} - ${Item.Item.Value} ◄");
            }

            Console.SetCursorPosition(0, StartLine + Game.player.Inventory.Count + 2);
        }
    }
}
/*

          ╔════╦════╦════╗          
          ║    ║    ║    ║           1 12,17,22
     ╔════╬════╩════╩════╬════╗      
     ║    ║              ║    ║      3 7,27
╔════╬════╝              ╚════╬════╗
║    ║                        ║    ║ 5 2,32
╠════╣                        ╠════╣ 
║    ║                        ║    ║ 7 2,32
╠════╣                        ╠════╣
║    ║                        ║    ║ 9 2,32
╚════╬════╗              ╔════╬════╝
     ║    ║              ║    ║      11 7,27
     ╚════╬════╦════╦════╬════╝      
          ║    ║    ║    ║           13 12,17,22
          ╚════╩════╩════╝           

          ╔════╦════╦════╗          
          ║    ║    ║    ║           1 12,17,22
     ╔════╬════╩════╩════╬════╗      
     ║    ║              ║    ║      3 7,27
╔════╬════╝              ╚════╬════╗
║    ║                        ║    ║ 5 2,32
╠════╣                        ╠════╣
║    ║                        ║    ║ 7 2,32
╚════╬════╗              ╔════╬════╝
     ║    ║              ║    ║      9 7,27
     ╚════╬════╦════╦════╬════╝      
          ║    ║    ║    ║           11 12,17,22
          ╚════╩════╩════╝           

17,1
22,1
27,3
32,5
32,7
27,9
22,11
17,11
13,11
7,9
2,7
2,5
7,3
12,1

          ╔════╦════╦════╗
       ╔╦╦╣ 15 ║  0 ║  1 ╠╦╦╗
     ╔═╩╩╩╬╦╦╦╦╩════╩╦╦╦╦╬╩╩╩═╗
  ╔╦╦╣ 14 ╠╩╩╩╝      ╚╩╩╩╣  2 ╠╦╦╗
╔═╩╩╩╬╦╦══╝              ╚══╦╦╬╩╩╩═╗
║ 13 ╠╬╝                    ╚╬╣  3 ║
╠════╬╝                      ╚╬════╣
║ 12 ║                        ║  4 ║
╠════╬╗                      ╔╬════╣
║ 11 ╠╬╗                    ╔╬╣  5 ║
╚═╦╦╦╬╩╩══╗              ╔══╩╩╬╦╦╦═╝
  ╚╩╩╣ 10 ╠╦╦╦╗      ╔╦╦╦╣  6 ╠╩╩╝
     ╚═╦╦╦╬╩╩╩╩╦════╦╩╩╩╩╬╦╦╦═╝
       ╚╩╩╣  9 ║  8 ║  7 ╠╩╩╝
          ╚════╩════╩════╝

*/