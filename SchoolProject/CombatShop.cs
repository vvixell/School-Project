using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject
{
    static class CombatShop
    {
        public static BoostItem[] HealthItems =
        {
            new BoostItem("Upgrade Max Health", "+20 Max Health", 25, 20),
            new BoostItem("Upgrade Health Leach", "+3% Health leach from enemies", 25, 3)
        };

        public static BoostItem[] StrengthItems =
        {
            new BoostItem("Sharpen Sword", $"+10 Strength", 15, 10),
            new BoostItem("Upgrade Sword", $"+50 Strength", 60, 50)
        };

        public static void Enter()
        {
            Game.player.Location = "Combat Shop";
            Game.player.DrawStats();
            for (int i = 1; i < 2; i++)
            {
                Dialogue.RollInDialougue(Dialogue.Characters.CombatShop, "CombatShop-Welcome" + i, 10, 1000, Game.player.stats.Name);
            }

            while(true)
            {
                Dialogue.RollInDialougue(Dialogue.Characters.CombatShop, "CombatShop-OptionsQuestion", 10, 0);
                ConsoleKey Input = Dialogue.AskQuestionWithOptions(Dialogue.Characters.CombatShop, "CombatShop-Options", new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3 });
                
                if (Input == ConsoleKey.D3) break;
                Game.Wait(300);
                Util.WipeScreen(4);

                switch (Input)
                {
                    case ConsoleKey.D1: //Health
                        Dialogue.RollInDialougue(Dialogue.Characters.CombatShop, "CombatShop-Health", 10, 0);
                        DisplayOffers(HealthItems);
                        Input = Util.GetInput(false, ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3);
                        switch (Input)
                        {
                            case ConsoleKey.D1:
                                Purchase(HealthItems[0]);
                                break;
                            case ConsoleKey.D2:
                                Purchase(HealthItems[1]);
                                break;
                        }
                        Game.Wait(300);
                        Util.WipeScreen(0);
                        Game.player.DrawStats();
                        break;
                    case ConsoleKey.D2: //Strength
                        Dialogue.RollInDialougue(Dialogue.Characters.CombatShop, "CombatShop-Strength",10, 0);
                        DisplayOffers(StrengthItems);
                        Input = Util.GetInput(false, ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3);
                        switch (Input)
                        {
                            case ConsoleKey.D1:
                                Purchase(StrengthItems[0]);
                                break;
                            case ConsoleKey.D2:
                                Purchase(StrengthItems[1]);
                                break;
                        }
                        Game.Wait(300);
                        Util.WipeScreen(0);
                        Game.player.DrawStats();
                        break;
                }
            }
            
            Dialogue.RollInDialougue(Dialogue.Characters.CombatShop, "CombatShop-Leave", 10);
            Util.ClearInputBuffer();
            Game.Wait(500);
            Game.Camp();
        }

        public static void DisplayOffers(BoostItem[] Items)
        {
            Console.WriteLine();
            for (int i = 0; i < Items.Length; i++)
            {
                Console.WriteLine($"    ({i + 1}) {Items[i].Name} | {Items[i].Description} | ${Items[i].Price}");
            }
            Console.WriteLine($"    ({Items.Length + 1}) Go back");
            Console.WriteLine();
        }

        public static void Purchase(BoostItem Item)
        {
            Game.Wait(500);
            Util.WipeScreen(4);

            Dialogue.RollInDialougue(Dialogue.Characters.CombatShop, "CombatShop-Confirm", 10, 0, Item.Name, Item.Price.ToString());
            ConsoleKey Input = Dialogue.AskQuestionWithOptions(Dialogue.Characters.CombatShop, "CombatShop-ConfirmOptions", new ConsoleKey[] { ConsoleKey.Y, ConsoleKey.N });

            if(Input == ConsoleKey.Y)
            {
                if (Item.Price <= Game.player.Money)
                {
                    Dialogue.RollInDialougue(Dialogue.Characters.CombatShop, "CombatShop-Thanks", 10);

                    if (HealthItems[0].Equals(Item))
                        Game.player.stats.MaxHealth += Item.BoostAmount;
                    else if (HealthItems[1].Equals(Item))
                        Game.player.stats.LeechPercent += Item.BoostAmount;
                    else if (StrengthItems[0].Equals(Item))
                        Game.player.stats.Strength += Item.BoostAmount;
                    else
                        Game.player.stats.Strength += Item.BoostAmount;

                    Game.player.Money -= Item.Price;

                    Item.Price += (int)(Item.Price / 3);
                }
                else
                {
                    Dialogue.RollInDialougue(Dialogue.Characters.CombatShop, "CombatShop-Fail", 10);
                }
            }
        }
    }

    class BoostItem
    {
        public string Name;
        public string Description;
        public int Price;
        public int BoostAmount;

        public BoostItem(string _Name, string _Description, int _Price, int _BoostAmount)
        {
            Name = _Name;
            Description = _Description;
            Price = _Price;
            BoostAmount = _BoostAmount;
        }
    }
}


