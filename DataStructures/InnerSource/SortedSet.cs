using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace AOC.DataStructures.InnerSource
{
    internal class SortedSet
    {
        public static void Example()
        {
        //Un insieme ordinato di elementi univoci.
        //Caratteristiche:
        //  Mantiene automaticamente gli elementi in ordine crescente.
        //Uso consigliato: Per insiemi univoci che devono essere ordinati.
        
            SortedSet<int> sortedNumbers = new SortedSet<int> { 3, 1, 2 };
            foreach (var num in sortedNumbers)
            {
                Console.WriteLine(num); // Stampa 1, 2, 3.
            }
        }
    }
}
