using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
 
 
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using static AOC.SearchAlghoritmhs.ResearchAlghoritmsAttribute;

namespace AOC2016
{
    public class Day6 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string solutiontemp = "";
            string message = (string)input;
            int rowlegth = message.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0].Length;
            string[] lines = message.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            for(int c = 0; c < rowlegth; c++)
            {
                Dictionary<char, int> letters = new Dictionary<char, int>();
                for(int r = 0; r < lines.Length; r++)
                {
                    if (!letters.ContainsKey(lines[r][c])) letters.Add(lines[r][c], 1);
                    else letters[lines[r][c]] += 1;
                }
                
                solutiontemp += letters.Keys.Where(l => letters[l].Equals(int.Parse(letters.Values.Max().ToString()))).First();
            }
            solution=solutiontemp;
        }

        public void Part2(object input, bool test, ref object solution)
        {
            string solutiontemp = "";
            string message = (string)input;
            int rowlegth = message.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0].Length;
            string[] lines = message.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            for (int c = 0; c < rowlegth; c++)
            {
                Dictionary<char, int> letters = new Dictionary<char, int>();
                for (int r = 0; r < lines.Length; r++)
                {
                    if (!letters.ContainsKey(lines[r][c])) letters.Add(lines[r][c], 1);
                    else letters[lines[r][c]] += 1;
                }

                solutiontemp += letters.Keys.Where(l => letters[l].Equals(int.Parse(letters.Values.Min().ToString()))).First();
            }
            solution = solutiontemp;
        }
    }
}
