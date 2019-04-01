using System;
using System.Collections.Generic;
using System.IO;

namespace Nakov
{
    class Program
    {
        const string PhoneBookFileName = "PhoneBook.txt";
        const string QueriesFileName = "Queries.txt";

        static Dictionary<string, List<string>> phoneBook = new Dictionary<string, List<string>>();

        static void Main(string[] args)
        {
            ReadPhoneBook();
            ProcessQueries();
            Console.ReadLine();
        }

        static void ReadPhoneBook()
        {
            StreamReader reader = new StreamReader(PhoneBookFileName);
            using (reader)
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    string[] entry = line.Split(new char[] { '|' });
                    string names = entry[0].Trim();
                    string town = entry[1].Trim();

                    string[] nameTokens = names.Split(new char[] { ' ', '\t' });
                    foreach (string name in nameTokens)
                    {
                        AddToPhoneBook(name, line);
                        string nameAndTown = CombineNameAndTown(town, name);
                        AddToPhoneBook(nameAndTown, line);
                    }
                }
            }
        }

        static string CombineNameAndTown(string town, string name)
        {
            return name + " from " + town;
        }

        static void AddToPhoneBook(string name, string entry)
        {
            name = name.ToLower();
            List<string> entries;
            if (!phoneBook.TryGetValue(name, out entries))
            {
                entries = new List<string>();
                phoneBook.Add(name, entries);
            }
            entries.Add(entry);
        }

        static void ProcessQueries()
        {
            StreamReader reader = new StreamReader(QueriesFileName);
            using (reader)
            {
                while (true)
                {
                    string query = reader.ReadLine();
                    if (query == null)
                    {
                        break;
                    }
                    ProcessQuery(query);
                }
            }
        }

        static void ProcessQuery(string query)
        {
            if (query.StartsWith("list("))
            {
                int listLen = "list(".Length;
                string name = query.Substring(listLen, query.Length - listLen - 1);
                name = name.Trim().ToLower();
                PrintAllMatches(name);
            }
            else if (query.StartsWith("find("))
            {
                string[] queryParams = query.Split(new char[] { '(', ' ', ',', ')' }, StringSplitOptions.RemoveEmptyEntries);
                string name = queryParams[1];
                name = name.Trim().ToLower();
                string town = queryParams[2];
                town = town.Trim().ToLower();
                if (queryParams.Length > 3) town += " " + queryParams[3].Trim().ToLower();
                string nameAndTown = CombineNameAndTown(town, name);
                PrintAllMatches(nameAndTown);
            }
            else
            {
                Console.WriteLine(query + "is invalid command!");
            }
        }

        static void PrintAllMatches(string key)
        {
            List<string> allMatches;
            if(phoneBook.TryGetValue(key, out allMatches))
            {
                foreach (string entry in allMatches)
                {
                    Console.WriteLine(entry);
                }
            }
            else
            {
                Console.WriteLine("Not found!");
            }
            Console.WriteLine();
        }













        /*public static void SortingPhoneBook()
        {
            SortedDictionary<string, SortedDictionary<string, string>> phonesByTown = new SortedDictionary<string, SortedDictionary<string, string>>();
            StreamReader reader = new StreamReader("data.txt");

            using (reader)
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    string[] entry = line.Split(new char[] { '|' });
                    string name = entry[0].Trim();
                    string town = entry[1].Trim();
                    string phone = entry[2].Trim();

                    SortedDictionary<string, string> phoneBook;
                    if (!phonesByTown.TryGetValue(town, out phoneBook))
                    {
                        phoneBook = new SortedDictionary<string, string>();
                        phonesByTown.Add(town, phoneBook);
                    }
                    phoneBook.Add(name, phone);
                }

                foreach (string town in phonesByTown.Keys)
                {
                    Console.WriteLine("Town " + town + ":");
                    SortedDictionary<string, string> phoneBook = phonesByTown[town];

                    foreach (var entry in phoneBook)
                    {
                        string name = entry.Key;
                        string phone = entry.Value;
                        Console.WriteLine("\t{0} - {1}", name, phone);
                    }
                }
            }
            Console.ReadLine();
        }*/
    }
}
