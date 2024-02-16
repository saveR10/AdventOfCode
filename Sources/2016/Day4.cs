using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Linq;
using static AOC.SearchAlghoritmhs.ResearchAlghoritmsAttribute;

namespace AOC2016
{
    public class Day4 : Solver, IDay
    {
        Dictionary<char, int> letters;
        public void Part1(object input, bool test, ref object solution)
        {
            int conta = 0;
            string inputText = (string)input;
            foreach (string t in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(t))
                {
                    letters = new Dictionary<char, int>();
                    string[] rooms = t.Split(Delimiter.delimiter_SquareBrackets, StringSplitOptions.None)[0].Split(Delimiter.delimiter_dash, StringSplitOptions.None);
                    int sectorID = int.Parse(rooms[rooms.Length - 1]);

                    string checkSum = t.Split(Delimiter.delimiter_SquareBrackets, StringSplitOptions.None)[1];
                    for (int i = 0; i < rooms.Length - 1; i++)
                    {
                        foreach (var l in rooms[i])
                        {
                            if (!letters.ContainsKey(l)) letters.Add(l, 1);
                            else letters[l] += 1;
                        }
                    }
                    if (IsRoom(checkSum))
                    {
                        conta += sectorID;
                    }
                }
            }
            solution = conta;
        }
        //436438 th
        public bool IsRoom(string checkSum)
        {
            int indice = 0;
            int num = 0;
            bool ret = false;
            int max = 0;
            char key;
            foreach (var c in checkSum)
            {
                if (letters.ContainsKey(c))
                    if (IsMaxOccurency(c))
                    {
                        ret = true;
                        letters.Remove(c);
                    }
                    else
                    {
                        ret = false;
                    }
            }
            return ret;
        }

        public bool IsMaxOccurency(char c)
        {
            bool ret = true;
            int occ = letters[c];
            foreach (var l in letters.Keys)
            {
                if (letters[l] > occ)
                {
                    ret=false;
                }
            }
            return ret;
        }
        public void Part2(object input, bool test, ref object solution)
        {
            int conta = 0;


            string inputText = (string)input;

            if (test)
            {
                inputText = @"101 301 501
102 302 502
103 303 503
201 401 601
202 402 602
203 403 603";

            }

            for (int i = 0; i < inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None).Length; i += 3)
            {
                for (int j = 0; j < 3; j++)
                {

                    var aa = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[i].Trim();
                    int a = int.Parse(System.Text.RegularExpressions.Regex.Split(aa, @"\s+")[j]);

                    var bb = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[i + 1].Trim();
                    int b = int.Parse(System.Text.RegularExpressions.Regex.Split(bb, @"\s+")[j]);

                    var cc = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[i + 2].Trim();
                    int c = int.Parse(System.Text.RegularExpressions.Regex.Split(cc, @"\s+")[j]);

                    if (IsTriangle(a, b, c)) conta++;

                }
            }
            solution = conta;
            //prova 3
        }
        public bool IsTriangle(int a, int b, int c)
        {
            bool ret = false;
            if ((a + b) > c && (b + c) > a && (a + c) > b) { ret = true; }
            else { }
            return ret;
        }
    }
}
