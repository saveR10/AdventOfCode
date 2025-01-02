using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
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
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;

namespace AOC2015
{
    [ResearchAlgorithmsAttribute(TypologyEnum.BinaryOperation)]
    public class Day7 : Solver, IDay
    {
        //https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators
        //https://learn.microsoft.com/it-it/dotnet/csharp/language-reference/builtin-types/integral-numeric-types
        Dictionary<string, Wire> Wires;
        public bool all = false;
        public class Wire
        {
            public Wire(string name)
            {
                this.name = name;
            }
            public string name { set; get; }
            private int _value = -1;
            public int Value
            {
                get { return _value; }
                set
                {
                    _value = (ushort)value;
                }
            }
        }
        public void Part1(object input, bool test, ref object solution)
        {

            Wires = new Dictionary<string, Wire>();
            string inputText = (string)input;
            var list = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            try
            {

                while (all == false)
                {
                    all = true;
                    for (int i = 0; i < list.Length - 1; i++)
                    {
                        string line = list[i];
                        var elem = line.Split(Delimiter.delimiter_space, StringSplitOptions.None);

                        if (elem[elem.Length - 1] == "OK")
                        {
                            continue;
                        }
                        else
                        {


                            //Binary operation
                            if (line.Contains("AND") || line.Contains("OR") || line.Contains("SHIFT"))
                            {
                                var a_operation = FindValue(elem[0]);
                                var b_operation = FindValue(elem[2]);
                                FindValue(elem[elem.Length - 1]);
                                if (a_operation < 0 || b_operation < 0)
                                {
                                    all = false;
                                }
                                else
                                {
                                    if (line.Contains("AND")) Wires[elem[elem.Length - 1]].Value = a_operation & b_operation;
                                    if (line.Contains("OR")) Wires[elem[elem.Length - 1]].Value = a_operation | b_operation;
                                    if (line.Contains("LSHIFT")) Wires[elem[elem.Length - 1]].Value = a_operation << b_operation;
                                    if (line.Contains("RSHIFT")) Wires[elem[elem.Length - 1]].Value = a_operation >> b_operation;
                                    list[i] = list[i] + " OK";
                                }
                            }
                            //Unary operation
                            else if (line.Contains("NOT"))
                            {
                                var a_operation = FindValue(elem[1]);
                                FindValue(elem[elem.Length - 1]);
                                if (a_operation < 0)
                                {
                                    all = false;
                                }
                                else
                                {
                                    Wires[elem[elem.Length - 1]].Value = ~a_operation;
                                    list[i] = list[i] + " OK";
                                }
                            }

                            //Assignment
                            else
                            {
                                var a_assignment = FindValue(elem[0]);
                                FindValue(elem[elem.Length - 1]);
                                if (a_assignment < 0)
                                {
                                    all = false;
                                }
                                else
                                {
                                    Wires[elem[elem.Length - 1]].Value = a_assignment;
                                    list[i] = list[i] + " OK";
                                }

                            }

                        }
                    }
                }
                solution = Wires["a"].Value;
            }
            catch (Exception ex)
            {

            }

        }

        public int FindValue(string elem)
        {
            int ret = 0;
            if (int.TryParse(elem, out ret)) return ret;
            else
            {
                if (!Wires.ContainsKey(elem)) Wires.Add(elem, new Wire(elem));
                return Wires[elem].Value;
            }
        }

        public void Part2(object input, bool test, ref object solution)
        {
            Wires = new Dictionary<string, Wire>();
            string inputText = (string)input;
            var list = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            try
            {

                while (all == false)
                {
                    all = true;
                    for (int i = 0; i < list.Length - 1; i++)
                    {
                        string line = list[i];
                        if (list[i] == "19138 -> b")
                        {
                            list[i] = "16076 -> b";
                            line = list[i];
                        }

                        var elem = line.Split(Delimiter.delimiter_space, StringSplitOptions.None);
                        
                        if (elem[elem.Length - 1] == "OK")
                        {
                            continue;
                        }
                        else
                        {


                            //Binary operation
                            if (line.Contains("AND") || line.Contains("OR") || line.Contains("SHIFT"))
                            {
                                var a_operation = FindValue(elem[0]);
                                var b_operation = FindValue(elem[2]);
                                FindValue(elem[elem.Length - 1]);
                                if (a_operation < 0 || b_operation < 0)
                                {
                                    all = false;
                                }
                                else
                                {
                                    if (line.Contains("AND")) Wires[elem[elem.Length - 1]].Value = a_operation & b_operation;
                                    if (line.Contains("OR")) Wires[elem[elem.Length - 1]].Value = a_operation | b_operation;
                                    if (line.Contains("LSHIFT")) Wires[elem[elem.Length - 1]].Value = a_operation << b_operation;
                                    if (line.Contains("RSHIFT")) Wires[elem[elem.Length - 1]].Value = a_operation >> b_operation;
                                    list[i] = list[i] + " OK";
                                }
                            }
                            //Unary operation
                            else if (line.Contains("NOT"))
                            {
                                var a_operation = FindValue(elem[1]);
                                FindValue(elem[elem.Length - 1]);
                                if (a_operation < 0)
                                {
                                    all = false;
                                }
                                else
                                {
                                    Wires[elem[elem.Length - 1]].Value = ~a_operation;
                                    list[i] = list[i] + " OK";
                                }
                            }

                            //Assignment
                            else
                            {
                                var a_assignment = FindValue(elem[0]);
                                FindValue(elem[elem.Length - 1]);
                                if (a_assignment < 0)
                                {
                                    all = false;
                                }
                                else
                                {
                                    Wires[elem[elem.Length - 1]].Value = a_assignment;
                                    list[i] = list[i] + " OK";
                                }

                            }

                        }
                    }
                }
                solution = Wires["a"].Value;
            }
            catch (Exception ex) { }

        }
    }
}
