using System;
using System.IO;
using System.Collections.Generic;

namespace Advent_of_Code
{
    class Program
    {
        static int Count_trees(string[] lines, int right, int down, bool show = false)
        {
            int x = 0;
            int y = 0;
            int len = lines[0].Length; // all lines have the same length
            int tree_counter = 0;
            if (show)
                Console.WriteLine($"Slope for right: {right}, {down}");
            for (int i = 0; i < lines.Length; i = i + down)
            {
                if(show)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(lines[i][0..x]);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(lines[i][x]);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(lines[i][(x + 1)..] + "\n");
                }
                if (lines[i][x] == '#') tree_counter++;
                x = (x + right) % len;
            }
            if(show)
                Console.ResetColor();
            return tree_counter;
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

            List<Tuple<int, int>> par = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(1,1),
                new Tuple<int, int>(3,1),
                new Tuple<int, int>(5,1),
                new Tuple<int, int>(7,1),
                new Tuple<int, int>(1,2)
            };

            bool show = false; // show slopes
            int answer = 1;
            foreach (Tuple<int,int> t in par)
            {
                int trees = Count_trees(lines, t.Item1, t.Item2, show);
                Console.WriteLine($"Right: {t.Item1} Down: {t.Item2} -> {trees}");
                answer *= trees;
            }

            Console.WriteLine($"Answer is {answer}");
        }
    }
}
