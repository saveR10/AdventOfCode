using AOC;
using AOC.SearchAlghoritmhs;
using AOC.Utilities.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;

namespace AOC2016
{
    [ResearchAlgorithms(title: "Day 17: Two Steps Forward",
                        typology: TypologyEnum.Map | TypologyEnum.Hashing, // mappa + MD5
                        resolution: ResolutionEnum.DFS | ResolutionEnum.BFS,
                        difficult: ResearchAlgorithmsAttribute.DifficultEnum.Hard,
                        note: "Percorsi in una griglia 4x4 controllati da hash MD5 del passcode")]
    public class Day17 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputString = (string)input;
            int testcase = 1;
            var passcodes = inputString.Split(new[] { "\n\n", "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (!test) testcase = 0;
            string passcode = passcodes[testcase];

            var StartPosition = (0, 0);
            var TargetPosition = (3, 3); // La griglia è 0..3 per un 4x4
            var openChars = new char[] { 'b', 'c', 'd', 'e', 'f' };

            // Definiamo le direzioni e i delta
            var moves = new Dictionary<string, (int dx, int dy)>()
            {
                {"U", (0, -1)},
                {"D", (0, 1)},
                {"L", (-1, 0)},
                {"R", (1, 0)}
            };

            // ================= BFS =================
            // BFS usa una coda di stati: posizione corrente + percorso (stringa)
            var queue = new Queue<((int x, int y) pos, string path)>();
            queue.Enqueue((StartPosition, passcode));

            while (queue.Count > 0)
            {
                var (position, path) = queue.Dequeue(); // estrai lo stato corrente
                var hash = CreateMD5(path).ToLower();   // calcola l'hash MD5 del percorso corrente

                // Verifica quali porte sono aperte
                var doors = new Dictionary<string, bool>();
                doors["U"] = openChars.Contains(hash[0]);
                doors["D"] = openChars.Contains(hash[1]);
                doors["L"] = openChars.Contains(hash[2]);
                doors["R"] = openChars.Contains(hash[3]);

                // Controlla se siamo arrivati al target
                if (position == TargetPosition)
                {
                    // Rimuovo il prefisso del passcode iniziale
                    solution = path.Substring(passcode.Length);
                    return; // BFS garantisce che il primo trovato è il più corto
                }

                // Per ogni porta aperta, genera il nuovo stato
                foreach (var dir in doors.Keys)
                {
                    if (doors[dir])
                    {
                        var newX = position.x + moves[dir].dx;
                        var newY = position.y + moves[dir].dy;

                        // Controlla che la nuova posizione sia valida
                        if (newX >= 0 && newX <= 3 && newY >= 0 && newY <= 3)
                        {
                            queue.Enqueue(((newX, newY), path + dir)); // aggiungi lo stato alla coda
                        }
                    }
                }
            }
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                //return  HexadecimalEncoding.ToHexString(hashBytes); // .NET 5 +

                // Convert the byte array to hexadecimal string prior to .NET 5
                StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        // ================= DFS per percorso più lungo =================
        public void Part2(object input, bool test, ref object solution)
        {
            string inputString = (string)input;
            int testcase = 1;
            var passcodes = inputString.Split(new[] { "\n\n", "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (!test) testcase = 0;
            string passcode = passcodes[testcase];

            var StartPosition = (0, 0);
            var TargetPosition = (3, 3);
            var openChars = new char[] { 'b', 'c', 'd', 'e', 'f' };

            var moves = new Dictionary<string, (int dx, int dy)>()
    {
        {"U", (0, -1)},
        {"D", (0, 1)},
        {"L", (-1, 0)},
        {"R", (1, 0)}
    };

            int maxLength = 0;

            // DFS ricorsivo
            void DFS((int x, int y) pos, string path)
            {
                var hash = CreateMD5(path).ToLower();
                var doors = new Dictionary<string, bool>();
                doors["U"] = openChars.Contains(hash[0]);
                doors["D"] = openChars.Contains(hash[1]);
                doors["L"] = openChars.Contains(hash[2]);
                doors["R"] = openChars.Contains(hash[3]);

                if (pos == TargetPosition)
                {
                    // Se arriviamo al target, aggiorna la lunghezza massima
                    int length = path.Length - passcode.Length;
                    if (length > maxLength)
                        maxLength = length;
                    return;
                }

                // Esplora tutte le mosse valide
                foreach (var dir in doors.Keys)
                {
                    if (doors[dir])
                    {
                        var newX = pos.x + moves[dir].dx;
                        var newY = pos.y + moves[dir].dy;

                        if (newX >= 0 && newX <= 3 && newY >= 0 && newY <= 3)
                        {
                            DFS((newX, newY), path + dir); // chiamata ricorsiva
                        }
                    }
                }
            }

            DFS(StartPosition, passcode);
            solution = maxLength;
        }
    }
}