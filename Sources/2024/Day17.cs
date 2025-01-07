using AOC;
using AOC.DataStructures.Clustering;
using AOC.Documents.LINQ;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Threading;

namespace AOC2024
{
    [ResearchAlgorithms(ResearchAlgorithmsAttribute.TypologyEnum.BinaryOperation)]
    [ResearchAlgorithms(ResearchAlgorithmsAttribute.TypologyEnum.Overflow)]
    public class Day17 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            long[] program;
            long registerA = 0;
            long registerB = 0;
            long registerC = 0;
            /*If register C contains 9, the program 2,6 would set register B to 1.
            If register A contains 10, the program 5,0,5,1,5,4 would output 0,1,2.
            If register A contains 2024, the program 0,1,5,4,3,0 would output 4,2,5,6,7,7,7,7,3,1,0 and leave 0 in register A.
            If register B contains 29, the program 1,7 would set register B to 26.
            If register B contains 2024 and register C contains 43690, the program 4,0 would set register B to 44354.*/
            string inputText = (string)input;
            long nTest = 0;
            if (test)
            {
                switch (nTest)
                {
                    case 0: break;
                    case 1:
                        inputText = "Register A: 10\r\nRegister B: 0\r\nRegister C: 0\r\n\r\nProgram: 5,0,5,1,5,4";
                        break;
                    case 2:
                        inputText = "Register A: 2024\r\nRegister B: 0\r\nRegister C: 0\r\n\r\nProgram: 0,1,5,4,3,0";
                        break;
                    case 3:
                        inputText = "Register A: 0\r\nRegister B: 29\r\nRegister C: 0\r\n\r\nProgram: 1,7";
                        break;
                    case 4:
                        inputText = "Register A: 0\r\nRegister B: 2024\r\nRegister C: 43690\r\n\r\nProgram: 4,0";
                        break;
                }
            }
            bool ProgramInstr = false;
            foreach (string instr in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (ProgramInstr)
                {
                    if (instr.StartsWith("Program:"))
                    {
                        var Progr = instr.Split(':')[1].Trim().Split(',');
                        program = new long[Progr.Length];
                        for (long i = 0; i < Progr.Length; i++)
                        {
                            program[i] = long.Parse(Progr[i].Trim());
                        }

                        string result = ExecuteProgram(program, registerA, registerB, registerC);
                        solution = result;
                    }
                }

                if (!string.IsNullOrEmpty(instr))
                {
                    if (instr.StartsWith("Register A:"))
                    {
                        registerA = long.Parse(instr.Split(':')[1].Trim());
                    }
                    if (instr.StartsWith("Register B:"))
                    {
                        registerB = long.Parse(instr.Split(':')[1].Trim());
                    }
                    if (instr.StartsWith("Register C:"))
                    {
                        registerC = long.Parse(instr.Split(':')[1].Trim());
                    }
                }
                else
                {
                    ProgramInstr = true;
                }
            }
        }
        public static long count = 0;
        public static string ExecuteProgram(long[] program, long registerA, long registerB = 0, long registerC = 0)
        {
            long A = registerA, B = registerB, C = registerC;
            long pointer = 0;
            List<long> output = new List<long>();

            // Funzione per ottenere il valore da usare nelle operazioni
            long GetComboValue(long combo)
            {
                switch (combo)
                {
                    case 0: return 0;
                    case 1: return 1;
                    case 2: return 2;
                    case 3: return 3;
                    case 4: return A;
                    case 5: return B;
                    case 6: return C;
                    default: throw new Exception("Invalid combo operand");
                }
            }

            // Esegui il programma finché non si raggiunge la fine
            while (pointer < program.Length)
            {
                long opcode = program[pointer];
                long operand = program[pointer + 1];
                pointer += 2; // Incremento del puntatore per passare alla prossima istruzione

                switch (opcode)
                {
                    case 0: // adv: A = A / (2^operand)
                        {
                            long divisor = (long)Math.Pow(2, GetComboValue(operand));
                            if (divisor != 0)
                            {
                                A /= divisor;
                            }
                            else
                            {
                                Console.WriteLine("Errore: Tentata divisione per zero.");
                                return string.Join(",", output);
                            }
                            break;
                        }
                    case 1: // bxl: B = B XOR operand
                        B ^= operand;
                        break;
                    case 2: // bst: B = operand % 8
                        B = GetComboValue(operand) % 8;
                        break;
                    case 3: // jnz: if (A != 0) pointer = operand
                        if (A != 0) pointer = operand;
                        break;
                    case 4: // bxc: B = B XOR C
                        B ^= C;
                        break;
                    case 5: // out: output operand % 8
                        output.Add(GetComboValue(operand) % 8);
                        break;
                    case 6: // bdv: B = A / (2^operand)
                        {
                            long divisor = (long)Math.Pow(2, GetComboValue(operand));
                            if (divisor != 0)
                            {
                                B = A / divisor;
                            }
                            else
                            {
                                Console.WriteLine("Errore: Tentata divisione per zero.");
                                return string.Join(",", output);
                            }
                            break;
                        }
                    case 7: // cdv: C = A / (2^operand)
                        {
                            long divisor = (long)Math.Pow(2, GetComboValue(operand));
                            if (divisor != 0)
                            {
                                C = A / divisor;
                            }
                            else
                            {
                                Console.WriteLine("Errore: Tentata divisione per zero.");
                                return string.Join(",", output);
                            }
                            break;
                        }
                    default:
                        throw new Exception("Invalid opcode");
                }
            }

            // Restituisci l'output finale come stringa separata da virgole
            return string.Join(",", output);
        }
        public void Part2(object input, bool test, ref object solution)
        {

            string inputText = (string)input;
            var matches = Regex.Matches(inputText, @"\d+").Cast<Match>().Select(m => int.Parse(m.Value)).ToList();

            // Ottiene il valore iniziale del registro A e il programma
            int reg_a = matches[0];
            var program_int = matches.Skip(3).ToList();
            List<long> program = new List<long>();
            foreach(var p in program_int)
            {
                program.Add(p);
            }
            // Esegue il programma e stampa l'output
            var output = string.Join(",", RunProgram(reg_a, program));
            Console.WriteLine(output);

            // Trova il valore iniziale richiesto
            var init_a = FindQuine(reg_a, program);
            Console.WriteLine(init_a);



            solution = init_a;

            //164278899142333
        }
        static List<long> RunProgram(long init_a, List<long> program)
        {
            // Registri
            long reg_a = init_a;
            long reg_b = 0;
            long reg_c = 0;

            // Funzione per determinare il valore di un operando
            long OperandValue(long operand)
            {
                if (operand >= 0 && operand <= 3)
                    return operand;
                else if (operand == 4)
                    return reg_a;
                else if (operand == 5)
                    return reg_b;
                else if (operand == 6)
                    return reg_c;
                throw new InvalidOperationException("Operando non valido");
            }

            // Puntatore alle istruzioni
            long instructionPtr = 0;
            var output = new List<long>();

            // Esegue il programma
            while (instructionPtr < program.Count)
            {
                long opcode = program[(int)instructionPtr];
                long operand = program[(int)instructionPtr + 1];

                switch (opcode)
                {
                    case 0: // Divisione sul registro A
                        reg_a /= (int)Math.Pow(2, OperandValue(operand));
                        break;
                    case 1: // XOR del registro B con l'operando
                        reg_b ^= operand;
                        break;
                    case 2: // Modulo sul registro B
                        reg_b = OperandValue(operand) % 8;
                        break;
                    case 3: // Salto condizionato
                        if (reg_a != 0)
                            instructionPtr = operand - 2;
                        break;
                    case 4: // XOR tra registro B e registro C
                        reg_b ^= reg_c;
                        break;
                    case 5: // Aggiunge un valore all'output
                        output.Add(OperandValue(operand) % 8);
                        break;
                    case 6: // Divisione sul registro B
                        reg_b = reg_a / (long)Math.Pow(2, OperandValue(operand));
                        break;
                    case 7: // Divisione sul registro C
                        reg_c = reg_a / (long)Math.Pow(2, OperandValue(operand));
                        break;
                    default:
                        throw new InvalidOperationException($"Opcode non valido: {opcode}");
                }

                // Avanza di due istruzioni
                instructionPtr += 2;
            }

            return output;
        }

        static int FindQuine(int reg_a, List<long> program)
        {
            var output = new List<long>();
            var matched = program.Skip(program.Count - 1).ToList(); // Ultimo elemento del programma
            reg_a = (int)Math.Pow(8, 15); // Valore minimo per output a 16 cifre
            int power = 14;

            while (!output.SequenceEqual(program))
            {
                reg_a += (int)Math.Pow(8, power);
                output = RunProgram(reg_a, program);

                // Verifica se gli ultimi elementi corrispondono
                if (MatchLastElements(output, matched))
                {
                    power = Math.Max(0, power - 1);
                    matched = program.Skip(program.Count - matched.Count - 1).Take(matched.Count + 1).ToList();
                }
            }

            return reg_a;
        }

        static bool MatchLastElements(List<long> output, List<long> matched)
        {
            if (output.Count < matched.Count)
                return false;

            for (int i = 0; i < matched.Count; i++)
            {
                if (output[output.Count - matched.Count + i] != matched[i])
                    return false;
            }

            return true;
        }
    }
}