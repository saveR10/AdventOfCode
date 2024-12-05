using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2024
{
    public class Day5 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            List<List<int>> OrderRules = new List<List<int>>();
            List<List<int>> UpdatingPages = new List<List<int>>();
            bool orderrules = true;
            string inputText = (string)input;
            foreach (string item in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(item))
                {
                    if (!orderrules)
                    {
                        List<int> update = new List<int>();
                        string[] r = item.Split(',');
                        for (int i = 0; i < r.Length; i++)
                        {
                            update.Add(int.Parse(r[i]));
                        }
                        UpdatingPages.Add(update);

                    }
                    if (orderrules)
                    {
                        List<int> rule = new List<int>();
                        string[] r = item.Split('|');
                        rule.Add(int.Parse(r[0]));
                        rule.Add(int.Parse(r[1]));
                        OrderRules.Add(rule);
                    }

                }
                else
                {
                    orderrules = false;
                }
            }
            bool nextRule = false;
            int sum = 0;
            foreach (var u in UpdatingPages)
            {
                nextRule = false;
                for (int i = 0; i < u.Count; i++)
                {
                    for (int j = i + 1; j < u.Count; j++)
                    {
                        foreach (var r in OrderRules)
                        {
                            if ((r[0].Equals(u[i]) && r[1].Equals(u[j])) || (r[0].Equals(u[j]) && r[1].Equals(u[i])))
                            {
                                if (r[0].Equals(u[i]) && r[1].Equals(u[j]))
                                {

                                }
                                else
                                {
                                    nextRule = true;
                                }
                            }
                            if (nextRule) break;
                        }
                    }
                }
                if (!nextRule)
                {
                    sum += u[u.Count / 2];
                }
            }

            solution = sum;
        }

        public void Part2(object input, bool test, ref object solution)
        {
            List<List<int>> OrderRules = new List<List<int>>();
            List<List<int>> UpdatingPages = new List<List<int>>();
            bool orderrules = true;
            string inputText = (string)input;
            foreach (string item in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(item))
                {
                    if (!orderrules)
                    {
                        List<int> update = new List<int>();
                        string[] r = item.Split(',');
                        for (int i = 0; i < r.Length; i++)
                        {
                            update.Add(int.Parse(r[i]));
                        }
                        UpdatingPages.Add(update);

                    }
                    if (orderrules)
                    {
                        List<int> rule = new List<int>();
                        string[] r = item.Split('|');
                        rule.Add(int.Parse(r[0]));
                        rule.Add(int.Parse(r[1]));
                        OrderRules.Add(rule);
                    }

                }
                else
                {
                    orderrules = false;
                }
            }
            bool Fixed = false;
            int sum = 0;
            sum = 0;
            for (int u = 0; u < UpdatingPages.Count; u++)
            {
                Fixed = false;
                for (int i = 0; i < UpdatingPages[u].Count; i++)
                {
                    for (int j = i + 1; j < UpdatingPages[u].Count; j++)
                    {
                        foreach (var r in OrderRules)
                        {
                            if ((r[0].Equals(UpdatingPages[u][i]) && r[1].Equals(UpdatingPages[u][j])) || (r[0].Equals(UpdatingPages[u][j]) && r[1].Equals(UpdatingPages[u][i])))
                            {
                                if (r[0].Equals(UpdatingPages[u][i]) && r[1].Equals(UpdatingPages[u][j]))
                                {

                                }
                                else
                                {
                                    var temp = UpdatingPages[u][i];
                                    UpdatingPages[u][i] = UpdatingPages[u][j];
                                    UpdatingPages[u][j] = temp;
                                    Fixed = true;
                                }
                            }
                        }
                    }
                }
                if (Fixed)
                {
                    sum += UpdatingPages[u][UpdatingPages[u].Count / 2];
                }
            }



            solution = sum;
        }
    }
}
