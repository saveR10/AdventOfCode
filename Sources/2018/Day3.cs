using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using AOC.Utilities.Map;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Schema;
using static AOC.SearchAlghoritmhs.ResearchAlghoritmsAttribute;
using static AOC2015.Day7;

namespace AOC2018
{
    public class Day3 : Solver, IDay
    {
        public static string[,] map = new string[10000,10000];
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            foreach (var claim in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(claim))
                {
                    int ID = int.Parse(Regex.Match(claim, @"#(\d+)").ToString().Trim('#').Trim());
                    int FromLeft = int.Parse(Regex.Match(claim, @"@ (\d+),").ToString().Trim('@').Trim(',').Trim());
                    int FromTop = int.Parse(Regex.Match(claim, @",(\d+):").ToString().Trim(':').Trim(',').Trim());
                    int wide = int.Parse(Regex.Match(claim, @": (\d+)x").ToString().Trim(':').Trim('x').Trim());
                    int tall = int.Parse(Regex.Match(claim, @"x(\d+)").ToString().Trim('x').Trim());

                    for(int i = FromTop; i < FromTop+tall; i++)
                    {
                        for (int j = FromLeft; j < FromLeft+wide; j++)
                        {
                            if (map[i, j] == null) map[i, j] = "";
                            map[i, j] += $"{ID}:";
                        }
                    }
                }
            }
            int overlapped = CountOverlapping();
            solution = overlapped;
        }
        public int CountOverlapping()
        {
            int overlapping = 0;
            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < 10000; j++)
                {
                    if (map[i, j]!=null && map[i, j].Trim(':').Split(':').Count() > 1) 
                        overlapping++;
                }
            }
            return overlapping;
        }
        public List<int> IDs;
        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            IDs = new List<int>();
            foreach (var claim in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(claim))
                {
                    int ID = int.Parse(Regex.Match(claim, @"#(\d+)").ToString().Trim('#').Trim());
                    int FromLeft = int.Parse(Regex.Match(claim, @"@ (\d+),").ToString().Trim('@').Trim(',').Trim());
                    int FromTop = int.Parse(Regex.Match(claim, @",(\d+):").ToString().Trim(':').Trim(',').Trim());
                    int wide = int.Parse(Regex.Match(claim, @": (\d+)x").ToString().Trim(':').Trim('x').Trim());
                    int tall = int.Parse(Regex.Match(claim, @"x(\d+)").ToString().Trim('x').Trim());

                    for (int i = FromTop; i < FromTop + tall; i++)
                    {
                        for (int j = FromLeft; j < FromLeft + wide; j++)
                        {
                            if (map[i, j] == null) map[i, j] = "";
                            map[i, j] += $"{ID}:";
                            
                            if(!IDs.Contains(ID))IDs.Add(ID);
                        }
                    }
                }
            }
            int freeclaim = FindFreeClaim();
            solution = freeclaim;
        }
        public int FindFreeClaim()
        {
            int freeclaim = 0;
            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < 10000; j++)
                {
                    if (map[i, j] != null && map[i, j].Trim(':').Split(':').Count() == 1)
                    {
                        List<int> tempid = new List<int>();
                        foreach(var tid in map[i, j].Trim(':').Split(':'))
                        {
                            tempid.Add(int.Parse(tid));
                        }
                        foreach(var id in tempid)
                        {
                            if(IDs.Contains(id)) IDs.Remove(id);
                        }
                    }                        
                }
            }

            return freeclaim;
        }
    }
}
