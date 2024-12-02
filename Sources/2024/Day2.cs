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
    public class Day2 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            int safetyReports = 0;
            string inputText = (string)input;
            foreach (string report in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(report))
                {
                    bool safety = true;
                    safety = Check(report);

                    if (safety)
                    {
                        safetyReports++;
                    }
                }
            }
            solution = safetyReports;
        }

        public bool Check(string report)
        {
            List<int> levels = report.Split(' ').Select(int.Parse).ToList();

            if (levels.Count < 2)
                return false;

            bool isAscending = levels[1] > levels[0];
            bool isDescending = levels[1] < levels[0];

            if (levels[1] == levels[0])
                return false;

            for (int i = 1; i < levels.Count; i++)
            {
                int diff = levels[i] - levels[i - 1];

                if (diff == 0)
                    return false;

                if (Math.Abs(diff) < 1 || Math.Abs(diff) > 3)
                    return false;

                if (isAscending && diff < 0)
                    return false;

                if (isDescending && diff > 0)
                    return false;
            }

            return true;
        }
        public enum OrderType
        {
            Ascendent,
            Descendent
        }

        public void Part2(object input, bool test, ref object solution)
        {
            int safetyReports = 0;
            string inputText = (string)input;
            foreach (string report in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(report))
                {
                    bool safety = true;
                    safety = Check(report);

                    if (!safety)
                    {
                        int countLevels = report.Split(' ').Select(int.Parse).ToList().Count();

                        for (int i = countLevels - 1; i >= 0; i--)
                        {
                            List<int> NewLevels = new List<int>();
                            NewLevels = report.Split(' ').Select(int.Parse).ToList();
                            NewLevels.RemoveAt(i);
                            if (!safety)
                            {
                                safety = CheckDampener(NewLevels); 
                            }
                        }
                    }

                    if (safety)
                    {
                        safetyReports++;
                    }
                }
            }
            solution = safetyReports;
        }

        public bool CheckDampener(List<int> levels)
        {
            // Controllo rapido per array con meno di due elementi
            if (levels.Count < 2)
                return false;

            // Determina l'ordine (crescente o decrescente)
            bool isAscending = levels[1] > levels[0];
            bool isDescending = levels[1] < levels[0];

            // Se i primi due elementi sono uguali, non valido
            if (levels[1] == levels[0])
                return false;

            // Itera sui livelli
            for (int i = 1; i < levels.Count; i++)
            {
                int diff = levels[i] - levels[i - 1];

                // Controllo se ci sono numeri uguali
                if (diff == 0)
                    return false;

                // Controllo incremento/decremento minimo/massimo
                if (Math.Abs(diff) < 1 || Math.Abs(diff) > 3)
                    return false;

                // Controllo che mantenga lo stesso ordine (ascendente o discendente)
                if (isAscending && diff < 0)
                    return false;

                if (isDescending && diff > 0)
                    return false;
            }

            return true;
        }
    }
}
