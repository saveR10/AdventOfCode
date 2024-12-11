using AOC.Documents.LINQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace AOC.DataStructures.InnerSource
{
    internal class Dictionary
    {
        public static void Example()
        {
            //Descrizione: Una collezione di coppie chiave - valore(Dictionary<TKey, TValue>).
            //Caratteristiche:
            //  Consente l'accesso rapido ai valori tramite chiavi univoche.
            //  Basato su hashing, accesso O(1) in media.
            //Uso consigliato: Per mappe o associazioni tra chiavi e valori.
            Dictionary<string, int> ages = new Dictionary<string, int>();
            ages["Alice"] = 25;
            ages["Bob"] = 30;


            //Una struttura dati chiave-valore con lookup efficiente.
            Dictionary<int, string> dict = new Dictionary<int, string>();
            dict[1] = "One";
            Console.WriteLine(dict[1]); // Output: One
        }
    }
}
