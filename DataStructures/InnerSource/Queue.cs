using AOC.Documents.LINQ;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace AOC.DataStructures.InnerSource
{
    internal class Queue
    {
        //Una struttura dati FIFO(First In, First Out).
        //Caratteristiche:
        //  Inserimento alla fine(Enqueue).
        //  Rimozione dall'inizio (Dequeue).
        //Uso consigliato: Per gestire una sequenza ordinata di elementi da processare.
        public static void Example()
        {
            Queue<string> tasks = new Queue<string>();
            tasks.Enqueue("Task1");
            tasks.Enqueue("Task2");
            string nextTask = tasks.Dequeue();

            //Una coda FIFO(First - In, First - Out).
            Queue<string> queue = new Queue<string>();
            queue.Enqueue("First");
            queue.Enqueue("Second");
            Console.WriteLine(queue.Dequeue()); // Output: First
        }
    }
}
