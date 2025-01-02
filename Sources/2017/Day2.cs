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
    public class Day2 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            int sum = 0;
            int checksum = 0;
            string inputText = (string)input;
            foreach (var spreadsheet in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(spreadsheet))
                {
                    List<int> s = spreadsheet.Split(Delimiter.delimiter_Tab, StringSplitOptions.None).ToList().ConvertAll(x => int.Parse(x));
                    sum += (s.Max() - s.Min());
                }
            }
            solution = sum;
        }
        public void Part2(object input, bool test, ref object solution)
        {
            float sum = 0;
            int checksum = 0;
            string inputText;
            if (test)
            {
                inputText = @"5 9 2 8
9 4 7 3
3 8 6 5";
            }
            else
            {
                inputText = (string)input;
            }
            foreach (var spreadsheet in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(spreadsheet))
                {
                    List<float> s;
                    if (test)
                    {
                        s = spreadsheet.Split(Delimiter.delimiter_space, StringSplitOptions.None).ToList().ConvertAll(x => float.Parse(x));
                    }
                    else
                    {
                        s = spreadsheet.Split(Delimiter.delimiter_Tab, StringSplitOptions.None).ToList().ConvertAll(x => float.Parse(x));
                    }
                    for (int i = 0; i < s.Count-1; i++)
                    {
                        for (int j = i+1; j < s.Count; j++)
                        {
                            var d = Math.Max(s[i], s[j]) / Math.Min(s[i], s[j]);
                            if (d==(int)d)
                            {
                                sum += d;
                            }
                        }
                    }

                }
            }
            solution = sum;
        }
    }
}
