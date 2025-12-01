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
//    [ResearchAlgorithmsAttribute(ResolutionEnum.Regex)]
    public class Day1 : Solver, IDay
    {
        public enum Direction
        {
            Left,
            Right
        }
        public Direction dir;
        public int dial = 50;
        public int step = 0;
        public int password = 0;
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;

            foreach (string move in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(move))
                {
                    switch (move.Substring(0, 1))
                    {
                        case "L":
                            dir = Direction.Left;
                            break;
                        case "R":
                            dir = Direction.Right;
                            break;

                    }
                    step = int.Parse(move.Substring(1, move.Length - 1));
                    TurnDialer();
                }
            }
            solution = password;
        }
        public void TurnDialer()
        {
            while (step != 0)
            {
                if (dir.Equals(Direction.Left))
                {
                    dial--;
                    step--;
                }
                else
                {
                    dial++;
                    step--;
                }
                if (dial == 100)
                    dial = 0;
                if (dial == -1)
                    dial = 99;
            }
            if (dial == 0)
                password += 1;

        }
        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;

            foreach (string move in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(move))
                {
                    switch (move.Substring(0, 1))
                    {
                        case "L":
                            dir = Direction.Left;
                            break;
                        case "R":
                            dir = Direction.Right;
                            break;

                    }
                    step = int.Parse(move.Substring(1, move.Length - 1));
                    TurnDialerComplex();
                }
            }
            solution = password;

        }
        public void TurnDialerComplex()
        {
            while (step != 0)
            {
                if (dir.Equals(Direction.Left))
                {
                    dial--;
                    step--;
                }
                else
                {
                    dial++;
                    step--;
                }
                if (dial == 100)
                    dial = 0;
                if (dial == -1)
                    dial = 99;
                if (dial == 0)
                    password += 1;
            }

        }

    }
}
