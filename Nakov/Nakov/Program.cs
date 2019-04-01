using System;
using System.Collections.Generic;
using System.IO;

namespace Nakov
{
    class Program
    {
        static void Main(string[] args)
        {
            SortingPhoneBook();
        }

        public static void SortingPhoneBook()
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
        }

        public static void SearchingInPhoneBook()
        {

        }
    }
}
