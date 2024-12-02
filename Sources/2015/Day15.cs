using AOC;
using AOC.DataStructures.Clustering;
using AOC.DataStructures.PriorityQueue;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
 
 
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using static AOC.SearchAlghoritmhs.ResearchAlghoritmsAttribute;
using Solver = AOC.Solver;

namespace AOC2015
{
    //Si potrebbe risolvere con un sistema lineari di equazioni; ma bisognerebbe derivare...
    [ResearchAlghoritms(TypologyEnum.Ingredients)]
    public class Day15 : Solver, IDay
    {
        public Dictionary<string, Ingredient> Ingredients = new Dictionary<string, Ingredient>();
        public int Teaspoon = 100;
        public int score = 0;
        public class Ingredient
        {
            public string Name { get; set; }
            public int Capacity { get; set; }
            public int Durability { get; set; }
            public int Flavor { get; set; }
            public int Texture { get; set; }
            public int Calories { get; set; }
        }
        public List<int> Cookies = new List<int>();

        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var inputlist = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);

            foreach (var i in inputlist)
            {
                if (!string.IsNullOrEmpty(i))
                {
                    string[] ingr = i.Split(Delimiter.delimiter_space, StringSplitOptions.None);
                    Ingredients.Add(ingr[0], new Ingredient
                    {
                        Name = ingr[0].Split(Delimiter.DoubleDot, StringSplitOptions.None)[0],
                        Capacity = int.Parse(ingr[2].Split(Delimiter.delimiter_comma, StringSplitOptions.None)[0]),
                        Durability = int.Parse(ingr[4].Split(Delimiter.delimiter_comma, StringSplitOptions.None)[0]),
                        Flavor = int.Parse(ingr[6].Split(Delimiter.delimiter_comma, StringSplitOptions.None)[0]),
                        Texture = int.Parse(ingr[8].Split(Delimiter.delimiter_comma, StringSplitOptions.None)[0])
                    });

                }
            }
            List<string> IngredientsName = new List<string>();
            foreach (var ingr in Ingredients)
            {
                IngredientsName.Add(ingr.Key);
            }
            //bool[,] Recipes = new bool[Teaspoon+1, Teaspoon+1];         
            bool[,,,] Recipes = new bool[Teaspoon + 1, Teaspoon + 1, Teaspoon + 1, Teaspoon + 1];
            for (int b = 0; b <= Teaspoon; b++)
            {
                for (int c = 0; c <= Teaspoon; c++)
                {
                    for (int d = 0; d <= Teaspoon; d++)
                    {
                        for (int e = 0; e <= Teaspoon; e++)
                        {
                            if (b + c + d + e != Teaspoon) Recipes[b, c, d, e] = true;
                            else
                            {
                                int cap = b * (Ingredients[IngredientsName[0]].Capacity) + c * (Ingredients[IngredientsName[1]].Capacity) + d * (Ingredients[IngredientsName[2]].Capacity) + e * (Ingredients[IngredientsName[3]].Capacity);
                                int dur = b * (Ingredients[IngredientsName[0]].Durability) + c * (Ingredients[IngredientsName[1]].Durability) + d * (Ingredients[IngredientsName[2]].Durability) + e * (Ingredients[IngredientsName[3]].Durability);
                                int tex = b * (Ingredients[IngredientsName[0]].Texture) + c * (Ingredients[IngredientsName[1]].Texture) + d * (Ingredients[IngredientsName[2]].Texture) + e * (Ingredients[IngredientsName[3]].Texture);
                                int fla = b * (Ingredients[IngredientsName[0]].Flavor) + c * (Ingredients[IngredientsName[1]].Flavor) + d * (Ingredients[IngredientsName[2]].Flavor) + e * (Ingredients[IngredientsName[3]].Flavor);
                                if (cap > 0 && dur > 0 && tex > 0 && fla > 0)
                                    Cookies.Add(cap * dur * tex * fla);
                            }
                        }
                    }
                }
            }
            solution = Cookies.Max(c => c);

        }
        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var inputlist = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);

            foreach (var i in inputlist)
            {
                if (!string.IsNullOrEmpty(i))
                {
                    string[] ingr = i.Split(Delimiter.delimiter_space, StringSplitOptions.None);
                    Ingredients.Add(ingr[0], new Ingredient
                    {
                        Name = ingr[0].Split(Delimiter.DoubleDot, StringSplitOptions.None)[0],
                        Capacity = int.Parse(ingr[2].Split(Delimiter.delimiter_comma, StringSplitOptions.None)[0]),
                        Durability = int.Parse(ingr[4].Split(Delimiter.delimiter_comma, StringSplitOptions.None)[0]),
                        Flavor = int.Parse(ingr[6].Split(Delimiter.delimiter_comma, StringSplitOptions.None)[0]),
                        Texture = int.Parse(ingr[8].Split(Delimiter.delimiter_comma, StringSplitOptions.None)[0]),
                        Calories = int.Parse(ingr[10].Split(Delimiter.delimiter_comma, StringSplitOptions.None)[0])
                    });

                }
            }
            List<string> IngredientsName = new List<string>();
            foreach (var ingr in Ingredients)
            {
                IngredientsName.Add(ingr.Key);
            }
            //bool[,] Recipes = new bool[Teaspoon+1, Teaspoon+1];         
            bool[,,,] Recipes = new bool[Teaspoon + 1, Teaspoon + 1, Teaspoon + 1, Teaspoon + 1];
            for (int b = 0; b <= Teaspoon; b++)
            {
                for (int c = 0; c <= Teaspoon; c++)
                {
                    for (int d = 0; d <= Teaspoon; d++)
                    {
                        for (int e = 0; e <= Teaspoon; e++)
                        {
                            if (b + c + d + e != Teaspoon) Recipes[b, c, d, e] = true;
                            else
                            {
                                int cal = b * (Ingredients[IngredientsName[0]].Calories) + c * (Ingredients[IngredientsName[1]].Calories) + d * (Ingredients[IngredientsName[2]].Calories) + e * (Ingredients[IngredientsName[3]].Calories);
                                if (cal == 500)
                                {
                                    int cap = b * (Ingredients[IngredientsName[0]].Capacity) + c * (Ingredients[IngredientsName[1]].Capacity) + d * (Ingredients[IngredientsName[2]].Capacity) + e * (Ingredients[IngredientsName[3]].Capacity);
                                    int dur = b * (Ingredients[IngredientsName[0]].Durability) + c * (Ingredients[IngredientsName[1]].Durability) + d * (Ingredients[IngredientsName[2]].Durability) + e * (Ingredients[IngredientsName[3]].Durability);
                                    int tex = b * (Ingredients[IngredientsName[0]].Texture) + c * (Ingredients[IngredientsName[1]].Texture) + d * (Ingredients[IngredientsName[2]].Texture) + e * (Ingredients[IngredientsName[3]].Texture);
                                    int fla = b * (Ingredients[IngredientsName[0]].Flavor) + c * (Ingredients[IngredientsName[1]].Flavor) + d * (Ingredients[IngredientsName[2]].Flavor) + e * (Ingredients[IngredientsName[3]].Flavor);
                                    if (cap > 0 && dur > 0 && tex > 0 && fla > 0)
                                        Cookies.Add(cap * dur * tex * fla);
                                }
                            }
                        }
                    }
                }
            }
            solution = Cookies.Max(c => c);
        }
    }
}