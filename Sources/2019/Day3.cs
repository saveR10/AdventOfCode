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
using static AOC.SearchAlghoritmhs.ResearchAlghoritmsAttribute;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using AOC.Utilities.Map;
using AOC.Utilities.Dynamic;

namespace AOC2019
{
    public class Day3 : Solver, IDay
    {
        int[,] Map;
        public void Part1(object input, bool test, ref object solution)
        {
            int ManhattanDistance = 0;
            int ntest = 0;
            string inputText = "";
            if (test)
            {
                switch (ntest)
                {
                    case 0:
                        inputText = (string)input;
                        break;
                    case 1:
                        inputText = "R8,U5,L5,D3\nU7,R6,D4,L4";
                        break;
                }
            }
            Map = new int[10000,10000];
            int[] Pos = new int[2];
            Pos[0] = 0; Pos[1]=0;
            foreach (var l in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                var points = l.Split(',');
                foreach(var p in points)
                {
                    string d = Regex.Match(p,@"[A-Z]").Value;
                    int n = int.Parse(Regex.Match(p, @"\d+").Value);
                    switch (d)
                    {
                        case "U":
                            Move(ref Pos[0],n);
                            break;
                        case "R":
                            Pos[1] += n;
                            break;
                        case "D":
                            Pos[0] -= n;
                            break;
                        case "L":
                            Pos[1] -= n;
                            break;
                    }
                    Map[Pos[0], Pos[1]] += 1;
                }
            }
            solution = ManhattanDistance;
        }
        public void Move(ref int pos, int n)
        {
            while (n > 0)
            {
                //Map[]
            }
        }
        public void Part2(object input, bool test, ref object solution)
        {
            long sum = 0;
            string inputText = (string)input;
            foreach (var f in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (test) sum = 0;
                if (!string.IsNullOrEmpty(f))
                {
                    long _f = long.Parse(f);
                    while (CalculateMass(ref _f) != 0)
                    {
                        sum += _f;
                    }
                }
            }
            solution = sum;
        }
        public long CalculateMass(ref long fuel)
        {
            long mass = 0;
           fuel = (fuel/ 3) - 2;
            if (fuel < 0) mass = 0;
            else  mass = fuel;
            return mass;
        }
    }
}

