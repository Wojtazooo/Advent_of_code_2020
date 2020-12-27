using System;
using System.IO;

namespace Advent_of_Code
{
    class Program
    {
        static void print_seat_layout(string[] seat_layout)
        {
            foreach(string line in seat_layout)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("-------------------------");
        }

        static int count_occupied(string[] seat_layout)
        {
            int occupied = 0;
            foreach(string line in seat_layout)
                foreach(char c in line)
                    if(c == '#')
                        occupied++;
            return occupied;
        }

        static int next_round(string[] seat_layout)
        {
            int rows = seat_layout.Length;
            int cols = seat_layout[0].Length;
            int changes = 0;

            string[] after_changes = new string[rows];
            seat_layout.CopyTo(after_changes, 0);

            for (int i = 0; i < rows; i++)
            {
                char[] act_line = seat_layout[i].ToCharArray(); // take next line to edit
                for(int j = 0; j < cols; j++)
                {
                    if (seat_layout[i][j] == '.') continue;
                    int neighbours = count_neighbours(i, j, seat_layout);
                    if (seat_layout[i][j] == 'L' && neighbours == 0)
                    {
                        act_line[j] = '#';
                        changes++;
                    }
                    else if(seat_layout[i][j] == '#' && neighbours >= 4)
                    {
                        act_line[j] = 'L';
                        changes++;
                    }
                }
                after_changes[i] = new string(act_line);
            }
            
            after_changes.CopyTo(seat_layout, 0);
            return changes;
        }

        static int next_round2(string[] seat_layout)
        {
            int rows = seat_layout.Length;
            int cols = seat_layout[0].Length;
            int changes = 0;

            string[] after_changes = new string[rows];
            seat_layout.CopyTo(after_changes, 0);

            for (int i = 0; i < rows; i++)
            {
                char[] act_line = seat_layout[i].ToCharArray(); // take next line to edit
                for (int j = 0; j < cols; j++)
                {
                    if (seat_layout[i][j] == '.') continue;
                    int neighbours = count_neighbours2(i, j, seat_layout);
                    if (seat_layout[i][j] == 'L' && neighbours == 0)
                    {
                        act_line[j] = '#';
                        changes++;
                    }
                    else if (seat_layout[i][j] == '#' && neighbours >= 5)
                    {
                        act_line[j] = 'L';
                        changes++;
                    }
                }
                after_changes[i] = new string(act_line);
            }

            after_changes.CopyTo(seat_layout, 0);
            return changes;
        }

        static int count_neighbours(int row, int col, string[] seat_layout)
        {
            int rows = seat_layout.Length;
            int cols = seat_layout[0].Length;
            int neighbours = 0;

            for (int i = row - 1; i <= row + 1; i++)
            {
                if (i >= 0 && i <= rows - 1)
                {
                    for(int j = col - 1; j <= col + 1; j++)
                    {
                        if(j >= 0 && j <= cols -1)
                        {
                            if(!(i == row && j == col) && seat_layout[i][j] == '#')
                            {
                                neighbours++;
                            }
                        }
                    }
                }
            }
            return neighbours;
        }

        static int count_neighbours2(int row, int col, string[] seat_layout)
        {
            int rows = seat_layout.Length;
            int cols = seat_layout[0].Length;
            int neighbours = 0;

            int i = row;
            int j = col;
            // left
            while(--j >= 0)
            {
                if (seat_layout[i][j] == '#')
                {
                    neighbours++;
                    break;
                }
                else if (seat_layout[i][j] == 'L')
                    break;
            }
            // right
            i = row;
            j = col;
            while (++j < cols)
            {
                if (seat_layout[i][j] == '#')
                {
                    neighbours++;
                    break;
                }
                else if (seat_layout[i][j] == 'L')
                    break;
            }
            // up
            i = row;
            j = col;
            while (--i >= 0)
            {
                if (seat_layout[i][j] == '#')
                {
                    neighbours++;
                    break;
                }
                else if (seat_layout[i][j] == 'L')
                    break;
            }
            // down
            i = row;
            j = col;
            while (++i < rows)
            {
                if (seat_layout[i][j] == '#')
                {
                    neighbours++;
                    break;
                }
                else if (seat_layout[i][j] == 'L')
                    break;
            }
            // left up
            i = row;
            j = col;
            while (--j >= 0 && --i >=0)
            {
                if (seat_layout[i][j] == '#')
                {
                    neighbours++;
                    break;
                }
                else if (seat_layout[i][j] == 'L')
                    break;
            }
            // right up
            i = row;
            j = col;
            while (++j < cols && --i >= 0)
            {
                if (seat_layout[i][j] == '#')
                {
                    neighbours++;
                    break;
                }
                else if (seat_layout[i][j] == 'L')
                    break;
            }
            // left down
            i = row;
            j = col;
            while (--j >= 0 && ++i < rows)
            {
                if (seat_layout[i][j] == '#')
                {
                    neighbours++;
                    break;
                }
                else if (seat_layout[i][j] == 'L')
                    break;
            }
            // right down
            i = row;
            j = col;
            while (++j < cols && ++i < rows)
            {
                if (seat_layout[i][j] == '#')
                {
                    neighbours++;
                    break;
                }
                else if (seat_layout[i][j] == 'L')
                    break;
            }
            return neighbours;
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
            string[] seat_layout = File.ReadAllLines(file_name);

            // do until chaos stabilizes
            while (next_round2(seat_layout) > 0) { }

            print_seat_layout(seat_layout);

            Console.WriteLine("Occupied = " + count_occupied(seat_layout));
            
        }
    }
}
