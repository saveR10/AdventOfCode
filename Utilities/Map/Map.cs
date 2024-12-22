﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Utilities.Map
{
    public static class Map
    {
        public static void ShowBooleanMap(bool[,] Map, int dimGrid)
        {
            for (int r = 0; r < dimGrid; r++)
            {
                for (int c = 0; c < dimGrid; c++)
                {
                    string ToWrite = Map[r, c] == true ? "X" : ".";
                    Console.Write(ToWrite);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static void ShowStringMap(string[,] Map)
        {
            int dimX = Map.GetLength(0); // Numero di righe
            int dimY = Map.GetLength(1); // Numero di colonne

            // Costruire l'intestazione delle colonne
            string tensRow = "   "; // Riga delle decine
            string unitsRow = "   "; // Riga delle unità

            for (int c = 0; c < dimY; c++)
            {
                tensRow += (c / 10) > 0 ? (c / 10).ToString() : " "; // Decine o spazio vuoto
                unitsRow += (c % 10).ToString();                     // Unità
            }

            // Stampare intestazione
            Console.WriteLine(tensRow); // Riga decine
            Console.WriteLine(unitsRow); // Riga unità

            // Stampare la mappa
            for (int r = 0; r < dimX; r++)
            {
                // Stampa l'indice di riga (con spaziatura per allineamento)
                Console.Write(r.ToString().PadLeft(2) + " ");
                for (int c = 0; c < dimY; c++)
                {
                    Console.Write(Map[r, c]);
                }
                Console.WriteLine(); // Nuova riga
            }
        }

        public static void ShowCharMap(char[,] Map, int dimX, int dimY)
        {
            for (int r = 0; r < dimX; r++)
            {
                for (int c = 0; c < dimY; c++)
                {
                    Console.Write(Map[r, c]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }


        public static ((int Row, int Col), (int Row, int Col)) FindStartAndEnd(string[,] grid)
        {
            (int Row, int Col) start = (0, 0);
            (int Row, int Col) end = (0, 0);

            for (int r = 0; r < grid.GetLength(0); r++)
            {
                for (int c = 0; c < grid.GetLength(1); c++)
                {
                    if (grid[r, c] == "S") start = (r, c);
                    if (grid[r, c] == "E") end = (r, c);
                }
            }

            return (start, end);
        }
    }
}
