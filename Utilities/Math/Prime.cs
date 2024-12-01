using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Utilities.Math
{
    public static class Prime
    {
        //TODO aggiungere descrizione
        public static List<int> PrimeFactors(int a)
        {
            List<int> retval = new List<int>();
            for (int b = 2; a > 1; b++)
            {
                while (a % b == 0)
                {
                    a /= b;
                    retval.Add(b);
                }
            }
            return retval;
        }
    }
}
