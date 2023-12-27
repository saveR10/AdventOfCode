using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AOC.DataStructures.Searching
{
    public class LinearProbingHashST<Key, Value>
    {
        private const int INIT_CAPACITY = 4;
        private int n;           // number of key-value pairs in the symbol table
        private int m;           // size of linear probing table
        private Key[] keys;      // the keys
        private Value[] vals;    // the values

        public LinearProbingHashST(int capacity)
        {
            m = capacity;
            n = 0;
            keys = new Key[m];
            vals = new Value[m];
        }
        public LinearProbingHashST()
        {
            m = INIT_CAPACITY;
            n = 0;
            keys = new Key[m];
            vals = new Value[m];
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
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "argument to contains() is null");
            }
            return Get(key) != null;
        }
        private int Hash(Key key)
        {
            int h = key.GetHashCode();
            h ^= (h >> 20) ^ (h >> 12) ^ (h >> 7) ^ (h >> 4);
            return h & (m - 1);
        }

        private void Resize(int capacity)
        {
            LinearProbingHashST<Key, Value> temp = new LinearProbingHashST<Key, Value>(capacity);
            for (int i = 0; i < m; i++)
            {
                if (keys[i] != null)
                {
                    temp.Put(keys[i], vals[i]);
                }
            }
            keys = temp.keys;
            vals = temp.vals;
            m = temp.m;
        }

        public void Put(Key key, Value val)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "first argument to put() is null");
            }

            if (val == null)
            {
                Delete(key);
                return;
            }

            if (n >= m / 2)
            {
                Resize(2 * m);
            }

            int i;
            for (i = Hash(key); keys[i] != null; i = (i + 1) % m)
            {
                if (keys[i].Equals(key))
                {
                    vals[i] = val;
                    return;
                }
            }
            keys[i] = key;
            vals[i] = val;
            n++;
        }

        public Value Get(Key key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "argument to get() is null");
            }
            for (int i = Hash(key); keys[i] != null; i = (i + 1) % m)
            {
                if (keys[i].Equals(key))
                {
                    return vals[i];
                }
            }
            return default(Value);
        }

        public void Delete(Key key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "argument to delete() is null");
            }
            if (!Contains(key))
            {
                return;
            }
            // find position i of key
            int i = Hash(key);
            while (!key.Equals(keys[i]))
            {
                i = (i + 1) % m;
            }

            // delete key and associated value
            keys[i] = default(Key);
            vals[i] = default(Value);

            // rehash all keys in same cluster
            i = (i + 1) % m;
            while (keys[i] != null)
            {
                // delete keys[i] and vals[i] and reinsert
                Key keyToRehash = keys[i];
                Value valToRehash = vals[i];
                keys[i] = default(Key);
                vals[i] = default(Value);
                n--;
                Put(keyToRehash, valToRehash);
                i = (i + 1) % m;
            }

            n--;

            // halves size of array if it's 12.5% full or less
            if (n > 0 && n <= m / 8)
            {
                Resize(m / 2);
            }

            //Assert Check();
        }

        public IEnumerable<Key> Keys()
        {
            Queue<Key> queue = new Queue<Key>();
            for (int i = 0; i < m; i++)
            {
                if (keys[i] != null)
                {
                    queue.Enqueue(keys[i]);
                }
            }
            return queue;
        }

        private bool Check()
        {
            // check that hash table is at most 50% full
            if (m < 2 * n)
            {
                Console.Error.WriteLine($"Hash table size m = {m}; array size n = {n}");
                return false;
            }

            // check that each key in table can be found by get()
            for (int i = 0; i < m; i++)
            {
                if (keys[i] == null)
                {
                    continue;
                }
                else if (!Get(keys[i]).Equals(vals[i]))
                {
                    Console.Error.WriteLine($"get[{keys[i]}] = {Get(keys[i])}; vals[i] = {vals[i]}");
                    return false;
                }
            }
            return true;
        }

        public static void Example()
        {
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            // Initialization of variables
            string inputName = "tinyST.txt";
            string exercise = "LinearProbingHashST";

            // Connect to the file; create it if necessary. Attach a buffer to the file. Use the buffer to write to the file
            string filePath = $"../../DataStructures/IO/Output/{exercise}_{inputName}";
            using (StreamWriter fileWriter = new StreamWriter(filePath))
            {
                fileWriter.WriteLine($"Questo è il risultato dell'esercizio {exercise} con input {inputName} :");

                // Read from the input file and process the data
                string inputFilePath = $"../../DataStructures/IO/Input/{inputName}";
                List<string> lista = new List<string>();
                using (StreamReader fileReader = new StreamReader(inputFilePath))
                {
                    string line = fileReader.ReadLine()?.Trim();
                    while (line != null)
                    {
                        int n = line.Trim().Split(' ').Length;
                        for (int i = 0; i < n; i++)
                        {
                            lista.Add(line.Split(' ')[i]);
                        }
                        line = fileReader.ReadLine();
                    }
                }

                LinearProbingHashST<string, int> st = new LinearProbingHashST<string, int>();
                for (int i = 0; i < lista.Count; i++)
                {
                    string key = lista[i];
                    st.Put(key, i);
                }

                foreach (string s in st.Keys())
                {
                    fileWriter.WriteLine(s + " " + st.Get(s));
                }

                fileWriter.WriteLine();
                long endTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                long timeElapsed = endTime - startTime;
                fileWriter.WriteLine("Execution time in milliseconds: " + timeElapsed);
                fileWriter.WriteLine("Execution time in seconds: " + (timeElapsed / 1000));
            }
        }
    }
}
