using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.DataStructures.Searching
{
    public class ST<Key, Value> : IEnumerable<Key> where Key : IComparable<Key>
    {
        private SortedDictionary<Key, Value> st;

        public ST()
        {
            st = new SortedDictionary<Key, Value>();
        }

        public Value Get(Key key)
        {
            if (key == null) throw new ArgumentException("calls Get() with null key");
            return st.ContainsKey(key) ? st[key] : default(Value);
        }

        public void Put(Key key, Value val)
        {
            if (key == null) throw new ArgumentException("calls Put() with null key");
            if (val == null) st.Remove(key);
            else st[key] = val;
        }

        public void Delete(Key key)
        {
            if (key == null) throw new ArgumentException("calls Delete() with null key");
            st.Remove(key);
        }

        public void Remove(Key key)
        {
            if (key == null) throw new ArgumentException("calls Remove() with null key");
            st.Remove(key);
        }

        public bool Contains(Key key)
        {
            if (key == null) throw new ArgumentException("calls Contains() with null key");
            return st.ContainsKey(key);
        }

        public int Size()
        {
            return st.Count;
        }

        public bool IsEmpty()
        {
            return Size() == 0;
        }

        public IEnumerable<Key> Keys()
        {
            return st.Keys;
        }

        public IEnumerator<Key> GetEnumerator()
        {
            return st.Keys.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Key Min()
        {
            if (IsEmpty()) throw new Exception("calls Min() with empty symbol table");
            return st.First().Key;
        }

        public Key Max()
        {
            if (IsEmpty()) throw new Exception("calls Max() with empty symbol table");
            return st.Last().Key;
        }
        public Key Ceiling(Key key)
        {
            if (key == null) throw new ArgumentException("argument to Ceiling() is null");

            foreach (var kvp in st)
            {
                if (kvp.Key.CompareTo(key) >= 0)
                {
                    return kvp.Key;
                }
            }

            throw new Exception("argument to Ceiling() is too large");
        }

        public Key Floor(Key key)
        {
            if (key == null) throw new ArgumentException("argument to Floor() is null");

            Key floorKey = default(Key);
            foreach (var kvp in st)
            {
                if (kvp.Key.CompareTo(key) > 0)
                {
                    return floorKey;
                }
                floorKey = kvp.Key;
            }

            throw new Exception("argument to Floor() is too small");
        }


        public static void Example()
        {
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            string input_name = "tinyST.txt";
            string exercise = "ST";

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

                ST<string, int> st = new ST<string, int>();
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

            fw.Close();
        }
    }
}
