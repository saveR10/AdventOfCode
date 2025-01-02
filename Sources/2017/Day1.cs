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

namespace AOC2017
{
    public class Day1 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            int count = 0;
            string inputText = (string)input;
            foreach (var l in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                for (int i = 0; i < l.Length - 1; i++)
                {
                    if (l[i] == l[i + 1]) count += int.Parse(l[i].ToString());
                }
                if (l[0] == l[l.Length - 1]) count += int.Parse(l[0].ToString());
            }
            solution = count;
        }
        public void Part2(object input, bool test, ref object solution)
        {
            int count = 0;
            string inputText = (string)input;
            if (test)
            {
                inputText = @"1212
1221
123425
123123
12131415";
            }
            foreach (var l in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                count = 0;
                for (int i = 0; i < l.Length; i++)
                {
                    if (l[i] == l[(i+(l.Length / 2))% l.Length]) count += int.Parse(l[i].ToString());
                }
            }
            solution = count;
        }
    }
}
