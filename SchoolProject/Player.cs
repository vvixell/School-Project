using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SchoolProject
{
    public class Player
    {
        public int Money = 0;
        public string Location;
        public int MonstersKilled = 0;
        public Stats stats;
        public List<InventoryItem> Inventory = new List<InventoryItem>();

        public Player()
        {
            stats = new Stats(100, 20, 30);
        }

        public void Die(string Reason = "")
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            string[] YouDied = OtherAscii.YouDied;
            for (int x = 0; x < 30; x++)
            {
                for (int y = 0; y < 11; y++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(YouDied[y][x]);
                    Console.SetCursorPosition(59 - x, y);
                    Console.Write(YouDied[y][59 - x]);
                }
                Thread.Sleep(10);
            }

            Console.SetCursorPosition(0, 12);
            Game.WaitForKeyPress();
            Environment.Exit(0);
        }

        public void DrawStats(int BackToLine = 4)
        {
            string HSB = new string('═', stats.Health.ToString().Length);
            string MHSB = new string('═', stats.MaxHealth.ToString().Length);
            string SSB = new string('═', stats.Strength.ToString().Length);
            string MSB = new string('═', Money.ToString().Length);
            string LSB = new string('═', Location.Length);

            Console.SetCursorPosition(0, 0);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 1);
            Console.WriteLine(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 2);
            Console.WriteLine(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"╔═══{HSB}═{MHSB}═╦═══{SSB}═╦═══{MSB}═╦═══{LSB}═╗");
            Console.WriteLine($"║ ♥ {stats.Health}/{stats.MaxHealth} ║ ‼ {stats.Strength} ║ $ {Money} ║ ¤ {Location} ║");
            Console.WriteLine($"╚═══{HSB}═{MHSB}═╩═══{SSB}═╩═══{MSB}═╩═══{LSB}═╝");
            Console.SetCursorPosition(0, BackToLine);
        }
        
        public void AddItemToInventory(Item item, int Amount = 1)
        {
            InventoryItem inventoryItem = null;
            foreach (InventoryItem I in Inventory)
                if (I.Item.Equals(item)) inventoryItem = I;
            if (inventoryItem != null)
                inventoryItem.Amount += Amount;
            else
                Inventory.Add(new InventoryItem(item, Amount));
        }
    }

    public class Stats
    {
        public string Name = string.Empty;
        public int Health;
        public int Strength;
        public int MaxHealth;
        public int LeechPercent = 0;

        public Stats(int _MaxHealth, int _Strength, int _Health = 0)
        {
            MaxHealth = _MaxHealth;
            Strength = _Strength;
            if (_Health != 0) Health = _Health;
            else Health = MaxHealth;
        }

        public void TakeDamage(int Amount)
        {
            Health -= Amount;
            if (Health < 0) Health = 0;
        }

        public void Heal(int Amount)
        {
            Health += Amount;
            if (Health > MaxHealth) Health = MaxHealth;
        }
    }

    public class Item
    {
        public string Name;
        public int Value;

        public Item(string _Name, int _Value)
        {
            Name = _Name;
            Value = _Value;
        }
    }

    public class InventoryItem 
    {
        public Item Item;
        public int Amount;

        public InventoryItem(Item item, int amount = 1)
        {
            Item = item;
            Amount = amount;
        }

        public void Modify(int amount = 1)
        {
            Amount += amount;
        }
    }
}
