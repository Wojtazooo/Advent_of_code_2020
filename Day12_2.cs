using System;
using System.IO;
using System.Collections.Generic;

namespace Advent_of_Code
{
    class Program
    {
        static (int x, int y) rotate_vector(int x, int y, int angle)
        {
            angle = angle % 360;
            if (angle < 0) angle += 360; // replace left rotation with right rotation
            Console.WriteLine("angle: " + angle);
            if (angle == 90) return (y, -x);
            if (angle == 180) return (-x, -y);
            if (angle == 270) return (-y, x);
            return (x, y);
        }
        static void next_move(string command, ref int x_pos, ref int y_pos, ref int x_waypoint, ref int y_waypoint)
        {
            Console.WriteLine("before " + command + ": x=" + x_pos + " y=" + y_pos + " waypoint: x="+ x_waypoint + " y=" + y_waypoint);
            int val = int.Parse(command.Substring(1, command.Length - 1));

            // change waypoint
            if (command[0] == 'N') y_waypoint += val;
            else if (command[0] == 'S') y_waypoint -= val;
            else if (command[0] == 'E') x_waypoint += val;
            else if (command[0] == 'W') x_waypoint -= val;

            // rotations
            else if (command[0] == 'R') (x_waypoint, y_waypoint) = rotate_vector(x_waypoint, y_waypoint, val);
            else if (command[0] == 'L') (x_waypoint, y_waypoint) = rotate_vector(x_waypoint, y_waypoint, -1 * val);

            // move forward
            else if (command[0] == 'F')
            {
                x_pos += (x_waypoint * val);
                y_pos += (y_waypoint * val);
            }
            Console.WriteLine("after " + command + ": x=" + x_pos + " y=" + y_pos + " waypoint: x=" + x_waypoint + " y=" + y_waypoint);

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
            int x_waypoint = 10;
            int y_waypoint = 1;
            foreach(string line in lines)
            {
                next_move(line, ref x_pos, ref y_pos, ref x_waypoint, ref y_waypoint);
            }
            Console.WriteLine("-------------------");
            Console.WriteLine("Final: x=" + x_pos + " y=" + y_pos + " waypoint: x=" + x_waypoint + " y=" + y_waypoint);
            Console.WriteLine("Manhattan distance = " + (Math.Abs(x_pos) + Math.Abs(y_pos)));
        }
    }
}