using System;
using System.IO;
using System.Collections.Generic;

namespace Advent_of_Code
{
    class Program
    {
        static void count(List<int> values)
        {
            long[] arr = new long[values.Count];
            arr[0] = arr[1] = arr[2] = 1;

            if (values[2] - values[0] <= 3) arr[2] += arr[0];
            for(int i = 3; i < arr.Length; i++)
            {
                arr[i] = arr[i - 1];
                if (values[i] - values[i - 2] <= 3) arr[i] += arr[i - 2];
                if (values[i] - values[i - 3] <= 3) arr[i] += arr[i - 3];
            }
            foreach(long x in arr)
            {
                Console.WriteLine(x);
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

            List<int> values = new List<int>();
            values.Add(0); // outlet
            foreach (string line in lines)
            {
                values.Add(int.Parse(line));
            }
            values.Sort();
            values.Add(values[values.Count - 1] + 3); // device's built-in joltage

            count(values);

        }
    }
}