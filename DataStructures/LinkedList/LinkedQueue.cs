using System;
using System.IO;

namespace AOC.DataStructures.LinkedList
{
    public class LinkedQueue<Item>
    {
        private int n;         // number of elements on queue
        private Node first;    // beginning of queue
        private Node last;     // end of queue

        public LinkedQueue()
        {
            first = null;
            last = null;
            n = 0;
            Check();
        }

        private class Node
        {
            public Item item;
            public Node next;
        }

        public bool IsEmpty()
        {
            return first == null;
        }

        public int Size()
        {
            return n;
        }

        public Item Peek()
        {
            if (IsEmpty()) throw new InvalidOperationException("Queue underflow");
            return first.item;
        }

        public void Enqueue(Item item)
        {
            Node oldLast = last;
            last = new Node
            {
                item = item,
                next = null
            };

            if (IsEmpty())
            {
                first = last;
            }
            else
            {
                oldLast.next = last;
            }

            n++;
            Check();
        }

        public Item Dequeue()
        {
            if (IsEmpty()) throw new InvalidOperationException("Queue underflow");

            Item item = first.item;
            first = first.next;
            n--;

            if (IsEmpty())
            {
                last = null;   // to avoid loitering
            }

            Check();
            return item;
        }

        private bool Check()
        {
            if (n < 0)
            {
                return false;
            }
            else if (n == 0)
            {
                if (first != null || last != null) return false;
            }
            else if (n == 1)
            {
                if (first == null || last == null) return false;
                if (first != last || first.next != null) return false;
            }
            else
            {
                if (first == null || last == null || first == last || first.next == null || last.next != null) return false;

                // check internal consistency of instance variable n
                int numberOfNodes = 0;
                for (Node x = first; x != null && numberOfNodes <= n; x = x.next)
                {
                    numberOfNodes++;
                }
                if (numberOfNodes != n) return false;

                // check internal consistency of instance variable last
                Node lastNode = first;
                while (lastNode.next != null)
                {
                    lastNode = lastNode.next;
                }
                if (last != lastNode) return false;
            }
            return true;
        }

        public static void Example()
        {
            string inputName = "tobe.txt";
            string esercizio = "LinkedQueue";

            using (StreamWriter writer = new StreamWriter($"../../DataStructures/IO/Output/{esercizio}_{inputName}"))
            {
                writer.Write($"Questo è il risultato dell'esercizio {esercizio} con input {inputName} :");
                writer.WriteLine();

                string stringReader = string.Empty;
                using (StreamReader reader = new StreamReader($"../../DataStructures/IO/Input/{inputName}"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        stringReader = line;
                    }
                }

                string result = "";
                LinkedQueue<string> queue = new LinkedQueue<string>();
                string[] splits = stringReader.Split(' ');
                foreach (string elem in splits)
                {
                    if (elem.Equals("-"))
                    {
                        result += queue.Dequeue() + " ";
                    }
                    else
                    {
                        queue.Enqueue(elem);
                    }
                }

                writer.Write("Il risultato è " + result);
                writer.WriteLine();
            }
        }
    }
}
