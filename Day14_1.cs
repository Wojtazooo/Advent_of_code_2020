using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Advent_of_Code
{
    class Program
    {
        static ulong modify(ulong value, string mask)
        {
            string temp = Convert.ToString((long)value,2);
            char[] bit_array = new char[36];
                
            for(int i = 0;i < 36; i++)
            {
                if(mask[i] != 'X')
                {
                    bit_array[i] = mask[i];
                }
                else
                {
                    if (i <= 35 - temp.Length)
                    {
                        bit_array[i] = '0';
                    }
                    else
                    {
                        bit_array[i] = temp[i - (36 - temp.Length)];
                    }
                }
            }
            string after = new string(bit_array);
            Console.WriteLine("Value: {0,36}", temp);
            Console.WriteLine("Mask:  {0,36}", mask);
            Console.WriteLine("After: {0,36}", after);
            long ret = Convert.ToInt64(after, 2);
            Console.WriteLine("Value before: " + value);
            Console.WriteLine("Value after: " + ret);
            return (ulong)ret;

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

            Dictionary<int, ulong> memory = new Dictionary<int, ulong>();

            string act_mask = "";
            foreach(string line in lines)
            {
                if(line.Contains("mask = "))
                {
                    act_mask = line.Substring("mask = ".Length);
                    Console.WriteLine("mask changed to " + act_mask);
                }
                else
                {
                    int mem_address = int.Parse(line.Substring(line.IndexOf("[") + 1, line.IndexOf("]") - line.IndexOf("[") - 1));
                    ulong mem_value = ulong.Parse(line.Substring(line.IndexOf("=") + 2));
                    ulong modified = modify(mem_value, act_mask);

                    if (memory.ContainsKey(mem_address))
                    {
                        memory[mem_address] = modified;
                    }
                    else
                    {
                        memory.Add(mem_address, modified);
                    }
                }
                Console.WriteLine("-----------------------------------");
            }
            ulong sum = 0;

            foreach(KeyValuePair<int,ulong> pair in memory)
            {
                sum += pair.Value;
            }

            Console.WriteLine("Sum = " + sum);
        }
    }
}