using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.DataStructures.Searching
{
    public class SeparateChainingHashST<Key, Value>
    {
        private const int INIT_CAPACITY = 4;

        private int n; // number of key-value pairs
        private int m; // hash table size
        private SequentialSearchST<Key, Value>[] st; // array of linked-list symbol tables

        public SeparateChainingHashST()
        {
            this.m = INIT_CAPACITY;
            this.st = new SequentialSearchST<Key, Value>[m];
            for (int i = 0; i < m; i++)
                st[i] = new SequentialSearchST<Key, Value>();
        }

        public SeparateChainingHashST(int m)
        {
            this.m = m;
            this.st = new SequentialSearchST<Key, Value>[m];
            for (int i = 0; i < m; i++)
                st[i] = new SequentialSearchST<Key, Value>();
        }

        private void Resize(int chains)
        {
            var temp = new SeparateChainingHashST<Key, Value>(chains);
            for (int i = 0; i < m; i++)
            {
                foreach (Key key in st[i].Keys())
                {
                    temp.Put(key, st[i].Get(key));
                }
            }
            this.m = temp.m;
            this.n = temp.n;
            this.st = temp.st;
        }

        private int Hash(Key key)
        {
            int h = key.GetHashCode();
            h ^= (h >> 20) ^ (h >> 12) ^ (h >> 7) ^ (h >> 4);
            return h & (m - 1);
        }

        public int Size()
        {
            return n;
        }

        public bool IsEmpty()
        {
            return Size() == 0;
        }

        public bool Contains(Key key)
        {
            if (key == null) throw new ArgumentException("argument to contains() is null");
            return Get(key) != null;
        }

        public Value Get(Key key)
        {
            if (key == null) throw new ArgumentException("argument to get() is null");
            int i = Hash(key);
            return st[i].Get(key);
        }

        public void Put(Key key, Value val)
        {
            if (key == null) throw new ArgumentException("first argument to put() is null");
            if (val == null)
            {
                Delete(key);
                return;
            }

            // double table size if average length of list >= 10
            if (n >= 10 * m) Resize(2 * m);

            int i = Hash(key);
            if (!st[i].Contains(key)) n++;
            st[i].Put(key, val);
        }

        public void Delete(Key key)
        {
            if (key == null) throw new ArgumentException("argument to delete() is null");

            int i = Hash(key);
            if (st[i].Contains(key)) n--;
            st[i].Delete(key);

            // halve table size if average length of list <= 2
            if (m > INIT_CAPACITY && n <= 2 * m) Resize(m / 2);
        }

        // return keys in symbol table as an Iterable
        public IEnumerable<Key> Keys()
        {
            Queue<Key> queue = new Queue<Key>();
            for (int i = 0; i < m; i++)
            {
                foreach (Key key in st[i].Keys())
                    queue.Enqueue(key);
            }
            return queue;
        }

        public static void Example()
        {
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            string input_name = "tinyST.txt";
            string exercise = "SeparateChainingHashST";


            StreamWriter fw = new StreamWriter($"../../DataStructures/IO/Output/{exercise}_{input_name}");
            fw.WriteLine($"Questo è il risultato dell'esercizio {exercise} con input {input_name} :");
            fw.WriteLine();

            using (StreamReader reader = new StreamReader($"../../DataStructures/IO/Input/{input_name}"))
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

                SeparateChainingHashST<string, int> st = new SeparateChainingHashST<string, int>();
                for (int i = 0; i < lista.Count; i++)
                {
                    string key = lista[i];
                    st.Put(key, i);
                }

                foreach (string s in st.Keys())
                {
                    fw.WriteLine($"{s} {st.Get(s)}");
                }
            }

            fw.WriteLine();
            long endTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            long timeElapsed = endTime - startTime;
            fw.WriteLine($"Execution time in milliseconds: {timeElapsed}");
            fw.WriteLine("Execution time in seconds: " + (timeElapsed / 1000));
            fw.Close();
        }
    }
}
