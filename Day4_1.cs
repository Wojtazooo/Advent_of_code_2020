using System;
using System.IO;
using System.Collections.Generic;

namespace Advent_of_Code
{
    class Program
    {
        public static bool is_valid(string passp)
        {
            List<string> required_fieds= new List<string>() { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"};
            foreach(string field in required_fieds)
                if (!passp.Contains(field))
                {
                    Console.WriteLine(passp + " does not contains " + field);
                    return false;
                }
            return true;
            
        }
        static void Main(string[] args)
        {
            string file_name = "text.txt";
            if (!File.Exists(file_name))
            {
                Console.WriteLine("file not exists");
                return;
            }
            string[] lines = File.ReadAllLines(file_name);

            string passp = "";
            int valid_counter = 0;
            
            for(int i = 0; i < lines.Length; i++)
            {
                passp += lines[i];
                if ( (lines[i] == "") || (i == (lines.Length-1)))
                {
                    if (is_valid(passp))
                    {
                        Console.WriteLine("Passport:" + passp + "");
                        valid_counter++;
                    }
                    passp = "";
                }
            }

            Console.WriteLine("Found " + valid_counter + " passports");
        }
    }
}
