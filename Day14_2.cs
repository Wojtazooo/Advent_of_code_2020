using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Advent_of_Code
{
    class Program
    {
        static void next_address(ref HashSet<ulong> set, string add)
        {
            if (add.Contains('X'))
            {
                StringBuilder s0 = new StringBuilder(add);
                StringBuilder s1 = new StringBuilder(add);
                s0[add.IndexOf('X')] = '0';
                s1[add.IndexOf('X')] = '1';
                next_address(ref set, s0.ToString());
                next_address(ref set, s1.ToString());
            }
            else
            {
               set.Add((ulong)Convert.ToInt64(add, 2));
            }
        }

        static HashSet<ulong> matching_addresses(int add, string mask)
        {
            HashSet<ulong> matching = new HashSet<ulong>();

            string temp = Convert.ToString((long)add, 2);

            StringBuilder sb = new StringBuilder();
            sb.Append('0', 36 - temp.Length);
            sb.Append(temp);

            Console.WriteLine("Value:  {0,36}", sb);
            Console.WriteLine("Mask:   {0,36}", mask);

            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i] == '1') sb[i] = '1';
                else if (mask[i] == 'X') sb[i] = 'X';
            }

            Console.WriteLine("result: {0,36}", sb);

            next_address(ref matching, sb.ToString());
            return matching;
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

            Dictionary<ulong, ulong> memory = new Dictionary<ulong, ulong>();

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

                    HashSet<ulong> matching = matching_addresses(mem_address, act_mask);

                    foreach(ulong x in matching)
                    {
                        if(memory.ContainsKey(x))
                        {
                            memory[x] = mem_value;
                        }
                        else
                        {
                            memory.Add(x, mem_value);
                        }
                    }

                }
                Console.WriteLine("-----------------------------------");
            }
            ulong sum = 0;

            foreach(KeyValuePair<ulong,ulong> pair in memory)
            {
                sum += pair.Value;
            }

            Console.WriteLine("Sum = " + sum);
        }
    }
}