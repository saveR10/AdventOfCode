using System;
using System.Collections.Generic;
using System.IO;

namespace AOC.DataStructures.PriorityQueue
{
    public class MaxPQ<Key>
    {
        private Key[] pq;                    // store items at indices 1 to n
        private int n;                       // number of items on priority queue
        private Comparer<Key> comparator;    // optional comparator

        public MaxPQ(int initCapacity)
        {
            pq = new Key[initCapacity + 1];
            n = 0;
        }

        public MaxPQ() : this(1)
        {
        }

        public MaxPQ(int initCapacity, Comparer<Key> comparator)
        {
            this.comparator = comparator;
            pq = new Key[initCapacity + 1];
            n = 0;
        }

        public MaxPQ(Comparer<Key> comparator) : this(1, comparator)
        {
        }

        public MaxPQ(Key[] keys)
        {
            n = keys.Length;
            pq = new Key[keys.Length + 1];
            Array.Copy(keys, 0, pq, 1, keys.Length);
            for (int k = n / 2; k >= 1; k--)
                Sink(k);
            if (!IsMaxHeap())
                throw new InvalidOperationException("Heap construction failed");
        }

        public bool IsEmpty()
        {
            return n == 0;
        }

        public int Size()
        {
            return n;
        }

        public Key Max()
        {
            if (IsEmpty()) throw new InvalidOperationException("Priority queue underflow");
            return pq[1];
        }

        private void Resize(int capacity)
        {
            if (capacity <= n)
                throw new ArgumentException("New capacity is smaller than current size");

            Key[] temp = new Key[capacity];
            Array.Copy(pq, 1, temp, 1, n);
            pq = temp;
        }

        public void Insert(Key x)
        {
            if (n == pq.Length - 1)
                Resize(2 * pq.Length);

            pq[++n] = x;
            Swim(n);
            if (!IsMaxHeap())
                throw new InvalidOperationException("Heap invariant violated");
        }

        public Key DelMax()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Priority queue underflow");

            Key max = pq[1];
            Exchange(1, n--);
            Sink(1);
            pq[n + 1] = default(Key); // To avoid loitering and help with garbage collection

            if (n > 0 && n == (pq.Length - 1) / 4)
                Resize(pq.Length / 2);

            if (!IsMaxHeap())
                throw new InvalidOperationException("Heap invariant violated");

            return max;
        }

        private void Swim(int k)
        {
            while (k > 1 && Less(k / 2, k))
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
                if (j < n && Less(j, j + 1))
                    j++;
                if (!Less(k, j))
                    break;
                Exchange(k, j);
                k = j;
            }
        }

        private bool Less(int i, int j)
        {
            if (comparator == null)
                return Comparer<Key>.Default.Compare(pq[i], pq[j]) < 0;
            else
                return comparator.Compare(pq[i], pq[j]) < 0;
        }

        private void Exchange(int i, int j)
        {
            Key temp = pq[i];
            pq[i] = pq[j];
            pq[j] = temp;
        }

        private bool IsMaxHeap()
        {
            for (int i = 1; i <= n; i++)
            {
                if (pq[i] == null)
                    return false;
            }
            for (int i = n + 1; i < pq.Length; i++)
            {
                if (pq[i] != null)
                    return false;
            }
            if (pq[0] != null)
                return false;

            return IsMaxHeapOrdered(1);
        }

        private bool IsMaxHeapOrdered(int k)
        {
            if (k > n)
                return true;

            int left = 2 * k;
            int right = 2 * k + 1;
            if (left <= n && Less(k, left))
                return false;
            if (right <= n && Less(k, right))
                return false;

            return IsMaxHeapOrdered(left) && IsMaxHeapOrdered(right);
        }

        public static void Example()
        {
            long startTime = DateTime.Now.Ticks;

            // Initialization of variables
            string input_name = "tinyPQ.txt";
            string exercise = "MaxPQ";

            // Connect to the file; create it if necessary. Attach a buffer to the file. Use the buffer to write to the file
            using (StreamWriter sw = new StreamWriter($"../../DataStructures/IO/Output/{exercise}_{input_name}"))
            {
                sw.WriteLine($"This is the result of exercise {exercise} with input {input_name}:");
                sw.WriteLine();

                using (StreamReader reader = new StreamReader($"../../DataStructures/IO/Input/{input_name}"))
                {
                    string line = reader.ReadLine()?.Trim();
                    List<string> lista = new List<string>();
                    while (!string.IsNullOrEmpty(line))
                    {
                        int n = line.Trim().Split(' ').Length;
                        for (int i = 0; i < n; i++)
                        {
                            lista.Add(line.Split(' ')[i]);
                        }
                        line = reader.ReadLine();
                    }
                    MaxPQ<string> pq = new MaxPQ<string>();

                    foreach (string a in lista)
                    {
                        if (a != "-")
                            pq.Insert(a);
                        else if (!pq.IsEmpty())
                            sw.Write(pq.DelMax() + " ");
                    }
                    sw.WriteLine();
                    sw.WriteLine($"({pq.Size()} left on pq)");

                    sw.WriteLine();
                    long endTime = DateTime.Now.Ticks;
                    long timeElapsed = endTime - startTime;
                    sw.WriteLine($"Execution time in ticks: {timeElapsed}");
                    sw.WriteLine($"Execution time in milliseconds: {timeElapsed / TimeSpan.TicksPerMillisecond}");
                    sw.WriteLine($"Execution time in seconds: {timeElapsed / TimeSpan.TicksPerSecond}");
                }
            }
        }
    }
}
