using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;
using System.Linq;
using System.Numerics;   // <- importante

namespace AOC2025
{
    public class Problema
    {
        public List<int> Numeri { get; set; } = new List<int>();
        public char Operazione { get; set; }
        public long Risultato;

        public long Calcola()
        {
            if (Numeri.Count == 0)
                return 0;

            long risultato = Numeri[0];

            for (int i = 1; i < Numeri.Count; i++)
            {
                switch (Operazione)
                {
                    case '*':
                        risultato *= Numeri[i];
                        break;

                    case '+':
                        risultato += Numeri[i];
                        break;

                    case '-':
                        risultato -= Numeri[i];
                        break;

                    case '/':
                        risultato /= Numeri[i];
                        break;

                    default:
                        throw new Exception("Operazione non valida");
                }
            }

            Risultato = risultato;
            return risultato;
        }
        public static List<Problema> ParseProblemiComplex(string input, bool UseCephalopodMath = false)
        {
            var lines = input
                .Split(new[] { '\n','\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(l => l)
                .ToList();

            var Righe = lines.Count();

            // Parsing degli operatori
            var operazioni = lines[Righe - 1]
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s[0])
                .ToList();

            int Problemi = operazioni.Count;

            var PosColumns = new Dictionary<int, int>();

            int progr = 0;
            for (int i = 0; i < lines[Righe - 1].Length; i++)
            {

                char c = lines[Righe - 1][i];
                if (c == '*' || c == '+' || c == '-' || c == '/') // se vuoi supportare tutti gli operatori
                {
                    PosColumns.Add(progr,i); // qui key = indice, value = operatore
                                              // oppure se vuoi key = operatore, value = indice, fai PosColumns.Add(c, i)
                    progr++;
                }
            }
            var numeri = new List<List<int>>();

            //Ciclo sui problemi
            for (int p = 0;  p < Problemi; p++)
            {
                //Definisco intervallo di colonne
                int minCol = PosColumns[p];
                int maxCol = 0;
                if (p == Problemi - 1)
                {
                    maxCol = lines[Righe - 1].Length -1;
                }
                else
                {
                    maxCol = PosColumns[p + 1] - 2;
                }

                List<string> ListaNumeri = new List<string>();
                for (int rig = 0; rig < Righe - 1; rig++)
                {
                    ListaNumeri.Add(lines[rig].Substring(minCol, maxCol - minCol +1));   
                }
                int H = ListaNumeri.Count;
                int W = ListaNumeri.Max(s => s.Length); // larghezza massima

                // Pad right a sinistra con spazi per uniformare la lunghezza
                for (int r = 0; r < H; r++)
                    ListaNumeri[r] = ListaNumeri[r].PadLeft(W, ' ');

                // Lista finale dei numeri
                List<int> numeriFinali = new List<int>();

                // Ciclo da destra a sinistra
                for (int c = W - 1; c >= 0; c--)
                {
                    string numStr = "";
                    for (int r = 0; r < H; r++)
                    {
                        char ch = ListaNumeri[r][c];
                        if (char.IsDigit(ch))
                            numStr += ch;
                    }

                    if (numStr.Length > 0)
                        numeriFinali.Add(int.Parse(numStr));
                }
                numeri.Add(numeriFinali);
            }

          

     



            var problemi = new List<Problema>();

            for (int col = 0; col < Problemi; col++)
            {
                var p = new Problema
                {
                    Operazione = operazioni[col]
                };
                /*for (int rig = 0; rig < Righe - 1; rig++)
                {
                    p.Numeri.Add(numeri[col][rig]);
                }*/
                //foreach(var r in )
                p.Numeri.AddRange(numeri[col]);

                problemi.Add(p);
            }
            return problemi;


        }

        public static List<Problema> ParseProblemi(string input, bool UseCephalopodMath = false)
        {
            var lines = input
                .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(l => l.Trim())
                .ToList();

            var conta = lines.Count();

            // Parsing dei numeri
            var numeri = new List<List<int>>();
            for (int i = 0; i < conta - 1; i++)
            {
                numeri.Add(
                    lines[i]
                        .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToList()
                );
            }

            // Parsing degli operatori
            var operazioni = lines[conta - 1]
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s[0])
                .ToList();


            int colonne = numeri[0].Count;

            var problemi = new List<Problema>();

            for (int col = 0; col < colonne; col++)
            {
                var p = new Problema
                {
                    Operazione = operazioni[col]
                };
                for (int rig = 0; rig < conta - 1; rig++)
                {
                    p.Numeri.Add(numeri[rig][col]);
                }


                problemi.Add(p);
            }
            return problemi;


        }

        //public override string ToString()
        //{
        //   return string.Join($" {Operazione} ", Numeri) + " = " + Calcola();
        //}
    }

    //[ResearchAlgorithmsAttribute(ResolutionEnum)]
    public class Day6 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var problemi = Problema.ParseProblemi(inputText);
            foreach (var p in problemi)
                p.Calcola();

            long sum = problemi.Sum(p => p.Risultato);

            solution = sum;
            //50299438018 too low
        }




        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var problemi = Problema.ParseProblemiComplex(inputText);
            foreach (var p in problemi)
                p.Calcola();

            long sum = problemi.Sum(p => p.Risultato);

            solution = sum;
        }
    }
}