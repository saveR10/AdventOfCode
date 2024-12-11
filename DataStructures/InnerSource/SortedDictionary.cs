using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace AOC.DataStructures.InnerSource
{
    internal class SortedDictionary
    {
        public static void Example()
        {
            //Un dizionario che mantiene le chiavi in ordine crescente.
            //Caratteristiche:
            //  Le chiavi sono ordinate.
            //  Operazioni leggermente più lente rispetto a un Dictionary.
            //Uso consigliato: Per mappe che richiedono ordine.
            SortedDictionary<int, string> sortedDict = new SortedDictionary<int, string>();
            sortedDict[1] = "One";
            sortedDict[3] = "Three";
            sortedDict[2] = "Two";


            //Simile a Dictionary, ma mantiene le chiavi ordinate.
            SortedDictionary<int, string> _sortedDict = new SortedDictionary<int, string>();
            _sortedDict[2] = "Two";
            _sortedDict[1] = "One";
            foreach (var kv in _sortedDict)
            {
                Console.WriteLine(kv.Key + " " + kv.Value);
            }
        }
    }
}
