using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Advent_of_Code
{
    class Program
    {
        static int count_questions(string group)
        {
            HashSet<char> set = new HashSet<char>();
            foreach (char c in group)
                if (c != '\n' && c != ' ')
                    if (set.Contains(c) == false)
                        set.Add(c);
            Console.WriteLine(set.Count);
            return set.Count;
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

            string group = "";
            int sum = 0;
            for(int i = 0; i < lines.Length; i++)
            {
                group += lines[i] + " ";
                if ((lines[i] == "") || (i == (lines.Length - 1)))
                {
                    Console.WriteLine(group);
                    sum += count_questions(group);
                    group = "";
                }
            }
            Console.WriteLine("Sum = " + sum);


        }
    }
}
