using AOC;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using AOC2015;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;

namespace AOC2016
{
    [ResearchAlgorithms(title: "Day 18: Like a Rogue",
                        typology: ResearchAlgorithmsAttribute.TypologyEnum.None,
                        resolution: ResearchAlgorithmsAttribute.ResolutionEnum.None,
                        difficult: ResearchAlgorithmsAttribute.DifficultEnum.Easy,
                        note: "Automaton-like tile evolution. Each row is derived only from the previous one using local rules. No graph search required; linear DP with state reduction. Part 2 scales by avoiding full grid storage.")]
    public class Day18 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputString = (string)input;
            int testcase = 1;
            var tiles = inputString.Split(new[] { "\n\n", "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            int rows = 0;
            if (test)
            {
                switch (testcase)
                {
                    case 0: rows = 3; break;
                    case 1: rows = 10; break;
                }
            }
            else
            {
                testcase = 0;
                rows = 40;
            }
            string tile = tiles[testcase];
            string oldtile = tile;
            int traps = 0;
            traps += oldtile.Count(t => t.Equals('.'));
            string newtile = "";
            for (int r = 0; r < rows-1; r++)
            {
                for (int c = 0; c < oldtile.Length; c++)
                {
                    string last = "";
                    if (c == 0) last = "." + oldtile.Substring(c, 2);
                    else if (c == oldtile.Length - 1) last = oldtile.Substring(c - 1, 2) + ".";
                    else last = oldtile.Substring(c - 1, 3);

                    if (firstRule(last) || secondRule(last) || thirdRule(last) || fourthRule(last))
                        newtile+="^";
                    else
                        newtile+=".";
                }
                traps += newtile.Count(t => t.Equals('.'));
                oldtile = newtile;
                newtile = "";
            }
            solution = traps;
        }
        public bool firstRule(string last)
        {
            return ((last[0].Equals('^') && last[1].Equals('^') && last[2] == '.'));
        }
        public bool secondRule(string last)
        {
            return ((last[0].Equals('.') && last[1].Equals('^') && last[2] == '^'));
        }
        public bool thirdRule(string last)
        {
            return ((last[0].Equals('^') && last[1].Equals('.') && last[2] == '.'));
        }
        public bool fourthRule(string last)
        {
            return ((last[0].Equals('.') && last[1].Equals('.') && last[2] == '^'));
        }

        public void Part2(object input, bool test, ref object solution)
        {
            string inputString = (string)input;
            int testcase = 1;
            var tiles = inputString.Split(new[] { "\n\n", "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            int rows = 0;
            if (test)
            {
                switch (testcase)
                {
                    case 0: rows = 3; break;
                    case 1: rows = 10; break;
                }
            }
            else
            {
                testcase = 0;
                rows = 400000;
            }
            string tile = tiles[testcase];
            string oldtile = tile;
            int traps = 0;
            traps += oldtile.Count(t => t.Equals('.'));
            string newtile = "";
            for (int r = 0; r < rows - 1; r++)
            {
                for (int c = 0; c < oldtile.Length; c++)
                {
                    string last = "";
                    if (c == 0) last = "." + oldtile.Substring(c, 2);
                    else if (c == oldtile.Length - 1) last = oldtile.Substring(c - 1, 2) + ".";
                    else last = oldtile.Substring(c - 1, 3);

                    if (firstRule(last) || secondRule(last) || thirdRule(last) || fourthRule(last))
                        newtile += "^";
                    else
                        newtile += ".";
                }
                traps += newtile.Count(t => t.Equals('.'));
                oldtile = newtile;
                newtile = "";
            }
            solution = traps;
        }

    }
}