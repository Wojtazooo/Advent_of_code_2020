using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Advent_of_Code
{
    class Program
    {
        static (int n_position, int n_acc) next_command(string command, int acc, int pos)
        { 
            // read value
            Regex rx_value = new Regex(@"[+-][0-9]+");
            string s_value = rx_value.Match(command).ToString();
            int value = int.Parse(s_value.Substring(1));
            if (s_value[0] == '-')
                value *= -1;

            if (command.Contains("nop")) 
                return (pos + 1, acc);
            else if (command.Contains("acc"))
                return (pos + 1, acc + value);
            else if (command.Contains("jmp"))
                return (pos + value, acc);
            else return (0, 0); // error
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


            
            HashSet<int> executed = new HashSet<int>();

            int acc = 0;
            int last = 0;
            for(int i = 0; i < lines.Length;)
            {
                Console.WriteLine(lines[i]);
                if (executed.Add(i) == false) // line repeated
                { 
                    break;
                }
                last = i;
                (i, acc) = next_command(lines[i], acc, i);               
            }

            Console.WriteLine("Acc = " + acc);
        }
    }
}
