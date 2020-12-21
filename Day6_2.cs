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
            HashSet<char> result_set = new HashSet<char>();
            HashSet<char> next = new HashSet<char>();

            bool first = true;
            foreach(char c in group)
            {
                if(first)
                {
                    if (c != ' ')
                        result_set.Add(c);
                    else
                        first = false;
                }
                else
                {
                    if (c != ' ')
                        next.Add(c);
                    else
                    {
                        if(next.Count != 0)
                            result_set.IntersectWith(next);
                        next.Clear();
                    }
                }
            }

            Console.WriteLine("result set: ");
            foreach(char c in result_set)
            {
                Console.Write(c);
            }
            Console.Write('\n');


            return result_set.Count;
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
                    Console.WriteLine("-----------------------");
                }
            }
            Console.WriteLine("Sum = " + sum);


        }
    }
}
