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
            Console.Write(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Проверяет таблицу на пустоту
        /// </summary>
        /// <param name="matrix">Проверяемая таблица</param>
        /// <returns>True если пустая</returns>
        private static bool CheckEmpty(int[,] matrix)
        {
            return matrix.Length == 0;
        }

        /// <summary>
        /// Проверяет таблицу на пустоту
        /// </summary>
        /// <param name="jagged">Проверяемая таблица</param>
        /// <returns>True если пустая</returns>
        private static bool CheckEmpty(int[][] jagged)
        {
            return jagged.Length == 0;
        }

        /// <summary>
        /// Печатает меню и принимает выбор пользователя
        /// </summary>
        /// <param name="menu">Массив возможных действий</param>
        /// <returns>Выбранное действие</returns>
        public static uint PrintMenu(string[] menu, string message = "Программа реализует следующую функциональность:")
        {
            uint action;
            string? choice;
            do
            {
                bool isCorrectAction;
                do
                {
                    PrintMessage(message + '\n');
                    for (uint p = 0; p < menu.Length; p++)
                    {
                        PrintMessage($"  {p + 1} " + menu[p] + '\n' + '\n', ConsoleColor.White);
                    }

                    PrintMessage("Введите номер выбранного действия:  ", ConsoleColor.White);
                    isCorrectAction = uint.TryParse(Console.ReadLine(), out action);

                    if (action > menu.Length || action == 0)
                    {
                        PrintError();
                        isCorrectAction = false;
                    }
                } while (!isCorrectAction);

                PrintMessage("Вы выбрали дейстиве: " + menu[action - 1] + '\n', ConsoleColor.White);
                PrintMessage("Вы уверены в своём выборе? Если уверены, напишите ДА(в любом регистре), любой другой ввод будет воспринят как НЕТ:  ", ConsoleColor.White);
                choice = Console.ReadLine();

            } while (!string.Equals(choice, "Да", StringComparison.OrdinalIgnoreCase));

            PrintMessage("Приступаю к выполнению команды" + '\n');
            return action;
        }

        /// <summary>
        /// Читает целое число и сообщает об ошибках ввода оного
        /// </summary>
        /// <param name="message">Приглашение к нужному вводу</param>
        /// <param name="error">Уведомление об ошибочном вводе</param>
        /// <returns>Прочитанное число</returns>
        private static int ReadInteger(string message = "Введите количество элементов массива:  ", string error = "Вы не ввели целое число в разрешённом дипазоне!")
        {
            bool isNumber;
            int number;
            do
            {
                PrintMessage(message, ConsoleColor.White);

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
            PrintMessage("Здравствуйте!" + '\n', ConsoleColor.White);
            PrintMessage("Работа начата" + '\n', ConsoleColor.White);
            stopwatch.Start();
        }

        /// <summary>
        /// Уведомляет о завершении, времени выполнения и прощается
        /// </summary>
        public static void FinishWork()
        {
            PrintMessage("Работа закончена", ConsoleColor.White);
            stopwatch.Stop();
            PrintMessage($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс" + '\n');
            PrintMessage("До свидания!");
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
        /// Удаляет строки из рваного массива
        /// </summary>
        /// <param name="jagged">Массив, из которого надо удалить</param>
        /// <returns></returns>
        public static int[][] DeleteStrings(int[][] jagged)
        {
            bool isCorrect;
            
            int start;
            do
            {
                int strings = jagged.GetLength(0);
                start = ReadInteger("Введите индекс, с которого начинается удаление строк:");
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

            int delete;
            do
            {
                delete = ReadInteger("Введите количество строк, которые нужно удалиить:");
                if (delete < 0)
                {
                    PrintError("Невозможно удалить отрицательное количество строк!");
                    isCorrect = false;
                }
                else if (delete + start - 1 > jagged.GetLength(0))
                {
                    PrintError("В массиве меньше строк!");
                    isCorrect = false;
                }
                else
                {
                    isCorrect = true;
                }
            } while (!isCorrect);

            int[][] result = new int[jagged.GetLength(0) - delete][]; // сколько строк было - сколько надо удалить

           // if (start +  delete > table.GetLength(0)) // TODO: придумать адекватное услвоие
            for (int p = 0; p < start; p++)
            {
                for (int q = 0; q < jagged[p].Length; q++)
                {
                    result[p][q] = jagged[p][q];
                }
            }

            for (int p = start; p < jagged.GetLength(0); p++)
            {
                for (int q = 0; q < jagged[p].Length; q++)
                {
                    result[p][q] = jagged[p][q];
                }
            }
            jagged = result;
            return jagged;
        }

        #region Создание массива
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
                switch (PrintMenu(arrayMenu, "Выберете способ создания массива:  "))
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

        /// <summary>
        /// Создаёт рваный массив
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
        /// Создаёт рваный массив датчиком случайных чисел
        /// </summary>
        /// <param name="randomJagged">Создаваемый массив</param>
        /// <returns>Созданный массив</returns>
        private static int[][] MakeRandomTable(int[][] randomJagged)
        {
            PrintMessage("Введите количество строк:", ConsoleColor.White);
            int strings = GetTableSize();
            randomJagged = new int[strings][];
            for (uint p = 0; p < strings; p++)
            {
                int columns = GetTableSize("Количество столбцов должно быть большу нуля!", "Введите количество столбцов таблицы");
                randomJagged[p] = new int[columns];

                for (uint q = 0; q < columns; q++)
                {
                    randomJagged[p][q] = random.Next(int.MinValue, int.MaxValue);
                }
            }
            return randomJagged;
        }

        /// <summary>
        /// Создаёт двумерный массив датчиком случайных чисел
        /// </summary>
        /// <param name="randomTable">Создаваемый массив</param>
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
        /// Читает рваный массив целых чисел с клавиатуры
        /// </summary>
        /// <returns>Прочитанный массив</returns>
        private static int[][] ReadTable(int[][] readJagged)
        {
            int strings, columns;
            strings = GetTableSize();
            readJagged = new int[strings][];

            for (int p = 0; p < strings; p++)
            {
                columns = GetTableSize("Количество столбцов должно быть больше нуля!","Введите количество столбцов:  ");
                readJagged[p] = new int[columns];
                
                for (int q = 0; q < columns; q++)
                {
                    readJagged[p][q] = random.Next(-100, 100); 
                }
            }
            return readJagged;
        }

        /// <summary>
        /// Читает двумерный массив целых чисел с клавиатуры
        /// </summary>
        /// <returns>Прочитанный массив</returns>
        private static int[,] ReadTable(int[,] readMatrix)
        {
            int strings = GetTableSize();
            int columns = GetTableSize("Количество столбцов должно быть больше нуля!", "Введите количество столбцов таблицы: ");
            readMatrix = new int[strings, columns];

            for (int q = 0; q < strings; q++)
            {
                for (int p = 0; p < columns; p++)
                {
                    readMatrix[q, p] = ReadInteger("Введите элемент таблицы");
                }
            }
            return readMatrix;
        }
        #endregion

        #region Печать массива

        /// <summary>
        /// Печатает двумерный массив
        /// </summary>
        /// <param name="table">Печатаемый массив</param>
        public static void PrintTable(int[,] table)
        {
            if (CheckEmpty(table))
            {
                PrintMessage("Таблица пустая", ConsoleColor.White);
                return;
            }

            int count = 0;
            int length = table.GetLength(1);
            foreach (int item in table)
            {
                PrintMessage(item + " ");
                count++;
                if (count == length)
                {
                    PrintMessage("\n");
                    count = 0;
                }
            }
        }

        /// <summary>
        /// Печатает рваный массив
        /// </summary>
        /// <param name="table">Печатаемый массив</param>
        public static void PrintTable(int[][] jagged)
        {
            if (CheckEmpty(jagged))
            {
                PrintMessage("Таблица пустая", ConsoleColor.White);
            }
            else
            {
                int count = 0;
                int length = jagged.GetLength(1);
                foreach (int[] item in jagged)
                {
                    Console.Write(item + " ");
                    count++;
                    if (count == length) // чтобы это выглядело построчно
                    {
                        Console.WriteLine();
                        count = 0;
                    }
                }
            }
        }

        #endregion
    }
}
