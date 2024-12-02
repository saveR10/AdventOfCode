using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC.DataStructures.Searching
{

    public class BinarySearchST<Key, Value> where Key : IComparable<Key>
    {
        private const int INIT_CAPACITY = 2;
        private Key[] keys;
        private Value[] vals;
        private int n = 0;

        public BinarySearchST()
        {
            this.Init(INIT_CAPACITY);
        }

        public BinarySearchST(int capacity)
        {
            this.Init(capacity);
        }

        private void Init(int capacity)
        {
            keys = new Key[capacity];
            vals = new Value[capacity];
        }

        /*private void Resize(int capacity)
        {
            Array.Resize(ref keys, capacity);
            Array.Resize(ref vals, capacity);
        }*/
        private void Resize(int capacity)
        {
            if (capacity < n)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            Key[] tempk = new Key[capacity];
            Value[] tempv = new Value[capacity];
            for (int i = 0; i < n; i++)
            {
                tempk[i] = keys[i];
                tempv[i] = vals[i];
            }
            vals = tempv;
            keys = tempk;
        }
        public int Size()
        {
            return n;
        }
        public int Size(Key lo, Key hi)
        {
            if (lo == null)
            {
                throw new ArgumentNullException(nameof(lo), "first argument to Size() is null");
            }
            if (hi == null)
            {
                throw new ArgumentNullException(nameof(hi), "second argument to Size() is null");
            }

            if (lo.CompareTo(hi) > 0)
            {
                return 0;
            }
            if (Contains(hi))
            {
                return Rank(hi) - Rank(lo) + 1;
            }
            else
            {
                return Rank(hi) - Rank(lo);
            }
        }
        public bool IsEmpty()
        {
            return Size() == 0;
        }

        public bool Contains(Key key)
        {
            if (key == null) throw new ArgumentNullException("Argument to contains() is null");
            return Get(key) != null;
        }

        public Value Get(Key key)
        {
            if (key == null) throw new ArgumentNullException("Argument to get() is null");
            if (IsEmpty()) return default;

            int i = Rank(key);
            if (i < n && keys[i].Equals(key)) return vals[i];
            return default;
        }
        public int Rank(Key key)
        {
            if (key == null) throw new ArgumentNullException("Argument to rank() is null");

            int lo = 0, hi = n - 1;
            while (lo <= hi)
            {
                int mid = lo + (hi - lo) / 2;
                int cmp = key.CompareTo(keys[mid]);
                if (cmp < 0) hi = mid - 1;
                else if (cmp > 0) lo = mid + 1;
                else return mid;
            }
            return lo;
        }
        public void Put(Key key, Value val)
        {
            if (key == null) throw new ArgumentNullException("First argument to put() is null");

            if (val == null)
            {
                Delete(key);
                return;
            }

            int i = Rank(key);

            if (i < n && keys[i].Equals(key))
            {
                vals[i] = val;
                return;
            }

            if (n == keys.Length)
            {
                Resize(2 * keys.Length);
            }

            for (int j = n; j > i; j--)
            {
                keys[j] = keys[j - 1];
                vals[j] = vals[j - 1];
            }
            keys[i] = key;
            vals[i] = val;
            n++;

            //Assert check();

        }

        public void Delete(Key key)
        {
            if (key == null) throw new ArgumentNullException("Argument to delete() is null");
            if (IsEmpty()) return;

            int i = Rank(key);

            if (i == n || !keys[i].Equals(key)) return;

            for (int j = i; j < n - 1; j++)
            {
                keys[j] = keys[j + 1];
                vals[j] = vals[j + 1];
            }

            n--;
            keys[n] = default; // To avoid loitering
            vals[n] = default;

            if (n > 0 && n == keys.Length / 4)
            {
                Resize(keys.Length / 2);
            }

            //Assert check();

        }

        public void DeleteMin()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Symbol table underflow error");
            }
            Delete(Min());
        }

        public void DeleteMax()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Symbol table underflow error");
            }
            Delete(Max());
        }


        public Key Min()
        {
            if (IsEmpty()) throw new InvalidOperationException("Called Min() with empty symbol table");
            return keys[0];
        }

        public Key Max()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("called Max() with empty symbol table");
            }
            return keys[n - 1];
        }

        public IEnumerable<Key> Keys()
        {
            return Keys(Min(), Max());
        }

        public IEnumerable<Key> Keys(Key lo, Key hi)
        {
            if (lo == null || hi == null) throw new ArgumentNullException("Argument to keys() is null");
            if (lo.CompareTo(hi) > 0) return Enumerable.Empty<Key>();

            var queue = new Queue<Key>();
            for (int i = Rank(lo); i < Rank(hi); i++)
            {
                queue.Enqueue(keys[i]);
            }
            if (Contains(hi))
            {
                queue.Enqueue(keys[Rank(hi)]);
            }
            return queue;
        }

        public Key Select(int k)
        {
            if (k < 0 || k >= Size())
            {
                throw new ArgumentOutOfRangeException(nameof(k), "called Select() with invalid argument: " + k);
            }
            return keys[k];
        }

        public Key Floor(Key key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "argument to Floor() is null");
            }
            int i = Rank(key);
            if (i < n && key.CompareTo(keys[i]) == 0)
            {
                return keys[i];
            }
            if (i == 0)
            {
                throw new InvalidOperationException("argument to Floor() is too small");
            }
            else
            {
                return keys[i - 1];
            }
        }

        public Key Ceiling(Key key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "argument to Ceiling() is null");
            }
            int i = Rank(key);
            if (i == n)
            {
                throw new InvalidOperationException("argument to Ceiling() is too large");
            }
            else
            {
                return keys[i];
            }
        }
        #region Checks
        private bool Check()
        {
            return IsSorted() && RankCheck();
        }
        // Are the items in the array in ascending order?
        private bool IsSorted()
        {
            for (int i = 1; i < Size(); i++)
            {
                if (keys[i].CompareTo(keys[i - 1]) < 0)
                {
                    return false;
                }
            }
            return true;
        }

        // Check that rank(select(i)) = i
        private bool RankCheck()
        {
            for (int i = 0; i < Size(); i++)
            {
                if (i != Rank(Select(i)))
                {
                    return false;
                }
            }
            for (int i = 0; i < Size(); i++)
            {
                if (keys[i].CompareTo(Select(Rank(keys[i]))) != 0)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
        public static void Example()
        {
            long startTime = DateTime.Now.Ticks;
            string input_name = "tinyTale.txt";//"tinyST.txt";
            string exercise = "BinarySearchST";

            // Connect to the file; create it if necessary. Attach a buffer to the file. Use the buffer to write to the file
            using (StreamWriter writer = new StreamWriter($"../../DataStructures/IO/Output/{exercise}_{input_name}"))
            {
                writer.WriteLine($"Questo è il risultato dell'esercizio {exercise} con input {input_name} :");

                List<string> lines = new List<string>();
                using (StreamReader reader = new StreamReader($"../../DataStructures/IO/Input/{input_name}"))
                {
                    string line;
                    while ((line = reader.ReadLine()?.Trim()) != null)
                    {
                        lines.AddRange(line.Trim().Split(' '));
                    }
                }

                var st = new BinarySearchST<string, int>();
                foreach (var key in lines)
                {
                    st.Put(key, lines.IndexOf(key));
                }

                foreach (var s in st.Keys())
                {
                    writer.WriteLine($"{s} {st.Get(s)}");
                }

                writer.WriteLine();

                long endTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                long timeElapsed = endTime - startTime;
                writer.WriteLine();
                writer.Write("Execution time in ticks: " + timeElapsed);
                writer.WriteLine();
                writer.Write("Execution time in milliseconds: " + timeElapsed / TimeSpan.TicksPerMillisecond);
                writer.WriteLine();
                writer.Write("Execution time in seconds: " + timeElapsed / TimeSpan.TicksPerSecond);
                writer.WriteLine();
                // Close the buffer and the file writer to make sure the file is saved properly
                writer.Close();
            }
        }
    }
}