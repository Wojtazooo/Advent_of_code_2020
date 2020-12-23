using System;
using System.IO;
using System.Collections.Generic;

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

            List<int> values = new List<int>();
            values.Add(0); // outlet
            foreach(string line in lines)
            {
                values.Add(int.Parse(line));
            }
            values.Sort();
            values.Add(values[values.Count - 1] + 3); // device's built-in joltage

            int one_jolt_diff = 0;
            int three_jolt_diff = 0;
            for(int i = 1; i < values.Count; i++)
            {
                if (values[i] == (values[i - 1] + 3))
                    three_jolt_diff++;
                else if (values[i] == (values[i - 1] + 1))
                    one_jolt_diff++;
            }

            Console.WriteLine("1-jolt differences = " + one_jolt_diff);
            Console.WriteLine("3-jolt differences = " + three_jolt_diff);
            Console.WriteLine("multiplied = " + one_jolt_diff * three_jolt_diff);
        }
    }
}
