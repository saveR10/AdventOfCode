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


    }
}
