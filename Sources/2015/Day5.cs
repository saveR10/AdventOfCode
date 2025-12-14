using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;

namespace AOC2015
{
    [ResearchAlgorithms(title: "Day 5: Doesn't He Have Intern-Elves For This?",
                        TypologyEnum.TextRules, 
                        ResolutionEnum.Regex, //Opzionale, ma preferibile in questo contesto
                        DifficultEnum.Medium,
                        "Pattern matching su stringhe con regole testuali")]
    public class Day5 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var list = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            string vowels = "aeiou";
            List<string> words = new List<string>() { "ab", "cd", "pq", "xy" };
            int nice = 0;
            for (int i = 0; i < list.Length - 1; i++)
            {
                bool f_vowels = false;
                bool f_twice = false;
                bool f_words = false;

                int n_vowels = 0;
                for (int j = 0; j < list[i].Length; j++)
                {
                    if (vowels.Contains(list[i][j])) n_vowels++;
                    if(j>0) if (list[i][j].ToString().Equals(list[i][j-1].ToString()))  f_twice = true;
                }
                if (n_vowels >= 3) f_vowels = true;
                foreach (var w in words)
                {
                    if (list[i].Contains(w)) f_words = true;
                }

                if (f_vowels && f_twice && !f_words) { nice++; }
            }
            solution = nice;
        }

        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var list = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            int nice = 0;
            for (int i = 0; i < list.Length - 1; i++)
            {
                bool f_twice = false;
                bool f_repeat = false;

                for (int j = 0; j < list[i].Length-3; j++)
                {
                    var a = list[i][j].ToString() + list[i][j + 1].ToString();
                    for (int k = j+2; k < list[i].Length-1; k++)
                    {
                        var b = list[i][k].ToString()+ list[i][k+1].ToString();
                        if(a==b) f_twice=true;
                    }

                }

                for (int j = 0; j < list[i].Length; j++)
                { 

                if(j>1) if (list[i][j] == list[i][j-2]) f_repeat = true;
                }

                    if (f_twice && f_repeat) { nice++; }
            }
            solution = nice;
        }
    }
}
