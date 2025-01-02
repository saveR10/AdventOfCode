using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2024
{
    [ResearchAlgorithmsAttribute(ResearchAlgorithmsAttribute.ResolutionEnum.Overflow)]
    public class Day11 : Solver, IDay
    {
        public List<long> Stones = new List<long>();
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;//"0 1 10 99 999";

            foreach (string stone in inputText.Split(Delimiter.delimiter_space, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(stone))
                {
                    Stones.Add(long.Parse(stone));
                }
            }
            int Blink = 0;
            while (Blink < 25)
            {
                List<long> NewStones = new List<long>();

                foreach (var stone in Stones)
                {
                    if (stone == 0) NewStones.Add(1);
                    else if (stone.ToString().Length % 2 == 0)
                    {
                        NewStones.Add(long.Parse(stone.ToString().Substring(0, stone.ToString().Length / 2)));
                        NewStones.Add(long.Parse(stone.ToString().Substring(stone.ToString().Length / 2, stone.ToString().Length / 2)));
                    }
                    else
                    {
                        NewStones.Add((long)stone * 2024);
                    }
                }
                Stones.Clear();
                Stones = NewStones;
                Blink++;
            }
            solution = Stones.Count();

        }

        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;//"0 1 10 99 999";
            Dictionary<long, long> stonesCount = new Dictionary<long, long>();
            foreach (string stone in inputText.Split(' '))
            {
                if (!string.IsNullOrEmpty(stone))
                {
                    long value = long.Parse(stone);
                    if (stonesCount.ContainsKey(value))
                        stonesCount[value]++;
                    else
                        stonesCount[value] = 1;
                }
            }

            int blink = 0;
            while (blink < 75)
            {
                Dictionary<long, long> newStonesCount = new Dictionary<long, long>();

                foreach (var kvp in stonesCount)
                {
                    long stoneValue = kvp.Key;
                    long count = kvp.Value;

                    if (stoneValue == 0)
                    {
                        if (newStonesCount.ContainsKey(1))
                            newStonesCount[1] += count;
                        else
                            newStonesCount[1] = count;
                    }
                    else if (stoneValue.ToString().Length % 2 == 0)
                    {
                        string stoneStr = stoneValue.ToString();
                        long left = long.Parse(stoneStr.Substring(0, stoneStr.Length / 2));
                        long right = long.Parse(stoneStr.Substring(stoneStr.Length / 2));

                        if (newStonesCount.ContainsKey(left))
                            newStonesCount[left] += count;
                        else
                            newStonesCount[left] = count;

                        if (newStonesCount.ContainsKey(right))
                            newStonesCount[right] += count;
                        else
                            newStonesCount[right] = count;
                    }
                    else
                    {
                        long newValue = stoneValue * 2024;
                        if (newStonesCount.ContainsKey(newValue))
                            newStonesCount[newValue] += count;
                        else
                            newStonesCount[newValue] = count;
                    }
                }

                stonesCount = newStonesCount;
                blink++;
            }

            long totalStones = 0;
            foreach (var kvp in stonesCount)
            {
                totalStones += kvp.Value;
            }
            solution = totalStones;
        }

    }
}
