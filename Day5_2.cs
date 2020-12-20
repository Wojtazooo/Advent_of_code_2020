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

            SortedList<int,int> IDs = new SortedList<int,int>();
               
            foreach(string line in lines)
            {
                int curr = getID(line);
                IDs.Add(curr,curr);
            }

            foreach (KeyValuePair<int, int> i in IDs)
            {
                Console.WriteLine(i.Key);
            }

            int last = 0;
            bool first = true;
            foreach(KeyValuePair<int,int> i in IDs)
            { 
                if(first == true)
                {
                    first = false;
                }
                else
                {
                    if ((i.Key - 2) == last)
                        Console.WriteLine("your seat has ID = " + (i.Key - 1));
                }
                last = i.Key;
            }
        }
    }
}
