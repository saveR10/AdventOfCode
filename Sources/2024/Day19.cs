using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static AOC2023.Day10;

namespace AOC2024
{
    public class Day19 : Solver, IDay
    {
        [ResearchAlgorithms(ResearchAlgorithmsAttribute.TypologyEnum.Combinatorial)]
        [ResearchAlgorithms(ResearchAlgorithmsAttribute.ResolutionEnum.Cache)]
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            List<string> AvailableTowels = new List<string>();
            List<string> TowelsDesign = new List<string>();

            bool TowelsDesignBool = false;
            foreach (string line in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (TowelsDesignBool)
                {
                    TowelsDesign.Add(line.Trim());
                }

                else if (!string.IsNullOrEmpty(line))
                {
                    var towels = line.Split(',');
                    foreach(var t in towels)
                    {
                        AvailableTowels.Add(t.Trim());
                    }
                }
                else
                {
                    TowelsDesignBool= true;
                }
            }
            int formed = 0;
            foreach(var d in TowelsDesign)
            {
                if (CanBuildTarget(d, AvailableTowels)) formed++;
            }
            solution = formed;
            //294
        }
        public bool CanBuildTarget(string target, List<string> pieces)
        {
            var memo = new Dictionary<string, bool>(); // Memorizzazione per ottimizzare
            return CanBuildHelper(target, pieces, memo);
        }

        private bool CanBuildHelper(string remaining, List<string> pieces, Dictionary<string, bool> memo)
        {
            // Se la stringa rimanente è vuota, abbiamo trovato una soluzione
            if (string.IsNullOrEmpty(remaining)) return true;

            // Se abbiamo già calcolato il risultato per questa stringa, restituiscilo
            if (memo.ContainsKey(remaining)) return memo[remaining];

            // Prova ogni elemento della lista
            foreach (var piece in pieces)
            {
                // Controlla se il `piece` corrisponde all'inizio della stringa rimanente
                if (remaining.StartsWith(piece))
                {
                    // Rimuovi il prefisso `piece` e chiama ricorsivamente
                    string next = remaining.Substring(piece.Length);
                    if (CanBuildHelper(next, pieces, memo))
                    {
                        memo[remaining] = true; // Memorizza il risultato positivo
                        return true;
                    }
                }
            }

            // Se nessun pezzo corrisponde, memorizza il risultato negativo
            memo[remaining] = false;
            return false;
        }

        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            List<string> AvailableTowels = new List<string>();
            List<string> TowelsDesign = new List<string>();

            bool TowelsDesignBool = false;
            foreach (string line in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (TowelsDesignBool)
                {
                    TowelsDesign.Add(line.Trim());
                }

                else if (!string.IsNullOrEmpty(line))
                {
                    var towels = line.Split(',');
                    foreach (var t in towels)
                    {
                        AvailableTowels.Add(t.Trim());
                    }
                }
                else
                {
                    TowelsDesignBool = true;
                }
            }
            long totalWays = 0;

            foreach (var target in TowelsDesign)
            {
                long ways = CountWays(target, AvailableTowels);
                Console.WriteLine($"Il target \"{target}\" può essere costruito in {ways} modi.");
                totalWays += ways;
            }
            solution = totalWays;
        }
        public long CountWays(string target, List<string> pieces)
        {
            var memo = new Dictionary<string, long>(); // Memorizzazione per ottimizzare
            return CountWaysHelper(target, pieces, memo);
        }

        private long CountWaysHelper(string remaining, List<string> pieces, Dictionary<string, long> memo)
        {
            // Se la stringa rimanente è vuota, abbiamo trovato una combinazione valida
            if (string.IsNullOrEmpty(remaining)) return 1;

            // Se abbiamo già calcolato il risultato per questa stringa, restituiscilo
            if (memo.ContainsKey(remaining)) return memo[remaining];

            long count = 0;

            // Prova ogni elemento della lista
            foreach (var piece in pieces)
            {
                // Controlla se il `piece` corrisponde all'inizio della stringa rimanente
                if (remaining.StartsWith(piece))
                {
                    // Rimuovi il prefisso `piece` e chiama ricorsivamente
                    string next = remaining.Substring(piece.Length);
                    count += CountWaysHelper(next, pieces, memo); // Somma i modi validi
                }
            }

            // Memorizza il risultato per la stringa rimanente
            memo[remaining] = count;
            return count;
        }
    }
}
