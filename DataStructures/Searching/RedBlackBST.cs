using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
 
using System.Collections;
using System.Runtime.Remoting.Messaging;

namespace AOC.DataStructures.Searching
{
    public class RedBlackBST<Key, Value> where Key : IComparable<Key>
    {
        private const bool RED = true;
        private const bool BLACK = false;

        private Node root; // root of the BST

        private class Node
        {
            public Key key; // key
            public Value val; // associated data
            public Node Left, Right; // links to Left and Right subtrees
            public bool Color;  // Color of parent link
            public int Size;  // subtree count

            public Node(Key key, Value val, bool Color, int Size)
            {
                this.key = key;
                this.val = val;
                this.Color = Color;
                this.Size = Size;
            }
        }

        public RedBlackBST()
        {
            // Constructor logic (if needed)
        }

        private bool IsRed(Node x)
        {
            if (x == null) return false;
            return x.Color == RED;
        }

        private int Size(Node x)
        {
            if (x == null) return 0;
            return x.Size;
        }

        public int Size()
        {
            return Size(root);
        }

        public bool IsEmpty()
        {
            return root == null;
        }

        public Value Get(Key key)
        {
            if (key == null) throw new ArgumentException("argument to get() is null");
            return Get(root, key);
        }

        // value associated with the given key in subtree rooted at x; null if no such key
        private Value Get(Node x, Key key)
        {
            while (x != null)
            {
                int cmp = key.CompareTo(x.key);
                if (cmp < 0) x = x.Left;
                else if (cmp > 0) x = x.Right;
                else return x.val;
            }
            return default; // Return default value for Value type (null for reference types)
        }

        public void Put(Key key, Value val)
        {
            if (key == null) throw new ArgumentException("first argument to put() is null");
            if (val == null)
            {
                Delete(key);
                return;
            }

            root = Put(root, key, val);
            root.Color = BLACK;
            // assert check();
        }
        // insert the key-value pair in the subtree rooted at h
        private Node Put(Node h, Key key, Value val)
        {
            if (h == null) return new Node(key, val, RED, 1);

            int cmp = key.CompareTo(h.key);
            if (cmp < 0) h.Left = Put(h.Left, key, val);
            else if (cmp > 0) h.Right = Put(h.Right, key, val);
            else h.val = val;

            // fix-up any Right-leaning links
            if (IsRed(h.Right) && !IsRed(h.Left)) h = RotateLeft(h);
            if (IsRed(h.Left) && IsRed(h.Left.Left)) h = RotateRight(h);
            if (IsRed(h.Left) && IsRed(h.Right)) FlipColors(h);
            h.Size = Size(h.Left) + Size(h.Right) + 1;

            return h;
        }

        public void DeleteMin()
        {
            if (IsEmpty()) throw new InvalidOperationException("BST underflow");

            if (!IsRed(root.Left) && !IsRed(root.Right))
                root.Color = RED;

            root = DeleteMin(root);
            if (!IsEmpty()) root.Color = BLACK;
        }
        private Node DeleteMin(Node h)
        {
            if (h.Left == null)
                return null;

            if (!IsRed(h.Left) && !IsRed(h.Left.Left))
                h = MoveRedLeft(h);

            h.Left = DeleteMin(h.Left);
            return Balance(h);
        }
        public void DeleteMax()
        {
            if (IsEmpty())
            {
                // Handle the case when the tree is empty
                throw new InvalidOperationException("BST underflow: Tree is empty.");
            }

            // if both children of root are black, set root to red
            if (!IsRed(root.Left) && !IsRed(root.Right))
                root.Color = RED;

            root = DeleteMax(root);
            if (!IsEmpty()) root.Color = BLACK;
            // assert check();
        }
        private Node DeleteMax(Node h)
        {
            if (IsRed(h.Left))
                h = RotateRight(h);

            if (h.Right == null)
                return null;

            if (!IsRed(h.Right) && !IsRed(h.Right.Left))
                h = MoveRedRight(h);

            h.Right = DeleteMax(h.Right);

            return Balance(h);
        }

        public void Delete(Key key)
        {
            if (key == null) throw new ArgumentException("Argument to delete() is null");
            if (!Contains(key)) return;

            // If both children of root are black, set root to red
            if (!IsRed(root.Left) && !IsRed(root.Right))
                root.Color = RED;

            root = Delete(root, key);
            if (!IsEmpty()) root.Color = BLACK;
            // assert check();
        }

        private Node Delete(Node h, Key key)
        {
            // assert get(h, key) != null;

            if (key.CompareTo(h.key) < 0)
            {
                if (!IsRed(h.Left) && !IsRed(h.Left.Left))
                    h = MoveRedLeft(h);
                h.Left = Delete(h.Left, key);
            }
            else
            {
                if (IsRed(h.Left))
                    h = RotateRight(h);
                if (key.CompareTo(h.key) == 0 && (h.Right == null))
                    return null;
                if (!IsRed(h.Right) && !IsRed(h.Right.Left))
                    h = MoveRedRight(h);
                if (key.CompareTo(h.key) == 0)
                {
                    Node x = Min(h.Right);
                    h.key = x.key;
                    h.val = x.val;
                    h.Right = DeleteMin(h.Right);
                }
                else h.Right = Delete(h.Right, key);
            }
            return Balance(h);
        }
        private Node RotateRight(Node h)
        {
            Debug.Assert(h != null && IsRed(h.Left));
            // Debug.Assert(h != null && IsRed(h.Left) && !IsRed(h.Right));  // for insertion only
            Node x = h.Left;
            h.Left = x.Right;
            x.Right = h;
            x.Color = h.Color;
            h.Color = RED;
            x.Size = h.Size;
            h.Size = Size(h.Left) + Size(h.Right) + 1;
            return x;
        }
        // make a Right-leaning link lean to the Left
        private Node RotateLeft(Node h)
        {
            Debug.Assert(h != null && IsRed(h.Right));
            // Debug.Assert(h != null && IsRed(h.Right) && !IsRed(h.Left));  // for insertion only
            Node x = h.Right;
            h.Right = x.Left;
            x.Left = h;
            x.Color = h.Color;
            h.Color = RED;
            x.Size = h.Size;
            h.Size = Size(h.Left) + Size(h.Right) + 1;
            return x;
        }
        // flip the Colors of a node and its two children
        private void FlipColors(Node h)
        {
            h.Color = !h.Color;
            h.Left.Color = !h.Left.Color;
            h.Right.Color = !h.Right.Color;
        }
        private Node MoveRedRight(Node h)
        {
            // assert (h != null);
            // assert IsRed(h) && !IsRed(h.Right) && !IsRed(h.Right.Left);
            FlipColors(h);
            if (IsRed(h.Left.Left))
            {
                h = RotateRight(h);
                FlipColors(h);
            }
            return h;
        }

        private Node MoveRedLeft(Node h)
        {
            // assert (h != null);
            // assert IsRed(h) && !IsRed(h.Left) && !IsRed(h.Left.Left);

            FlipColors(h);
            if (IsRed(h.Right.Left))
            {
                h.Right = RotateRight(h.Right);
                h = RotateLeft(h);
                FlipColors(h);
            }
            return h;
        }

        // restore red-black tree invariant
        private Node Balance(Node h)
        {
            // assert (h != null);

            if (IsRed(h.Right) && !IsRed(h.Left)) h = RotateLeft(h);
            if (IsRed(h.Left) && IsRed(h.Left.Left)) h = RotateRight(h);
            if (IsRed(h.Left) && IsRed(h.Right)) FlipColors(h);

            h.Size = Size(h.Left) + Size(h.Right) + 1;
            return h;
        }

        public int Height()
        {
            return Height(root);
        }

        private int Height(Node x)
        {
            if (x == null) return -1;
            return 1 + Math.Max(Height(x.Left), Height(x.Right));
        }

        public Key Min()
        {
            if (IsEmpty())
            {
                // Handle the case when the tree is empty
                throw new InvalidOperationException("calls min() with empty symbol table");
            }
            return Min(root).key;
        }

        // The smallest key in subtree rooted at x; null if no such key
        private Node Min(Node x)
        {
            // assert x != null;
            if (x.Left == null) return x;
            else return Min(x.Left);
        }
        public Key Max()
        {
            if (IsEmpty())
            {
                // Handle the case when the tree is empty
                throw new InvalidOperationException("calls max() with empty symbol table");
            }
            return Max(root).key;
        }

        // The largest key in the subtree rooted at x; null if no such key
        private Node Max(Node x)
        {
            // assert x != null;
            if (x.Right == null) return x;
            else return Max(x.Right);
        }

        public Key Floor(Key key)
        {
            if (key == null) throw new ArgumentException("argument to floor() is null");
            if (IsEmpty())
            {
                // Handle the case when the tree is empty
                throw new InvalidOperationException("calls floor() with empty symbol table");
            }
            Node x = Floor(root, key);
            if (x == null)
            {
                // Handle the case when the tree is empty
                throw new InvalidOperationException("argument to floor() is too small");
            }
            else return x.key;
        }

        // The largest key in the subtree rooted at x less than or equal to the given key
        private Node Floor(Node x, Key key)
        {
            if (x == null) return null;
            int cmp = key.CompareTo(x.key);
            if (cmp == 0) return x;
            if (cmp < 0) return Floor(x.Left, key);
            Node t = Floor(x.Right, key);
            if (t != null) return t;
            else return x;
        }

        public Key Ceiling(Key key)
        {
            if (key == null) throw new ArgumentException("argument to ceiling() is null");
            if (IsEmpty())
            {
                // Handle the case when the tree is empty
                throw new InvalidOperationException("calls ceiling() with empty symbol table");
            }
            Node x = Ceiling(root, key);
            if (x==null)
            {
                // Handle the case when the tree is empty
                throw new InvalidOperationException("argument to ceiling() is too large");
            }
            else return x.key;
        }

        // The smallest key in the subtree rooted at x greater than or equal to the given key
        private Node Ceiling(Node x, Key key)
        {
            if (x == null) return null;
            int cmp = key.CompareTo(x.key);
            if (cmp == 0) return x;
            if (cmp > 0) return Ceiling(x.Right, key);
            Node t = Ceiling(x.Left, key);
            if (t != null) return t;
            else return x;
        }

        public Key Select(int rank)
        {
            if (rank < 0 || rank >= Size())
            {
                throw new ArgumentException("argument to select() is invalid: " + rank);
            }
            return Select(root, rank);
        }

        // Return key in BST rooted at x of given rank.
        // Precondition: rank is in legal range.
        private Key Select(Node x, int rank)
        {
            if (x == null) throw new Exception("Select not found any key");//return null;
            int LeftSize = Size(x.Left);
            if (LeftSize > rank) return Select(x.Left, rank);
            else if (LeftSize < rank) return Select(x.Right, rank - LeftSize - 1);
            else return x.key;
        }

        public int Rank(Key key)
        {
            if (key == null) throw new ArgumentException("argument to rank() is null");
            return Rank(key, root);
        }

        // number of keys less than key in the subtree rooted at x
        private int Rank(Key key, Node x)
        {
            if (x == null) return 0;
            int cmp = key.CompareTo(x.key);
            if (cmp < 0) return Rank(key, x.Left);
            else if (cmp > 0) return 1 + Size(x.Left) + Rank(key, x.Right);
            else return Size(x.Left);
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
            // if (IsEmpty() || lo.CompareTo(hi) > 0) return queue;
            Keys(root, queue, lo, hi);
            return queue;
        }

        // add the keys between lo and hi in the subtree rooted at x to the queue
        private void Keys(Node x, Queue<Key> queue, Key lo, Key hi)
        {
            if (x == null) return;
            int cmplo = lo.CompareTo(x.key);
            int cmphi = hi.CompareTo(x.key);
            if (cmplo < 0) Keys(x.Left, queue, lo, hi);
            if (cmplo <= 0 && cmphi >= 0) queue.Enqueue(x.key);
            if (cmphi > 0) Keys(x.Right, queue, lo, hi);
        }

        public int Size(Key lo, Key hi)
        {
            if (lo == null) throw new ArgumentException("first argument to Size() is null");
            if (hi == null) throw new ArgumentException("second argument to Size() is null");

            if (lo.CompareTo(hi) > 0) return 0;
            if (Contains(hi)) return Rank(hi) - Rank(lo) + 1;
            else return Rank(hi) - Rank(lo);
        }

        private bool Check()
        {
            if (!IsBST()) Console.WriteLine("Not in symmetric order");
            if (!IsSizeConsistent()) Console.WriteLine("Subtree counts not consistent");
            if (!IsRankConsistent()) Console.WriteLine("Ranks not consistent");
            if (!Is23()) Console.WriteLine("Not a 2-3 tree");
            if (!IsBalanced()) Console.WriteLine("Not balanced");
            return IsBST() && IsSizeConsistent() && IsRankConsistent() && Is23() && IsBalanced();
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
            return IsBST(x.Left, min, x.key) && IsBST(x.Right, x.key, max);
        }

        private bool IsSizeConsistent()
        {
            return IsSizeConsistent(root);
        }

        private bool IsSizeConsistent(Node x)
        {
            if (x == null) return true;
            if (x.Size != Size(x.Left) + Size(x.Right) + 1) return false;
            return IsSizeConsistent(x.Left) && IsSizeConsistent(x.Right);
        }
        private bool IsRankConsistent()
        {
            for (int i = 0; i < Size(); i++)
                if (i != Rank(Select(i))) return false;
            foreach (Key key in Keys())
                if (key.CompareTo(Select(Rank(key))) != 0) return false;
            return true;
        }

        private bool Is23()
        {
            return Is23(root);
        }

        private bool Is23(Node x)
        {
            if (x == null) return true;
            if (IsRed(x.Right)) return false;
            if (x != root && IsRed(x) && IsRed(x.Left))
                return false;
            return Is23(x.Left) && Is23(x.Right);
        }

        private bool IsBalanced()
        {
            int black = 0;     // number of black links on path from root to min
            Node x = root;
            while (x != null)
            {
                if (!IsRed(x)) black++;
                x = x.Left;
            }
            return IsBalanced(root, black);
        }
        // Does every path from the root to a leaf have the given number of black links?
        private bool IsBalanced(Node x, int black)
        {
            if (x == null) return black == 0;
            if (!IsRed(x)) black--;
            return IsBalanced(x.Left, black) && IsBalanced(x.Right, black);
        }

        public bool Contains(Key key)
        {
            return Get(key) != null;
        }

        
        

        public static void Example()
        {
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            // Initialization of variables
            string input_name = "tinyST.txt";
            string exercise = "RedBlackBST";

            // Connect to the file; create it if necessary. Attach a buffer to the file. Use the buffer to write to the file
            string filePath = $"../../DataStructures/IO/Output/{exercise}_{input_name}";
            using (StreamWriter fileWriter = new StreamWriter(filePath))
            {
                fileWriter.WriteLine($"Questo è il risultato dell'esercizio {exercise} con input {input_name} :");

                // Read from the input file and process the data
                string inputFilePath = $"../../DataStructures/IO/Input/{input_name}";
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
                RedBlackBST<string, int> st = new RedBlackBST<string, int>();
                for (int i = 0; i < lista.Count; i++)
                {
                    string key = (string)lista[i];
                    st.Put(key, i);
                }
                foreach(string s in st.Keys())
                {
                    fileWriter.Write(s + " " + st.Get(s));
                    fileWriter.WriteLine();
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
