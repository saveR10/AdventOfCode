using AOC;
using AOC.DataStructures.Clustering;
using AOC.Documents.LINQ;
using AOC.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace AOC2023
{
    //DA SISTEMARE
    public class Day20 : Solver, IDay
    {
        public static String[] delimiters = { "\r\n", " " };
        public static String[] delimiter_space = { " " };
        public static String[] delimiter_line = { "\r\n" };
        public static String[] delimiter_equals = { "=" };
        public static String[] delimiter_signs = { "=", "-" };
        public static String[] delimiter_parentesi = { "{", "}" };
        public static String[] delimiter_parentesi_inizio = { "{" };
        public static String[] delimiter_parentesi_fine = { "}" };
        public static String[] delimiter_comma = { "," };
        public static String[] delimiter_puntovirgola = { ";" };
        public string ModuleConfiguration = "";
        public string[,] ExploredTile;
        char[,] Tile;
        public int n;
        public void Part1(object input, bool test, ref object solution)
        {
            test = true;

            if (!test)
            {
                ModuleConfiguration = "";
            }
            else
            {
                ModuleConfiguration = @"broadcaster -> a, b, c
%a -> b
%b -> c
%c -> inv
&inv -> a";
            }
            int i = 0;
            Dictionary<string, List<string>> Workflows = new Dictionary<string, List<string>>();
            Dictionary<int, List<string>> Parts = new Dictionary<int, List<string>>();

            var lines = ModuleConfiguration.Split(delimiter_line, StringSplitOptions.None);
            for (i = 0; i < lines.Length; i++)
            {
                if (!string.IsNullOrEmpty(lines[i]))
                {
                    string wf_name = lines[i].Split(delimiter_parentesi, StringSplitOptions.None)[0];
                    string[] rules = lines[i].Substring(lines[i].Split(delimiter_parentesi_inizio, StringSplitOptions.None)[0].Length + 1).Split(delimiter_parentesi_fine, StringSplitOptions.None)[0].Split(delimiter_comma, StringSplitOptions.None);
                    List<string> wf_rules = new List<string>();
                    for (int j = 0; j < rules.Count(); j++)
                    {
                        wf_rules.Add(rules[j]);
                    }
                    Workflows.Add(wf_name, wf_rules);
                }
                else break;
            }
            int n_parts = 0;
            for (i = i + 1; i < lines.Length; i++)
            {
                n_parts++;
                string[] parts = lines[i].Split(delimiter_parentesi, StringSplitOptions.None)[1].Split(delimiter_comma, StringSplitOptions.None);
                List<string> parts_ = new List<string>();
                for (int j = 0; j < parts.Count(); j++)
                {
                    parts_.Add(parts[j]);
                }
                Parts.Add(n_parts, parts_);
            }
            long sum = 0;
            foreach (var part in Parts)
            {
                bool accepted = false;
                bool processing = true;
                string target = "in";
                while (processing)
                {
                    foreach (var rule in Workflows[target])
                    {
                        accepted = ValidateRule(rule);
                    }
                }

                if (accepted) sum += 1;
            }
        }

        public bool ValidateRule(string rule)
        {
            if (rule.Contains(":"))
            {
                var a = rule.Split(':')[0];
            }
            bool ret = false;



            return ret;
        }

        public void Part2(object input, bool test, ref object solution)
        {

        }

    }
}
