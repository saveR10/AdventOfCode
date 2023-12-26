using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

    private void Resize(int capacity)
    {
        Array.Resize(ref keys, capacity);
        Array.Resize(ref vals, capacity);
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
    }

    public IEnumerable<Key> Keys()
    {
        return Keys(Min(), Max());
    }
    public Key Min()
    {
        if (IsEmpty()) throw new InvalidOperationException("Called Min() with empty symbol table");
        return keys[0];
    }

    public Key Max()
    {
        if (IsEmpty()) throw new InvalidOperationException("Called Max() with empty symbol table");
        return keys[n - 1];
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

    // Other methods remain unchanged...

    public static void Main(string[] args)
    {
        long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        // Initialize variables
        string lesson = "lezione3";
        string subpath = "search";
        string input_name = "tinyST.txt";
        string exercise = "BinarySearchST";

        // Connect to the file; create it if necessary. Attach a buffer to the file. Use the buffer to write to the file
        using (StreamWriter writer = new StreamWriter($"./out/{lesson}/{subpath}/{exercise}_{input_name}"))
        {
            writer.WriteLine($"Questo è il risultato dell'esercizio {exercise} con input {input_name} :");

            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader($"./resources/{lesson}/{subpath}/{input_name}"))
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
            writer.WriteLine($"Execution time in milliseconds: {timeElapsed}");
            writer.WriteLine($"Execution time in seconds: {timeElapsed / 1000.0}");
        }
    }
}
