using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Documents.LINQ
{
    internal class Person
    {
        public string Name, Email;

        public Person(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
    internal class Data
    {
        public string Mail, SlackId;

        public Data(string mail, string slackId)
        {
            Mail = mail;
            SlackId = slackId;
        }
    }


    public static class Join
    {
        static Person[] people = new Person[]
        {
            new Person("Sudi", "sudi@try.cd"),
            new Person("Simba", "simba@try.cd"),
            new Person("Sarah", string.Empty)
        };

        static Data[] records = new Data[]
        {
            new Data("sudi@try.cd", "Sudi_Try"),
            new Data("sudi@try.cd", "Sudi@Test"),
            new Data("simba@try.cd", "SimbaLion")
        };
        public static void Example()
        {
            JoinMethod();
            GroupJoinMethod();
        }

        public static void JoinMethod() 
        {
            var query = people.Join(records, x => x.Email, y => y.Mail, (person, record) => new { Name = person.Name, SlackId = record.SlackId });
            foreach (var item in query)
            {
                Console.WriteLine($"{item.Name} has Slack ID {item.SlackId}");
            }
        }

        public static void GroupJoinMethod()
        {
            var query = people.GroupJoin(records,
                            x => x.Email,
                            y => y.Mail,
                            (person, recs) => new {Name = person.Name,SlackIds = recs.Select(r => r.SlackId).ToArray() });
            foreach (var item in query)
            {
                Console.WriteLine($"{item.Name} has Slack ID {item.SlackIds}");
            }
        }


        public static void ZipMethod()
        {
            List<int> numbers = new List<int>();
            Dictionary<string, int> keyValuePair = new Dictionary<string, int>();
            int[] numbersSequence = { 10, 20, 30, 40, 50 };
            string[] wordsSequence = { "Ten", "Twenty", "Thirty", "Fourty" };
            var resultSequence = numbersSequence.Zip(wordsSequence, (first, second) => first + " - " + second);
            foreach (var item in resultSequence)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();


            var keys = new List<string> { "ID", "Name", "Email", "Mobile" };
            var values = new List<string> { "1", "Pranaya", "Pranaya@example.com", "1234567890" };
            var dictionary = keys.Zip(values, (k, v) => new { k, v })
                                 .ToDictionary(x => x.k, x => x.v);
            // Now dictionary contains { { "ID", "1" }, { "Name", "Pranaya" }, { "Email", "Pranaya@example.com" }, { "Mobile", "1234567890" } }
            foreach (var item in dictionary)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }
            Console.ReadKey();
        }
    }
}
