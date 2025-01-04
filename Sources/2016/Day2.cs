using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;

namespace AOC2016
{
    [ResearchAlgorithms(TypologyEnum.Keypad)]
    public class Day2 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            int[,] tastierino = new int[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    tastierino[i, j] = (j) + 1 + (3 * i);
                }
            }
            int[] pos = new int[2];
            pos[0] = 1;
            pos[1] = 1;
                foreach (string s in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
                {
                if (!string.IsNullOrEmpty(s))
                {
                    string button = s.Trim();
                    foreach (char instruction in button)
                    {
                        switch (instruction)
                        {
                            case 'U':
                                if (pos[0] > 0) pos[0]--;
                                break;
                            case 'R':
                                if (pos[1] < 2) pos[1]++;
                                break;
                            case 'D':
                                if (pos[0] < 2) pos[0]++;
                                break;
                            case 'L':
                                if (pos[1] > 0) pos[1]--;
                                break;
                        }
                    }
                    solution += tastierino[pos[0], pos[1]].ToString();
                }
            }
        }



        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            int[,] tastierino = new int[5, 5];
            tastierino[0, 2] = 1;
            tastierino[1, 1] = 2;
            tastierino[1, 2] = 3;
            tastierino[1, 3] = 4;
            tastierino[2, 0] = 5;
            tastierino[2, 1] = 6;
            tastierino[2, 2] = 7;
            tastierino[2, 3] = 8;
            tastierino[2, 4] = 9;
            tastierino[3, 1] = 10;
            tastierino[3, 2] = 11;
            tastierino[3, 3] = 12;
            tastierino[4, 2] = 13;
            int[] pos = new int[2];
            pos[0] = 2;
            pos[1] = 0;
            foreach (string s in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(s))
                {
                    string button = s.Trim();
                    foreach (char instruction in button)
                    {
                        switch (instruction)
                        {
                            case 'U':
                                if (pos[0] > 0 && (tastierino[pos[0] - 1, pos[1]]) != 0) pos[0]--;
                                break;
                            case 'R':
                                if (pos[1] < 4 && (tastierino[pos[0], pos[1] + 1]) != 0) pos[1]++;
                                break;
                            case 'D':
                                if (pos[0] < 4 && (tastierino[pos[0] + 1, pos[1]]) != 0) pos[0]++;
                                break;
                            case 'L':
                                if (pos[1] > 0 && (tastierino[pos[0], pos[1] - 1]) != 0) pos[1]--;
                                break;
                        }
                    }
                    solution += tastierino[pos[0], pos[1]].ToString("X");
                }
            }
        }
    }
}
