using System;
using System.IO;
using System.Collections.Generic;

namespace Advent_of_Code
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> values = new List<int>();
            string file_name = "text.txt";
            if (!File.Exists(file_name))
            {
                Console.WriteLine("file not exists");
                return;
            }
            string[] lines = File.ReadAllLines(file_name);

            foreach(string s in lines)
            {
                values.Add(int.Parse(s));
            }

            for(int i = 0; i < values.Count; i++) // o(n^3)
            {
                for(int j = i; j < values.Count; j++)
                {
                    if (values[i] + values[j] == 2020)
                    {
                        Console.WriteLine($"{values[i]} + {values[j]} = 2020");
                        Console.WriteLine($"{values[i]} * {values[j]} = {values[i] * values[j]}");
                    }
                    for (int k = j; k < values.Count; k++)
                    {
                        if((values[k] + values[i] + values[j]) == 2020)
                        {
                            Console.WriteLine($"{values[i]} + {values[j]} + {values[k]} = 2020");
                            Console.WriteLine($"{values[i]} * {values[j]} * {values[k]} = {values[i] * values[j] * values[k]}");
                        }
                    }
                }
            }
        }
    }
}
