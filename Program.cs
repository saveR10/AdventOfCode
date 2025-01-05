using AOC.DataStructures;
using AOC.DataStructures.AlghoritmsOptimization;
using AOC.DataStructures.Cache;
using AOC.DataStructures.Clustering;
using AOC.DataStructures.LinkedList;
using AOC.DataStructures.PriorityQueue;
using AOC.DataStructures.Searching;
using AOC.DataStructures.Sorting;
using AOC.Documents.LINQ;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC
{
    class Program : Solver
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose the option:");
            Console.WriteLine("1 - DataStructures tests:");
            Console.WriteLine("2 - Search Alghoritms:");
            Console.WriteLine("3 - Run Puzzles:");
            var k = Console.ReadKey().Key;
            while (k != ConsoleKey.D1 && k != ConsoleKey.D2 && k != ConsoleKey.D3)
            {
                Console.WriteLine("\r\nOption not possible");
                k = Console.ReadKey().Key;
            }
            Console.WriteLine();
            switch (k)
            {
                case ConsoleKey.D1:
                    //DATASTRUCTURES TESTS
                    //BST<string, int>.Example();
                    //LinearProbingHashST<string, int>.Example();
                    //RedBlackBST<string, int>.Example();
                    //SeparateChainingHashST<string, int>.Example();
                    //SequentialSearchST<string, int>.Example();
                    //ST<string, int>.Example();
                    //MinPQ<int>.Example();
                    //MinPQ<int>.Example2();
                    //MaxPQ<int>.Example();
                    //Board.Example();
                    //Documents.REGEX.REGEX.Example();
                    //SortingTest.TestSortingAlgorithms();
                    Cache.Example();
                    break;
                case ConsoleKey.D2:
                    //SEARCH ALGHORITMS
                    List<string> InterestedClasses = ResearchAlgorithmsAttribute.SearchFolder(ResearchAlgorithmsAttribute.TypologyEnum.MachineInstructions);
                    foreach (var c in InterestedClasses) 
                        Console.WriteLine(c);
                    Console.ReadLine();
                    break;
                case ConsoleKey.D3:
                    //RUN PUZZLES
                    //Set Year, Day, Part and Test (Yes/No). If you are going to play Test, you must set a string data test in ReaderInput.cs
                    string part = "1"; //2, 1, 1T, 2T
                    Solver solver = new Solver(2016, 11, part);
                    object input = solver.FetchInput(Model.InputType.Text);
                    solver.RunPuzzle(input);
                    Console.WriteLine();
                    Console.WriteLine($"Solution for part {part} is {solver.solution}");
                    Console.ReadLine();
                    break;
            }
        }
    }
}