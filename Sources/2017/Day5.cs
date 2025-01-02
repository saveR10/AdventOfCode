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
using AOC.Utilities.Dynamic;
using System.Text.RegularExpressions;

namespace AOC2017
{
    public class Day5 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        { 
            int step = 0;
            List<int> Maze = new List<int>();
            string inputText = (string)input;
            foreach (var data in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(data))
                {
                    Maze.Add(int.Parse(data));
                }
            }

            int init = 0;
            while(init + Maze[init] < Maze.Count())
            {
                int tempInit = init;
                init = init + Maze[init];
                Maze[tempInit] += 1;
                step++;
            }
            solution = step+1;
        }
        public void Part2(object input, bool test, ref object solution)
        {
            int step = 0;
            List<int> Maze = new List<int>();
            string inputText = (string)input;
            foreach (var data in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(data))
                {
                    Maze.Add(int.Parse(data));
                }
            }

            int init = 0;
            while (init + Maze[init] < Maze.Count())
            {
                int tempInit = init;
                init = init + Maze[init];
                if (Maze[tempInit] >= 3) Maze[tempInit] -= 1;
                else Maze[tempInit] += 1;
                step++;
            }
            solution = step + 1;
        }
    }
}
