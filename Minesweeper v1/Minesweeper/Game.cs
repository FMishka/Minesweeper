using System;
using System.Collections.Generic;
using System.Text;

namespace Minesweeper
{
    static class Game
    {
        static public void PrintField(in string[,] field)
        {

            Console.Write("   ");
            for (int i = 0; i < field.GetUpperBound(1) + 1; i++)
            {
                Console.Write($"{i + 1,3}");
            }
            Console.WriteLine();
            for (int i = 0; i < field.GetUpperBound(0) + 1; i++)
            {
                Console.Write($"{i + 1,3} ");
                for (int j = 0; j < field.GetUpperBound(1) + 1; j++)
                {
                    if (field[i, j] != null)
                        Console.Write($"{field[i, j],2} ");
                    else
                        Console.Write("   ");
                }
                Console.WriteLine();
            }

        }
        static public int CountMines(in string[,] field)
        {
            int countMines = 0;
            for (int i = 0; i < field.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < field.GetUpperBound(1) + 1; j++)
                {
                    if (field[i, j] == "*")
                    {
                        countMines++;
                    }
                }
            }
            return countMines;
        }
    }
}
