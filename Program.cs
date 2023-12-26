using AOC.DataStructures;
using AOC.DataStructures.AlghoritmsOptimization;
using AOC.DataStructures.Clustering;
using AOC.DataStructures.LinkedList;
using AOC.DataStructures.PriorityQueue;
using AOC.DataStructures.Sorting;
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
            string part = "2"; //2, 1, 1T, 2T
            Solver solver = new Solver(2015,7,part);
            object input = solver.FetchInput(Model.InputType.Text);
            solver.RunPuzzle(input);
            Console.WriteLine($"Solution for part {part} is {solver.solution}");
            Console.ReadLine();

            //Search Alghoritms
            //List<string> InterestedClasses = ResearchAlghoritmsAttribute.SearchFolder(ResearchAlghoritmsAttribute.TypologyEnum.Hashing);

        }
    }
}
