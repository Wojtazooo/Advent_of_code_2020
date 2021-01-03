using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

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

            Regex rx_range = new Regex(@"[0-9]+-[0-9]+");

            // create list of values which are correct
            int ind = 0;
            var list = Enumerable.Range(0, 0);
            for(int i = 0; i < lines.Length; i++)
            {
                if(rx_range.IsMatch(lines[i]))
                {
                    var matches = rx_range.Matches(lines[i]);
                    foreach(Match m in matches)
                    {
                        string act = m.Value;
                        int min = int.Parse(act.Substring(0, act.IndexOf("-")));
                        //Console.WriteLine("min = " + min);
                        int max = int.Parse(act.Substring(act.IndexOf("-")+1));
                        //Console.WriteLine("max = " + max);

                        list = list.Concat(Enumerable.Range(min, max - min + 1));
                    }
                }
                if(lines[i].Contains("nearby tickets:"))
                {
                    ind = i;
                    break;
                }
            }

            // sum incorrect values in nearby tickets
            int sum = 0;
            for(int i = ind+1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');
                foreach(string val in values)
                {
                    int x = int.Parse(val);
                    if(list.Contains(x) == false)
                    {
                        sum += int.Parse(val);
                    }
                }
            }
            Console.WriteLine("sum = " + sum);
             


        }
    }
}