using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Advent_of_Code
{
    class Program
    {
        static int recursively_count_bags(string name, Dictionary<string, Dictionary<string,int>> data)
        {
            int counter = 0;

            if (data.ContainsKey(name))
            {
                foreach (KeyValuePair<string, int> bag in data[name])
                {
                    counter += bag.Value;
                    for(int i = 0; i < bag.Value; i++)
                    {
                        counter += recursively_count_bags(bag.Key, data);
                    }
                }
            }
            return counter;
        }

        static void Main(string[] args)
        {
            // read file
            string file_name = "text.txt";
            if (!File.Exists(file_name))
            {
                Console.WriteLine("file not exists");
                return;
            }
            string[] lines = File.ReadAllLines(file_name);

            Regex rx_rules = new Regex(@"[0-9]+\s\w+\s\w+");
            Regex rx_how_many = new Regex(@"[0-9]+");
            Regex rx_name = new Regex(@"[^0-9\s]\w+\s\w+");

            Dictionary<string, Dictionary<string, int>> data = new Dictionary<string, Dictionary<string, int>>();
            string name_of_bag,rules;
            for(int i = 0; i < lines.Length; i++)
            {
                name_of_bag = lines[i].Substring(0, lines[i].IndexOf("bags") - 1);
                rules = lines[i].Remove(0, lines[i].IndexOf("bags contain") + "bags contain".Length + 1);
                
                // create dictionary from rules
                Dictionary<string, int> curr_rules = new Dictionary<string, int>();
                MatchCollection matches = rx_rules.Matches(rules);
                foreach (Match x in matches)
                {
                    string curr = x.ToString();
                    int how_many = int.Parse(rx_how_many.Match(curr).ToString());
                    string name_of_subbag = rx_name.Match(curr).ToString();
                    curr_rules.Add(name_of_subbag, how_many);
                }
                data.Add(name_of_bag, curr_rules);
            }

            // print all bags
            foreach(KeyValuePair<string, Dictionary<string,int>> bag in data)
            {
                Console.WriteLine("Name of bag: " + bag.Key);
                foreach (KeyValuePair<string, int> rule in bag.Value)
                {
                    Console.WriteLine(" - " + rule.Value + " x " + rule.Key);
                }
                Console.WriteLine("--------------------------");
            }
            Console.WriteLine(recursively_count_bags("shiny gold", data));
        }
    }
}
