using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using NUnit.Framework.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Schema;
using static AOC.SearchAlghoritmhs.ResearchAlghoritmsAttribute;
using System.Runtime.InteropServices;

namespace AOC2021
{
    public class Day1 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            int count= 0;
            int depth_memory = 0;
            string inputText = (string)input;
            foreach (var l in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(l))
                {
                    int depth = int.Parse(l);
                    if (depth_memory!=0) if (depth > depth_memory) count += 1;
                    depth_memory= depth;
                }
            }
            solution = count;
        }
        public void Part2(object input, bool test, ref object solution)
        {
            int count = 0;
            int depth_memory = 0;
            List<int> depths = new List<int>();
            string inputText = (string)input;
            foreach (var l in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(l))
                {
                    depths.Add(int.Parse(l));
                }
            }
            int depth = 0;
            for (int i = 2; i < depths.Count; i++)
            {
                depth = depths[i] + depths[i - 1] + depths[i - 2];
                if (depth_memory != 0) if (depth > depth_memory) count += 1;
                depth_memory = depth;
            }


            solution = count;
        }
    }
}

