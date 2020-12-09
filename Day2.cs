using System;
using System.IO;
using System.Collections.Generic;

namespace Advent_of_Code
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> values = new List<int>();
            string file_name = "text.txt";
            if (!File.Exists(file_name))
            {
                Console.WriteLine("file not exists");
                return;
            }
            string[] lines = File.ReadAllLines(file_name);

            // Part One
            int counter_of_valid = 0;
            int min;
            int max;
            char c;
            string password;
            foreach(string s in lines)
            {
                Console.WriteLine(s);
                // read password policy info
                min = int.Parse(s[0..s.IndexOf('-')]);
                max = int.Parse(s[(s.IndexOf('-') + 1)..s.IndexOf(' ')]);
                c = s[s.IndexOf(':') - 1];
                password = s[(s.IndexOf(':') + 2)..];

                Console.WriteLine($"Check if <{min},{max}> * {c} in {password}");

                int counter = 0;
                foreach(char x in password)
                {
                    if (x == c) counter++;
                }
                if (min <= counter && counter <= max)
                {
                    Console.WriteLine($"Password {password} is valid");
                    counter_of_valid++;
                }
                Console.WriteLine("----------");
            }

            Console.WriteLine($"Found {counter_of_valid} valid passwords");

        }
    }
}
