using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace AOC.DataStructures.InnerSource
{
    internal class LinkedList
    {
        public static void Example()
        {
            //Descrizione: Una lista generica collegata(LinkedList<T>) che consente inserimenti e rimozioni efficienti.
            //Caratteristiche:
            //  Supporta operazioni su nodi(aggiunta, rimozione) con complessità O(1).
            //Uso consigliato: Per scenari che richiedono modifiche frequenti della sequenza.
            LinkedList<int> linkedList = new LinkedList<int>();
            linkedList.AddLast(1);
            linkedList.AddLast(2);
            linkedList.AddFirst(0);

        }
    }
}
