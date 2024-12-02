using AOC;
using AOC.DataStructures.Clustering;
using AOC.DataStructures.PriorityQueue;
using AOC.Model;
using AOC.SearchAlghoritmhs;
 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using static AOC.SearchAlghoritmhs.ResearchAlghoritmsAttribute;
using Solver = AOC.Solver;

namespace AOC2015
{
    [ResearchAlghoritmsAttribute(TypologyEnum.Game)] //Look And Say game
    public class Day10 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var inputnumber = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0];
            int match = 40;
            long number=long.Parse(inputnumber);
            string stringnumber = number.ToString();
            string newNumber = "";
            while (match != 0)
            {
                match--;
                string memory = "";
                int count = 0;
                newNumber = "";
                foreach(var item in stringnumber.ToString())
                {
                    if (string.IsNullOrEmpty(memory)) memory = item.ToString();
                    else
                    {
                        if (item.ToString().Equals(memory)) count++;
                        else
                        {
                            newNumber+=(count+1).ToString()+memory;
                            memory = item.ToString();
                            count = 0;
                        }
                    }
                }
                newNumber += (count + 1).ToString() + memory;
                stringnumber = newNumber.ToString();
            }

            solution = stringnumber.Count();
        }
        //https://www.reddit.com/r/adventofcode/comments/3w6h3m/comment/cxtso95/?utm_source=share&utm_medium=web2x&context=3
        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var inputnumber = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0];
            int match = 50;
            string stringinput = inputnumber;
            for (int i = 0; i < match; i++)
            {
                stringinput = LookAndSay(stringinput);
                Console.WriteLine($"{i + 1}:{ stringinput.Length}");
            }

            solution = stringinput.Length;
        }

        public string LookAndSay(string stringinput)
        {
            var captures = Regex.Match(stringinput, @"((.)\2*)+").Groups[1].Captures;
            return string.Concat(from c in captures.Cast<Capture>()
                                 let v = c.Value
                                 select v.Length + v.Substring(0, 1));
        }
    }
}
