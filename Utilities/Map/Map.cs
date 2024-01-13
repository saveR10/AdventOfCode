using System;
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
                    string ToWrite = Map[r, c] == true ? "#" : ".";
                    Console.Write(ToWrite);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

    }
}
