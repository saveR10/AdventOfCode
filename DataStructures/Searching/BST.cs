using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace AOC.DataStructures.Searching
{
    public class BST<Key, Value> where Key : IComparable<Key>
    {
        // root of BST
        private Node root;

        private class Node
        {
            public Key key;// sorted by key
            public Value val;// associated data
            public Node left, right; // left and right subtrees
            public int size;// number of nodes in subtree

            public Node(Key key, Value val, int size)
            {
                this.key = key;
                this.val = val;
                this.size = size;
            }
        }

        public BST()
        {
        }

        public bool IsEmpty()
        {
            return Size() == 0;
        }

        public int Size()
        {
            return Size(root);
        }

        private int Size(Node x)
        {
            if (x == null) return 0;
            else return x.size;
        }

        public bool Contains(Key key)
        {
            if (key == null) throw new ArgumentException("argument to contains() is null");
            return Get(key) != null;
        }

        public Value Get(Key key)
        {
            return Get(root, key);
        }

        private Value Get(Node x, Key key)
        {
            if (key == null) throw new ArgumentException("calls get() with a null key");
            if (x == null) return default(Value);
            int cmp = key.CompareTo(x.key);
            if (cmp < 0) return Get(x.left, key);
            else if (cmp > 0) return Get(x.right, key);
            else return x.val;
        }

        public void Put(Key key, Value val)
        {
            if (key == null) throw new ArgumentException("calls put() with a null key");
            if (val == null)
            {
                Delete(key);
                return;
            }
            root = Put(root, key, val);
            //Assert Check();
        }

        private Node Put(Node x, Key key, Value val)
        {
            if (x == null) return new Node(key, val, 1);
            int cmp = key.CompareTo(x.key);
            if (cmp < 0) x.left = Put(x.left, key, val);
            else if (cmp > 0) x.right = Put(x.right, key, val);
            else x.val = val;
            x.size = 1 + Size(x.left) + Size(x.right);
            return x;
        }

        /// <summary>
        /// Removes the smallest key and associated value from the symbol table.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void DeleteMin()
        {
            if (IsEmpty()) throw new InvalidOperationException("Symbol table underflow");
            root = DeleteMin(root);
            //Assert check();
        }

        private Node DeleteMin(Node x)
        {
            if (x.left == null) return x.right;
            x.left = DeleteMin(x.left);
            x.size = Size(x.left) + Size(x.right) + 1;
            return x;
        }

        public void DeleteMax()
        {
            if (IsEmpty()) throw new InvalidOperationException("Symbol table underflow");
            root = DeleteMax(root);
            //Assert check();
        }

        private Node DeleteMax(Node x)
        {
            if (x.right == null) return x.left;
            x.right = DeleteMax(x.right);
            x.size = Size(x.left) + Size(x.right) + 1;
            return x;
        }

        public void Delete(Key key)
        {
            if (key == null) throw new ArgumentException("calls delete() with a null key");
            root = Delete(root, key);
            //Assert check();
        }

        private Node Delete(Node x, Key key)
        {
            if (x == null) return null;

            int cmp = key.CompareTo(x.key);
            if (cmp < 0) x.left = Delete(x.left, key);
            else if (cmp > 0) x.right = Delete(x.right, key);
            else
            {
                if (x.right == null) return x.left;
                if (x.left == null) return x.right;
                Node t = x;
                x = Min(t.right);
                x.right = DeleteMin(t.right);
                x.left = t.left;
            }
            x.size = Size(x.left) + Size(x.right) + 1;
            return x;
        }

        public Key Min()
        {
            if (IsEmpty()) throw new InvalidOperationException("calls min() with empty symbol table");
            return Min(root).key;
        }

        private Node Min(Node x)
        {
            if (x.left == null) return x;
            else return Min(x.left);
        }

        public Key Max()
        {
            if (IsEmpty()) throw new InvalidOperationException("calls max() with empty symbol table");
            return Max(root).key;
        }

        private Node Max(Node x)
        {
            if (x.right == null) return x;
            else return Max(x.right);
        }

        public Key Floor(Key key)
        {
            if (key == null) throw new ArgumentException("argument to floor() is null");
            if (IsEmpty()) throw new InvalidOperationException("calls floor() with empty symbol table");
            Node x = Floor(root, key);
            if (x == null) throw new InvalidOperationException("argument to floor() is too small");
            else return x.key;
        }

        private Node Floor(Node x, Key key)
        {
            if (x == null) return null;
            int cmp = key.CompareTo(x.key);
            if (cmp == 0) return x;
            if (cmp < 0) return Floor(x.left, key);
            Node t = Floor(x.right, key);
            if (t != null) return t;
            else return x;
        }

        public Key Floor2(Key key)
        {
            Key x = Floor2(root, key, default(Key));
            if (x == null) throw new InvalidOperationException("argument to floor() is too small");
            else return x;
        }

        private Key Floor2(Node x, Key key, Key best)
        {
            if (x == null) return best;
            int cmp = key.CompareTo(x.key);
            if (cmp < 0) return Floor2(x.left, key, best);
            else if (cmp > 0) return Floor2(x.right, key, x.key);
            else return x.key;
        }

        public Key Ceiling(Key key)
        {
            if (key == null) throw new ArgumentException("argument to ceiling() is null");
            if (IsEmpty()) throw new InvalidOperationException("calls ceiling() with empty symbol table");
            Node x = Ceiling(root, key);
            if (x == null) throw new InvalidOperationException("argument to ceiling() is too large");
            else return x.key;
        }

        private Node Ceiling(Node x, Key key)
        {
            if (x == null) return null;
            int cmp = key.CompareTo(x.key);
            if (cmp == 0) return x;
            if (cmp < 0)
            {
                Node t = Ceiling(x.left, key);
                if (t != null) return t;
                else return x;
            }
            return Ceiling(x.right, key);
        }

        public Key Select(int rank)
        {
            if (rank < 0 || rank >= Size()) throw new ArgumentException("argument to select() is invalid: " + rank);
            return Select(root, rank);
        }

        private Key Select(Node x, int rank)
        {
            if (x == null) return default(Key);
            int leftSize = Size(x.left);
            if (leftSize > rank) return Select(x.left, rank);
            else if (leftSize < rank) return Select(x.right, rank - leftSize - 1);
            else return x.key;
        }

        public int Rank(Key key)
        {
            if (key == null) throw new ArgumentException("argument to rank() is null");
            return Rank(key, root);
        }

        private int Rank(Key key, Node x)
        {
            if (x == null) return 0;
            int cmp = key.CompareTo(x.key);
            if (cmp < 0) return Rank(key, x.left);
            else if (cmp > 0) return 1 + Size(x.left) + Rank(key, x.right);
            else return Size(x.left);
        }

        public IEnumerable<Key> Keys()
        {
            if (IsEmpty()) return new Queue<Key>();
            return Keys(Min(), Max());
        }

        public IEnumerable<Key> Keys(Key lo, Key hi)
        {
            if (lo == null) throw new ArgumentException("first argument to keys() is null");
            if (hi == null) throw new ArgumentException("second argument to keys() is null");

            Queue<Key> queue = new Queue<Key>();
            Keys(root, queue, lo, hi);
            return queue;
        }

        private void Keys(Node x, Queue<Key> queue, Key lo, Key hi)
        {
            if (x == null) return;
            int cmplo = lo.CompareTo(x.key);
            int cmphi = hi.CompareTo(x.key);
            if (cmplo < 0) Keys(x.left, queue, lo, hi);
            if (cmplo <= 0 && cmphi >= 0) queue.Enqueue(x.key);
            if (cmphi > 0) Keys(x.right, queue, lo, hi);
        }

        public int Size(Key lo, Key hi)
        {
            if (lo == null) throw new ArgumentException("first argument to size() is null");
            if (hi == null) throw new ArgumentException("second argument to size() is null");

            if (lo.CompareTo(hi) > 0) return 0;
            if (Contains(hi)) return Rank(hi) - Rank(lo) + 1;
            else return Rank(hi) - Rank(lo);
        }

        public int Height()
        {
            return Height(root);
        }

        private int Height(Node x)
        {
            if (x == null) return -1;
            return 1 + Math.Max(Height(x.left), Height(x.right));
        }

        public IEnumerable<Key> LevelOrder()
        {
            Queue<Key> keys = new Queue<Key>();
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                Node x = queue.Dequeue();
                if (x == null) continue;
                keys.Enqueue(x.key);
                queue.Enqueue(x.left);
                queue.Enqueue(x.right);
            }
            return keys;
        }

        private bool Check()
        {
            if (!IsBST()) Console.WriteLine("Not in symmetric order");
            if (!IsSizeConsistent()) Console.WriteLine("Subtree counts not consistent");
            if (!IsRankConsistent()) Console.WriteLine("Ranks not consistent");
            return IsBST() && IsSizeConsistent() && IsRankConsistent();
        }

        private bool IsBST()
        {
            return IsBST(root, default(Key), default(Key));
        }

        private bool IsBST(Node x, Key min, Key max)
        {
            if (x == null) return true;
            if (min != null && x.key.CompareTo(min) <= 0) return false;
            if (max != null && x.key.CompareTo(max) >= 0) return false;
            return IsBST(x.left, min, x.key) && IsBST(x.right, x.key, max);
        }

        private bool IsSizeConsistent() { return IsSizeConsistent(root); }
        private bool IsSizeConsistent(Node x)
        {
            if (x == null) return true;
            if (x.size != Size(x.left) + Size(x.right) + 1) return false;
            return IsSizeConsistent(x.left) && IsSizeConsistent(x.right);
        }

        private bool IsRankConsistent()
        {
            for (int i = 0; i < Size(); i++)
                if (i != Rank(Select(i))) return false;
            foreach (Key key in Keys())
                if (key.CompareTo(Select(Rank(key))) != 0) return false;
            return true;
        }

        public static void Example()
        {
            long startTime = DateTime.Now.Ticks;

            string inputName = "tinyST.txt";
            string exercise = "BST";

            // Connect to the file; create it if necessary.
            using (StreamWriter writer = new StreamWriter($"../../DataStructures/IO/Output/{exercise}_{inputName}"))
            {
                writer.WriteLine($"This is the result of exercise {exercise} with input {inputName}:");

                List<string> lista = new List<string>();
                using (StreamReader reader = new StreamReader($"../../DataStructures/IO/Input/{inputName}"))
                {
                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        string[] items = line.Trim().Split(' ');
                        foreach (string item in items)
                        {
                            lista.Add(item);
                        }
                        line = reader.ReadLine();
                    }
                }

                BST<string, int> st = new BST<string, int>();
                for (int i = 0; i < lista.Count; i++)
                {
                    string key = lista[i];
                    st.Put(key, i);
                }

                foreach (string s in st.LevelOrder())
                {
                    writer.WriteLine($"{s} {st.Get(s)}");
                }

                writer.WriteLine();

                foreach (string s in st.Keys())
                {
                    writer.WriteLine($"{s} {st.Get(s)}");
                }

                writer.WriteLine();

                long endTime = DateTime.Now.Ticks;
                long timeElapsed = endTime - startTime;
                writer.WriteLine($"Execution time in ticks: {timeElapsed}");
                writer.WriteLine($"Execution time in milliseconds: {timeElapsed / TimeSpan.TicksPerMillisecond}");
                writer.WriteLine($"Execution time in seconds: {timeElapsed / TimeSpan.TicksPerSecond}");
            }
        }
    }
}
