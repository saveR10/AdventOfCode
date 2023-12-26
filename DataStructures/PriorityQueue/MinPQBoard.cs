using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.DataStructures.PriorityQueue
{
    public class MinPQBoard<Key> where Key : class
    {
        private Key[] pq;  // store items at indices 1 to n
        private int n;  // number of items on priority queue
        private IComparer<Key> comparator;// optional comparator

        public MinPQBoard(int initCapacity)
        {
            pq = new Key[initCapacity + 1];
            n = 0;
        }

        public MinPQBoard() : this(1)
        {
        }

        public MinPQBoard(Key[] keys)
        {
            n = keys.Length;
            pq = new Key[keys.Length + 1];
            for (int i = 0; i < n; i++)
                pq[i + 1] = keys[i];
            for (int k = n / 2; k >= 1; k--)
                Sink(k);
           // Assert isMinHeap();
        }

        public bool IsEmpty(){return n == 0;}

        public int Size(){return n;}

        public Key Min()
        {
            if (IsEmpty()) throw new InvalidOperationException("Priority queue underflow");
            return pq[1];
        }

        // resize the underlying array to have the given capacity
        private void Resize(int capacity)
        {
            //Assert capacity > n
            if (capacity <= n) return;
            Key[] temp = new Key[capacity];
            for (int i = 1; i <= n; i++)
            {
                temp[i] = pq[i];
            }
            pq = temp;
        }

        public void Insert(Key x)
        {
            if (n == pq.Length - 1) Resize(2 * pq.Length);

            pq[++n] = x;
            Swim(n);
            //Assert isMinHeap();
        }
        
        // Does this board contained in PQ?
        public bool Contain(Key x)
        {
            dynamic t = x;
            dynamic tpq = pq;
            bool isContain = false;
            //int puzzleSize = ((Board)x).N; // Update with your Board class property
            int puzzleSize = ((Board)t).n; // Update with your Board class property
            for (int i = 1; i <= n; i++)
            {
                //if (((Board)pq[i]).ManhattanMeasure == ((Board)x).ManhattanMeasure &&
                //    ((Board)pq[i]).HammingMeasure == ((Board)x).HammingMeasure)
                if (((Board)tpq[i]).manhattanMeasure == ((Board)t).manhattanMeasure &&
                    ((Board)tpq[i]).hammingMeasure == ((Board)t).hammingMeasure)
                {
                    if (Equal(x, i, puzzleSize))
                    {
                        isContain = true;
                        break;
                    }
                }
            }
            return isContain;
        }

        public Board Select(Key x)
        {
            Board result = new Board();
            dynamic t = x;
            dynamic tpq= pq;
            //int puzzleSize = ((Board)x).n; // Update with your Board class property
            int puzzleSize = ((Board)t).n; // Update with your Board class property
            for (int i = 1; i <= n; i++)
            {
                //if (((Board)pq[i]).ManhattanMeasure == ((Board)x).ManhattanMeasure &&
                //    ((Board)pq[i]).HammingMeasure == ((Board)x).HammingMeasure)
                if (((Board)tpq[i]).manhattanMeasure == ((Board)t).manhattanMeasure &&
                    ((Board)tpq[i]).hammingMeasure == ((Board)t).hammingMeasure)
                {
                    if (Equal(x, i, puzzleSize))
                    {
                        result = tpq[i];
                        break;
                    }
                }
            }
            return result;
        }

        public Key DelMin()
        {
            if (IsEmpty()) throw new InvalidOperationException("Priority queue underflow");
            Key min = pq[1];
            Exchange(1, n--);
            Sink(1);
            pq[n + 1] = null;
            if ((n > 0) && (n == (pq.Length - 1) / 4)) Resize(pq.Length / 2);
            return min;
        }

        public Key Remove(Key x)
        {
            dynamic t = x;
            dynamic tpq= pq;
            if (IsEmpty()) throw new InvalidOperationException("Priority queue underflow");
            //int puzzleSize = ((Board)x).N; // Update with your Board class property
            int puzzleSize = ((Board)t).n; // Update with your Board class property
            for (int i = 1; i <= n; i++)
            {
                //if (((Board)pq[i]).ManhattanMeasure == ((Board)x).ManhattanMeasure &&
                //    ((Board)pq[i]).HammingMeasure == ((Board)x).HammingMeasure)
                if (((Board)tpq[i]).manhattanMeasure == ((Board)t).manhattanMeasure &&
                    ((Board)tpq[i]).hammingMeasure == ((Board)t).hammingMeasure)
                {
                    if (Equal(x, i, puzzleSize))
                    {
                        Key del = pq[i];
                        for (int j = i + 1; j <= n; j++)
                        {
                            Exchange(j - 1, j);
                        }
                        n--;
                        Sink(1);
                        pq[n + 1] = null;
                        if ((n > 0) && (n == (pq.Length - 1) / 4)) Resize(pq.Length / 2);
                        return del;
                    }
                }
            }
            return x;
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
            dynamic tpq = pq;
            if (comparator == null)
            {
                return  (((Board)tpq[i]).priorityFunction).CompareTo(((Board)tpq[j]).priorityFunction) > 0;
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

        private bool Equal(Key x, int i, int puzzleSize)
        {
            dynamic t = x;
            dynamic tpq = pq;
            bool isEqual = true;
            for (int r = 0; r < puzzleSize; r++)
            {
                for (int c = 0; c < puzzleSize; c++)
                {
                    //if (((Board)pq[i]).Tiles[r][c] != ((Board)x).Tiles[r][c])
                    if (((Board)tpq[i]).tiles[r,c] != ((Board)t).tiles[r,c])
                    {
                        isEqual = false;
                        return isEqual;
                    }
                }
            }
            return isEqual;
        }
        // is pq[1..n] a min heap?
        private bool isMinHeap()
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
        // is subtree of pq[1..n] rooted at k a min heap?
        private bool isMinHeapOrdered(int k)
        {
            if (k > n) return true;
            int left = 2 * k;
            int right = 2 * k + 1;
            if (left <= n && Greater(k, left)) return false;
            if (right <= n && Greater(k, right)) return false;
            return isMinHeapOrdered(left) && isMinHeapOrdered(right);
        }
    }
}


