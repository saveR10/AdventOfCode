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

namespace AOC2018
{
    public class Day1 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            int count = 0;
            string inputText = (string)input;
            foreach (var l in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if(!string.IsNullOrEmpty(l)) count += int.Parse(l);
            }
            solution = count;
        }
        public void Part2(object input, bool test, ref object solution)
        {
            List<int> twice = new List<int>();
            int count = 0;
            string inputText = (string)input;
            bool founded = false;
            while (!founded)
            {
                foreach (var l in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
                {
                    if (!string.IsNullOrEmpty(l))
                    {
                        count += int.Parse(l);
                        if (!twice.Contains(count)) twice.Add(count);
                        else
                        {
                            solution = count;
                            founded = true;
                            break;
                        }
                    }
                    Console.WriteLine(count);
                }
                Console.WriteLine();
                Console.WriteLine(count);
                Console.WriteLine();
            }
        }
    }
}
