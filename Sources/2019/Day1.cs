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
using System.Runtime.InteropServices;

namespace AOC2019
{
    public class Day1 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            int sum = 0;
            string inputText = (string)input;
            foreach (var l in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(l)) sum += (int.Parse(l) / 3) - 2;
            }
            solution = sum;
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

