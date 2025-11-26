using System;
using System.Diagnostics;

namespace Library
{
    /// <summary>
    /// Вспомогательные функции
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// Время выполнения программы
        /// </summary>
        private static Stopwatch stopwatch = new();

        public static (int strings, int columns) GetTableSize()
        {
            bool isCorrect;
            int strings = -1;
            int columns = -1;
            do
            {
                while (strings <= 0)
                {
                    strings = ReadInteger("Введите количество строк таблицы: "); // разбираемся со строками
                    if (strings <= 0)
                    {
                        PrintError("Количество строк должно быть больше нуля!");
                    }
                }
                while (columns <= 0)
                {
                    columns = ReadInteger("Введите количество столбцов таблицы: "); // разбираемся со столбцами
                    if (columns <= 0)
                    {
                        PrintError("Количество столбцов должно быть больше нуля!");
                    }
                }

                isCorrect = CheckTableSize(strings, columns);
                if (!isCorrect)
                {
                    strings = -1;
                    columns = -1;
                }

            } while (!isCorrect);
            return (strings, columns);
        }

        /// <summary>
        /// Проверяет переполнение памяти таблицей
        /// </summary>
        /// <param name="strings">Количество строк таблицы</param>
        /// <param name="columns">Количество столбцов таблицы</param>
        /// <returns>True если размеры верные</returns>
        private static bool CheckTableSize(int strings, int columns)
        {
            bool isCorrectTableSize;
            try
            {
                int[] stringsArray = new int[strings];
                int[] columnsArray = new int[columns];
                PrintMessage();
                isCorrectTableSize = true;
            }
            catch (OutOfMemoryException)
            {
                PrintError("Переполнение памяти слишком большим массивом!");
                isCorrectTableSize = false;
            }
            return isCorrectTableSize;
        }

        /// <summary>
        /// Печатаем красивые сообщения пользователю
        /// </summary>
        /// <param name="message">Сообщение на печать</param>
        /// <param name="color">Цвет печать</param>
        public static void PrintMessage(string message = "Ввод корректен", ConsoleColor color = ConsoleColor.Green)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Печатает таблицу
        /// </summary>
        /// <param name="table">Печатаемая таблица</param>
        public static void PrintTable(int[,] table)
        {
            if (CheckEmpty(table))
            {
                PrintMessage("Таблица пустая", ConsoleColor.Cyan);
                return;
            }

            int count = 0;
            int length = table.GetLength(1);
            foreach (int item in table)
            {
                Console.Write(item + " ");
                count++;
                if (count == length) // чтобы это выглядело по-людски построчно а не по сишарповски
                {
                    Console.WriteLine();
                    count = 0;
                }
            }
        }

        /// <summary>
        /// Проверяет таблицу на пустоту
        /// </summary>
        /// <param name="table">Проверяемая таблица</param>
        /// <returns>True если пустая</returns>
        public static bool CheckEmpty(int[,] table)
        {
            return table.Length == 0;
        }

        /// <summary>
        /// Печатает меню и принимает выбор пользователя
        /// </summary>
        /// <param name="menu">Массив возможных действий</param>
        /// <returns>Выбранное действие</returns>
        public static uint PrintMenu(string[] menu, string message = "Программа реализует следующую функциональность: ")
        {
            uint action;
            string? choice;
            do
            {
                bool isCorrectAction;
                do
                {
                    PrintMessage(message);
                    for (int p = 0; p < menu.Length; p++)
                    {
                        Console.WriteLine($"  {p + 1} " + menu[p]);
                    }

                    Console.Write("Введите номер выбранного действия: ");
                    isCorrectAction = uint.TryParse(Console.ReadLine(), out action);

                    if (action > menu.Length || action == 0)
                    {
                        PrintError();
                        isCorrectAction = false;
                    }
                } while (!isCorrectAction);

                Console.WriteLine("Вы выбрали дейстиве: " + menu[action - 1]);
                Console.WriteLine("Вы уверены в своём выборе? Если уверены, напишите ДА(в любом регистре), любой другой ввод будет воспринят как НЕТ:");
                choice = Console.ReadLine();

            } while (!string.Equals(choice, "Да", StringComparison.OrdinalIgnoreCase));

            PrintMessage("Приступаю к выполнению команды");
            return action;
        }

        /// <summary>
        /// Читает целое число и сообщает об ошибках ввода оного
        /// </summary>
        /// <param name="message">Приглашение к нужному вводу</param>
        /// <param name="error">Уведомление об ошибочном вводе</param>
        /// <returns>Прочитанное число</returns>
        public static int ReadInteger(string message = "Введите количество элементов массива:", string error = "Вы не ввели целое число в разрешённом дипазоне!")
        {
            bool isNumber;
            int number;
            do
            {
                Console.WriteLine(message);

                isNumber = int.TryParse(Console.ReadLine(), out number);
                if (!isNumber)
                {
                    PrintError(error);
                }

            } while (!isNumber);
            return number;
        }

        /// <summary>
        /// Сообщает об ошибках
        /// </summary>
        /// <param name="error">Печатаемая ошибка</param>
        public static void PrintError(string error = "Нераспознанная команда! Проверьте корректность ввода")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ошибка: " + error);
            Console.ResetColor();
        }


        /// <summary>
        /// Здоровается, начинает работу
        /// </summary>
        public static void StartWork()
        {
            Console.WriteLine("Здравствуйте!");
            Helper.PrintMessage("Работа начата", ConsoleColor.Cyan);
            stopwatch.Start();
        }

        /// <summary>
        /// Уведомляет о завершении, времени выполнения и прощается
        /// </summary>
        public static void FinishWork()
        {
            Helper.PrintMessage("Работа закончена", ConsoleColor.Cyan);
            stopwatch.Stop();
            Console.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");
            Console.WriteLine("До свидания!");
        }
    }
}
