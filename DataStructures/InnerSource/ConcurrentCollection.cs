using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AOC.DataStructures.InnerSource
{
    internal class ConcurrentCollection
    {
        public static void Example()
        {
            //Descrizione: Collezioni thread-safe per applicazioni multithreading.
            //Esempi comuni:
            //  ConcurrentDictionary<TKey, TValue>: Versione thread-safe del dizionario.
            //  ConcurrentQueue<T>: Versione thread-safe della coda.
            //  ConcurrentStack<T>: Versione thread-safe dello stack.
            //Uso consigliato: Per applicazioni multithreading o parallele.

            ConcurrentDictionary<int, int> concDict = new ConcurrentDictionary<int, int>();
            ConcurrentQueue<int> concQueue = new ConcurrentQueue<int>();
            ConcurrentStack<int> concStack = new ConcurrentStack<int>();


            //Una versione thread - safe di Dictionary.
            var concurrentDict = new ConcurrentDictionary<int, string>();
            concurrentDict[1] = "Safe";
        }
    }
}
