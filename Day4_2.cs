using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Advent_of_Code
{
    class Program
    {
        public static bool is_valid(string passp)
        {
            // PART ONE
            List<string> required_fieds = new List<string>() { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
            foreach (string field in required_fieds)
                if (!passp.Contains(field))
                {
                    Console.ForegroundColor = System.ConsoleColor.Red;
                    Console.WriteLine("doesnt contain "+field);
                    Console.ResetColor();
                    return false;
                }
            
            // PART TWO
            Regex r_byr = new Regex("byr:(19[2-9][0-9]|200[0-2])[\" \"]");
            Regex r_iyr = new Regex("iyr:20((20)|(1[0-9]))[\" \"]");
            Regex r_eyr = new Regex("eyr:20(2[0-9]|30)[\" \"]");
            Regex r_hgt = new Regex("hgt:((1([5-8][0-9]|9[0-3])cm)|(59|[6][0-9]|7[0-6])(in))[\" \"]");
            Regex r_hcl = new Regex("hcl:#([0-9a-f]){6}[\" \"]");
            Regex r_ecl = new Regex("ecl:amb|blu|brn|gry|grn|hzl|oth[\" \"]");
            Regex r_pid = new Regex("pid:[0-9]{9}[\" \"]");

            List<Regex> requirements = new List<Regex>() { r_byr, r_iyr, r_eyr, r_hgt, r_hcl, r_ecl, r_pid };

            foreach(Regex r in requirements)
            {
                if(r.IsMatch(passp) == false)
                {
                    Console.ForegroundColor = System.ConsoleColor.Red;
                    Console.WriteLine(r.ToString() + " doesnt match");
                    Console.ResetColor();
                    return false;
                }
            }
            return true;
        }
        static void Main(string[] args)
        {
            Console.ForegroundColor = System.ConsoleColor.White;
            string file_name = "text.txt";
            if (!File.Exists(file_name))
            {
                Console.WriteLine("file not exists");
                return;
            }
            string[] lines = File.ReadAllLines(file_name);

            string passp = "";
            int valid_counter = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                passp += lines[i];
                passp += " ";
                if ((lines[i] == "") || (i == (lines.Length - 1)))
                {
                    if (is_valid(passp))
                    {
                        Console.ForegroundColor = System.ConsoleColor.Green;
                        Console.WriteLine(passp);
                        Console.ResetColor();
                        valid_counter++;
                    }
                    else
                    {
                        Console.ForegroundColor = System.ConsoleColor.Red;
                        Console.WriteLine(passp);
                        Console.ResetColor();
                    }
                    passp = "";
                    Console.WriteLine("---------------------------------");
                }
            }
            Console.WriteLine("Found " + valid_counter + " passports");
        }
    }
}
