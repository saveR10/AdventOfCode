using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Schema;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;
using System.Runtime.InteropServices;

namespace AOC2022
{
    public class Day1 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            List<int> Elves = new List<int>();
            int Calories = 0;
            foreach (var l in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(l))
                {
                    Calories += int.Parse(l);
                }
                else
                {
                    Elves.Add(Calories);
                    Calories = 0;
                }
            }
            Elves.Add(Calories);
            Calories = 0;

            solution = Elves.Max();
        }
        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            AOC.DataStructures.PriorityQueue.MaxPQ<int> Elves = new AOC.DataStructures.PriorityQueue.MaxPQ<int>();
            int Calories = 0;
            foreach (var l in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(l))
                {
                    Calories += int.Parse(l);
                }
                else
                {
                    Elves.Insert(Calories);
                    Calories = 0;
                }
            }
            Elves.Insert(Calories);
            Calories = 0;

            int sum = 0;
            sum += Elves.DelMax();
            sum += Elves.DelMax();
            sum += Elves.DelMax();
            solution = sum;
        }
    }
}

