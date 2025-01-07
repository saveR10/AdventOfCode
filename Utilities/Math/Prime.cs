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

        // Calcolo del massimo comune divisore usando l'algoritmo euclideo
        public static int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
        
        // Calcolo del minimo comune multiplo
        public static int LCM(int a, int b)
        {
            return System.Math.Abs(a * b) / GCD(a, b);
        }
        // Metodo per calcolare il LCM di una lista di numeri
        public static int LCMOfList(List<int> numbers)
        {
            if (numbers == null || numbers.Count == 0)
                throw new ArgumentException("La lista non può essere vuota o nulla.");

            return numbers.Aggregate(LCM); // Riduci la lista calcolando LCM progressivi
        }
    }
}
