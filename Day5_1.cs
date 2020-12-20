using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Advent_of_Code
{
    class Program
    {
        static int getID(string seat)
        {
            int ID = 0;
            int power = 1;
            for(int i = seat.Length-1; i >= 0; i--)
            {
                if(seat[i] == 'B' || seat[i] == 'R')
                {
                    ID += power;
                }
                power *= 2;
            }
            return ID;

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

            int maxID = 0;
            int curr;
            foreach(string line in lines)
            {
                Console.WriteLine(line);
                curr = getID(line);
                if (curr > maxID)
                    maxID = curr;
            }
            Console.WriteLine(maxID);

        }
    }
}
