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
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;
using Solver = AOC.Solver;

namespace AOC2015
{
    [ResearchAlgorithms(TypologyEnum.Cronometers)]
    public class Day14 : Solver, IDay
    {
        Dictionary<string, Reindeer> Reindeers = new Dictionary<string, Reindeer>();
        public class Reindeer
        {
            public Reindeer(string name,int fly, int timefly, int timerest)
            {
                this.name=name;
                this.fly=fly;
                this.TimeFly=timefly;
                this.TimeRest=timerest;
            }
            public string name { get; set; }
            public int fly { get; set; }
            public int TimeFly { get; set; }
            public int TimeRest { get; set; }
            public int Km { get; set; }
            public int Last { get; set; }
            public bool Rest { get; set; }
            public int Point { get; set; }
        }
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var inputlist = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);

            foreach(var i in inputlist)
            {
                if (!string.IsNullOrEmpty(i))
                {
                    string[] reindeerstring = i.Split(Delimiter.delimiter_space, StringSplitOptions.None);
                    Reindeers.Add(reindeerstring[0], new Reindeer(reindeerstring[0], int.Parse(reindeerstring[3]), int.Parse(reindeerstring[6]), int.Parse(reindeerstring[13])));

                }
            }

            int timer = 2503;//1000;//;

            while (timer != 0)
            {
                foreach(var r in Reindeers)
                {
                    if (!r.Value.Rest)
                    {
                        r.Value.Km += r.Value.fly;
                        r.Value.Last += 1;
                        if (r.Value.Last == r.Value.TimeFly) { r.Value.Rest = !r.Value.Rest; r.Value.Last = 0; }
                    }
                    else
                    {
                        r.Value.Last += 1;
                        if (r.Value.Last == r.Value.TimeRest) { r.Value.Rest = !r.Value.Rest; r.Value.Last = 0; }
                    }

                }
                timer--;
            }

            solution = Reindeers.Values.Max(r => r.Km);
        }

        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var inputlist = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            List<string> reindeername = new List<string>();
            foreach (var i in inputlist)
            {
                if (!string.IsNullOrEmpty(i))
                {
                    string[] reindeerstring = i.Split(Delimiter.delimiter_space, StringSplitOptions.None);
                    Reindeers.Add(reindeerstring[0], new Reindeer(reindeerstring[0], int.Parse(reindeerstring[3]), int.Parse(reindeerstring[6]), int.Parse(reindeerstring[13])));
                    reindeername.Add(reindeerstring[0]);
                }
            }

            int timer = 2503;//1000;

            while (timer != 0)
            {
                foreach (var r in Reindeers)
                {
                    if (!r.Value.Rest)
                    {
                        r.Value.Km += r.Value.fly;
                        r.Value.Last += 1;
                        if (r.Value.Last == r.Value.TimeFly) { r.Value.Rest = !r.Value.Rest; r.Value.Last = 0; }
                    }
                    else
                    {
                        r.Value.Last += 1;
                        if (r.Value.Last == r.Value.TimeRest) { r.Value.Rest = !r.Value.Rest; r.Value.Last = 0; }
                    }

                }
                int ind = 0;
                foreach(var i in Reindeers.Values.Select(r => r.Km == Reindeers.Values.Max(rm => rm.Km)))
                {
                    if(i) Reindeers[reindeername[ind]].Point += 1;
                    ind++;
                }

                timer--;
            }

            solution = Reindeers.Values.Max(r => r.Point);
        }
    }
}