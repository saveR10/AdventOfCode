using AOC;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;
using Solver = AOC.Solver;

namespace AOC2015
{
    [ResearchAlgorithms(title: "Day 24: It Hangs in the Balance",
                        TypologyEnum.Combinatorial | TypologyEnum.Recursive,                   // Ottimizzazione combinatoria / subset sum
                        ResolutionEnum.Reduction | ResolutionEnum.DFS,  //DFS con BackTracking e Pruning 
                        DifficultEnum.Hard,
                        "Divisione di pacchi in gruppi con lo stesso peso; trovare il gruppo iniziale con il minor numero di pacchi e il minimo quantum entanglement")]
    public class Day24 : Solver, IDay
    {

        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var instructions = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.RemoveEmptyEntries)
                             .ToArray();
            List<int> packages = new List<int>();
            foreach (var p in instructions)
            {
                packages.Add(int.Parse(p));
            }

            long result = Solve(packages.ToArray());
            Console.WriteLine($"Quantum entanglement of the ideal configuration: {result}");
            solution = result;
        }
        public static long Solve(int[] packages, bool optimized = true)
        {
            int totalWeight = packages.Sum();
            if (totalWeight % 3 != 0)
            {
                throw new ArgumentException("Packages cannot be evenly divided into three groups.");
            }
            int targetWeight = totalWeight / 3;

            var validCombinations = (optimized
                                        ? GetCombinationsOptimized(packages, targetWeight)
                                        : GetCombinations(packages, targetWeight))
                                    .OrderBy(c => c.Count) // Minimo numero di pacchetti
                                    .ThenBy(c => QuantumEntanglement(c)); // Minimo QE

            foreach (var firstGroup in validCombinations)
            {
                var remainingPackages = packages.Except(firstGroup).ToArray();
                if (CanSplitIntoTwoGroups(remainingPackages, targetWeight))
                {
                    return QuantumEntanglement(firstGroup);
                }
            }

            throw new Exception("No valid configuration found.");
        }

        static IEnumerable<List<int>> GetCombinationsOptimized(int[] packages, int target)
        {
            return BacktrackYield(0, 0, target, packages, new List<int>());
        }
        //Lazy evaluation
        static IEnumerable<List<int>> BacktrackYield(int start, int currentSum, int target, int[] packages, List<int> current)
        {
            if (currentSum == target)
            {
                yield return new List<int>(current);
                yield break;
            }

            for (int i = start; i < packages.Length; i++)
            {
                if (currentSum + packages[i] > target) continue; //pruning

                current.Add(packages[i]);
                foreach (var combination in BacktrackYield(i + 1, currentSum + packages[i], target, packages, current))
                {
                    yield return combination;
                }
                current.RemoveAt(current.Count - 1); // backtrack
            }
        }
        
        


        // Verifica se i pacchetti rimanenti possono essere divisi in due gruppi di peso target
        static bool CanSplitIntoTwoGroups(int[] packages, int target)
        {
            var combinations = GetCombinationsOptimized(packages, target);
            foreach (var combination in combinations)
            {
                var remainingPackages = packages.Except(combination).ToArray();
                if (remainingPackages.Sum() == target)
                {
                    return true;
                }
            }
            return false;
        }


        // Calcola tutte le combinazioni di un array che sommano a un target
        static IEnumerable<List<int>> GetCombinations(int[] packages, int target)
        {
            int n = packages.Length;
            for (int i = 1; i <= n; i++)
            {
                foreach (var combination in Combinations(packages, i))
                {
                    if (combination.Sum() == target)
                    {
                        yield return combination.ToList();
                    }
                }
            }
        }

        // Genera tutte le combinazioni di k elementi da un array
        static IEnumerable<IEnumerable<int>> Combinations(int[] array, int length)
        {
            if (length == 0) yield return Enumerable.Empty<int>();
            else
            {
                for (int i = 0; i < array.Length; i++)
                {
                    foreach (var combination in Combinations(array.Skip(i + 1).ToArray(), length - 1))
                    {
                        yield return combination.Prepend(array[i]);
                    }
                }
            }
        }

        // Calcola il quantum entanglement
        static long QuantumEntanglement(IEnumerable<int> group) =>
            group.Aggregate(1L, (prod, x) => prod * x);





        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var instructions = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.RemoveEmptyEntries)
                             .ToArray();
            List<int> packages = new List<int>();
            foreach (var p in instructions)
            {
                packages.Add(int.Parse(p));
            }

            long result = SolveComplex(packages.ToArray());
            Console.WriteLine($"Quantum entanglement of the ideal configuration: {result}");
            solution = result;
        }

        public static long SolveComplex(int[] packages, bool optimized = true)
        {
            int totalWeight = packages.Sum();
            if (totalWeight % 4 != 0)
            {
                throw new ArgumentException("Packages cannot be evenly divided into four groups.");
            }
            int targetWeight = totalWeight / 4;

            var validCombinations = (optimized
                                        ? GetCombinationsOptimized(packages, targetWeight)
                                        : GetCombinations(packages, targetWeight))
                                    .OrderBy(c => c.Count) // Minimo numero di pacchetti
                                    .ThenBy(c => QuantumEntanglement(c)); // Minimo QE

            foreach (var firstGroup in validCombinations)
            {
                var remainingPackages = packages.Except(firstGroup).ToArray();
                if (CanSplitIntoThreeGroups(remainingPackages, targetWeight))
                {
                    return QuantumEntanglement(firstGroup);
                }
            }

            throw new Exception("No valid configuration found.");
        }

        static bool CanSplitIntoThreeGroups(int[] packages, int target)
        {
            var combinations = GetCombinationsOptimized(packages, target);
            foreach (var combination in combinations)
            {
                var remainingPackages = packages.Except(combination).ToArray();
                if (CanSplitIntoTwoGroups(remainingPackages, target))
                {
                    return true;
                }
            }
            return false;
        }

    }
}