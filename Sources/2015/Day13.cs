using AOC;
using AOC.DataStructures.Clustering;
using AOC.DataStructures.PriorityQueue;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization; 
using System;
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
    [ResearchAlgorithms(title: "Day 13: Knights of the Dinner Table",
                   TypologyEnum.Graph,
                   ResolutionEnum.Dijkstra,
                   DifficultEnum.Medium,
                   "Permutazioni circolari degli ospiti per massimizzare la felicità totale usando coda di priorità")]
    public class Day13 : Solver, IDay
    {
        public Dictionary<string, List<Tuple<string, int>>> RelativeHappiness;
        public int Happiness = 0;
        public Seats seats;
        public class Seats
        {
            public Dictionary<string, Head> Heads { get; set; }
        }
        public class Head
        {
            public string HeadName { get; set; }
            public Attender Attender { get; set; }
            public string ListAttenders { get; set; }
            public int Happiness { get; set; }
            public int nAttenders { get; set; }
        }
        public class Attender
        {
            public Attender()
            {
            }
            public Attender(Attender att, bool right = false)
            {
                if (!right) this.right = att; else this.left = att;
            }
            public string name { get; set; }
            public Attender left { get; set; }
            public Attender right { get; set; }
        }
        public string Headname { get; set; }
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var inputlist = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);

            RelativeHappiness = new Dictionary<string, List<Tuple<string, int>>>();
            foreach (var a in inputlist)
            {
                if (!string.IsNullOrEmpty(a))
                {
                    var att = a.Split(Delimiter.delimiter_space, StringSplitOptions.None);
                    AddRelativeHappiness(att);
                }
            }


            seats = new Seats() { Heads = new Dictionary<string, Head>() };
            foreach (var a in RelativeHappiness)
            {
                seats.Heads.Add(a.Key, new Head { HeadName = a.Key, nAttenders = 1, Happiness = 0, ListAttenders = a.Key, Attender = new Attender() { name = a.Key } });
            }

            foreach (var a in RelativeHappiness)
            {
                Headname = a.Key;
                SearchFreeSeat(seats.Heads[Headname].Attender);
            }
            foreach (var a in seats.Heads)
            {
                if (a.Value.Happiness > Happiness) Happiness = a.Value.Happiness;
            }
            solution = Happiness;
        }
        public void SearchFreeSeat(Attender att)
        {

            while (seats.Heads[Headname].nAttenders < RelativeHappiness.Count())
            {
                if (att.left == null)
                {
                    AddNewSeat(att, false);
                }
                else
                {
                    SearchFreeSeat(att.left);
                }

                if (seats.Heads[Headname].nAttenders == RelativeHappiness.Count() - 1)
                {
                    AddNewSeat(att.left, true);
                }
            }
        }
        public void AddNewSeat(Attender s, bool right = false)
        {
            if (right)
            {
                int relHappiness = 0;
                foreach (var r in RelativeHappiness)
                {
                    if (!seats.Heads[Headname].HeadName.Contains(r.Key) && !seats.Heads[Headname].ListAttenders.Contains(r.Key))
                    {
                        seats.Heads[Headname].nAttenders += 1;

                        seats.Heads[Headname].Happiness += (RelativeHappiness[s.name].Where(t => t.Item1.Equals(r.Key))).FirstOrDefault().Item2;
                        seats.Heads[Headname].Happiness += (RelativeHappiness[r.Key].Where(t => t.Item1.Equals(s.name))).FirstOrDefault().Item2;
                        seats.Heads[Headname].Happiness += (RelativeHappiness[Headname].Where(t => t.Item1.Equals(r.Key))).FirstOrDefault().Item2;
                        seats.Heads[Headname].Happiness += (RelativeHappiness[r.Key].Where(t => t.Item1.Equals(Headname))).FirstOrDefault().Item2;
                        seats.Heads[Headname].ListAttenders += $"-{r.Key}";
                        s.left = new Attender(s) { name = r.Key };
                        s.left.left = seats.Heads[Headname].Attender;
                        seats.Heads[Headname].Attender.right = new Attender(s, true) { name = r.Key };
                        seats.Heads[Headname].Attender.right.right = new Attender(s, true) { name = s.name };
                    }
                }
            }
            else
            {

                int relHappiness = 0;
                string seat1 = "";
                string seat2 = "";
                foreach (var r in RelativeHappiness)
                {
                    if (!seats.Heads[Headname].HeadName.Contains(r.Key) && !seats.Heads[Headname].ListAttenders.Contains(r.Key))
                    {
                        int tempHappiness = 0;
                        tempHappiness += (RelativeHappiness[s.name].Where(t => t.Item1.Equals(r.Key)).FirstOrDefault().Item2);
                        tempHappiness += (RelativeHappiness[r.Key].Where(t => t.Item1.Equals(s.name)).FirstOrDefault().Item2);

                        if (tempHappiness >= relHappiness)
                        {
                            relHappiness = tempHappiness;
                            seat1 = s.name;
                            seat2 = r.Key;
                        }

                    }
                }
                seats.Heads[Headname].nAttenders += 1;
                seats.Heads[Headname].Happiness += relHappiness;
                seats.Heads[Headname].ListAttenders += $"-{seat2}";
                s.left = new Attender(s) { name = seat2 };
            }
        }
        public void AddRelativeHappiness(string[] att)
        {
            string att1 = att[0];
            string att2 = att[att.Length - 1].Split(Delimiter.Dot, StringSplitOptions.None)[0];
            if (!RelativeHappiness.ContainsKey($"{att1}")) RelativeHappiness.Add(att1, new List<Tuple<string, int>> { new Tuple<string, int>(att2, int.Parse(att[3]) * (att[2] == "gain" ? 1 : -1)) });
            else RelativeHappiness[att1].Add(new Tuple<string, int>(att2, int.Parse(att[3]) * (att[2] == "gain" ? 1 : -1)));

        }

        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var inputlist = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);

            RelativeHappiness = new Dictionary<string, List<Tuple<string, int>>>();
            foreach (var a in inputlist)
            {
                if (!string.IsNullOrEmpty(a))
                {
                    var att = a.Split(Delimiter.delimiter_space, StringSplitOptions.None);
                    AddRelativeHappiness(att);
                }
            }
            foreach (var r in RelativeHappiness)
            {
                string[] att = $"{r.Key} would gain 0 happiness units by sitting next to Saverio.".Split(Delimiter.delimiter_space,StringSplitOptions.None);
                AddRelativeHappiness(att);
            }
            RelativeHappiness.Add("Saverio",new List<Tuple<string, int>>());
            foreach (var r in RelativeHappiness)
            {
                if (r.Key != "Saverio")
                {
                    string[] att = $"Saverio would gain 0 happiness units by sitting next to {r.Key}.".Split(Delimiter.delimiter_space, StringSplitOptions.None);
                    AddRelativeHappiness(att);
                }
            }

            seats = new Seats() { Heads = new Dictionary<string, Head>() };
            foreach (var a in RelativeHappiness)
            {
                seats.Heads.Add(a.Key, new Head { HeadName = a.Key, nAttenders = 1, Happiness = 0, ListAttenders = a.Key, Attender = new Attender() { name = a.Key } });
            }

            foreach (var a in RelativeHappiness)
            {
                Headname = a.Key;
                SearchFreeSeat(seats.Heads[Headname].Attender);
            }
            foreach (var a in seats.Heads)
            {
                if (a.Value.Happiness > Happiness) Happiness = a.Value.Happiness;
            }
            solution = Happiness;
        }
    }
}