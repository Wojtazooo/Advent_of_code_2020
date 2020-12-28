using System;
using System.IO;
using System.Collections.Generic;

enum dirs{ N = 0, E = 1, S = 2, W = 3 };

namespace Advent_of_Code
{
    class Program
    {
        static void next_move(string command, ref int x, ref int y, ref int facing)
        {
            Console.WriteLine("before " + command + ": x=" + x + " y=" + y + " facing: " + (dirs)facing);
            int val = int.Parse(command.Substring(1, command.Length - 1));
            if (command[0] == 'R') facing = (facing + val / 90) % 4;
            else if (command[0] == 'L') facing = (facing + 4 - (val / 90)) % 4;
            else if (command[0] == 'N') y += val;
            else if (command[0] == 'E') x += val;
            else if (command[0] == 'W') x -= val;
            else if (command[0] == 'S') y -= val;
            else if (command[0] == 'F')
            { 
                if (facing == 0) y += val;
                else if (facing == 2) y -= val;
                else if (facing == 1) x += val;
                else if (facing == 3) x -= val;
            }
            Console.WriteLine("after " + command + ": x=" + x + " y=" + y + " facing: " + (dirs)facing);
        }
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

            int x_pos = 0;
            int y_pos = 0;
            int facing = 1;

            foreach(string line in lines)
            {
                Console.WriteLine(line);
                next_move(line, ref x_pos, ref y_pos, ref facing);
            }
            Console.WriteLine("-------------------");
            Console.WriteLine("Final: x=" + x_pos + " y=" + y_pos + " facing: " + (dirs)facing);
            Console.WriteLine("Manhattan distance = " + (Math.Abs(x_pos) + Math.Abs(y_pos)));
        }
    }
}