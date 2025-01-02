using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;

namespace AOC2024
{
    public class Day23 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            List<string> connections = new List<string>();
            int id = 0;
            foreach (string pair in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(pair))
                {
                    connections.Add(pair);
                }
            }
            // Costruzione del grafo
            var graph = new Dictionary<string, HashSet<string>>();
            foreach (var connection in connections)
            {
                var nodes = connection.Split('-');
                if (!graph.ContainsKey(nodes[0])) graph[nodes[0]] = new HashSet<string>();
                if (!graph.ContainsKey(nodes[1])) graph[nodes[1]] = new HashSet<string>();
                graph[nodes[0]].Add(nodes[1]);
                graph[nodes[1]].Add(nodes[0]);
            }

            // Trova i triangoli
            var triangles = new HashSet<string>();
            foreach (var nodeA in graph.Keys)
            {
                foreach (var nodeB in graph[nodeA])
                {
                    // Evitare di considerare triangoli già trattati
                    if (String.Compare(nodeA, nodeB) >= 0) continue;

                    foreach (var nodeC in graph[nodeB])
                    {
                        if (String.Compare(nodeB, nodeC) >= 0 || !graph[nodeA].Contains(nodeC)) continue;

                        // Normalizzare il triangolo ordinandolo alfabeticamente
                        var triangle = new List<string> { nodeA, nodeB, nodeC };
                        triangle.Sort();
                        triangles.Add(string.Join(",", triangle));
                    }
                }
            }

            // Conta triangoli che soddisfano i criteri
            int count = triangles
                .Where(triangle => triangle.Split(',').Any(node => node.StartsWith("t")))
                .Count();

            Console.WriteLine($"Numero di triangoli che includono un nodo che inizia con 't': {count}");
            solution = count;
        }

        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            List<string> connections = new List<string>();
            int id = 0;
            foreach (string pair in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(pair))
                {
                    connections.Add(pair);
                }
            }
            // Costruzione del grafo
            var graph = new Dictionary<string, HashSet<string>>();
            foreach (var connection in connections)
            {
                var nodes = connection.Split('-');
                if (!graph.ContainsKey(nodes[0])) graph[nodes[0]] = new HashSet<string>();
                if (!graph.ContainsKey(nodes[1])) graph[nodes[1]] = new HashSet<string>();
                graph[nodes[0]].Add(nodes[1]);
                graph[nodes[1]].Add(nodes[0]);
            }

            // Trova la clique massima
            var largestClique = FindLargestClique(graph);

            // Ordina i nodi alfabeticamente e costruisce la password
            var password = string.Join(",", largestClique.OrderBy(node => node));
            Console.WriteLine($"Password del LAN party: {password}");
            solution = password;
        }

        // Funzione per trovare la clique massima
        static List<string> FindLargestClique(Dictionary<string, HashSet<string>> graph)
        {
            var nodes = graph.Keys.ToList();
            List<string> bestClique = new List<string>();

            void Backtrack(List<string> currentClique, int index)
            {
                if (index == nodes.Count)
                {
                    if (currentClique.Count > bestClique.Count)
                    {
                        bestClique = new List<string>(currentClique);
                    }
                    return;
                }

                var node = nodes[index];

                // Verifica se il nodo può essere aggiunto alla clique attuale
                if (currentClique.All(existingNode => graph[existingNode].Contains(node)))
                {
                    currentClique.Add(node);
                    Backtrack(currentClique, index + 1);
                    currentClique.RemoveAt(currentClique.Count - 1);
                }

                // Prosegui senza aggiungere il nodo
                Backtrack(currentClique, index + 1);
            }

            Backtrack(new List<string>(), 0);
            return bestClique;

        }
    }
}
