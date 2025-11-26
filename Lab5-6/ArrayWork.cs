using System;
using System.Diagnostics;
using Library;

namespace Lab5_6
{
    /// <summary>
    /// Работает с многомерными и рваными массивами
    /// </summary>
    internal class ArrayWork
    {
        /// <summary>
        /// Решает поставленные в лабе задачи
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {            
            Helper.StartWork();
            DoWork();
            Helper.FinishWork();
        }

        /// <summary>
        /// Датчик случайных чисел
        /// </summary>
        private static Random random = new();

        /// <summary>
        /// Выполняет все требования пользователя
        /// </summary>
        private static void DoWork()
        {
            string[] mainMenu =
            [
                    "Создать таблицу",
                    "Напечатать таблицу",
                    "Добавить строку в начало таблицы",
                    "Удалить строки начиная с номера N",
                    "Завершить работу"
            ];

            string end = "Нет";
            int[,] table = new int[0, 0];
            do
            {
                switch (Helper.PrintMenu(mainMenu))
                {
                    case 1:
                        {
                            table = CreateTable();
                            break;
                        }
                    case 2:
                        {
                            Helper.PrintTable(table);
                            break;
                        }
                    case 3:
                        {
                            table = AddString(table);
                            break;
                        }
                    case 4:
                        {
                            // TODO: вставить метод который удаляет К строк начиная с номера Н в рваном массиве;
                            break;
                        }
                    case 5:
                        {
                            end = "Да";
                            break;
                        }
                }
            } while (string.Equals(end, "Нет", StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Время выполнения программы
        /// </summary>
        private static Stopwatch stopwatch = new();

        /// <summary>
        /// Читает массив целых чисел с клавиатуры
        /// </summary>
        /// <returns>Прочитанный массив</returns>
        private static int[,] ReadTable()
        {
            int strings, columns;
            (strings, columns) = Helper.GetTableSize();
            int[,] readTable = new int[strings, columns];
            
            for (int q = 0; q < strings; q++)
            {
                for (int p = 0; p < columns; p++)
                {
                    readTable[q, p] = Helper.ReadInteger("Введите элемент таблицы");
                }
            }
            return readTable;
        }

        /// <summary>
        /// Создаёт массив датчиком случайных чисел
        /// </summary>
        /// <returns>Созданный массив</returns>
        private static int[,] MakeRandomTable()
        {
            int strings, columns;
            (strings, columns) = Helper.GetTableSize();
            int[,] randomTable = new int[strings, columns];
            
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
        private static int[,] AddString(int[,] table)
        {
            int columns = table.GetLength(1);
            int newString;
            bool isCorrect;
            do
            {
                newString = Helper.ReadInteger("Введите количество элементов в добавляемой строке");
                if (newString != columns)
                {
                    Helper.PrintError($"Элементов в новой строке должно быть {columns}, чтобы массив не стал рваным!");
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

            switch (Helper.PrintMenu(addMenu, "Выберете способ добавления элементов:"))
            {
                case 1:
                    {
                        for (int p = 0; p < newString; p++)
                        {
                            result[0, p] = Helper.ReadInteger("Введите элемент массива: ");
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

        // TODO: написать метод который удаляет K строк начиная с номера N в рваном массиве;
       /// <summary>
       /// 
       /// </summary>
       /// <param name="table"></param>
        private static void DeleteStrings(int[][] table)
        {
            bool isCorrect = true;
            int start;
            do
            {
                start = Helper.ReadInteger("Введите номер, с которого начинается удаление строк:");
                if (start < 0)
                {
                    Helper.PrintError("Номер строки не может быть отрицательным числом!");
                    isCorrect = false;
                }
                else if (start > table.GetLength(0))
                {
                    Helper.PrintError("В массиве меньше строк!");
                    isCorrect = false;
                }
            } while (!isCorrect);

            int strings;
            do
            {
                strings = Helper.ReadInteger("Введите количество строк, которые нужно удалиить:");
                if (strings < 0)
                {
                    Helper.PrintError("Невозможно удалить отрицательное количество строк!");
                    isCorrect = false;
                }
                else if (strings + start > table.GetLength(0))
                {
                    Helper.PrintError("В массиве меньше строк!");
                    isCorrect = false;
                }
            } while (!isCorrect);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static int[,] CreateTable()
        {
            string[] arrayMenu =
            [
                    "Создать таблицу самостоятельно",
                    "Создать таблицу случайно"
            ];

            int[,] table = new int[0, 0];
            bool isCreated = true;
            do
            {
                switch (Helper.PrintMenu(arrayMenu, "Выберете способ создания массива:"))
                {
                    case 1:
                        {
                            table = ReadTable();
                            break;
                        }
                    case 2:
                        {
                            table = MakeRandomTable();
                            break;
                        }
                }
            } while (!isCreated);
            return table;
        }
    }
}
