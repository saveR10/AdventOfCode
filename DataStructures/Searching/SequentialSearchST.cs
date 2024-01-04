using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.DataStructures.Searching
{
    public class SequentialSearchST<Key, Value>
    {
        private int n; // number of key-value pairs
        private Node first; // the linked list of key-value pairs

        private class Node
        {
            public Key key;
            public Value val;
            public Node next;

            public Node(Key key, Value val, Node next)
            {
                this.key = key;
                this.val = val;
                this.next = next;
            }
        }

        public SequentialSearchST()
        {
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
            for (Node x = first; x != null; x = x.next)
            {
                if (key.Equals(x.key))
                    return x.val;
            }
            return default(Value);
        }

        public void Put(Key key, Value val)
        {
            if (key == null) throw new ArgumentException("first argument to put() is null");
            if (val == null)
            {
                Delete(key);
                return;
            }

            for (Node x = first; x != null; x = x.next)
            {
                if (key.Equals(x.key))
                {
                    x.val = val;
                    return;
                }
            }
            first = new Node(key, val, first);
            n++;
        }

        public void Delete(Key key)
        {
            if (key == null) throw new ArgumentException("argument to delete() is null");
            first = Delete(first, key);
        }

        private Node Delete(Node x, Key key)
        {
            if (x == null) return null;
            if (key.Equals(x.key))
            {
                n--;
                return x.next;
            }
            x.next = Delete(x.next, key);
            return x;
        }

        public IEnumerable<Key> Keys()
        {
            Queue<Key> queue = new Queue<Key>();
            for (Node x = first; x != null; x = x.next)
                queue.Enqueue(x.key);
            return queue;
        }

        public static void Example()
        {
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            string input_name = "tinyST.txt";
            string exercise = "SequentialSearchST";

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

                SequentialSearchST<string, int> st = new SequentialSearchST<string, int>();
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
