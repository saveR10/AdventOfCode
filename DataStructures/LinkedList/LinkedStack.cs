using System;
using System.IO;

namespace AOC.DataStructures.LinkedList
{
    public class LinkedStack<Item>
    {
        private int n;          // size of the stack
        private Node first;     // top of stack

        public LinkedStack()
        {
            first = null;
            n = 0;
            Check();
        }

        public bool IsEmpty()
        {
            return first == null;
        }

        public int Size()
        {
            return n;
        }

        public void Push(Item item)
        {
            Node oldFirst = first;
            first = new Node();
            first.Item = item;
            first.Next = oldFirst;
            n++;
            Check();
        }

        public Item Pop()
        {
            if (IsEmpty()) throw new InvalidOperationException("Stack underflow");
            Item item = first.Item;        // save item to return
            first = first.Next;            // delete first node
            n--;
            Check();
            return item;                   // return the saved item
        }

        public Item Peek()
        {
            if (IsEmpty()) throw new InvalidOperationException("Stack underflow");
            return first.Item;
        }

        // helper linked list class
        private class Node
        {
            public Item Item { get; set; }
            public Node Next { get; set; }
        }

        // check internal invariants
        private bool Check()
        {
            // check a few properties of instance variable 'first'
            if (n < 0)
            {
                return false;
            }
            if (n == 0)
            {
                if (first != null) return false;
            }
            else if (n == 1)
            {
                if (first == null) return false;
                if (first.Next != null) return false;
            }
            else
            {
                if (first == null) return false;
                if (first.Next == null) return false;
            }

            // check internal consistency of instance variable n
            int numberOfNodes = 0;
            for (Node x = first; x != null && numberOfNodes <= n; x = x.Next)
            {
                numberOfNodes++;
            }
            if (numberOfNodes != n) return false;

            return true;
        }

        public static void Example()
        {
            string input_name = "tobe.txt";
            string esercizio = "LinkedStack";

            using (StreamWriter writer = new StreamWriter($"../../DataStructures/IO/Output/{esercizio}_{input_name}"))
            {
                writer.Write($"Questo è il risultato dell'esercizio {esercizio} con input {input_name} :");
                writer.WriteLine();

                string stringReader = string.Empty;
                using (StreamReader reader = new StreamReader($"../../DataStructures/IO/Input/{input_name}"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        stringReader = line;
                    }
                }

                string result = "";
                LinkedStack<string> stack = new LinkedStack<string>();
                string[] splits = stringReader.Split(' ');
                foreach (string elem in splits)
                {
                    if (elem.Equals("-"))
                    {
                        result += stack.Pop() + " ";
                    }
                    else
                    {
                        stack.Push(elem);
                    }
                }

                writer.Write($"Il risultato è {result}");
                writer.WriteLine();
            }
        }
    }
}
