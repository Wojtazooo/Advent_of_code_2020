using System;
using System.IO;
using System.Collections.Generic;

namespace Advent_of_Code
{
    class Program
    {
        static long modInverse(long a, long n)
        {
            long i = n, v = 0, d = 1;
            while (a > 0)
            {
                long t = i / a, x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= n;
            if (v < 0) v = (v + n) % n;
            return v;
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

            string[] bus_IDs = lines[1].Split(",");
            long M = 1; // all divisors multiplied


            Dictionary<long,long> values = new Dictionary<long, long>();
            for(int i = 0; i < bus_IDs.Length; i++)
            {
                if (bus_IDs[i] != "x")
                {
                    long div = long.Parse(bus_IDs[i]);
                    M *= div;
                    long rest = div - i;
                    if (rest == div) rest -= div;
                    while (rest < 0) rest += div;
                    values.Add(div, rest);
                    Console.WriteLine(rest + " (mod " + div + ")");
                }
            }

            long x = 0;
            foreach(KeyValuePair<long, long> pair in values)
            {
                long b = (M / pair.Key);
                long b2 = modInverse(b, pair.Key);
                x += pair.Value * b * b2;
             }
            Console.WriteLine("Answer = " + x % M);
        }
    }
}