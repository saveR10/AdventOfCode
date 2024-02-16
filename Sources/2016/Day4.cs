using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using NUnit.Framework;
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
        public void Part1(object input, bool test, ref object solution)
        {
            int conta = 0;
            string inputText = (string)input;
            foreach (string t in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(t))
                {
                    Dictionary <char,int> letters = new Dictionary<char,int>();
                    string[] rooms = t.Split(Delimiter.delimiter_SquareBrackets, StringSplitOptions.None)[0].Split(Delimiter.delimiter_dash,StringSplitOptions.None);
                    int sectorID = int.Parse(rooms[rooms.Length-1]);
                    
                    string checkSum = t.Split(Delimiter.delimiter_SquareBrackets, StringSplitOptions.None)[1];
                    for (int i=0;i<rooms.Length-1;i++)
                    {
                        foreach(var l in rooms[i])
                        {
                            if (!letters.ContainsKey(l)) letters.Add(l, 1);
                            else letters[l] += 1;
                        }
                    }
                    FindMax(letters);
                }
            }
            solution = conta;
        }

        public string FindMax(Dictionary<char,int> letters)
        {
            int max = 0;
            char key;
            foreach(var k in letters.Keys)
            {
                if (letters[k] > max) { max=letters[k]; key = k; }
            }
            return "";
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
