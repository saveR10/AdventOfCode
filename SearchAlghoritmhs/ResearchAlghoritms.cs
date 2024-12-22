using AOC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AOC.SearchAlghoritmhs
{
    [AttributeUsage(AttributeTargets.Class |AttributeTargets.Constructor |AttributeTargets.Field |AttributeTargets.Method |AttributeTargets.Property,AllowMultiple = true)]
    public class ResearchAlghoritmsAttribute : System.Attribute
    {
        public ResearchAlghoritmsAttribute(TypologyEnum typology)
        {
            this.typology = typology;
        }
        private TypologyEnum typology;
        public TypologyEnum Typology { get { return typology; } }
        public enum TypologyEnum
        {
            #region Typology
            Hashing,                    //algoritmi di criptazione/decriptazione
            BinaryOperation,            //operazioni sui valori di tipo binario
            Game,                       //rifacimento di giochi classici
            JSON,                       //gestione di oggetti di tipo JSON

            #endregion

            #region ResolutionStrategy
            AlphaStar,
            Dijkstra,
            Drawing,                    //schematizzarlo con un disegno può risultare utile
            DFS,                        //Depth-First Search (DFS) DFS esplora i nodi in profondità, seguendo un percorso fino al suo termine prima di tornare indietro ed esplorare altri percorsi.
            BFS,                        //Breadth-First Search (BFS) BFS esplora i nodi di un grafo o albero livello per livello, iniziando dal nodo di partenza e visitando tutti i suoi vicini prima di passare ai vicini di livello successivo
            SystemLinearEquations,      //rappresentabili come sistemi di equazioni lineari
            Cache,
            Overflow,                   //richiedono considerazioni matematiche o utilizzo di strutture dati adatte
            Map,
            #endregion

            Escaping, //è dei caratteri!
            TextRules,
            Cronometers,
            Ingredients,
            Combinatorial,
            Decompressing,
            Regex,
            Reduction,
            Recursive,
        }

        public static List<string> SearchFolder(TypologyEnum typology)
        {
            List<string> InterestedClasses = new List<string>();
            string[] Folder = System.IO.Directory.GetDirectories("C:\\Users\\s.cecere\\source\\repos\\AOC\\AOC\\Sources\\");
            
            foreach(var year in Folder)
            {
                 var f= System.IO.Directory.GetFiles(year);
                foreach(var day in f)
                {
                    var y = year.Split(Delimiter.Backslash, StringSplitOptions.None);
                    var d = day.Split(Delimiter.Backslash, StringSplitOptions.None)[day.Split(Delimiter.Backslash, StringSplitOptions.None).Length - 1].Split(Delimiter.Dot, StringSplitOptions.None)[0];
                    var i = Activator.CreateInstance(Type.GetType($"AOC{y[y.Length-1]}.{d}")) as IDay;

                    var t= i.GetType();
                    ResearchAlghoritmsAttribute[] MyAttributes = (ResearchAlghoritmsAttribute[]) System.Attribute.GetCustomAttributes(t,typeof(ResearchAlghoritmsAttribute));
                    if (MyAttributes.Length != 0)
                    {
                        foreach(var a in MyAttributes)
                        {
                            if (a.Typology.Equals(typology))
                            {
                                InterestedClasses.Add(day);
                            }
                        }
                    }
                }

            }

            return InterestedClasses;
        }
    }

}
