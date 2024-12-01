using AOC;
using AOC.Model;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AOC2023
{
    public class Day19 : Solver, IDay
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
        public string Instructions = "";
        public string[,] ExploredTile;
        char[,] Tile;
        public int n;
        public void Part1(object input, bool test, ref object solution)
        {
            string[] input_array = (string[])((string)input).Split(Delimiter.delimiter_line,StringSplitOptions.None);

            Dictionary<string, List<string>> Workflows = new Dictionary<string, List<string>>();
            int indice_separatore = 0;
            for(int i = 0; i < input_array.Length; i++)
            {
                if (string.IsNullOrEmpty(input_array[i]))
                {
                    indice_separatore = i; break;
                }
            }

            for (int p = 0; p < indice_separatore; p++)
            {
                string wf_name = input_array[p].Split(Delimiter.delimiter_parentesi_inizio, StringSplitOptions.None)[0];
                string[] rules = input_array[p].Substring(input_array[p].Split(Delimiter.delimiter_parentesi_inizio, StringSplitOptions.None)[0].Length + 1).Split(Delimiter.delimiter_parentesi_fine, StringSplitOptions.None)[0].Split(Delimiter.delimiter_comma, StringSplitOptions.None);
                List<string> wf_rules = new List<string>();
                for (int j = 0; j < rules.Count(); j++)
                {
                    wf_rules.Add(rules[j]);
                }
                Workflows.Add(wf_name, wf_rules);
            }

            long sumParts =0;
            for (int p = indice_separatore+1; p < input_array.Length; p++)
            {
                var part = input_array[p];
                var partarray= part.Substring(1,part.Length-2).Split(Delimiter.delimiter_comma, StringSplitOptions.None);
                Dictionary<string,int> singlePart = new Dictionary<string,int>();
                foreach(var item in partarray)
                {
                    var a = item.Split(Delimiter.delimiter_equals, StringSplitOptions.None);
                    singlePart.Add(a[0], int.Parse(a[1]));
                }
                string state = "";
                string WF = "in";
                while (string.IsNullOrEmpty(state))
                {
                    string next= NavigateWF(singlePart, Workflows[$"{WF}"]);
                    if (next == "A" || next == "R") state = next;
                    else WF = next;
                }

                if (state == "A") sumParts+=singlePart.Sum(x => x.Value);
            }

            solution = sumParts;
        }
        public string NavigateWF(Dictionary<string,int> singlePart, List<string> WF)
        {
            foreach(var wf in WF)
            {
                if (wf.Contains(':'))
                {
                    var op = wf.Split(':', (char)StringSplitOptions.None)[0];
                    string s = op.Split(Delimiter.delimiter_operator, StringSplitOptions.None)[0];
                    int v = int.Parse(op.Split(Delimiter.delimiter_operator, StringSplitOptions.None)[1]);

                    if (AOC.Utilities.Math.Extension.Operator(op.Substring(s.Length, 1), singlePart[s], v))
                    {
                        string ret = wf.Split(':', (char)StringSplitOptions.None)[1];
                        return ret;
                    }
                }
                else return wf;
            }
            
            return "A";
        }

        public void Part2(object input, bool test, ref object solution)
        {
            string[] input_array = (string[])input;
            Dictionary<string, List<string>> Workflows = new Dictionary<string, List<string>>();
            int indice_separatore = 0;
            for (int i = 0; i < input_array.Length; i++)
            {
                if (string.IsNullOrEmpty(input_array[i]))
                {
                    indice_separatore = i; break;
                }
            }

            for (int p = 0; p < indice_separatore; p++)
            {
                string wf_name = input_array[p].Split(Delimiter.delimiter_parentesi_inizio, StringSplitOptions.None)[0];
                string[] rules = input_array[p].Substring(input_array[p].Split(Delimiter.delimiter_parentesi_inizio, StringSplitOptions.None)[0].Length + 1).Split(Delimiter.delimiter_parentesi_fine, StringSplitOptions.None)[0].Split(Delimiter.delimiter_comma, StringSplitOptions.None);
                List<string> wf_rules = new List<string>();
                for (int j = 0; j < rules.Count(); j++)
                {
                    wf_rules.Add(rules[j]);
                }
                Workflows.Add(wf_name, wf_rules);
            }

            long l = 250000000000000;
            long sumParts = 0;

            for (int p = 0; p < indice_separatore; p++)
            {
                string state = "";
                string WF = "in";
                while (string.IsNullOrEmpty(state))
                {
                    string next = FindCombinations(Workflows[$"{WF}"]);
                    if (next == "A" || next == "R") state = next;
                    else WF = next;
                }

                //if (state == "A") sumParts += singlePart.Sum(x => x.Value);
            }

            solution = sumParts;
        
        }
        
        public string FindCombinations(List<string> WF)
        {
            return "";
        }
        /*bool test = true;

            if (!test)
            {
                Instructions = "";
            }
            else
            {
                Instructions = @"px{a<2006:qkq,m>2090:A,rfg}
pv{a>1716:R,A}
lnx{m>1548:A,A}
rfg{s<537:gd,x>2440:R,A}
qs{s>3448:A,lnx}
qkq{x<1416:A,crn}
crn{x>2662:A,R}
in{s<1351:px,qqz}
qqz{s>2770:qs,m<1801:hdj,R}
gd{a>3333:R,R}
hdj{m>838:A,pv}

{x=787,m=2655,a=1222,s=2876}
{x=1679,m=44,a=2067,s=496}
{x=2036,m=264,a=79,s=2244}
{x=2461,m=1339,a=466,s=291}
{x=2127,m=1623,a=2188,s=1013}";
            }
            int i = 0;
            Dictionary<string, List<string>> Workflows = new Dictionary<string, List<string>>();
            Dictionary<int, List<string>> Parts = new Dictionary<int, List<string>>();

            var lines = Instructions.Split(delimiter_line, StringSplitOptions.None);
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
                Parts.Add(n_parts,parts_);
            }
            long sum = 0;
            foreach(var part in Parts)
            {
                bool accepted = false;
                bool processing = true;
                string target = "in";
                while (processing)
                {
                    foreach(var rule in Workflows[target])
                    {
                        accepted=ValidateRule(rule);
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

/*        public class Workflow
        {
            public string name { get; set; }
            public List<string> rules { get; set; }
            public enum state
            {
                indefined,
                rejected,
                accepter
            }
            
        }*/

    }
}
