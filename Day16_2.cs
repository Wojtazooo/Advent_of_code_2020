using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Advent_of_Code
{
    class Program
    {
        static void create_matching(string my_ticket, ref Dictionary<int, List<int>> matching, SortedDictionary<int, SortedSet<int>> fields)
        {
            string[] values = my_ticket.Split(',');

            for (int i = 0; i < values.Length; i++)
            {
                int x = int.Parse(values[i]);

                // for each field in my ticket check which fields match their range
                List<int> match_ids = new List<int>();
                foreach(var field in fields)
                {
                    if(field.Value.Contains(x)) // if match add to list
                    {
                        match_ids.Add(field.Key);
                    }
                }

                // add index of field with ids of fields that match
                matching.Add(i, match_ids);
            }
        }

        static void edit_matching(string ticket, ref Dictionary<int, List<int>> matching, SortedDictionary<int, SortedSet<int>> fields)
        {
            string[] values = ticket.Split(',');

            for (int i = 0; i < values.Length; i++)
            {
                int x = int.Parse(values[i]);

                List<int> to_remove = new List<int>();
                foreach(int ind in matching[i])
                {
                    if(fields[ind].Contains(x) == false)
                    {
                        to_remove.Add(ind);
                    }
                }
                foreach(int r in to_remove)
                {
                    matching[i].Remove(r);
                }

            }
        }

        static void simplify(ref Dictionary<int, List<int>> matching)
        {
            // fields to specify
            List<int> to_specify = new List<int>(matching.Keys);
            while(to_specify.Count != 0)
            {
                int act = 0;
                foreach(int x in to_specify)
                {
                    if(matching[x].Count == 1)
                    {
                        act = x;
                        int val = matching[x][0];
                        for(int i = 0; i < matching.Count; i++)
                        {
                            if(i != x)
                                if (matching[i].Contains(val))
                                    matching[i].Remove(val);
                        }
                        break;
                    }
                }
                to_specify.Remove(act);
            }
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

            Regex rx_range = new Regex(@"[0-9]+-[0-9]+");

            // create set of values which are correct
            int ind = 0;
            SortedSet<int> set = new SortedSet<int>();
            
            // fields with their ranges
            var fields = new SortedDictionary<int, SortedSet<int>>();
            var field_names = new SortedDictionary<int, string>();

            for(int i = 0; i < lines.Length; i++)
            {
                if(rx_range.IsMatch(lines[i]))
                {
                    var matches = rx_range.Matches(lines[i]);
                    SortedSet<int> field_range = new SortedSet<int>();    
                    foreach(Match m in matches)
                    {
                        string act = m.Value;
                        int min = int.Parse(act.Substring(0, act.IndexOf("-")));
                        //Console.WriteLine("min = " + min);
                        int max = int.Parse(act.Substring(act.IndexOf("-")+1));
                        //Console.WriteLine("max = " + max);

                        var range = Enumerable.Range(min, max - min + 1);

                        foreach(int x in range)
                        {
                            field_range.Add(x);
                            set.Add(x);
                        }
                    }

                    field_names.Add(i, lines[i].Substring(0, lines[i].IndexOf(":")));
                    fields.Add(i, field_range);
                }
                if(lines[i].Contains("your ticket:"))
                {
                    ind = i;
                    break;
                }
            }

            // add "your ticket" to valid tickets 
            List<string> valid_tickets = new List<string>();
            string my_ticket = lines[ind + 1];
            valid_tickets.Add(my_ticket);
            
            // move to nearby tickets
            while (lines[ind].Contains("nearby tickets") == false) ind++;

            // find valid tickets
            bool is_valid;
            for(int i = ind+1; i < lines.Length; i++)
            {
                is_valid = true;
                string[] values = lines[i].Split(',');
                foreach(string val in values)
                {
                    int x = int.Parse(val);
                    if(set.Contains(x) == false)
                    {
                        is_valid = false;
                        break;
                    }
                }
                if (is_valid) valid_tickets.Add(lines[i]); 
            }

            var matching_fields = new Dictionary<int, List<int>>();
            create_matching(my_ticket, ref matching_fields, fields);
            foreach(string ticket in valid_tickets)
            {
                //Console.WriteLine(ticket);
                edit_matching(ticket, ref matching_fields, fields);
            }
            simplify(ref matching_fields);

            string[] my_ticket_values = my_ticket.Split(",");
            long ret = 1;

            foreach (var m_field in matching_fields)
            {
                if(field_names[m_field.Value[0]].Contains("departure"))
                {
                    Console.WriteLine(field_names[m_field.Value[0]] + " ind: " + m_field.Key + " val: " + my_ticket_values[m_field.Key]);
                    ret *= int.Parse(my_ticket_values[m_field.Key]);
                }
            }

            Console.WriteLine("Return value = " + ret);
        }
    }
}