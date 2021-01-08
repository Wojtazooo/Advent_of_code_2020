using System;
using System.IO;
using System.Collections.Generic;

namespace Advent_of_Code
{
    class Program
    {
        static long count(ref Stack<char> exp_stack)
        {
            char? operation = null;
            long value = 0;
            while (exp_stack.Count != 0)
            {
                char act = exp_stack.Pop();
                if (act == ')')
                {
                    return value;
                }
                else if (act == '(') // new expression -> return count()
                {
                    if (operation == null) 
                        value = count(ref exp_stack); 
                    if (operation == '+')
                        value += count(ref exp_stack);
                    else if (operation == '*')
                        value *= count(ref exp_stack);
                }
                else if (act == '*' || act == '+') // change operation
                {
                    operation = act;
                }
                else // act is a value
                {
                    if (operation == null)
                        value = int.Parse(act.ToString());
                    if(operation == '+')
                        value += int.Parse(act.ToString());
                    else if (operation == '*')
                        value *= int.Parse(act.ToString());
                }
            }
            return value;
        }

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

            // evaluate each expression and sum all results
            long sum = 0;
            foreach(string expression in lines)
            {
                Stack<char> exp_stack = new Stack<char>();
                for (int i = expression.Length - 1; i >= 0; i--)
                {
                    if (expression[i] != ' ')
                        exp_stack.Push(expression[i]);
                }
                long ret = count(ref exp_stack);
                Console.WriteLine(expression + " = " + ret);
                sum += ret;
            }
            Console.WriteLine(sum);
        }
    }
}