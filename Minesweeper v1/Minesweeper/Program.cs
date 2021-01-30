using System;

namespace Minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выберите уровень сложности:");
            Console.WriteLine("Easy, Normal, Hard");
            Difficulty difficulty = (Difficulty)Enum.Parse(typeof(Difficulty), Console.ReadLine(), ignoreCase: true);
            Console.Clear();
            Field table = new Field(difficulty);
            Game.PrintField(table._Field);

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Введите высоту поля:");
                int a = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите ширину поля:");
                int b = Convert.ToInt32(Console.ReadLine());

                Console.Clear();

                table.Opening(a - 1, b - 1);
                Game.PrintField(table.GamingField);
                if (!table.IsGaming)
                {
                    if (table.IsWin == false)
                    {
                        Console.WriteLine("Вы проиграли!");
                    }
                    else
                    {
                        Console.WriteLine("Вы выиграли!");
                    }
                    Console.WriteLine("Хотите начать заново? [y,n] ");
                    string startNewGame = Console.ReadLine();
                    if (startNewGame == "y")
                    {
                        Console.Clear();
                        Console.WriteLine("Выберите уровень сложности:");
                        Console.WriteLine("Easy, Normal, Hard");
                        difficulty = (Difficulty)Enum.Parse(typeof(Difficulty), Console.ReadLine(), ignoreCase: true);

                        table.NewGame(difficulty);
                        Console.Clear();
                        Game.PrintField(table._Field);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}
