using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject
{
    class Monster
    {
        public enum MonsterType
        {
            Goblin,
            Cyclops,
            Gorgon,
            Boss
        }
        public MonsterType type;
        public Stats stats;
        public bool DoDrops = true;

        public Monster(int MaxHealth, int Strength, bool _DoDrops = true, bool Boss = false)
        {
            DoDrops = _DoDrops;

            if(!Boss)
            {
                type = (MonsterType)Game.rand.Next(0, 3);
            }
            else
            {
                type = MonsterType.Boss;
            }

            stats = new Stats(MaxHealth, Strength)
            {
                Name = Enum.GetName(typeof(MonsterType), type)
            };
        }

        Drop[] NormalDrops =
        {
            new Drop(new Item("Rotten Flesh", 7), 0.3f, 10),
            new Drop(new Item("Bag Of Coins",0), 0.3f),
            new Drop(new Item("Iron Scrap", 25), 0.2f, 3),
            new Drop(new Item("Enchanted Crystal", 95), 0.1f)
        };

        Drop[] BossDrops =
        {
            new Drop(new Item("Scap Gold", 175), 0.4f, 3),
            new Drop(new Item("Luminous Shard", 235), 0.3f, 2),
            new Drop(new Item("Chest Of Coins",0), 0.3f)
        };

        public void Die()
        {
            float Num = (float)Game.rand.NextDouble();
            float CumulitiveChance = 0;

            Game.player.MonstersKilled++;

            if (type != MonsterType.Boss)
            {
                for (int i = 0; i < NormalDrops.Length; i++)
                {
                    CumulitiveChance += NormalDrops[i].Chance;
                    if (CumulitiveChance > Num)
                    {
                        if (NormalDrops[i].Item.Name == "Bag Of Coins")
                        {
                            Game.player.Money += Game.rand.Next(20, 55);
                        }
                        else
                        {
                            int Amount = Game.rand.Next(1, NormalDrops[i].MaxAmount + 1);
                            Game.player.AddItemToInventory(NormalDrops[i].Item, Amount);
                        }

                        Console.WriteLine($"{stats.Name} dropped -*{NormalDrops[i].Item.Name}*- ");
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < BossDrops.Length; i++)
                {
                    CumulitiveChance += BossDrops[i].Chance;
                    if (CumulitiveChance > Num)
                    {
                        if (BossDrops[i].Item.Name == "Chest Of Coins")
                        {
                            Game.player.Money += Game.rand.Next(90, 200);
                        }
                        else
                        {
                            int Amount = Game.rand.Next(1, NormalDrops[i].MaxAmount + 1);
                            Game.player.AddItemToInventory(NormalDrops[i].Item, Amount);
                        }

                        Console.WriteLine($"{stats.Name} dropped -*{NormalDrops[i].Item.Name}*- ");
                        break;
                    }
                }
            }
        }
    }

    class Drop
    {
        public Item Item;
        public float Chance; // 0 to 1
        public int MaxAmount;

        public Drop(Item _Item, float _Chance, int _MaxAmount = 1)
        {
            Item = _Item;
            Chance = _Chance;
            MaxAmount = _MaxAmount;
        }
    }
}
