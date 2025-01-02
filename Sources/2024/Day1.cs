using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;

namespace AOC2024
{
    [ResearchAlgorithmsAttribute(TypologyEnum.Regex)]
    public class Day1 : Solver, IDay
    {
        List<int> firstList = new List<int>();
        List<int> secondList = new List<int>();
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            foreach (string pair in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(pair))
                {
                    firstList.Add(int.Parse(Regex.Split(pair.Trim(), @"\s+")[0]));
                    secondList.Add(int.Parse(Regex.Split(pair.Trim(), @"\s+")[1]));
                }
            }
            int firstCount = firstList.Count();
            int secondCount = secondList.Count();

            int totalDistance = 0;
            for (int i = 0; i < firstCount; i++)
            {
                int firstMin = FindMin(firstList);
                int secondMin = FindMin(secondList);

                totalDistance += (Math.Abs(firstMin - secondMin));
            }
            solution = totalDistance;
        }

        public int FindMin(List<int> List)
        {
            int minTemp = int.MaxValue;
            int minTempPos = 0;
            int i = 0;
            foreach (var item in List)
            {
                if (item < minTemp)
                {
                    minTemp = item;
                    minTempPos = i;
                }
                i++;
            }
            List.RemoveAt(minTempPos);
            return minTemp;
        }
        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            foreach (string pair in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(pair))
                {
                    firstList.Add(int.Parse(Regex.Split(pair.Trim(), @"\s+")[0]));
                    secondList.Add(int.Parse(Regex.Split(pair.Trim(), @"\s+")[1]));
                }
            }
            int firstCount = firstList.Count();
            int secondCount = secondList.Count();

            int similarityScore = 0;
            for (int i = 0; i < firstCount; i++)
            {
                similarityScore += firstList[i]*CountOccurrences(secondList,firstList[i]);

            }
            solution = similarityScore;
        }
        static int CountOccurrences(List<int> list, int target)
        {
            int count = 0;
            foreach (int number in list)
            {
                if (number == target)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
