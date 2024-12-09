using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace AOC.DataStructures.InnerSource
{
    internal class List
    {
        public static void Example()
        {
            //Descrizione: Una lista generica dinamica(List<T>) che può crescere o ridursi.
            //Caratteristiche:
            //  Supporta operazioni di inserimento, rimozione e ricerca.
            //  Cresce automaticamente in base alle necessità.
            //Uso consigliato: Per collezioni dinamiche di dimensioni sconosciute.
            List<int> numbers = new List<int> { 1, 2, 3 };
            numbers.Add(4);
            numbers.Remove(2);

        }
    }
}
