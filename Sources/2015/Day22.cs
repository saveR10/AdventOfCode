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
    [ResearchAlgorithms(title: "Day 22: Wizard Simulator 20XX",
                        TypologyEnum.Game, //RPG combat
                        ResolutionEnum.DFS | ResolutionEnum.BFS,
                        DifficultEnum.Hard,
                        "Simulazione di combattimento RPG per wizard; gestione di mana, effetti temporanei e ricerca del minimo consumo di mana per vincere")]
    public class Day22 : Solver, IDay
    {
        private int MinManaSpent = int.MaxValue;
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var bossStats = inputText.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                             .Select(line => int.Parse(line.Split(':')[1].Trim()))
                             .ToArray();

            Character player = new Character();
            Character boss = new Character();

            int nTest = 1;

            if (test)
            {
                switch (nTest)
                {
                    case 0: //solution=226
                        player.HitPoints = 10;
                        player.Mana = 250;

                        boss.HitPoints = 13;
                        boss.Damage = 8;
                        break;
                    case 1://solution=641
                        player.HitPoints = 10;
                        player.Mana = 250;

                        boss.HitPoints = 14;
                        boss.Damage = 8;
                        break;
                }
            }
            else
            {
                player.HitPoints = 50;
                player.Mana = 500;

                boss.HitPoints = bossStats[0];
                boss.Damage = bossStats[1];
            }

            int resolution = 1;
            switch (resolution)
            {
                case 0:
                    // Inizializza il DFS
                    DFS(player, boss, new List<Effect>(), true);
                    break;
                case 1:
                    //Approccio BFS
                    BFS(player, boss, ref MinManaSpent);
                    solution = MinManaSpent;

                    break;
            }

            // Restituisci il costo minimo trovato
            solution = MinManaSpent;
        }

        private void DFS(Character player, Character boss, List<Effect> activeEffects, bool isPlayerTurn, bool poisoned = false)
        {
            if (isPlayerTurn && poisoned)
            {
                player.HitPoints -= 1;
                if (player.HitPoints <= 0 || player.ManaSpent >= MinManaSpent)
                {
                    return;
                }
            }

            // Applicazione degli effetti attivi
            ApplyEffects(player, boss, activeEffects);

            // Controllo delle condizioni di vittoria o sconfitta
            if (boss.HitPoints <= 0 && player.HitPoints > 0)
            {
                MinManaSpent = Math.Min(MinManaSpent, player.ManaSpent);
                return;
            }
            if (player.HitPoints <= 0 || player.ManaSpent >= MinManaSpent)
            {
                return;
            }

            // Turno del giocatore
            if (isPlayerTurn)
            {
                foreach (var spell in Spell.Spells)
                {
                    if (player.Mana < spell.Cost || activeEffects.Any(e => e.Name == spell.Name && e.Timer > 0))
                        continue;

                    // Crea un nuovo stato del gioco
                    var newPlayer = player.Clone();
                    var newBoss = boss.Clone();
                    var newEffects = activeEffects.Select(e => e.Clone()).ToList();

                    newPlayer.Mana -= spell.Cost;
                    newPlayer.ManaSpent += spell.Cost;

                    // Lancia l'incantesimo
                    spell.Cast(newPlayer, newBoss, newEffects);
                    newPlayer.AddStory($"Cast {spell.Name} for {spell.Cost}", newBoss);

                    // Continua il DFS
                    DFS(newPlayer, newBoss, newEffects, false,poisoned);
                }
            }
            else
            {
                // Turno del boss
                var damage = Math.Max(1, boss.Damage - player.Armor);
                player.HitPoints -= damage;
                player.AddStory($"Attacked by boss for {damage} HP", boss);

                // Continua il DFS
                DFS(player, boss, activeEffects, true,poisoned);
            }
        }

        private void ApplyEffects(Character player, Character boss, List<Effect> effects)
        {
            foreach (var effect in effects)
            {
                effect.Apply(player, boss);
                //effect.Timer--;
                effect.DecreaseTimer();
                if (effect.IsExpired()) effect.Remove(player, boss);
            }
            effects.RemoveAll(e => e.Timer <= 0);
        }

        private void BFS(Character player, Character boss, ref int minManaSpent)
        {
            var queue = new Queue<(Character player, Character boss, List<Effect> effects, bool isPlayerTurn)>();
            queue.Enqueue((player.Clone(), boss.Clone(), new List<Effect>(), true));

            while (queue.Count > 0)
            {
                var (currentPlayer, currentBoss, activeEffects, isPlayerTurn) = queue.Dequeue();

                // Applicazione degli effetti attivi
                ApplyEffects(currentPlayer, currentBoss, activeEffects);

                // Controllo delle condizioni di vittoria o sconfitta
                if (currentBoss.HitPoints <= 0)
                {
                    minManaSpent = Math.Min(minManaSpent, currentPlayer.ManaSpent);
                    continue;
                }
                if (currentPlayer.HitPoints <= 0 || currentPlayer.ManaSpent >= minManaSpent)
                {
                    continue;
                }

                if (isPlayerTurn)
                {
                    foreach (var spell in Spell.Spells)
                    {
                        if (currentPlayer.Mana < spell.Cost || activeEffects.Any(e => e.Name == spell.Name && e.Timer > 0))
                            continue;

                        var newPlayer = currentPlayer.Clone();
                        var newBoss = currentBoss.Clone();
                        var newEffects = activeEffects.Select(e => e.Clone()).ToList();

                        newPlayer.Mana -= spell.Cost;
                        newPlayer.ManaSpent += spell.Cost;

                        spell.Cast(newPlayer, newBoss, newEffects);
                        queue.Enqueue((newPlayer, newBoss, newEffects, false));
                    }
                }
                else
                {
                    var damage = Math.Max(1, currentBoss.Damage - currentPlayer.Armor);
                    currentPlayer.HitPoints -= damage;
                    queue.Enqueue((currentPlayer, currentBoss, activeEffects, true));
                }
            }
        }

        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var bossStats = inputText.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                             .Select(line => int.Parse(line.Split(':')[1].Trim()))
                             .ToArray();

            Character player = new Character();
            Character boss = new Character();

            int nTest = 1;

            if (test)
            {
                switch (nTest)
                {
                    case 0: //solution=226
                        player.HitPoints = 10;
                        player.Mana = 250;

                        boss.HitPoints = 13;
                        boss.Damage = 8;
                        break;
                    case 1://solution=641
                        player.HitPoints = 10;
                        player.Mana = 250;

                        boss.HitPoints = 14;
                        boss.Damage = 8;
                        break;
                }
            }
            else
            {
                player.HitPoints = 50;
                player.Mana = 500;

                boss.HitPoints = bossStats[0];
                boss.Damage = bossStats[1];
            }

            bool poisoned = true;
            // Inizializza il DFS
            DFS(player, boss, new List<Effect>(), true, poisoned);

            // Restituisci il costo minimo trovato
            solution = MinManaSpent;

            //1289 too low
        }
    }

    public class Character
    {
        public int HitPoints { get; set; }
        public int Mana { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public int ManaSpent { get; set; }
        public string Story { get; set; }
        public void AddStory(string newStory, Character boss)
        {
            this.Story += $"\nPlayer - HitPoints: {this.HitPoints}, Mana: {this.Mana}, ManaSpent: {this.ManaSpent}, Armor: {this.Armor} - {newStory}\nBoss - HitPoints: {boss.HitPoints}";
        }
        public void Attack(Character target)
        {
            int damageDealt = Math.Max(1, Damage - target.Armor);
            target.HitPoints -= damageDealt;
        }
        public Character Clone()
        {
            return new Character
            {
                HitPoints = this.HitPoints,
                Mana = this.Mana,
                Damage = this.Damage,
                Armor = this.Armor,
                ManaSpent = this.ManaSpent,
                Story = this.Story
            };
        }
    }
    public class Spell
    {
        public string Name { get; }
        public int Cost { get; }
        public Action<Character, Character, List<Effect>> Cast { get; }
        public Spell(string name, int cost, Action<Character, Character, List<Effect>> cast)
        {
            Name = name;
            Cost = cost;
            Cast = cast;
        }

        public static List<Spell> Spells => new List<Spell>
        {
            new Spell("Magic Missile", 53, (player, boss, effects) => {boss.HitPoints -= 4;}),
            new Spell("Drain", 73, (player, boss, effects) => { boss.HitPoints -= 2; player.HitPoints += 2;}),
            new Spell("Shield", 113, (player, boss, effects) => { effects.Add(new Effect("Shield", 6, (p, b) => p.Armor = 7, (p, b) => p.Armor = 0));}),
            new Spell("Poison", 173, (player, boss, effects) =>{effects.Add(new Effect("Poison",   6, (p, b) => b.HitPoints -= 3));}),
            new Spell("Recharge", 229, (player, boss, effects) =>{effects.Add(new Effect("Recharge", 5, (p, b) => p.Mana += 101));})
        };
    }
    public class Effect
    {
        public string Name { get; }
        public int Timer { get; set; } // Aggiunto il campo Timer
        private readonly Action<Character, Character> ApplyEffect;
        private readonly Action<Character, Character> RemoveEffect;

        public Effect(string name, int timer, Action<Character, Character> applyEffect, Action<Character, Character> removeEffect = null)
        {
            Name = name;
            Timer = timer;
            ApplyEffect = applyEffect;
            RemoveEffect = removeEffect;
        }

        public void Apply(Character player, Character boss)
        {
            ApplyEffect?.Invoke(player, boss);
        }

        public void DecreaseTimer()
        {
            Timer--;
        }

        public bool IsExpired()
        {
            return Timer <= 0;
        }

        public void Remove(Character player, Character boss)
        {
            RemoveEffect?.Invoke(player, boss);
        }
        public Effect Clone()
        {
            return new Effect(Name, Timer, ApplyEffect, RemoveEffect);
        }
    }
}