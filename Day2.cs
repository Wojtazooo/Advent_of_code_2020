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
            // Part Two
            int counter_of_valid_2 = 0;
            
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

                // Part One
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

                // Part Two
                if ((password[min - 1] == c && password[max - 1] != c) || (password[min - 1] != c && password[max-1] == c))
                {
                    Console.WriteLine($"Password {password} valid with the second policy");
                    counter_of_valid_2++;
                }
                Console.WriteLine("----------");
            }


           

            Console.WriteLine($"Part1 - Found {counter_of_valid} valid passwords");
            Console.WriteLine($"Part2 - Found {counter_of_valid_2} valid passwords");

        }
    }
}
