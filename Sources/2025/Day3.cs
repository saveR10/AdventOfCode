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
 //   [ResearchAlgorithmsAttribute(ResolutionEnum.Regex)]
    public class Day3 : Solver, IDay
    {
        public long sum = 0;
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;

            foreach (string bank in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (string.IsNullOrEmpty(bank))
                    continue;
                int max1 = -1;
                int i1 = -1;
                int max2 = -1;
                for (int i = 0; i < bank.Length-1; i++)
                {
                    var b_item = int.Parse(bank[i].ToString());
                    if (max1 < b_item)
                    {
                        max1 = b_item;
                        i1= i;
                    }
                }

                for (int i = bank.Length-1; i > i1 ; i--)
                {
                    var b_item = int.Parse(bank[i].ToString());
                    if (max2 <= b_item)
                    {
                        max2 = b_item;
                    }
                }

                
                string result = $"{max1}{max2}";
                Console.WriteLine(result); 
                sum += int.Parse(result);
                //17585
            }
            solution = sum;
        }


        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;

            foreach (string bank in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (string.IsNullOrEmpty(bank))
                    continue;
                

                string result = PickMaxSequence(bank,0,12);
                Console.WriteLine(result);
                sum += long.Parse(result);
                //17585
            }
            solution = sum;
        }

        string PickMaxSequence(string bank, int startIndex, int needed)
        {
            if (needed == 0)
                return "";

            int maxDigit = -1;
            int maxPos = -1;

            for (int i = startIndex; i < bank.Length - (needed - 1); i++)
            {
                int digit = bank[i] - '0';

                if (digit > maxDigit)
                {
                    maxDigit = digit;
                    maxPos = i;
                }
            }

            return maxDigit + PickMaxSequence(bank, maxPos + 1, needed - 1);
        }

    }
}
