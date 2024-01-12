using AOC;
using AOC.DataStructures.Clustering;
using AOC.DataStructures.PriorityQueue;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using AOC.Utilities.Math;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
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
using static AOC.SearchAlghoritmhs.ResearchAlghoritmsAttribute;
using Solver = AOC.Solver;

namespace AOC2015
{
    public class Day17 : Solver, IDay
    {
        public int counter = 0;
        public int n_ints;
        public List<int> ints;
        public void Part1(object input, bool test, ref object solution)
        {   //                          Get     Number
            //COMBINATION WITH REPT     OK      OK
            //COMBINATION WITHOUT REPT  OK      OK
            //PERMUTATION WITH REPT     OK      OK
            //PERMUTATION WITHOUT REPT  OK      OK
            //DISPOSITION WITH REPT
            //DISPOSITION WITHOUT REPT

            Combinatorial.ExampleDispositionsWithRept1();
            Combinatorial.ExampleDispositionsWithoutRept1();
            
            Combinatorial.ExamplePermutationsWithoutRept1();
            Combinatorial.ExamplePermutationsWithoutRept2();
            Combinatorial.ExamplePermutationsWithRept1();



            string inputText = (string)input;
            var inputlist = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            ints = new List<int>();
            foreach(var i in inputlist) 
            {
                ints.Add(int.Parse(i));
            }
            n_ints = ints.Count();

            for (int i = 1; i <= n_ints; i++)
            {
                Extract(i);
            }
        }
        public void Extract(int n)
        {
            int temp = 0;
            for(int i = 0; i < n_ints; i++)
            {
                temp = ints[i];
//                if()
            }
        }
        public void Part2(object input, bool test, ref object solution)
        {
            
        }

    }
}