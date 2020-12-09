using System;
using System.IO;
using System.Collections.Generic;

namespace Advent_of_Code
{
    class Program
    {
        static void Main(string[] args)
        {
            string file_name = "text.txt";
            if (!File.Exists(file_name))
            {
                Console.WriteLine("file not exists");
                return;
            }
            string[] lines = File.ReadAllLines(file_name);

            int x=0;
            int len = lines[0].Length; // all lines have the same length
            int tree_counter = 0;
            foreach(string line in lines)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(line[0..x]);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(line[x]);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(line[(x+1)..] + "\n");
                if (line[x] == '#') tree_counter++;
                x = (x + 3)%len;
            }
            Console.ResetColor();

            Console.WriteLine($"I would encounter {tree_counter} trees");
        }
    }
}
