using System;
using System.IO;
using System.Collections.Generic;

namespace Advent_of_Code
{
    class Program
    {
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

            string[] bus_IDs = lines[1].Split(",");
            List<int> in_service = new List<int>();
            foreach (string bus_ID in bus_IDs)
            {
                if(bus_ID != "x")
                {
                    in_service.Add(int.Parse(bus_ID));
                }
            }

            int earliest_timestamp = int.Parse(lines[0]);
            int min_time = int.MaxValue;
            int min_bus_id = int.MaxValue;
            foreach (int act_bus in in_service)
            {
                int waiting_time = (((earliest_timestamp / act_bus) + 1) * act_bus) - earliest_timestamp;
                Console.WriteLine("Bus " + act_bus + " waiting time: " + waiting_time);
                if (waiting_time < min_time)
                {
                    min_time = waiting_time;
                    min_bus_id = act_bus;
                }
            }
            Console.WriteLine("The least waiting time: " + min_time + " busID: " + min_bus_id);
            Console.WriteLine("Bus ID multiplied by the number of minutes = " + (min_time * min_bus_id));

        }
    }
}