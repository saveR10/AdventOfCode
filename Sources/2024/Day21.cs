using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2024
{
    public class Day21 : Solver, IDay
    {            // Coordinate per il tastierino numerico
        static readonly Dictionary<char, (int x, int y)> NumericKeypad = new Dictionary<char, (int x, int y)>()
        {
            { '7', (0, 0) }, { '8', (0, 1) }, { '9', (0, 2) },
            { '4', (1, 0) }, { '5', (1, 1) }, { '6', (1, 2) },
            { '1', (2, 0) }, { '2', (2, 1) }, { '3', (2, 2) },
            { ' ', (3, 0) }, { '0', (3, 1) }, { 'A', (3, 2) }
        };
        // Coordinate corrette per il tastierino direzionale
        static readonly Dictionary<char, (int x, int y)> DirectionalKeypad = new Dictionary<char, (int x, int y)>
        {
            { ' ', (0, 0) }, { '^', (0, 1) }, { 'A', (0, 2) },
            { '<', (1, 0) }, { 'v', (1, 1) }, { '>', (1, 2) }
        };
        public void Part1(object input, bool test, ref object solution)
        {

            string inputText = (string)input;
            List<string> codes = new List<string>();
            Dictionary<string, int> NumericParts = new Dictionary<string, int>();
            foreach (string line in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    codes.Add(line);
                    NumericParts.Add(line, int.Parse(line.Trim('A')));
                }
            }

            int totalComplexity = 0;

            foreach (string code in codes)
            {
                string NumericRobotSequence = CalculateNumericSequence(code);
                string DirectionalFirstRobotSequence = CalculateDirectionalSequence(NumericRobotSequence);
                string DirectionalSecondRobotSequence = CalculateDirectionalSequence(DirectionalFirstRobotSequence);

                int complexity = DirectionalSecondRobotSequence.Length * NumericParts[code];
                totalComplexity += complexity;

                Console.WriteLine($"Code: {code}");
                Console.WriteLine($"Numeric Robot: {NumericRobotSequence}");
                Console.WriteLine($"Directional Robot 1: {DirectionalFirstRobotSequence}");
                Console.WriteLine($"Directional Robot 2: {DirectionalSecondRobotSequence}");
                Console.WriteLine($"Complexity: {DirectionalSecondRobotSequence.Length}*{NumericParts[code]} {complexity}\n");
            }

            solution = totalComplexity;
        }




        // Calcola la sequenza di movimenti per il tastierino numerico
        // Calcola la sequenza di movimenti per il tastierino numerico
        static string CalculateNumericSequence(string code)
        {
            string type = "N";
            var sequence = new List<string>();
            (int x, int y) currentPos = NumericKeypad['A']; // Posizione iniziale

            for (int i = 0; i < code.Length; i++)
            {
                char key = code[i];
                (int targetX, int targetY) = NumericKeypad[key];

                // Aggiungi il movimento verso il tasto
                sequence.Add(GenerateMovement(currentPos, (targetX, targetY), type));
                if (sequence.Last() == "") sequence.RemoveAt(sequence.Count - 1);
                sequence.Add("A"); // Premere il tasto

                // Aggiorna la posizione corrente
                currentPos = (targetX, targetY);


            }

            return string.Join("", sequence);
        }



        // Calcola la sequenza di movimenti per il tastierino direzionale
        static string CalculateDirectionalSequence(string inputSequence)
        {
            string type = "D"; //Directional
            var sequence = new List<string>();
            (int x, int y) currentPos = DirectionalKeypad['A']; // Posizione iniziale

            foreach (char command in inputSequence)
            {
                (int targetX, int targetY) = DirectionalKeypad[command];
                sequence.Add(GenerateMovement(currentPos, (targetX, targetY), type));
                if (sequence.Last() == "") sequence.RemoveAt(sequence.Count - 1);
                sequence.Add("A"); // Premere il tasto
                currentPos = (targetX, targetY);
            }

            return string.Join("", sequence);
        }

        // Genera la sequenza di movimenti da una posizione di partenza a una destinazione
        static string GenerateMovement((int x, int y) start, (int x, int y) target, string type)
        {
            var movements = new List<char>();

            if (type == "N")
            {
                while (start.x > target.x) { movements.Add('^'); start.x--; }
                while (start.y < target.y) { movements.Add('>'); start.y++; }
                while (start.y > target.y) { movements.Add('<'); start.y--; }
                while (start.x < target.x) { movements.Add('v'); start.x++; }
            }
            else if (type == "D")
            {
                while (start.y < target.y) { movements.Add('>'); start.y++; }
                while (start.x < target.x) { movements.Add('v'); start.x++; }
                while (start.x > target.x) { movements.Add('^'); start.x--; }
                while (start.y > target.y) { movements.Add('<'); start.y--; }
            }

            bool ChecKOuter(int _back, int _start)
            {
                if (type == "D")
                {
                    if (start.x == 0 && start.y == 0) return true; else return false;
                }
                else if (type == "N")
                {
                    if (start.x == 3 && start.y == 0)
                    {
                        //_start = back;
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                return false;
            }
            return string.Join("", movements);
        }

        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            foreach (string pair in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(pair))
                {
                }
            }
        }
    }
}
