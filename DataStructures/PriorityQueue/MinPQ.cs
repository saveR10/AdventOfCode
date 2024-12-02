using System;
using System.Collections.Generic;
using System.IO;
namespace AOC.DataStructures.PriorityQueue
{
    public class MinPQ<Key>
    {
        private Key[] pq;                    // store items at indices 1 to n
        private int n;                       // number of items on priority queue
        private readonly Comparer<Key> comparator;  // optional comparator

        public MinPQ(int initCapacity)
        {
            pq = new Key[initCapacity + 1];
            n = 0;
        }

        public MinPQ() : this(1) { }

        public MinPQ(int initCapacity, Comparer<Key> comparator)
        {
            this.comparator = comparator;
            pq = new Key[initCapacity + 1];
            n = 0;
        }

        public MinPQ(Comparer<Key> comparator) : this(1, comparator) { }

        public MinPQ(Key[] keys)
        {
            n = keys.Length;
            pq = new Key[keys.Length + 1];
            for (int i = 0; i < n; i++)
                pq[i + 1] = keys[i];
            for (int k = n / 2; k >= 1; k--)
                Sink(k);
            //Assert isMinHeap();
        }

        public bool IsEmpty() => n == 0;

        public int Size() => n;

        public Key Min()
        {
            if (IsEmpty()) throw new InvalidOperationException("Priority queue underflow");
            return pq[1];
        }

        private void Resize(int capacity)
        {
            if (capacity <= n) return;
            Key[] temp = new Key[capacity];
            for (int i = 1; i <= n; i++)
                temp[i] = pq[i];
            pq = temp;
        }

        public void Insert(Key x)
        {
            if (n == pq.Length - 1) Resize(2 * pq.Length);
            pq[++n] = x;
            Swim(n);
            //Assert isMinHeap();
        }

        public Key DelMin()
        {
            if (IsEmpty()) throw new InvalidOperationException("Priority queue underflow");
            Key min = pq[1];
            Exchange(1, n--);
            Sink(1);
            pq[n + 1] = default; // to avoid loitering and help with garbage collection
            if ((n > 0) && (n == (pq.Length - 1) / 4)) Resize(pq.Length / 2);
            //assert isMinHeap();
            return min;
        }

        private void Swim(int k)
        {
            while (k > 1 && Greater(k / 2, k))
            {
                Exchange(k / 2, k);
                k = k / 2;
            }
        }

        private void Sink(int k)
        {
            while (2 * k <= n)
            {
                int j = 2 * k;
                if (j < n && Greater(j, j + 1)) j++;
                if (!Greater(k, j)) break;
                Exchange(k, j);
                k = j;
            }
        }

        private bool Greater(int i, int j)
        {
            if (comparator == null)
            {
                return Comparer<Key>.Default.Compare(pq[i], pq[j]) > 0;
            }
            else
            {
                return comparator.Compare(pq[i], pq[j]) > 0;
            }
        }

        private void Exchange(int i, int j)
        {
            Key swap = pq[i];
            pq[i] = pq[j];
            pq[j] = swap;
        }
        public bool isMinHeap()
        {
            for (int i = 1; i <= n; i++)
            {
                if (pq[i] == null) return false;
            }
            for (int i = n + 1; i < pq.Length; i++)
            {
                if (pq[i] != null) return false;
            }
            if (pq[0] != null) return false;
            return isMinHeapOrdered(1);
        }
        public bool isMinHeapOrdered(int k)
        {
            if (k > n) return true;
            int left = 2 * k;
            int right = 2 * k + 1;
            if (left <= n && Greater(k, left)) return false;
            if (right <= n && Greater(k, right)) return false;
            return isMinHeapOrdered(left) && isMinHeapOrdered(right);
        }

        public static void Example()
        {
            long startTime = DateTime.Now.Ticks;

            string inputName = "tinyPQ.txt";
            string exercise = "MinPQ";

            StreamWriter fw = new StreamWriter($"../../DataStructures/IO/Output/{exercise}_{inputName}");
            fw.Write($"Questo è il risultato dell'esercizio {exercise} con input {inputName} :");
            fw.WriteLine();

            using (StreamReader reader = new StreamReader($"../../DataStructures/IO/Input/{inputName}"))
            {
                string line = reader.ReadLine()?.Trim();
                List<string> lista = new List<string>();
                while (line != null)
                {
                    int n = line.Trim().Split(' ').Length;
                    for (int i = 0; i < n; i++)
                    {
                        lista.Add(line.Split(' ')[i]);
                    }
                    line = reader.ReadLine();
                }

                MinPQ<string> pq = new MinPQ<string>();

                for (int i = 0; i < lista.Count; i++)
                {
                    string a = lista[i];
                    if (a != "-")
                        pq.Insert(a);
                    else if (!pq.IsEmpty())
                        fw.Write(pq.DelMin() + " ");
                }

                fw.WriteLine();
                fw.Write($"({pq.Size()} left on pq)");

                fw.WriteLine();
                long endTime = DateTime.Now.Ticks;
                long timeElapsed = endTime - startTime;
                fw.Write($"Execution time in ticks: {timeElapsed}");
                fw.WriteLine();
                fw.Write($"Execution time in milliseconds: {timeElapsed / TimeSpan.TicksPerMillisecond}");
                fw.WriteLine();
                fw.Write($"Execution time in seconds: {timeElapsed / TimeSpan.TicksPerSecond}");
                fw.WriteLine();
            }
            fw.Close();
        }
    }
}

