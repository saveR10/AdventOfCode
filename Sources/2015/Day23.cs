using AOC;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Linq;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;
using Solver = AOC.Solver;

namespace AOC2015
{
    [ResearchAlgorithms(title: "Day 23: Opening the Turing Lock",
                        TypologyEnum.MachineInstructions,          // Simulazione di CPU/registri
                        ResolutionEnum.None,              // Nessun algoritmo complesso, solo esecuzione passo-passo
                        DifficultEnum.Medium,
                        "Simulazione di un piccolo linguaggio assembly con due registri; gestione di istruzioni condizionali e salti relativi per calcolare il valore finale dei registri")]
    public class Day23 : Solver, IDay
    {
        public int a = 0;
        public int b = 0;

        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var instructions = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.RemoveEmptyEntries)
                             .ToArray();

            for(int i = 0; i < instructions.Length; i++)
            {
                Console.WriteLine($"{i} - a: {a}, b: {b}");
                var r = instructions[i].Split(Delimiter.delimiter_space, StringSplitOptions.None)[1].Trim(',');
                int o=0;
                if (instructions[i].Contains(','))
                    o = int.Parse(instructions[i].Split(Delimiter.delimiter_comma, StringSplitOptions.None)[1].Trim());
                else if (instructions[i].Contains("jmp"))
                    o = int.Parse(instructions[i].Split(Delimiter.delimiter_space, StringSplitOptions.None)[1].Trim());
                switch (instructions[i].Split(Delimiter.delimiter_space, StringSplitOptions.None)[0])
                {
                    case "hlf":
                        Half(r);
                        break;
                    case "tpl":
                        Triple(r);
                        break;
                    case "inc":
                        Increase(r);
                        break;
                    case "jmp":
                        Jump(r, o, 0, ref i);
                        break;
                    case "jie":
                        Jump(r, o, 1,ref i);
                        break;
                    case "jio":
                        Jump(r,o,2, ref i);
                        break;


                }
            }
            solution = b;
        }
        public void Jump(string register, int offset, int type,ref int i)
        {
            switch (type)
            {
                case 0:
                    i += (offset - 1);
                    break;
                case 1:
                    if (JumpEven()) i += (offset - 1);
                    break;
                case 2:
                    if (JumpOne()) i += (offset-1);
                    break;

            }

            bool JumpOne()
            {
                switch (register)
                {
                    case "a":
                        if(a==1) return true;
                        break;
                    case "b":
                        if (b == 1) return true;
                        break;
                }
                return false;
            }
            bool JumpEven()
            {
                switch (register)
                {
                    case "a":
                        if (a % 2==0) return true;
                        break;
                    case "b":
                        if (b % 2==0) return true;
                        break;
                }
                return false;
            }

        }
        public void Half(string register)
        {
            switch (register)
            {
                case "a":
                    a /= 2;
                    break;
                case "b":
                    b /= 2;
                    break;
            }
        }

        public void Increase(string register)
        {
            switch (register)
            {
                case "a":
                    a++;
                    break;
                case "b":
                    b++;
                    break;
            }
        }
        public void Triple(string register)
        {
            switch (register)
            {
                case "a":
                    a *= 3;
                    break;
                case "b":
                    b *= 3;
                    break;
            }
        }
        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var instructions = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.RemoveEmptyEntries)
                             .ToArray();
            a = 1;
            for (int i = 0; i < instructions.Length; i++)
            {
                Console.WriteLine($"{i} - a: {a}, b: {b}");
                var r = instructions[i].Split(Delimiter.delimiter_space, StringSplitOptions.None)[1].Trim(',');
                int o = 0;
                if (instructions[i].Contains(','))
                    o = int.Parse(instructions[i].Split(Delimiter.delimiter_comma, StringSplitOptions.None)[1].Trim());
                else if (instructions[i].Contains("jmp"))
                    o = int.Parse(instructions[i].Split(Delimiter.delimiter_space, StringSplitOptions.None)[1].Trim());
                switch (instructions[i].Split(Delimiter.delimiter_space, StringSplitOptions.None)[0])
                {
                    case "hlf":
                        Half(r);
                        break;
                    case "tpl":
                        Triple(r);
                        break;
                    case "inc":
                        Increase(r);
                        break;
                    case "jmp":
                        Jump(r, o, 0, ref i);
                        break;
                    case "jie":
                        Jump(r, o, 1, ref i);
                        break;
                    case "jio":
                        Jump(r, o, 2, ref i);
                        break;


                }
            }
            solution = b;
        }

    }

}