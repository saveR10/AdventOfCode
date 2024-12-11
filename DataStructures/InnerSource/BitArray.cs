using AOC.DataStructures.Searching;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.DataStructures.InnerSource
{
    internal class BitArray_
    {
        public static void Example()
        {
            //Una collezione compatta di valori booleani rappresentati come bit.
            BitArray bits = new BitArray(4);
            bits[0] = true;
            Console.WriteLine(bits[0]); // Output: True
        }
    }
}
