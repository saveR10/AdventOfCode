using AOC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.DataStructures.ParsingInput
{
    public static class InputMapParser
    {
        /// <summary>
        /// Effettua il parsing di un input testuale multi-linea in una mappa bidimensionale
        /// rappresentata come <c>string[,]</c>.  
        /// È pensata per scenari come Advent of Code, dove l'input consiste spesso in una griglia.
        ///
        /// Funzionalità principali:
        /// - Supporta righe vuote (ignorabili o no)
        /// - Supporta padding opzionale per creare una mappa rettangolare
        /// - Restituisce una mappa 2D con ogni cella come stringa
        /// - Raccoglie automaticamente tutte le posizioni dei caratteri speciali
        ///   (ad esempio 'S', 'E', '#', '.', qualsiasi carattere)
        /// </summary>
        /// <param name="input">Testo di input completo, contenente linee separate da un delimitatore.</param>
        /// <param name="specialPositions">
        /// Dizionario in output contenente tutte le posizioni trovate per ogni carattere
        /// presente nella mappa.
        /// Esempio:
        /// <code>
        /// specialPositions["S"] → lista di tutte le coordinate di 'S'
        /// specialPositions["#"] → lista di tutte le mura
        /// </code>
        /// </param>
        /// <param name="ignoreEmptyLines">
        /// Se <c>true</c>, le righe vuote vengono ignorate durante il parsing.
        /// </param>
        /// <param name="padToMaxWidth">
        /// Se <c>true</c>, tutte le righe più corte vengono "paddate" con spazi
        /// per ottenere una mappa rettangolare.
        /// </param>
        /// <returns>
        /// Una matrice 2D <c>string[,]</c> contenente la mappa.
        /// Ogni cella è una stringa lunga 1 (carattere convertito in string).
        /// </returns>
        /// <exception cref="Exception">Se l’input risulta completamente vuoto.</exception>
        public static string[,] Parse(
            string input,
            out Dictionary<string, List<(int r, int c)>> specialPositions,
            bool ignoreEmptyLines = true,
            bool padToMaxWidth = false)
        {
            specialPositions = new Dictionary<string, List<(int r, int c)>>();

            // --- 1) Split dell’input in base al delimitatore definito ---
            //    Si usa StringSplitOptions.None perché AoC spesso richiede di mantenere righe vuote
            //    e il comportamento è controllato dal parametro ignoreEmptyLines.
            var lines = input
                .Split(Delimiter.delimiter_line, StringSplitOptions.None)
                .Where(l => !ignoreEmptyLines || l.Length > 0)
                .ToList();

            if (lines.Count == 0)
                throw new Exception("Input vuoto.");

            // Numero di righe
            int r = lines.Count;
            // Lunghezza massima delle righe (serve per mappa rettangolare)
            int c = lines.Max(l => l.Length);

            // --- 2) Padding opzionale ---
            // Se richiesto, tutte le righe vengono estese alla stessa lunghezza
            if (padToMaxWidth)
            {
                lines = lines
                    .Select(l => l.PadRight(c)) // pad con spazi
                    .ToList();
            }

            // --- 3) Creazione della griglia 2D ---
            string[,] map = new string[r, c];

            // --- 4) Conversione riga per riga ---
            for (int i = 0; i < r; i++)
            {
                var line = lines[i];

                for (int j = 0; j < line.Length; j++)
                {
                    string cell = line[j].ToString();
                    map[i, j] = cell;

                    // --- 5) Memorizzazione dei simboli speciali ---
                    // Crea la lista se non esiste e aggiunge la posizione.
                    if (!specialPositions.ContainsKey(cell))
                        specialPositions[cell] = new List<(int r, int c)>();

                    specialPositions[cell].Add((i, j));
                }

                // Se padToMaxWidth = false, le celle nelle righe corte restano null.
                // Questo è voluto e permette gestione flessibile delle mappe irregolari.
            }

            return map;
        }

    }

}
