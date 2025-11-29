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

        private static int GetTableSize(string error = "Количество строк должно быть больше нуля!", string message = "Введите количество строк таблицы: ")
        {
            int size = -1;
            while (size <= 0)
            {
                size = ReadInteger(message); // разбираемся со строками
                if (size <= 0)
                {
                    PrintError(error);
                    size = -1;
                }
            }
            return size;
        }

        /// <summary>
        /// Печатаем красивые сообщения пользователю
        /// </summary>
        /// <param name="message">Сообщение на печать</param>
        /// <param name="color">Цвет печать</param>
        private static void PrintMessage(string message = "Ввод корректен", ConsoleColor color = ConsoleColor.Green)
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
        private static bool CheckEmpty(int[,] table)
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
        private static int ReadInteger(string message = "Введите количество элементов массива:", string error = "Вы не ввели целое число в разрешённом дипазоне!")
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
        private static void PrintError(string error = "Нераспознанная команда! Проверьте корректность ввода")
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
            PrintMessage("Работа начата", ConsoleColor.Cyan);
            stopwatch.Start();
        }

        /// <summary>
        /// Уведомляет о завершении, времени выполнения и прощается
        /// </summary>
        public static void FinishWork()
        {
            PrintMessage("Работа закончена", ConsoleColor.Cyan);
            stopwatch.Stop();
            Console.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");
            Console.WriteLine("До свидания!");
        }

        /// <summary>
        /// Читает массив целых чисел с клавиатуры
        /// </summary>
        /// <returns>Прочитанный массив</returns>
        private static int[,] ReadTable(int[,] readTable)
        {
            int strings, columns;
            strings = GetTableSize();
            columns = GetTableSize("Количество столбцов должно быть больше нуля!", "Введите количество столбцов таблицы: ");
            readTable = new int[strings, columns];

            for (int q = 0; q < strings; q++)
            {
                for (int p = 0; p < columns; p++)
                {
                    readTable[q, p] = ReadInteger("Введите элемент таблицы");
                }
            }
            return readTable;
        }

        /// <summary>
        /// Создаёт массив датчиком случайных чисел
        /// </summary>
        /// <returns>Созданный массив</returns>
        private static int[,] MakeRandomTable(int[,] randomTable)
        {
            int strings, columns;
            strings = GetTableSize();
            columns = GetTableSize("Количество столбцов должно быть больше нуля!", "Введите количество столбцов таблицы: ");
            randomTable = new int[strings, columns];

            for (int q = 0; q < strings; q++)
            {
                for (int p = 0; p < columns; p++)
                {
                    randomTable[q, p] = random.Next(-100, 100);
                }
            }
            return randomTable;
        }

        /// <summary>
        /// Добавляет строку в начало таблицы
        /// </summary>
        /// <param name="table">Таблица куда добавляем</param>
        /// <returns>Изменённая таблица</returns>
        public static int[,] AddString(int[,] table)
        {
            int columns = table.GetLength(1);
            int newString;
            bool isCorrect;
            do
            {
                newString = ReadInteger("Введите количество элементов в добавляемой строке");
                if (newString != columns)
                {
                    PrintError($"Элементов в новой строке должно быть {columns}, чтобы массив не стал рваным!");
                    isCorrect = false;
                }
                else
                {
                    isCorrect = true;
                }
            } while (!isCorrect);

            int strings = table.GetLength(0);
            int[,] result = new int[strings + 1, columns];

            string[] addMenu =
            [
                    "Добавить строку самостоятельно",
                    "Добавить строку случайно"
            ];

            switch (PrintMenu(addMenu, "Выберете способ добавления элементов:"))
            {
                case 1:
                    {
                        for (int p = 0; p < newString; p++)
                        {
                            result[0, p] = ReadInteger("Введите элемент массива: ");
                        }
                        break;
                    }

                case 2:
                    {
                        for (int p = 0; p < newString; p++)
                        {
                            result[0, p] = random.Next(-100, 100);
                        }
                        break;
                    }
            }

            for (int p = 1; p < strings + 1; p++)
            {
                for (int q = 0; q < columns; q++)
                {
                    result[p, q] = table[p - 1, q];
                }
            }
            table = result;
            return table;
        }

        /// <summary>
        /// Датчик случайных чисел
        /// </summary>
        private static Random random = new();

        /// <summary>
        /// Удаляет K строк начиная с номера N в рваном массиве
        /// </summary>
        /// <param name="table">Изменённая таблица</param>
        public static int[][] DeleteStrings(int[][] table)
        {
            bool isCorrect;
            int start;
            do
            {
                int strings = table.GetLength(0);
                start = ReadInteger("Введите номер, с которого начинается удаление строк:");
                if (start < 0)
                {
                    PrintError("Номер строки не может быть отрицательным числом!");
                    isCorrect = false;
                }
                else if (start > strings)
                {
                    PrintError("В массиве меньше строк!");
                    isCorrect = false;
                }
                else
                {
                    isCorrect = true;
                }
            } while (!isCorrect);

            int deleteStrings;
            do
            {
                deleteStrings = ReadInteger("Введите количество строк, которые нужно удалиить:");
                if (deleteStrings < 0)
                {
                    PrintError("Невозможно удалить отрицательное количество строк!");
                    isCorrect = false;
                }
                else if (deleteStrings + start - 1 > table.GetLength(0))
                {
                    PrintError("В массиве меньше строк!");
                    isCorrect = false;
                }
                else
                {
                    isCorrect = true;
                }
            } while (!isCorrect);

            int[][] result = new int[table.GetLength(0) - deleteStrings][];

            for (int p = 0; p < start; p++)
            {
                for (int q = 0; q < CountColumns(table[p]); q++)
                {
                    result[p][q] = table[p][q];
                }
            }

            for (int p = start; p < table.GetLength(0); p++)
            {
                for (int q = 0; q < CountColumns(table[p]); q++)
                {
                    result[p][q] = table[p][q];
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static int CountColumns(int[] str)
        {
            int count = 0;
            foreach (int p in str)
            {
                count++;
            }
            return count;
        }

        /// <summary>
        /// Создаёт многомерный массив
        /// </summary>
        /// <returns>Созданный массив</returns>
        public static int[,] CreateTable(int[,] table)
        {
            string[] arrayMenu =
            [
                    "Создать таблицу самостоятельно",
                    "Создать таблицу случайно"
            ];

            bool isCreated = true;
            do
            {
                switch (PrintMenu(arrayMenu, "Выберете способ создания массива:"))
                {
                    case 1:
                        {
                            table = ReadTable(table);
                            break;
                        }
                    case 2:
                        {
                            table = MakeRandomTable(table);
                            break;
                        }
                }
            } while (!isCreated);
            return table;
        }


        private static int[][] MakeRandomTable(int[][] randomJagged)
        {
            int strings;
            strings = GetTableSize();
            randomJagged = new int[strings][];


            return randomJagged;
        }


        /// <summary>
        /// Создаёт многомерный массив
        /// </summary>
        /// <returns>Созданный массив</returns>
        public static int[][] CreateTable(int[][] jagged)
        {
            string[] arrayMenu =
            [
                    "Создать таблицу самостоятельно",
                    "Создать таблицу случайно"
            ];

            bool isCreated = true;
            do
            {
                switch (PrintMenu(arrayMenu, "Выберете способ создания массива:"))
                {
                    case 1:
                        {
                            jagged = ReadTable(jagged);
                            break;
                        }
                    case 2:
                        {
                            jagged = MakeRandomTable(jagged);
                            break;
                        }
                }
            } while (!isCreated);
            return jagged;
        }

        /// <summary>
        /// Читает массив целых чисел с клавиатуры
        /// </summary>
        /// <returns>Прочитанный массив</returns>
        private static int[][] ReadTable(int[][] jaggedTable)
        {
            int strings, columns;
            strings = GetTableSize();
            jaggedTable = new int[strings][];

            for (int p = 0; p < strings; p++)
            {
                columns = GetTableSize("Количество столбцов должно быть больше нуля!","Введите количество столбцов:");
                jaggedTable[p] = new int[columns];
                
                for (int q = 0; q < columns; q++)
                {
                    jaggedTable[p][q] = random.Next(-100, 100); 
                }
            }
            return jaggedTable;
        }

        /// <summary>
        /// Печатает таблицу
        /// </summary>
        /// <param name="table">Печатаемая таблица</param>
        public static void PrintTable(int[][] table)
        {
            int count = 0;
            int length = table.GetLength(1);
            foreach (int[] item in table)
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

    }
}
