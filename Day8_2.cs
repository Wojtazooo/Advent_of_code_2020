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

            // execute next command
            if (command.Contains("nop")) 
                return (pos + 1, acc);
            else if (command.Contains("acc"))
                return (pos + 1, acc + value);
            else if (command.Contains("jmp"))
                return (pos + value, acc);
            else return (0, 0); // error
        }

        static (bool,int acc_value) check_if_terminates(int start_pos, int start_acc, string[] lines)
        {
            HashSet<int> executed = new HashSet<int>();

            int i = start_pos;
            int acc = start_acc;
            while(true)
            {
                Console.WriteLine(i + ". " + lines[i]);
                if (executed.Add(i) == false) return (false, 0); // ifinite loop
                (i, acc) = next_command(lines[i], acc, i);
                if (i == lines.Length) return (true, acc);
                if (i > lines.Length) return (false, 0); // jmp out
            }
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

            string original_line;
            int acc_save = 0; 
            int acc = 0;
            bool terminates = false;
            int i = 0;
            // we can't use backtracking method here because we can only change one command 
            while (i < lines.Length) 
            {
                // we won't change acc commands -> move to next jmp or nop command
                while (lines[i].Contains("acc")) 
                {
                    (i, acc) = next_command(lines[i], acc, i);
                }

                // try to execute without changes;
                acc_save = acc;
                (terminates, acc) = check_if_terminates(i, acc, lines);
                if (terminates) break;
                acc = acc_save;

                // let's try to swap nop with jmp and check if terminates with this changes
                acc_save = acc;
                original_line = lines[i];
                if(lines[i].Contains("nop"))
                {
                    lines[i] = lines[i].Replace("nop", "jmp");
                    (terminates, acc) = check_if_terminates(i, acc, lines);
                    if (terminates) break;
                }
                else if (lines[i].Contains("jmp"))
                {
                    lines[i] = lines[i].Replace("jmp", "nop");
                    (terminates, acc) = check_if_terminates(i, acc, lines);
                    if (terminates) break;
                }

                // our changes didn't help -> move to next command
                lines[i] = original_line;
                acc = acc_save;
                (i, acc) = next_command(lines[i], acc, i);
            }
            Console.WriteLine("Acc = " + acc);
        }
    }
}
