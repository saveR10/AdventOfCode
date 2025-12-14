using AOC;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;
using Solver = AOC.Solver;

namespace AOC2015
{
    [ResearchAlgorithms(title: "Day 21: RPG Simulator 20XX",
                        TypologyEnum.Game,  //RPG combat
                        ResolutionEnum.BruteForce,
                        DifficultEnum.Medium,
                        "Simulazione di combattimenti RPG per combinazioni di equipaggiamento; ricerca del costo minimo/massimo con vittoria o sconfitta")]
    public class Day21 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var bossStats = inputText.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                             .Select(line => int.Parse(line.Split(':')[1].Trim()))
                             .ToArray();

            var boss = new Character("Boss", bossStats[0], bossStats[1], bossStats[2]);

            int minCost = int.MaxValue;

            foreach (var weapon in Inventory.Weapons)
            {
                foreach (var armor in Inventory.Armor)
                {
                    foreach (var rings in GetRingCombinations())
                    {
                        var player = new Character("Player", 100, 0, 0);

                        player.Damage += weapon.Damage + armor.Damage + rings.Sum(r => r.Damage);
                        player.Armor += weapon.Armor + armor.Armor + rings.Sum(r => r.Armor);

                        int totalCost = weapon.Cost + armor.Cost + rings.Sum(r => r.Cost);

                        if (SimulateBattle(player, new Character(boss.Name, boss.HitPoints, boss.Damage, boss.Armor)) && totalCost < minCost)
                        {
                            minCost = totalCost;
                        }
                    }
                }
            }
            Console.WriteLine($"Minimum cost to win: {minCost}");
            solution = minCost;
        }


        private IEnumerable<List<Item>> GetRingCombinations()
        {
            var rings = Inventory.Rings.Where(r => r.Name != "None").ToList();
            // No rings
            yield return new List<Item>();
            // Single rings
            foreach (var ring in rings)
            {
                yield return new List<Item> { ring };
            }
            // Pairs of rings
            for (int i = 0; i < rings.Count; i++)
            {
                for (int j = i + 1; j < rings.Count; j++)
                {
                    yield return new List<Item> { rings[i], rings[j] };
                }
            }
        }

        private bool SimulateBattle(Character player, Character boss)
        {
            while (true)
            {
                boss.HitPoints -= Math.Max(player.Damage - boss.Armor, 1);
                if (boss.HitPoints <= 0) return true;

                player.HitPoints -= Math.Max(boss.Damage - player.Armor, 1);
                if (player.HitPoints <= 0) return false;
            }
        }

        public class Character
        {
            public string Name { get; set; }
            public int HitPoints { get; set; }
            public int Damage { get; set; }
            public int Armor { get; set; }

            public Character(string name, int hitPoints, int damage, int armor)
            {
                Name = name;
                HitPoints = hitPoints;
                Damage = damage;
                Armor = armor;
            }
        }

        public class Item
        {
            public string Name { get; set; }
            public int Cost { get; set; }
            public int Damage { get; set; }
            public int Armor { get; set; }

            public Item(string name, int cost, int damage, int armor)
            {
                Name = name;
                Cost = cost;
                Damage = damage;
                Armor = armor;
            }
        }

        public static class Inventory
        {
            public static List<Item> Weapons = new List<Item>
        {
            new Item("Dagger", 8, 4, 0),
            new Item("Shortsword", 10, 5, 0),
            new Item("Warhammer", 25, 6, 0),
            new Item("Longsword", 40, 7, 0),
            new Item("Greataxe", 74, 8, 0),
        };

            public static List<Item> Armor = new List<Item>
        {
            new Item("None", 0, 0, 0),
            new Item("Leather", 13, 0, 1),
            new Item("Chainmail", 31, 0, 2),
            new Item("Splintmail", 53, 0, 3),
            new Item("Bandedmail", 75, 0, 4),
            new Item("Platemail", 102, 0, 5),
        };

            public static List<Item> Rings = new List<Item>
        {
            new Item("None", 0, 0, 0),
            new Item("Damage +1", 25, 1, 0),
            new Item("Damage +2", 50, 2, 0),
            new Item("Damage +3", 100, 3, 0),
            new Item("Defense +1", 20, 0, 1),
            new Item("Defense +2", 40, 0, 2),
            new Item("Defense +3", 80, 0, 3),
        };
        }

        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var bossStats = inputText.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                             .Select(line => int.Parse(line.Split(':')[1].Trim()))
                             .ToArray();

            var boss = new Character("Boss", bossStats[0], bossStats[1], bossStats[2]);

            int maxCost = 0;

            foreach (var weapon in Inventory.Weapons)
            {
                foreach (var armor in Inventory.Armor)
                {
                    foreach (var rings in GetRingCombinations())
                    {
                        var player = new Character("Player", 100, 0, 0);

                        player.Damage += weapon.Damage + armor.Damage + rings.Sum(r => r.Damage);
                        player.Armor += weapon.Armor + armor.Armor + rings.Sum(r => r.Armor);

                        int totalCost = weapon.Cost + armor.Cost + rings.Sum(r => r.Cost);

                        if (!SimulateBattle(player, new Character(boss.Name, boss.HitPoints, boss.Damage, boss.Armor)) && totalCost > maxCost)
                        {
                            maxCost = totalCost;
                        }
                    }
                }
            }
            Console.WriteLine($"Maximum cost to lose: {maxCost}");
            solution = maxCost;
        }
    }
}