using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;

namespace AOC2024
{
    public class Day4 : Solver, IDay
    {
        [ResearchAlgorithmsAttribute(TypologyEnum.Game)] //Crosswords

        public void Part1(object input, bool test, ref object solution)
        {
            int n;
            if (test)
            {
                n = 10;
            }
            else
            {
                n = 140;
            }
            string[,] matrix = new string[n, n];
            string inputText = (string)input;
            int r = 0;
            foreach (string line in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    for(int c = 0; c < n; c++)
                    {
                        matrix[r, c] = line.Substring(c,1);
                    }
                    r++;
                }
            }

            int matched = 0;

            // Estrai tutte le linee
            var allLines = ExtractLines(matrix, n);

            // Stampa le linee
            foreach (var line in allLines)
            {
                Console.WriteLine(string.Join(" -> ", line));
            }

            foreach(var line in allLines)
            {
                matched += ScanLine(string.Join("",line));
            }
            solution = matched;

        }
        public int ScanLine(string line)
        {
            var matches = Regex.Matches(line, "XMAS");
            return matches.Count;

        }
        public static List<List<string>> ExtractLines(string[,] matrix, int n)
        {
            var lines = new List<List<string>>();
            bool f_orizzontali = true;
            bool f_verticali = true;
            bool f_diag_princ = true;
            bool f_diag_sec = true;

            // Estrai linee orizzontali (da sinistra a destra e viceversa)
            if (f_orizzontali)
            for (int i = 0; i < n; i++)
            {
                var leftToRight = new List<string>();
                var rightToLeft = new List<string>();
                for (int j = 0; j < n; j++)
                {
                    leftToRight.Add(matrix[i, j]);
                    rightToLeft.Add(matrix[i, n - 1 - j]);
                }
                lines.Add(leftToRight);
                lines.Add(rightToLeft);
            }

            // Estrai linee verticali (dall'alto verso il basso e viceversa)
            if (f_verticali)
                for (int j = 0; j < n; j++)
            {
                var topToBottom = new List<string>();
                var bottomToTop = new List<string>();
                for (int i = 0; i < n; i++)
                {
                    topToBottom.Add(matrix[i, j]);
                    bottomToTop.Add(matrix[n - 1 - i, j]);
                }
                lines.Add(topToBottom);
                lines.Add(bottomToTop);
            }

            // Estrai diagonali principali (dal lato sinistro in alto a destra in basso)
            if (f_diag_princ)
            {

            for (int i = n-1; i > 0; i--)  // Start da ogni elemento della prima colonna (partendo dall'angolo in basso a sinistra)
            {
                var diagonal = new List<string>();
                var diagonalRev = new List<string>();

                for (int k = 0; k + i < n; k++)
                {
                    diagonal.Add(matrix[i+k, k]);
                }
                if (diagonal.Count >= 1)
                {
                    lines.Add(diagonal);
                    lines.Add(ReverseList(diagonal));
                }
            }

            for (int j = 0; j <n; j++)  // Start da ogni elemento della prima colonna (inclusa la diagonale principale)
            {
                var diagonal = new List<string>();
                for (int k = 0; k + j < n; k++)
                {
                    diagonal.Add(matrix[k, k+j]);
                }
                if (diagonal.Count >= 1) 
                {
                    lines.Add(diagonal);
                    lines.Add(ReverseList(diagonal));
                }
            }
            }

            // Estrai diagonali secondarie (dal lato destro in alto a sinistra in basso)
            if (f_diag_sec)
            {
                for (int i = n-1; i > 0; i--) // Start da ogni elemento dell'ultima colonna (partendo dall'angolo in basso a destra)
                {
                    var diagonal = new List<string>();

                    for (int k = 0; k + i < n; k++)
                    {
                        diagonal.Add(matrix[i + k, n-1-k]);
                    }
                    if (diagonal.Count >= 1)
                    {
                        lines.Add(diagonal);
                        lines.Add(ReverseList(diagonal));
                    }
                }
                
                for (int j = n-1; j > 0; j--)  // Start da ogni elemento della prima riga, inclusa la diagonale secondaria principale (partendo dall'angolo in alto a destra)
                {
                    var diagonal = new List<string>();
                    for (int k = 0; n-1+k-j < n; k++)
                    {
                        diagonal.Add(matrix[k, j-k]);
                    }
                    if (diagonal.Count >= 1)
                    {
                        lines.Add(diagonal);
                        lines.Add(ReverseList(diagonal));
                    }
                }
            }



            return lines;
        }
        public static List<string> ReverseList(List<string> inputList)
        {
            // Crea una nuova lista per contenere gli elementi invertiti
            List<string> reversedList = new List<string>(inputList);

            // Inverti la lista originale
            reversedList.Reverse();

            return reversedList;
        }
        public void Part2(object input, bool test, ref object solution)
        {
            int n;
            if (test)
            {
                n = 10;
            }
            else
            {
                n = 140;
            }
            string[,] matrix = new string[n, n];
            string inputText = (string)input;
            int r = 0;
            foreach (string line in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    for (int c = 0; c < n; c++)
                    {
                        matrix[r, c] = line.Substring(c, 1);
                    }
                    r++;
                }
            }
            int CrossMas= CountMasCrosses(matrix,n);
            solution = CrossMas;
        }
        public static int CountMasCrosses(string[,] matrix, int n)
        {
            int count = 0;

            // Scansiona ogni cella della matrice
            for (int i = 1; i < n - 1; i++) // Salta il bordo
            {
                for (int j = 1; j < n - 1; j++) // Salta il bordo
                {
                    if (matrix[i, j] == "A")
                    {
                        // Controlla entrambe le direzioni diagonali per formare una croce
                        bool hasMasCross =
                            (matrix[i - 1, j - 1] == "M" && matrix[i + 1, j + 1] == "S" &&
                             matrix[i - 1, j + 1] == "M" && matrix[i + 1, j - 1] == "S") ||

                             (matrix[i - 1, j - 1] == "S" && matrix[i + 1, j + 1] == "M" &&
                             matrix[i - 1, j + 1] == "M" && matrix[i + 1, j - 1] == "S") ||

                             (matrix[i - 1, j - 1] == "S" && matrix[i + 1, j + 1] == "M" &&
                             matrix[i - 1, j + 1] == "S" && matrix[i + 1, j - 1] == "M") ||

                             (matrix[i - 1, j - 1] == "M" && matrix[i + 1, j + 1] == "S" &&
                             matrix[i - 1, j + 1] == "S" && matrix[i + 1, j - 1] == "M");

                        if (hasMasCross)
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }
    }
}
