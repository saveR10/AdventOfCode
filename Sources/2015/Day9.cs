using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using static AOC.SearchAlghoritmhs.ResearchAlghoritmsAttribute;

namespace AOC2015
{
    [ResearchAlghoritmsAttribute(TypologyEnum.AlphaStar)]
    public class Day9 : Solver, IDay
    {
        public class Node
        {
            public Node(string name) { this.name = name; }
            public string name { get; set; }

            public List<Neighbor> Neighbors = new List<Neighbor>();
            public class Neighbor
            {
                public Neighbor(Node node, int distance)
                {
                    
                }
                public Node Node { get; set; }
                public int distance { get; set; }

            }

        }

        Dictionary<string, Node> Cities = new Dictionary<string, Node>();
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var list = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            for(int i = 0; i < list.Length; i++)
            {
                var line = list[i].Split(Delimiter.delimiter_space,StringSplitOptions.None);
                if (!Cities.ContainsKey(line[0])) Cities.Add(line[0], new Node(line[0]));
                if (!Cities.ContainsKey(line[2])) Cities.Add(line[2], new Node(line[2]));
                //Cities[line[0]].Neighbors.Add(Cities[line[2]], int.Parse(line[line.Length-1])));
            }

        }
        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var list = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);
        }
    }
}
