using AOC;
using AOC.DataStructures.Clustering;
using AOC.DataStructures.PriorityQueue;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using AOC.Utilities.Math;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;
using Solver = AOC.Solver;

namespace AOC2015
{
    [ResearchAlgorithms(title: "Day 17: No Such Thing as Too Much",
                        TypologyEnum.Ingredients | TypologyEnum.Combinatorial,
                        ResolutionEnum.None, //Forse BruteForce
                        DifficultEnum.Medium,
                        "Combinazioni di contenitori per riempire esattamente un volume target, incluso calcolo del numero minimo di contenitori")]

    public class Day17 : Solver, IDay
    {
        public int counter = 0;
        public int n_ints;
        public List<int> ints;
        public void Part1(object input, bool test, ref object solution)
        {  
            
            string inputText = (string)input;
            var inputlist = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            ints = new List<int>();
            foreach(var i in inputlist) 
            {
                if(!string.IsNullOrEmpty(i)) ints.Add(int.Parse(i));
            }
            List<List<int>> allCombinations = Combinatorial.GetAllCombinations(ints);
            int sum = 0;
            int target = test?25:150;

            foreach (var item in allCombinations)
            {
                if (item.Sum() == target)
                {
                    sum += 1;
                    Console.WriteLine(string.Join(", ", item));
                }
            }
            solution = sum;
        }


        

        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var inputlist = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            ints = new List<int>();
            foreach (var i in inputlist)
            {
                if (!string.IsNullOrEmpty(i)) ints.Add(int.Parse(i));
            }
            List<List<int>> allCombinations = Combinatorial.GetAllCombinations(ints);
            int sum = 0;
            int target = test ? 25 : 150;
            var minElement = allCombinations.OrderBy(list => list.Count).Where(c=>c.Count()!=0).Where(s=>s.Sum()==target).FirstOrDefault().Count();

            foreach (var item in allCombinations)
            {
                if (item.Sum() == target && item.Count()==minElement)
                {
                    sum += 1;
                    Console.WriteLine(string.Join(", ", item));
                }
            }
            solution = sum;
        }

    }
}