using AOC;
using AOC.DataStructures.Clustering;
using AOC.DataStructures.PriorityQueue;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization; 
using System;
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
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;
using Solver = AOC.Solver;

namespace AOC2015
{
    [ResearchAlgorithmsAttribute(TypologyEnum.JSON)]
    public class Day12 : Solver, IDay
    {
        long sum = 0;
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var inputlist = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);

            var jsonRequest = JsonConvert.SerializeObject(inputlist);
            bool IsValueToInsert = false;
            Dictionary<string, List<object>> param = new Dictionary<string, List<object>>();
            string PropertyToInsert = "";
            Newtonsoft.Json.JsonTextReader jsonTextReader = new JsonTextReader(new StringReader(jsonRequest));
            jsonTextReader.Read();
            var json = jsonTextReader.ReadAsString();
            JToken jtoken = JToken.Parse(json);
            foreach (var item in jtoken.Children())
            {
                if (item.Values() != null)
                {
                    Annidate(item);
                }
            }

            solution = sum;

        }

        public void Annidate(object item)
        {
            foreach (var item2 in (JToken)item)
            {
                if (item2.HasValues)
                {
                    Annidate(item2);
                }
                else
                {
                    try
                    {
                        sum += item2.Value<int>();
                        if (item2.Value<int>() == 118)
                        {
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }
        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var inputlist = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);

            var jsonRequest = JsonConvert.SerializeObject(inputlist);
            bool IsValueToInsert = false;
            Dictionary<string, List<object>> param = new Dictionary<string, List<object>>();
            string PropertyToInsert = "";
            Newtonsoft.Json.JsonTextReader jsonTextReader = new JsonTextReader(new StringReader(jsonRequest));
            jsonTextReader.Read();
            var json = jsonTextReader.ReadAsString();
            JToken jtoken = JToken.Parse(json);
            foreach (var item in jtoken.Children())
            {
                if (item.Values() != null)
                {
                    Annidate2(item);
                }
            }
            solution = sum;
        }
        public void Annidate2(object item)
        {
            foreach (var item2 in (JToken)item)
            {
                if (item2.HasValues)
                {
                    if (item2.Values().Contains("red") && item2.Type.Equals(JTokenType.Object)) { }
                    else Annidate2(item2);
                }
                else
                {
                    try
                    {
                        sum += item2.Value<int>();
                        if (item2.Value<int>() == 118)
                        {
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}
