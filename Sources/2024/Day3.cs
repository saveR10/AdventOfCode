using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;

namespace AOC2024
{
    [ResearchAlgorithms(ResolutionEnum.Regex)]
    public class Day3 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            int result = 0;

            string inputText = (string)input;
            foreach (string memory in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(memory))
                {
                    string pattern = @"mul\((\d+),(\d+)\)";

                    Regex regex = new Regex(pattern);

                    MatchCollection matches = regex.Matches(memory);
                    foreach (Match match in matches)
                    {
                        Console.WriteLine($"Trovato: {match.Value}");

                        string x = match.Groups[1].Value;
                        string y = match.Groups[2].Value;
                        Console.WriteLine($"X: {x}, Y: {y}");
                        result += int.Parse(x) * int.Parse(y);
                    }
                }
            }
            solution = result;
        }
        //private static readonly Regex Regex2 = new Regex(@"(mul\(([0-9]{1,3}),([0-9]{1,3})\))|(do\(\))|(don't\(\))", RegexOptions.Multiline);

        public void Part2(object input, bool test, ref object solution)
        {
            BigInteger result = 0;
            string inputText;
            if (test) inputText = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
            else inputText = (string)input;

            if (!string.IsNullOrEmpty(inputText))
            {
                string pattern = @"(do\(\)|don't\(\)|mul\((\d+),(\d+)\))";

                MatchCollection matches = Regex.Matches(inputText, pattern);

                bool isEnabled = true;

                foreach (Match match in matches)
                {
                    string token = match.Value;

                    if (token == "do()")
                    {
                        isEnabled = true;
                    }
                    else if (token == "don't()")
                    {
                        isEnabled = false;
                    }
                    else if (isEnabled && match.Groups[2].Success && match.Groups[3].Success)
                    {
                        BigInteger x = BigInteger.Parse(match.Groups[2].Value);
                        BigInteger y = BigInteger.Parse(match.Groups[3].Value);

                        result += (x * y);
                    }
                }
            }
            solution = result;
        }
    }
}
