using AOC.DataStructures;
using AOC.DataStructures.AlghoritmsOptimization;
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
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    class Program : Solver
    {
        static void Main(string[] args)
        {
            var a1 = "Primo commit - 1";
            var a2 = "Primo commit - 2";
            var b1 = "Secondo commit - 1";
            var b2 = "Secondo commit - 2";
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
                    SeparateChainingHashST<string, int>.Example();
                    SequentialSearchST<string, int>.Example();
                    ST<string, int>.Example();
                    break;
                case ConsoleKey.D2:
                    //SEARCH ALGHORITMS
                    List<string> InterestedClasses = ResearchAlghoritmsAttribute.SearchFolder(ResearchAlghoritmsAttribute.TypologyEnum.Hashing);
                    foreach (var c in InterestedClasses) Console.WriteLine(c);
                    Console.ReadLine();
                    break;
                case ConsoleKey.D3:
                    //RUN PUZZLES
                    string part = "1"; //2, 1, 1T, 2T
                    Solver solver = new Solver(2015, 4, part);
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