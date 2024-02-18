using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using Newtonsoft.Json.Bson;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using static AOC.SearchAlghoritmhs.ResearchAlghoritmsAttribute;

namespace AOC2016
{
    [ResearchAlghoritmsAttribute(TypologyEnum.Decompressing)]
    public class Day9 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputString = (string)input;
            int decompressedLength = 0;
            string[] inputList = inputString.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            foreach (var line in inputList)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        string marker = "";
                        if (line[i] == '(')
                        {
                            marker = FindMarker(line, i);
                            if (!string.IsNullOrEmpty(marker))
                            {
                                i = i + marker.Length + 2;
                                int characters = int.Parse(marker.Split(Delimiter.delimiter_x, StringSplitOptions.None)[0]);
                                int times = int.Parse(marker.Split(Delimiter.delimiter_x, StringSplitOptions.None)[1]);
                                string repetition = line.Substring(i, characters);
                                decompressedLength = decompressedLength + (repetition.Length) * times;
                                i = i + characters - 1;
                            }
                        }
                        else
                        {
                            decompressedLength++;
                        }
                    }
                }
            }
            solution = decompressedLength;
        }
        public string FindMarker(string line, int index)
        {
            string marker = "";
            int end = -1;
            for (int i = index; i < line.Length; i++)
            {
                if (line[i] == ')')
                {
                    end = i;
                    break;
                }
            }
            if (end >= 0)
            {
                var a = line.Substring(index + 1, end - index - 1);
                if (a.Split(Delimiter.delimiter_x, StringSplitOptions.None).Count() == 2)
                {
                    marker = a;
                }
            }
            return marker;
        }

        public void Part2(object input, bool test, ref object solution)
        {
            string inputString = (string)input;
            if (test) inputString = @"(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN
(3x3)XYZ
X(8x2)(3x3)ABCY
(27x12)(20x12)(13x14)(7x10)(1x12)A";
            long decompressedLength = 0;
            string[] inputList = inputString.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            foreach (var line in inputList)
            {
                decompressedLength = 0;
                string marker = "";
                if (!string.IsNullOrEmpty(line))
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] == '(')
                        {
                            marker = FindMarker(line, i);
                            if (!string.IsNullOrEmpty(marker))
                            {
                                i = i + marker.Length + 2;
                                DecompressedString decompressedString = ApplyMarker(line,i,marker);
                                while (CheckChars(decompressedString.repetition))
                                {
                                    marker = FindMarker(line, i);
                                    if (!string.IsNullOrEmpty(marker))
                                    {
                                        i = i + marker.Length + 2;
                                        decompressedString.characters = int.Parse(marker.Split(Delimiter.delimiter_x, StringSplitOptions.None)[0]);
                                        decompressedString.times *= int.Parse(marker.Split(Delimiter.delimiter_x, StringSplitOptions.None)[1]);
                                        decompressedString.repetition = line.Substring(i, decompressedString.characters);
                                    }
                                }
                                decompressedLength += decompressedString.characters * decompressedString.times;
                                i = i + decompressedString.characters -1;
                            }
                        }
                        else
                        {
                            decompressedLength += 1;
                        }
                    }
                }
            }
            solution = decompressedLength;
        }
        public DecompressedString ApplyMarker(string line, int i, string marker)
        {
            DecompressedString decompressedString = new DecompressedString();
            decompressedString.characters = int.Parse(marker.Split(Delimiter.delimiter_x, StringSplitOptions.None)[0]);
            decompressedString.times = int.Parse(marker.Split(Delimiter.delimiter_x, StringSplitOptions.None)[1]);
            decompressedString.repetition = line.Substring(i, decompressedString.characters);

            return decompressedString;
        }
        public class DecompressedString
        {
            public string repetition;
            public int times;
            public int characters;
        }
        public bool CheckChars(string repetition)
        {
            bool ret = false;
            foreach (var r in repetition)
            {
                if (r == '(') return true;

            }
            return ret;
        }
        public bool ContainsMarker(string line, int index)
        {
            bool ret = false;
            string marker = "";
            int end = -1;
            for (int j = 0; j < line.Length; j++)
            {
                if (line[j] == '(')
                {
                    marker = FindMarker(line, j);
                    if (!string.IsNullOrEmpty(marker)) ret = true;
                }
            }


            return ret;
        }

    }
}
