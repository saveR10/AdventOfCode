using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;
using static AOC2016.Day10;
using static AOC2024.Day24;

namespace AOC2016
{
    [ResearchAlgorithms(TypologyEnum.MachineInstructions)]
    [ResearchAlgorithms(ResolutionEnum.Reflection)]
    [ResearchAlgorithms(DifficultEnum.WarmUp)]
    public class Day12 : Solver, IDay
    {

        static public int a = 0, b = 0, c = 0, d = 0;
        public void Part1(object input, bool test, ref object solution)
        {
            string inputString = (string)input;
            string[] instructions = inputString.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            for(int i=0;i<instructions.Length;i++)
            {
                if (string.IsNullOrEmpty(instructions[i].ToString())) continue;
                var instruction = instructions[i].Split(Delimiter.delimiter_space, StringSplitOptions.RemoveEmptyEntries);
                
                switch (instruction[0])
                {
                    case "cpy":
                        var fieldSource = typeof(Day12).GetField(instruction[1], System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
                        int source=0;
                        if (fieldSource != null)
                        {
                            int currentValue = (int)fieldSource.GetValue(null); 
                            fieldSource.SetValue(null, currentValue);  
                            source = currentValue;
                        }
                        else
                        {
                            source = int.Parse(instruction[1]);
                        }

                        var fieldTarget = typeof(Day12).GetField(instruction[2], System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);

                        if (fieldTarget != null)
                        {
                            int currentValue = (int)fieldTarget.GetValue(null);
                            fieldTarget.SetValue(null, source);
                        }
                        break;
                    case "inc":
                        var fieldIncrease = typeof(Day12).GetField(instruction[1], System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);

                        if (fieldIncrease != null)
                        {
                            int currentValue = (int)fieldIncrease.GetValue(null);
                            fieldIncrease.SetValue(null, currentValue+1);
                        }
                        break;
                    case "dec":
                        var fieldDecrease = typeof(Day12).GetField(instruction[1], System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);

                        if (fieldDecrease != null)
                        {
                            int currentValue = (int)fieldDecrease.GetValue(null);
                            fieldDecrease.SetValue(null, currentValue - 1);
                        }
                        break;
                    case "jnz":
                        int condition = 0;
                        var fieldCondition = typeof(Day12).GetField(instruction[1], System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
                        if (fieldCondition != null)
                        {
                            int currentValue = (int)fieldCondition.GetValue(null);
                            fieldCondition.SetValue(null, currentValue);
                            condition = currentValue;
                        }
                        else
                        {
                            condition = int.Parse(instruction[1]);
                        }
                        if (condition != 0)
                        {
                            int jump= 0;
                            var fieldJump = typeof(Day12).GetField(instruction[2], System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
                            if (fieldJump != null)
                            {
                                int currentValue = (int)fieldJump.GetValue(null);
                                fieldJump.SetValue(null, currentValue);
                                jump = currentValue;
                            }
                            else
                            {
                                jump = int.Parse(instruction[2]);
                            }
                            i += jump-1;
                        }
                        break;
                }
            }
            solution = a;
        }

        public void Part2(object input, bool test, ref object solution)
        {
            c = 1;
            string inputString = (string)input;
            string[] instructions = inputString.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            for (int i = 0; i < instructions.Length; i++)
            {
                if (string.IsNullOrEmpty(instructions[i].ToString())) continue;
                var instruction = instructions[i].Split(Delimiter.delimiter_space, StringSplitOptions.RemoveEmptyEntries);

                switch (instruction[0])
                {
                    case "cpy":
                        var fieldSource = typeof(Day12).GetField(instruction[1], System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
                        int source = 0;
                        if (fieldSource != null)
                        {
                            int currentValue = (int)fieldSource.GetValue(null);
                            fieldSource.SetValue(null, currentValue);
                            source = currentValue;
                        }
                        else
                        {
                            source = int.Parse(instruction[1]);
                        }

                        var fieldTarget = typeof(Day12).GetField(instruction[2], System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);

                        if (fieldTarget != null)
                        {
                            int currentValue = (int)fieldTarget.GetValue(null);
                            fieldTarget.SetValue(null, source);
                        }
                        break;
                    case "inc":
                        var fieldIncrease = typeof(Day12).GetField(instruction[1], System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);

                        if (fieldIncrease != null)
                        {
                            int currentValue = (int)fieldIncrease.GetValue(null);
                            fieldIncrease.SetValue(null, currentValue + 1);
                        }
                        break;
                    case "dec":
                        var fieldDecrease = typeof(Day12).GetField(instruction[1], System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);

                        if (fieldDecrease != null)
                        {
                            int currentValue = (int)fieldDecrease.GetValue(null);
                            fieldDecrease.SetValue(null, currentValue - 1);
                        }
                        break;
                    case "jnz":
                        int condition = 0;
                        var fieldCondition = typeof(Day12).GetField(instruction[1], System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
                        if (fieldCondition != null)
                        {
                            int currentValue = (int)fieldCondition.GetValue(null);
                            fieldCondition.SetValue(null, currentValue);
                            condition = currentValue;
                        }
                        else
                        {
                            condition = int.Parse(instruction[1]);
                        }
                        if (condition != 0)
                        {
                            int jump = 0;
                            var fieldJump = typeof(Day12).GetField(instruction[2], System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
                            if (fieldJump != null)
                            {
                                int currentValue = (int)fieldJump.GetValue(null);
                                fieldJump.SetValue(null, currentValue);
                                jump = currentValue;
                            }
                            else
                            {
                                jump = int.Parse(instruction[2]);
                            }
                            i += jump - 1;
                        }
                        break;
                }
            }
            solution = a;
        }
    }
}