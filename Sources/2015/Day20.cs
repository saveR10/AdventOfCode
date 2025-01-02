using AOC;
using AOC.DataStructures.Clustering;
using AOC.DataStructures.PriorityQueue;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using AOC.Utilities.Map;
using AOC.Utilities.Math;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization; 
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;
using static AOC2015.Day19;
using static AOC2015.Day9;
using Solver = AOC.Solver;

namespace AOC2015
{
    [ResearchAlgorithms(ResolutionEnum.Overflow)]
    public class Day20 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var targetPresent = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            long present = 0;
            long houseNumber = 0;
            while(present < long.Parse(targetPresent[0]))
            {
                present = 0;
                houseNumber++;
                present +=AddPresents(houseNumber);
                Console.WriteLine($"houseNumber: {houseNumber}, primeFactors: {Prime.PrimeFactors((int)houseNumber).Aggregate("", (acc, factor) => acc + "-" + factor.ToString())}, present: {present}");
            }

        }
        public long AddPresents(long houseNumber)
        {
            long h = houseNumber;
            long n = h;
            long presents = 0;
            long AddPresentsRecursive(long n_)
            {
                if (h % n_ == 0) return n_ * 10;
                else return 0;
            }

            while (n > 0)
            {
               presents+=AddPresentsRecursive(n);
                n--;
            }
            return presents;
        }
        public void Part2(object input, bool test, ref object solution)
        {

        }

    }
}