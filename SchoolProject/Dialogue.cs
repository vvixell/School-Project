using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Threading;


namespace SchoolProject
{
    class Dialogue
    {
        public enum Characters
        {
            Narrator,
            Chief,
            Battle,
            CombatShop,
            Campfire,
            Marketplace
        }

        #region Narrator

        public static Dictionary<string, string> NarratorDialogue = new Dictionary<string, string>
        {
            { "BackStory1","In the heart of the medieval age, an eerie silence blankets the land." },
            { "BackStory2","Suddenly,# the earth trembles as hidden caves deep within the earth yawn open,\nreleasing grotesque creatures from the underworld." },
            { "BackStory3","These sinister beings converge with a malevolent purpose:# to wage war upon humanity." },
            { "BackStory4", "Only a resilient few manage to escape the relentless assault#.#.#." },
            { "BackStory5", "As you cautiously traverse the dense, ominous woodlands,\na pack of nightmarish monsters bursts forth from a newly opened cave,\nleaving you grievously wounded as they launch their attack" },
            { "BackStory6", "Summoning your last reserves of strength, you repel the grotesque assailants." },
            { "BackStory7", "In your desperate bid for survival, you muster the energy to head in the direction of the campfire,\ndrawn by the distant hum of voices and the inviting glow of the camp." },

            { "WanderOff1", "You decide to stay a nomad and venture back into the dark woods alone." },
            { "WanderOff2", "The ominous forest looms over you, its shadows growing ever more menacing." },
            { "WanderOff3", "Unbeknownst to you, the forest holds more challenges. In the distance, a guttural growl pierces the silence." },
            { "WanderOff4", "The air becomes still, everything becomes silent and you hear a rustle in the bushes#.#.#." },
            { "WanderOff-Attack1", "~#@ SLASH! #~@" },
            { "WanderOff-AttackBlock", "You block the strike and spin around to find a monster emerging from the bush." },
            { "WanderOff-AttackFail1", "You drop to the floor" },
            { "WanderOff-AttackFail2", "With your final ounce of life, you glance up to see the bush-dwelling monster standing over you." },
            { "WanderOff-AttackFail3", "!#@ CRACK! #!@" },
            { "WanderOff-Win1", "You defeat the ambushing monster and head off alone." },
            { "WanderOff-Win2", "Embracing the nomadic life, you roam aimlessly killing any foe that steps in your way." },
            { "WanderOff-Win3", "The End" },

            { "CaveCompleted", "You have completed this cave already" },

            { "Cave1-1", "As you step into the cave's gaping maw, darkness envelopes you, and the world outside fades away." },
            { "Cave1-2", "You venture deeper into the cavern's labyrinth, your torch casting eerie shadows on the uneven walls." },
            { "Cave1-3", "Your footsteps lead you to an entrance of a colossal chamber." },
            { "Cave1-4", "There, a fearsome presence awaits." },
            { "Cave1-5", "Do you proceed? (Recomended Strength 50)" },

            { "Cave2-1", "You enter Mammoth Cave throught the small opening but find the space opens up into a vast underground world" },
            { "Cave2-2", "The ceiling is so high that your torch's feeble light barely reaches the top." },
            { "Cave2-3", "Echoes of your every move reverberate through the immense chambers, hinting at the secrets hidden within." },
            { "Cave2-4", "Your feet start to shake due to an unknown creature ahead." },
            { "Cave2-5", "Do you proceed? (Recomended Strength 110)" },

            { "Cave3-1", "Entering the Luray Caverns, you're welcomed by a breathtaking display of nature's artistry." },
            { "Cave3-2", "Bioluminescent formations cast an enchanting glow, painting the cave's interior with vibrant colors." },
            { "Cave3-3", "Crystal-clear pools mirror the luminous spectacle, creating an otherworldly ambiance." },
            { "Cave3-4", "Ahead the luminous rocks look faded and have a deep red hue indicating danger." },
            { "Cave3-5", "Do you proceed? (Recomended Strength 170)" },

            { "Plateau-Intro-1", "Entering the Shadowed Plateau, you find yourself in a mysterious and dimly lit expanse." },
            { "Plateau-Intro-2", "The plateau's surface is littered with dead creatures" },
            { "Plateau-Intro-3", "Shadowy forms move in the distance, and the landscape is shrouded in an enigmatic aura." },
            { "Plateau-Intro-4", "Do you want to enter the battlefield and fight the creatures?" },
            { "Plateau-Question", "Do you want to keep fighting?" },

            { "YN", "   (Y) Yes\n   (N) No" }
        };
        public static ConsoleColor NarratorColour = ConsoleColor.Red;

        #endregion Narrator

        #region Chief

        public static Dictionary<string, string> ChiefDialogue = new Dictionary<string, string>
        {
            { "Intro-Welcome","Greetings Traveler. Welcome to our camp." },
            { "Intro-Name", "I am the local Chief.# What is your name?" },
            { "Intro-Name2", "Great!# Nice to meet you {0}" },
            { "Intro-Question", "It seems you've had quite an encounter out there.# Would you like some help?"},
            { "Intro-Question-Options", "   (Y) Yes\n   (N) No" },
            { "Intro-Question-Y", "Very well, I'll show you around" },
            { "Intro-Question-N", "Are you sure?# It Dangerous out there right now." },
            { "Intro-Question-N2", "Well,# good luck then... #I Warned you..." },

            { "CampIntro1", "Here is our modest camp. It might not be grand, but it'll keep you safe during your stay." },
            { "CampIntro2", "Here you'll find several essential places, including:" },
            { "CampIntro3", "    - Combat store\n    - Healing Campfire\n    - Town hall\n    - Marketplace" },
            { "CampIntro4", "If you require any guidance come over to the town hall and I'll be there." },
            { "CampIntro5", "Feel free to visit these places. See you later!" },
            { "CampIntro6", "Also, The ♥ symbol on your stat bar means health, The ‼ Means strength, Goodluck out there!" },

            { "TownHall-Welcome", "{0}, Welcome to the town hall." },
            { "TownHall-Question", "What do you need?" },
            { "TownHall-Question-Options", "   (1) Help\n   (2) See my stats\n   (3) Leave" },
            { "TownHall-HelpStart1", "Monsters drop items which you can sell for money so if you need to get some money to purchase upgrades,\nhead over to the *Forest* and go to the *Shadowed Plateau* to find monsters to kill." },
            { "TownHall-HelpStart2", "Near here are the 3 caves that the monsters came from.\nOnce you get enough money for some upgrades please help us and go to those caves and defeat the bosses." },
            { "TownHall-HelpStart3", "Watch out though you need to be quite powerfull to survive there." },
            { "TownHallStats", "Your Stats:\n  MaxHealth - {0}\n  Strength - {1}\n  Leech Percent - {2}%\n\n  Monsters killed - {3}" },

            { "End1", "You defeat all the bosses in each of their lairs" },
            { "End2", "All the monsters see this and flee back to the underworld to avoid getting killed" },
            { "End3", "The world finaly returns to its original state." }
        };

        public static ConsoleColor ChiefColour = ConsoleColor.Cyan;

        #endregion Chief

        #region CombatShop

        public static Dictionary<string, string> CombatShopDialogue = new Dictionary<string, string>
        {
            { "CombatShop-Welcome1","Greetings {0}! Welcome to the *Combat Shop*" },
            { "CombatShop-Welcome2", "I have many items to upgrade your combat ability."},
            { "CombatShop-OptionsQuestion", "What would you like to upgrade?:" },
            { "CombatShop-Options", "    (1) Health\n    (2) Strength\n    (3) Leave" },
            { "CombatShop-Health", "These are the health upgrade offers I have:" },
            { "CombatShop-Strength", "These are the strength upgrade offers I have:" },
            { "CombatShop-Confirm", "Are you sure you want to buy {0} for ${1}?" },
            { "CombatShop-ConfirmOptions", "   (Y) Yes\n   (N) No" },
            { "CombatShop-Thanks", "Great! Thanks for your purchase" },
            { "CombatShop-Fail", "Sorry buy you don't have enough money for this item." },
            { "CombatShop-Leave", "See you again soon!" }
        };

        public static ConsoleColor CombatShopColour = ConsoleColor.Green;

        #endregion CombatShop

        #region Campfire

        public static Dictionary<string, string> CampfireDialogue = new Dictionary<string, string>
        {
            { "Campfire-Question","What would you like to do?" },
            { "Campfire-Options", "   (1) Complete a skillcheck to regain health.\n   (2) Leave" },
            { "Campfire-Passed", "You successfully healed {0} health." },
            { "Campfire-Fail", "You failed the skillcheck. Try again." }
        };

        public static ConsoleColor CampfireColour = ConsoleColor.Yellow;

        #endregion Campfire

        #region Marketplace

        public static Dictionary<string, string> MarketplaceDialogue = new Dictionary<string, string>
        {
            { "Welcome","{0}! Welcome to the marketplace" },
            { "Options-Question", "What would you like to do?" },
            { "Options", "   (1) Sell Items\n   (2) Gamble\n   (3) Leave" },
            { "NoItems", "You dont have any items to sell. Once you get some i'll tell you what I can buy it for." },
            { "ListItems" , "These are all the items I can buy: (Use ▲ & ▼ Arrow Keys to move and Enter to select)" },
            { "BuyConfirm", "Are you sure you want to sell {0} for ${1}?" },
            { "ConfirmYN", "   (Y) Yes\n   (N) No" },
            { "BuyThanks", "Pleasure doing business with you." },
            { "Goodbye", "Goodbye." },
            { "Gamble", "What game would you like to bet?:" },
            { "GameOptions", "   (1) Roulette\n   (2) Go Back" },
            { "GambleAmount", "How much would you like to gamble? (Enter 0 To Exit)" },
            { "InvalidAmount", "That is not a valid amount" },
            { "RouletteBet", "What would you like to bet on?" },
            { "RouletteBetOptions", "   (1) Colour\n   (2) Number" },
            { "ColourBet", "Would you like to bet on Black or Red?" },
            { "ColourBetOptions", "   (1) Black (3x)\n   (2) Red (3x)"},
            { "NumberBet", "How would you like to bet on numbers?" },
            { "NumberBetOptions", "   (1) Even (2x)\n   (2) Odd (2x)\n   (3) A Certain Number (10x)" },
            { "CertainNumber", "What number would you like to bet on? (0-12)"},
            { "GambleWin", "You won {0} your money!"},
            { "GambleLose", "You lost your money!"}
        };

        public static ConsoleColor MarketplaceColour = ConsoleColor.Magenta;

        #endregion Marketplace

        #region Battle

        public static Dictionary<string, string> BattleDialogue = new Dictionary<string, string>
        {
            { "Battle-Question","What would you like to do?" },
            { "Battle-Question-Options", "  (1) Attack [Easy]\n  (2) Block opponents attack and heal [Medium]\n  (3) Reflect opponents attack back [Hard]\n" },
            { "Battle-Attack", "You successfully deal {0} damage." },
            { "Battle-Attack-Fail", "You miss your attack."},
            { "Battle-Heal", "You opponent attacks, you successfully dodge it and heal {0} health."},
            { "Battle-Heal-Fail", "You fail the dodge." },
            { "Battle-Reflect", "You successfully reflect your opponents attack. It damages them for {0}." },
            { "Battle-Reflect-Fail", "You miss the reflection." },
            { "Battle-Monster-Attack", "Your opponent attacks you and deals {0} damage." },
            { "Battle-Win", "You won the battle against a {0}!" }
        };

        public static ConsoleColor BattleColour = ConsoleColor.White;

        #endregion Battle

        #region Functions

        public static void WriteDialogue(Characters character, string Line, params string[] Variables)
        {
            Util.ClearInputBuffer();
            string EnumName = Enum.GetName(typeof(Characters), character);
            Console.ForegroundColor = (ConsoleColor)(typeof(Dialogue).GetField(EnumName + "Colour").GetValue(null));
            Dictionary<string, string> CharacterDictionary = (Dictionary<string, string>)(typeof(Dialogue).GetField(EnumName + "Dialogue").GetValue(null));

            Console.WriteLine(CharacterDictionary[Line]);
            Console.ResetColor();
        }

        public static void RollInDialougue(Characters character, string Line, int Delay, int StartDelay = 1000, params string[] Variables)
        {
            Util.ClearInputBuffer();
            Game.cts = new CancellationTokenSource();
            Util.CTSDisposed = false;

            Game.Wait(StartDelay);

            string EnumName = Enum.GetName(typeof(Characters), character);
            Console.ForegroundColor = (ConsoleColor)(typeof(Dialogue).GetField(EnumName + "Colour").GetValue(null));
            Dictionary<string, string> CharacterDictionary = (Dictionary<string, string>)(typeof(Dialogue).GetField(EnumName + "Dialogue").GetValue(null));

            string OutputString = CharacterDictionary[Line];
            if (Variables != null)
                OutputString = string.Format(CharacterDictionary[Line], Variables);
            int CursorPosition = Console.CursorTop;

            while(!Game.cts.IsCancellationRequested)
            {
                for (int i = 0; i < OutputString.Length; i++)
                {
                    char CurrentChar = OutputString[i];
                    if (CurrentChar == '#')
                        Game.Wait(500);
                    else
                    {
                        Game.Wait(Delay);
                        Console.Write(CurrentChar);
                    }
                }
                break;
            }
            Console.SetCursorPosition(0, CursorPosition);

            Console.WriteLine(OutputString.Replace("#", string.Empty));
            Console.WriteLine();
            Console.ResetColor();
            Util.CTSDisposed = true;
            Game.cts.Cancel();
            Game.cts.Dispose();
        }

        public static ConsoleKey AskQuestionWithOptions(Characters character, string Line, ConsoleKey[] Answers, params string[] Variables)
        {
            Util.ClearInputBuffer();
            string EnumName = Enum.GetName(typeof(Characters), character);
            Console.ForegroundColor = (ConsoleColor)(typeof(Dialogue).GetField(EnumName + "Colour").GetValue(null));
            Dictionary<string, string> CharacterDictionary = (Dictionary<string, string>)(typeof(Dialogue).GetField(EnumName + "Dialogue").GetValue(null));

            string OutputString = string.Format(CharacterDictionary[Line], Variables);
            Console.WriteLine(OutputString);
            Console.ResetColor();

            Console.WriteLine();
            ConsoleKey choice = Console.ReadKey().Key;
            while (!Answers.Contains(choice))
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(" ");
                Console.SetCursorPosition(0, Console.CursorTop);
                choice = Console.ReadKey().Key;
            }
            Console.WriteLine();
            Console.WriteLine();
            return choice;
        }

        #endregion Functions
    }
}
