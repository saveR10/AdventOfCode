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
using static AOC.SearchAlghoritmhs.ResearchAlghoritmsAttribute;
using static AOC2015.Day19;
using static AOC2015.Day9;
using Solver = AOC.Solver;

namespace AOC2015
{
    [ResearchAlghoritmsAttribute(TypologyEnum.Reduction)]
    public class Day19 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var inputlist = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            bool Transformation = true;
            string Molecules = "";
            Dictionary<string, List<string>> TransformationRules = new Dictionary<string, List<string>>();
            for (int i = 0; i < inputlist.Length; i++)
            {
                if (string.IsNullOrEmpty(inputlist[i]))
                {
                    Transformation = false;
                }
                if (Transformation)
                {
                    var r = inputlist[i].Split(Delimiter.delimiter_space, StringSplitOptions.None);
                    if (!TransformationRules.ContainsKey(r[0]))
                    {
                        TransformationRules.Add(r[0], new List<string> { });
                        TransformationRules[r[0]].Add(r[r.Length - 1]);
                    }
                    else
                    {
                        TransformationRules[r[0]].Add(r[r.Length - 1]);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(inputlist[i]))
                    {
                        Molecules = inputlist[i];
                        List<string> NewMolecules = new List<string>();
                        string Molecula = "";
                        List<string> MoleculePartList = new List<string>();
                        for (int m = 0; m < Molecules.Length; m++)
                        {

                            if (TransformationRules.ContainsKey(Molecules[m].ToString()))
                            {
                                Molecula = Molecules[m].ToString();
                            }
                            else
                            {

                                MoleculePartList = AddMoleculesPart(Molecules[m].ToString(), MoleculePartList);
                                foreach (var mol in MoleculePartList)
                                {
                                    if (TransformationRules.ContainsKey(mol))
                                    {
                                        Molecula = mol;
                                        //break;
                                    }
                                }
                            }

                            if (!string.IsNullOrEmpty(Molecula))
                            {
                                foreach (var rules in TransformationRules[Molecula])
                                {
                                    NewMolecules.Add(
                              Molecules.Substring(0, m - (Molecula.Length - 1))
                            + rules
                            + Molecules.Substring(m + 1, Molecules.Length - m - 1).Trim());
                                }
                                Molecula = "";
                                MoleculePartList = new List<string>();
                            }
                        }
                        solution = NewMolecules.Distinct().Count();
                    }
                }
            }
        }

        public static List<string> AddMoleculesPart(string Molecule, List<string> MoleculePartList)
        {
            List<string> NewMolecules = new List<string> { };
            NewMolecules.Add(Molecule);

            foreach (var molecula in MoleculePartList)
            {
                NewMolecules.Add(molecula + Molecule);
            }
            return NewMolecules;
        }
        public static Dictionary<string, List<string>> TransformationRules = new Dictionary<string, List<string>>();
        public static string Medicine;
        //Il metodo a riduzione inversa con sostituzioni greedy è il più efficiente e adatto per questo problema. Funziona bene con il tipo di regole e struttura molecolare forniti nel puzzle.
        public void Part2(object input, bool test, ref object solution)
        {
            int minStep = int.MaxValue;
            string inputText = (string)input;
            if (test) inputText = "e => H\r\ne => O\r\n" + inputText;
            var inputlist = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            bool Transformation = true;
            TransformationRules = new Dictionary<string, List<string>>();
            for (int i = 0; i < inputlist.Length; i++)
            {
                if (string.IsNullOrEmpty(inputlist[i]))
                {
                    Transformation = false;
                }
                if (Transformation)
                {
                    var r = inputlist[i].Split(Delimiter.delimiter_space, StringSplitOptions.None);
                    if (!TransformationRules.ContainsKey(r[r.Length - 1]))
                    {
                        TransformationRules.Add(r[r.Length - 1], new List<string> { });
                        TransformationRules[r[r.Length - 1]].Add(r[0]);
                    }
                    else
                    {
                        TransformationRules[r[r.Length - 1]].Add(r[0]);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(inputlist[i]))
                    {
                        Medicine = inputlist[i];
                        MinPQ<Molecula> minPQ = new MinPQ<Molecula>();


                      
                    }
                }
            }
            solution = minStep;
        }
    }
    internal class Molecula
    {
        internal string molecula;
        //in
    }
}