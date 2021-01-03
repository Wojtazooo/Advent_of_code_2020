using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Advent_of_Code
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "16,1,0,18,12,14,19";
            string[] values = input.Split(',');

            Dictionary<int, int> memory = new Dictionary<int, int>();

            int round_nr = 1;
            foreach (string s in values)
            {
                int val = int.Parse(s);
                memory.Add(val, round_nr);
                round_nr++;
            }
            
            int last = int.Parse(values[values.Length - 1]);
            memory.Remove(last);

            for (int i = round_nr - 1; i < 2020; i++)
            {
                int next = 0;

                if (memory.ContainsKey(last))
                {
                    next = i - memory[last];
                    memory[last] = i;
                }
                else
                {
                    memory.Add(last, i);
                }
                last = next;
                Console.WriteLine("Round: " + (i + 1) + " -> " + next);
            }

            Console.WriteLine("last: " + last);

        }
    }
}