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
using static AOC.SearchAlghoritmhs.ResearchAlghoritmsAttribute;
using System.Runtime.InteropServices;

namespace AOC2020
{
    public class Day1 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            long prod = 0;
            string inputText = (string)input;
            List<int> numbers = new List<int>();
            foreach (var l in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(l))
                {
                    numbers.Add(int.Parse(l));
                }
            }
            for(int i = 0; i < numbers.Count-1; i++)
            {
                for (int j = i+1; j < numbers.Count; j++)
                {
                    if (numbers[i] + numbers[j] == 2020) prod += (numbers[i] * numbers[j]);
                }

            }
            solution = prod;
        }
        public void Part2(object input, bool test, ref object solution)
        {
            long prod = 0;
            string inputText = (string)input;
            List<int> numbers = new List<int>();
            foreach (var l in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(l))
                {
                    numbers.Add(int.Parse(l));
                }
            }
            for (int i = 0; i < numbers.Count - 2; i++)
            {
                for (int j = i + 1; j < numbers.Count-1; j++)
                {
                    for (int k = j + 1; k < numbers.Count; k++)
                    {
                        if (numbers[i] + numbers[j]+ numbers[k] == 2020) prod += (numbers[i] * numbers[j]* numbers[k]);
                    }
                }
            }
            solution = prod;
        }
    }
}

