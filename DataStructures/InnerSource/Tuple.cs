using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.DataStructures.InnerSource
{
    internal class Tuple
    {
        public static void Example()
        {
            //Utilizzate per raggruppare un numero fisso di valori eterogenei.
            var tuple = System.Tuple.Create(1, "hello", true);
            Console.WriteLine(tuple.Item2); // Output: hello

            //ValueTuple usato dalla versione 7.0
            (int id, string name) person = (1, "John");
            Console.WriteLine(person.name); // Output: John
        }
    }
}
