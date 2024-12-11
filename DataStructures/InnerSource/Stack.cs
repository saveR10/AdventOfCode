using AOC.Documents.LINQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace AOC.DataStructures.InnerSource
{
    internal class Stack
    {
        public static void Example()
        {
            //Una struttura dati LIFO(Last In, First Out).
            //Caratteristiche:
            //  Inserimento e rimozione avvengono all'inizio (Push e Pop).
            //Uso consigliato: Per risolvere problemi che richiedono un "backtracking" o uno storico.

            Stack<int> stack = new Stack<int>();
            stack.Push(10);
            stack.Push(20);
            int lastAdded = stack.Pop();


            //Una pila LIFO(Last - In, First - Out).
            Stack<int> _stack = new Stack<int>();
            _stack.Push(1);
            _stack.Push(2);
            Console.WriteLine(_stack.Pop()); // Output: 2
        }
    }
}
