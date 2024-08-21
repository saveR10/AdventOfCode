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

namespace AOC2018
{
    public class Day2 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            int checksum = 0;
            string inputText = (string)input;
            int twicecount = 0;
            int threetimescount = 0;

            foreach (var id in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(id))
                {
                    bool isTwice = false;
                    bool isThreeTimes = false;

                    Dictionary<char, int> twice = new Dictionary<char, int>();
                    Dictionary<char, int> threetimes = new Dictionary<char, int>();
                    foreach (var c in id)
                    {
                        if (!twice.ContainsKey(c)) twice.Add(c, 1);
                        else twice[c] += 1;

                        if (!threetimes.ContainsKey(c)) threetimes.Add(c, 1);
                        else threetimes[c] += 1;
                    }

                    foreach (var t in twice.Keys)
                    {
                        if (twice[t] == 2) isTwice = true;
                    }
                    foreach (var t in threetimes.Keys)
                    {
                        if (threetimes[t] == 3) isThreeTimes = true;
                    }
                    if (isTwice) twicecount++;
                    if (isThreeTimes) threetimescount++;
                }
            }
            checksum = twicecount * threetimescount;
            solution = checksum;
        }
        public void Part2(object input, bool test, ref object solution)
        {
            int checksum = 0;
            string inputText;
            if (test)
            {
                inputText = @"abcde
fghij
klmno
pqrst
fguij
axcye
wvxyz";
            }
            else inputText = (string)input;

            var IDs = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            bool founded = false;
            for (int i = 0; i < IDs.Length - 1; i++)
            {
                for (int j = i + 1; j < IDs.Length; j++)
                {
                    int count = 0;
                    string keyword = "";
                    for (int k = 0; k < IDs[i].Length; k++)
                    {
                        if (IDs[i][k] == IDs[j][k]) { count++; keyword += IDs[i][k]; }

                    }
                    if (count == IDs[i].Length - 1)
                    {
                        solution = keyword;
                        founded = true;
                        break;
                    }
                }
                if (founded) break;
            }
        }
    }
}
