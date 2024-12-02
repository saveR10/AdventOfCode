using AOC;
using AOC.DataStructures.Clustering;
using AOC.DataStructures.PriorityQueue;
using AOC.Model;
using AOC.SearchAlghoritmhs;
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
using static AOC.SearchAlghoritmhs.ResearchAlghoritmsAttribute;
using Solver = AOC.Solver;

namespace AOC2015
{
    public class Day16 : Solver, IDay
    {
        public Dictionary<int, Analysys> Analysyses = new Dictionary<int, Analysys>();
        public Analysys RealSue;
        public class Analysys
        {
            public int children { get; set; }
            public int cats { get; set; }
            public int samoyeds { get; set; }
            public int vizslas { get; set; }
            public int pomeranians { get; set; }
            public int akitas { get; set; }
            public int goldfish { get; set; }
            public int trees  { get; set; }
            public int cars { get; set; }
            public int perfumes { get; set; }
        }

        public void Part1(object input, bool test, ref object solution)
        {
            RealSue = new Analysys()
            {
                children = 3,
                cats = 7,
                samoyeds = 2,
                pomeranians = 3,
                akitas = 0,
                vizslas = 0,
                goldfish = 5,
                trees = 3,
                cars = 2,
                perfumes = 1
            };
            
            string inputText = (string)input;
            var inputlist = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);

            foreach (var i in inputlist)
            {
                if (!string.IsNullOrEmpty(i))
                {
                    string[] sue = i.Split(Delimiter.delimiter_space, StringSplitOptions.None);
                    Tuple<string,int> t1 = new Tuple<string, int>(sue[2].Split(Delimiter.DoubleDot, StringSplitOptions.None)[0], int.Parse(sue[3].Split(Delimiter.delimiter_comma, StringSplitOptions.None)[0]));
                    Tuple<string, int> t2 = new Tuple<string, int>(sue[4].Split(Delimiter.DoubleDot, StringSplitOptions.None)[0], int.Parse(sue[5].Split(Delimiter.delimiter_comma, StringSplitOptions.None)[0]));
                    Tuple<string, int> t3 = new Tuple<string, int>(sue[6].Split(Delimiter.DoubleDot, StringSplitOptions.None)[0], int.Parse(sue[7].Split(Delimiter.delimiter_comma, StringSplitOptions.None)[0]));

                    if (CheckSue(t1,t2,t3))
                    {
                        solution = sue[1];
                    }
                }
            }

        }
        public bool CheckSue(Tuple<string,int> t1, Tuple<string, int> t2, Tuple<string, int> t3)
        {
            List<Tuple<string,int>> tlist = new List<Tuple<string,int>>();
            tlist.Add(t1);
            tlist.Add(t2);
            tlist.Add(t3);

            bool ret = false;
            for(int i = 0; i < 3; i++)
            {
                switch (tlist[i].Item1)
                {
                    case "children":
                        if (RealSue.children != tlist[i].Item2) return false;
                        break;
                    case "cats":
                        if (RealSue.cats != tlist[i].Item2) return false;
                        break;
                    case "samoyeds":
                        if (RealSue.samoyeds != tlist[i].Item2) return false;
                        break;
                    case "pomeranians":
                        if (RealSue.pomeranians != tlist[i].Item2) return false;
                        break;
                    case "akitas":
                        if (RealSue.akitas!= tlist[i].Item2) return false;
                        break;
                    case "vizslas":
                        if (RealSue.vizslas!= tlist[i].Item2) return false;
                        break;
                    case "goldfish":
                        if (RealSue.goldfish != tlist[i].Item2) return false;
                        break;
                    case "trees":
                        if (RealSue.trees!= tlist[i].Item2) return false;
                        break;
                    case "cars":
                        if (RealSue.cars != tlist[i].Item2) return false;
                        break;
                    case "perfumes":
                        if (RealSue.perfumes != tlist[i].Item2) return false;
                        break;
                }
            }


            return true;
        }
        public void Part2(object input, bool test, ref object solution)
        {
            RealSue = new Analysys()
            {
                children = 3,
                cats = 7,
                samoyeds = 2,
                pomeranians = 3,
                akitas = 0,
                vizslas = 0,
                goldfish = 5,
                trees = 3,
                cars = 2,
                perfumes = 1
            };

            string inputText = (string)input;
            var inputlist = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);

            foreach (var i in inputlist)
            {
                if (!string.IsNullOrEmpty(i))
                {
                    string[] sue = i.Split(Delimiter.delimiter_space, StringSplitOptions.None);
                    Tuple<string, int> t1 = new Tuple<string, int>(sue[2].Split(Delimiter.DoubleDot, StringSplitOptions.None)[0], int.Parse(sue[3].Split(Delimiter.delimiter_comma, StringSplitOptions.None)[0]));
                    Tuple<string, int> t2 = new Tuple<string, int>(sue[4].Split(Delimiter.DoubleDot, StringSplitOptions.None)[0], int.Parse(sue[5].Split(Delimiter.delimiter_comma, StringSplitOptions.None)[0]));
                    Tuple<string, int> t3 = new Tuple<string, int>(sue[6].Split(Delimiter.DoubleDot, StringSplitOptions.None)[0], int.Parse(sue[7].Split(Delimiter.delimiter_comma, StringSplitOptions.None)[0]));

                    if (CheckSue2(t1, t2, t3))
                    {
                        solution = sue[1];
                    }
                }
            }
        }
        public bool CheckSue2(Tuple<string, int> t1, Tuple<string, int> t2, Tuple<string, int> t3)
        {
            List<Tuple<string, int>> tlist = new List<Tuple<string, int>>();
            tlist.Add(t1);
            tlist.Add(t2);
            tlist.Add(t3);

            bool ret = false;
            for (int i = 0; i < 3; i++)
            {
                switch (tlist[i].Item1)
                {
                    case "children":
                        if (RealSue.children != tlist[i].Item2) return false;
                        break;
                    case "cats":
                        if (tlist[i].Item2 <=RealSue.cats ) return false;
                        break;
                    case "samoyeds":
                        if (RealSue.samoyeds != tlist[i].Item2) return false;
                        break;
                    case "pomeranians":
                        if (tlist[i].Item2 >= RealSue.pomeranians) return false;
                        break;
                    case "akitas":
                        if (RealSue.akitas != tlist[i].Item2) return false;
                        break;
                    case "vizslas":
                        if (RealSue.vizslas != tlist[i].Item2) return false;
                        break;
                    case "goldfish":
                        if (tlist[i].Item2 >= RealSue.goldfish) return false;
                        break;
                    case "trees":
                        if (tlist[i].Item2<= RealSue.trees) return false;
                        break;
                    case "cars":
                        if (RealSue.cars != tlist[i].Item2) return false;
                        break;
                    case "perfumes":
                        if (RealSue.perfumes != tlist[i].Item2) return false;
                        break;
                }
            }


            return true;
        }

    }
}