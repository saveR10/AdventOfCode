using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;

namespace AOC2025
{
    [ResearchAlgorithmsAttribute(ResolutionEnum.Regex)]
    public class Day2 : Solver, IDay
    {
        public long sum = 0;
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;

            foreach (string ranges in inputText.Split(Delimiter.delimiter_comma, StringSplitOptions.None))
            {
                long range1 = long.Parse(ranges.Split(Delimiter.delimiter_dash, StringSplitOptions.None)[0]);
                long range2 = long.Parse(ranges.Split(Delimiter.delimiter_dash, StringSplitOptions.None)[1]);
                Console.WriteLine($"Nuovi range: {range1}, {range2}");

                while (range1 <= range2)
                {
                    string s = range1.ToString();

                    if (s.Length % 2 != 0)
                    {
                        range1++;
                        continue;
                    }

                    int half = s.Length / 2;

                    if (s.Substring(0, half).Equals(s.Substring(half, half)))
                    {
                        sum += long.Parse(s);
                    }

                    range1++;
                }


            }
            solution = sum;
        }


        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;

            long sum = 0;

            foreach (string range in inputText.Split(','))
            {
                var parts = range.Split('-');

                long range1 = long.Parse(parts[0]);
                long range2 = long.Parse(parts[1]);

                Console.WriteLine($"Nuovi range: {range1}, {range2}");

                while (range1 <= range2)
                {
                    string s = range1.ToString();

                    if (Regex.IsMatch(s, @"^(\d+)\1+$"))
                    {
                        sum += range1;
                    }

                    range1++;
                }
            }

            solution = sum;


        }


    }
}
