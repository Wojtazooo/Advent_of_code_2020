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

            const int PREAMBLE = 25;

            long[] values = new long[lines.Length];
            for(int i = 0; i < lines.Length; i++)
            {
                values[i] = long.Parse(lines[i]);
            }

            bool exists;
            int invalid_ind = 0;
            for(int i = PREAMBLE; i < values.Length; i++)
            {
                exists = false;
                for(int j = i-PREAMBLE; j < i; j++)
                {
                    for(int k = j+1; k < i; k++)
                    {
                        if (values[j] + values[k] == values[i])
                        {
                            exists = true;
                        }
                    }
                    if (exists) break;
                }
                if (!exists)
                {
                    Console.WriteLine(values[i] + " doesnt follow the rule");
                    invalid_ind = i;
                    break;
                }
            }
            if (invalid_ind == 0)
            {
                Console.WriteLine("couldnt find invalid element");
                return;
            }


            int first_id = 0;
            int last_id = 0;
            long sum = values[0];
            while(true)
            {
                if (sum == values[invalid_ind]) break;
                else if(sum < values[invalid_ind]) // add next elem to contiguous set
                {
                    last_id++;
                    sum += values[last_id];
                }
                else if(sum > values[invalid_ind]) // remove first elem of contiguous set
                {
                    sum -= values[first_id];
                    first_id++;
                }
            }

            // find min and max in contigous set
            long min = values[first_id], max = values[first_id];
            for(int i = first_id; i <= last_id; i++)
            {
                if (values[i] < min)
                    min = values[i];
                else if (values[i] > max)
                    max = values[i];
            }

            Console.WriteLine("min = " + min);
            Console.WriteLine("max = " + max);
            Console.WriteLine(min + " + " + max + " = " + (min + max));
        }
    }
}
