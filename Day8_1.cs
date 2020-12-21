using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Advent_of_Code
{
    class Program
    {
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


            Regex rx_value = new Regex(@"[+-][0-9]+");
            HashSet<int> executed = new HashSet<int>();

            int acc = 0;
            for(int i = 0; i < lines.Length;i++)
            {
                Console.WriteLine(lines[i]);
                if (executed.Add(i) == false) break;
                if (lines[i].Contains("nop")) continue;
                
                // read value
                string s_value = rx_value.Match(lines[i]).ToString();
                int value = int.Parse(s_value.Substring(1));
                if (s_value[0] == '-')
                    value *= -1;
                Console.WriteLine(value);

                if (lines[i].Contains("acc"))
                    acc += value;
                else if (lines[i].Contains("jmp"))
                    i += (value - 1);
            }

            Console.WriteLine("Acc = " + acc);

        }
    }
}
