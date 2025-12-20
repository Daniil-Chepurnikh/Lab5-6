using System;
using System.Diagnostics;

namespace Library
{
    /// <summary>
    /// Все необходимые проверки
    /// </summary>
    public class Check
    {
        public const short MaxSize = 100;

        /// <summary>
        /// Проверяет таблицу на пустоту
        /// </summary>
        /// <param name="matrix">Проверяемая таблица</param>
        /// <returns>True если пустая</returns>
        public static bool Empty(int[,] matrix)
        {
            return matrix.Length == 0;
        }

        /// <summary>
        /// Проверяет таблицу на пустоту
        /// </summary>
        /// <param name="jagged">Проверяемая таблица</param>
        /// <returns>True если пустая</returns>
        public static bool Empty(int[][] jagged)
        {
            return jagged.Length == 0;
        }

        /// <summary>
        /// Проверяет вхождение размера в допустимый диапазон
        /// </summary>
        /// <param name="size">Проверяемый размер</param>
        /// <returns>True если размеры корректные</returns>
        public static bool TableSize(int size)
        {
            return size > 0 && size <= MaxSize;
        }

        /// <summary>
        /// Проверяет строку на соответствие требованиям задания
        /// </summary>
        /// <param name="str">Проверяемая строка</param>
        /// <returns>True если строка правильная</returns>
        public static bool String(string str)
        {
            bool result = true;
            uint punctuation = 0;
            char[] strArray = str.Trim().ToCharArray();
            for (uint p = 0; p < strArray.Length; p++)
            {
                if (strArray[p] == ' ')
                {
                    continue;
                }

                if (char.IsDigit(strArray[p]))
                {
                    Print.Error("Встречено число!");
                    result = false;
                    break;
                }

                if (char.IsPunctuation(strArray[p]) && p == 0)
                {
                    Print.Error("Строка не должна начинаться со знака препинания");
                    result = false;
                    break;
                }

                if (char.IsPunctuation(strArray[p]))
                {
                    ++punctuation;
                }
                else
                {
                    punctuation = 0;
                }

                if (punctuation == 2)
                {
                    Print.Error("Знаки препинания не должны идти подряд!");
                    result = false;
                    break;
                }
            }
            return result;
        }
    }

    /// <summary>
    /// Пользовательский вывод
    /// </summary>
    public class Print
    {
        /// <summary>
        /// Сообщает об ошибках
        /// </summary>
        /// <param name="error">Печатаемая ошибка</param>
        public static void Error(string error = "Нераспознанная команда! Проверьте корректность ввода")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ошибка: " + error);
            Console.ResetColor();
        }

        /// <summary>
        /// Печатает меню и принимает выбор пользователя
        /// </summary>
        /// <param name="menu">Массив возможных действий</param>
        /// <returns>Выбранное действие</returns>
        public static uint Menu(string[] menu, string message = "Программа реализует следующую функциональность:", string checkChoice = "Вы выбрали дейстиве: ")
        {
            uint action;
            string? choice;
            do
            {
                bool isCorrectAction;
                do
                {
                    Message(message + '\n');
                    for (uint p = 0; p < menu.Length; p++)
                    {
                        Message($"  {p + 1} " + menu[p] + '\n' + '\n', ConsoleColor.White);
                    }

                    Message("Введите номер выбранного действия:  ", ConsoleColor.White);
                    isCorrectAction = uint.TryParse(Read.Data(), out action);
                    if (action > menu.Length || action == 0)
                    {
                        Error();
                        isCorrectAction = false;
                    }
                } while (!isCorrectAction);

                Message(checkChoice + menu[action - 1] + '\n', ConsoleColor.White);
                Message("Вы уверены в своём выборе? Если уверены, напишите ДА(в любом регистре), любой другой ввод будет воспринят как НЕТ:  ", ConsoleColor.White);
                choice = Read.Data();

            } while (!string.Equals(choice, "Да", StringComparison.OrdinalIgnoreCase));

            Message("Приступаю к выполнению команды" + '\n');
            return action;
        }

        /// <summary>
        /// Печатаем красивые сообщения пользователю
        /// </summary>
        /// <param name="message">Сообщение на печать</param>
        /// <param name="color">Цвет печать</param>
        public static void Message(string message = "Ввод корректен", ConsoleColor color = ConsoleColor.Green)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Печатает двумерный массив
        /// </summary>
        /// <param name="table">Печатаемый массив</param>
        public static void Table(int[,] table)
        {
            if (Check.Empty(table))
            {
                Message("Матрица пустая" + '\n', ConsoleColor.White);
                return;
            }

            int count = 0;
            int length = table.GetLength(1);
            foreach (int item in table)
            {
                Message(item + " ", ConsoleColor.White);
                count++;
                if (count == length)
                {
                    Message("\n");
                    count = 0;
                }
            }
        }

        /// <summary>
        /// Печатает рваный массив
        /// </summary>
        /// <param name="table">Печатаемый массив</param>
        public static void Table(int[][] jagged)
        {
            if (Check.Empty(jagged))
            {
                Message("Рваный массив пустой" + '\n', ConsoleColor.White);
            }
            else
            {
                for (uint p = 0; p < jagged.GetLength(0); p++)
                {
                    foreach (int item in jagged[p])
                    {
                        Message(item + " ", ConsoleColor.White);
                    }
                    Message("\n");
                }
            }
        }
    }

    /// <summary>
    /// Получение различного пользовательского ввода
    /// </summary>
    public class Read
    {
        /// <summary>
        /// Читает целое число и сообщает об ошибках ввода оного
        /// </summary>
        /// <param name="message">Приглашение к нужному вводу</param>
        /// <param name="error">Уведомление об ошибочном вводе</param>
        /// <returns>Прочитанное число</returns>
        public static int Integer(string message = "Введите количество элементов массива:  ", string error = "Вы не ввели целое число в разрешённом дипазоне!")
        {
            bool isNumber;
            int number;
            do
            {
                Print.Message(message, ConsoleColor.White);

                isNumber = int.TryParse(Data(), out number);
                if (!isNumber)
                {
                    Print.Error(error);
                }

            } while (!isNumber);
            return number;
        }

        /// <summary>
        /// Получает ввод пользователя
        /// </summary>
        /// <returns>Ввод или NULL</returns>
        public static string? Data()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string? choice = Console.ReadLine();
            Console.ResetColor();
            return choice;
        }
    }

    /// <summary>
    /// Вспомогательные функции
    /// </summary>
    public class Work
    {
        /// <summary>
        /// Время выполнения программы
        /// </summary>
        private static Stopwatch stopwatch = new();

        /// <summary>
        /// Получает размер таблицы
        /// </summary>
        /// <param name="error">Сообщение об ошибке</param>
        /// <param name="message">Предложение к вводу</param>
        /// <returns>Корректный размер</returns>
        private static int GetTableSize(string message = "Введите количество строк таблицы:  ", string error = "Количество строк должно быть больше нуля и меньше максимального допустимого значения - 100!")
        {
            int size = -1;
            while (!Check.TableSize(size))
            {
                size = Read.Integer(message);
                if (!Check.TableSize(size))
                {
                    Print.Error(error);
                }
            }
            return size;
        }

        /// <summary>
        /// Здоровается, начинает работу
        /// </summary>
        public static void Start()
        {
            Print.Message("Здравствуйте!" + '\n', ConsoleColor.White);
            Print.Message("Работа начата" + '\n', ConsoleColor.White);
            stopwatch.Start();
        }

        /// <summary>
        /// Уведомляет о завершении, времени выполнения и прощается
        /// </summary>
        public static void Finish()
        {
            Print.Message("Работа закончена" + '\n', ConsoleColor.White);
            stopwatch.Stop();
            Print.Message($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс" + '\n');
            Print.Message("До свидания!");
        }

        /// <summary>
        /// Добавляет строку в начало таблицы
        /// </summary>
        /// <param name="table">Таблица куда добавляем</param>
        /// <returns>Изменённая таблица</returns>
        public static int[,] AddString(int[,] table)
        {
            int[,] result;
            if (!Check.TableSize(table.GetLength(0) + 1))
            {
                Print.Error("Массив вышел из допустимого диапазона!");
                result = table;
            }
            else
            {
                if (Check.Empty(table))
                {
                    result = new int[1, GetTableSize("Введите количество элементов в добавляемой строке:  ", $"Количество элементов в добавляемой строке должно быть больше 0 и меньше или равно {Check.MaxSize}!")];
                }
                else
                {
                    int newString;
                    do
                    {
                        newString = GetTableSize("Введите количество элементов в добавляемой строке:  ");
                        if (newString != table.GetLength(1))
                        {
                            Print.Error($"Количество элементов в добавляемой строке должно быть {table.GetLength(1)}, чтобы массив оставался двумерным!");
                        }

                    } while (newString != table.GetLength(1));
                    
                    result = new int[table.GetLength(0) + 1, newString];
                    for (int p = 1; p < table.GetLength(0) + 1; p++)
                    {
                        for (int q = 0; q < table.GetLength(1); q++)
                        {
                            result[p, q] = table[p - 1, q];
                        }
                    }
                }

                string[] addMenu =
                [
                        "Добавить строку самостоятельно",
                        "Добавить строку случайно"
                ];
                switch (Print.Menu(addMenu, "Выберете способ добавления элементов:  "))
                {
                    case 1:
                        {
                            for (int p = 0; p < result.Length / result.GetLength(0); p++)
                            {
                                result[0, p] = Read.Integer("Введите элемент массива:  ");
                            }
                            break;
                        }
                    case 2:
                        {
                            for (int p = 0; p < result.Length / result.GetLength(0); p++)
                            {
                                result[0, p] = random.Next(0, 9);
                            }
                            break;
                        }
                }
            }
            return result;
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
            int strings = jagged.GetLength(0);
            if (Check.Empty(jagged))
            {
                Print.Error("Невозможно удалить строки в пустом массиве!");
            }
            else
            {
                int start;
                do
                {
                    start = Read.Integer("Введите индекс, с которого начинается удаление строк:   ");
                    if (start <= 0)
                    {
                        Print.Error("Номер строки должен быть положительным числом!");
                    }
                    else if (start > jagged.GetLength(0))
                    {
                        Print.Error("В массиве недостаточно строк!");
                    }
                } while (start <= 0 || start > jagged.GetLength(0));

                int delete;
                do
                {
                    delete = Read.Integer("Введите количество строк, которые нужно удалить:   ");
                    if (delete <= 0)
                    {
                        Print.Error("Возможно удалить только положительное количество строк!");
                    }
                    else if (strings + 1 - start < delete)
                    {
                        Print.Error("В массиве недостаточно строк!");
                    }
                } while (delete <= 0 || strings + 1 - start < delete);

                int[][] result = new int[strings - delete][]; // сколько строк было - сколько надо удалить
                uint resultIndex = 0;
                for (int p = 0; p < start - 1; p++, resultIndex++)
                {
                    result[resultIndex] = new int[jagged[p].Length]; 
                    for (int q = 0; q < jagged[p].Length; q++)
                    {
                        result[resultIndex][q] = jagged[p][q];
                    }
                }

                for (int p = start + delete - 1; p < jagged.GetLength(0); p++, resultIndex++)
                {
                    result[resultIndex] = new int[jagged[p].Length];
                    for (int q = 0; q < jagged[p].Length; q++)
                    {
                        result[resultIndex][q] = jagged[p][q];
                    }
                }
                jagged = result;
            }
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
            switch (Print.Menu(arrayMenu, "Выберете способ создания массива:  "))
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
            
            switch (Print.Menu(arrayMenu, "Выберете способ создания массива:"))
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
            return jagged;
        }

        /// <summary>
        /// Создаёт рваный массив датчиком случайных чисел
        /// </summary>
        /// <param name="randomJagged">Создаваемый массив</param>
        /// <returns>Созданный массив</returns>
        private static int[][] MakeRandomTable(int[][] randomJagged)
        {
            int strings = GetTableSize();
            if (!Check.TableSize(strings))
            {
                Print.Error("Массив вышел из допустимого диапазона!");
            }
            else
            {
                randomJagged = new int[strings][];
                for (uint p = 0; p < strings; p++)
                {
                    int columns = GetTableSize("Введите количество элементов в строке:  ", "Количество столбцов должно быть большу нуля!");
                    if (!Check.TableSize(columns))
                    {
                        Print.Error("Массив вышел из допустимого диапазона!");
                        break;
                    }
                    randomJagged[p] = new int[columns];
                    for (uint q = 0; q < columns; q++)
                    {
                        randomJagged[p][q] = random.Next(0, 9);
                    }
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
            int strings = GetTableSize();
            int columns = GetTableSize("Введите количество столбцов таблицы: ", "Количество столбцов должно быть больше нуля и меньше максимального допустимого значния 100!");
            if (Check.TableSize(strings) && Check.TableSize(columns))
            {
                randomTable = new int[strings, columns];
                for (int q = 0; q < strings; q++)
                {
                    for (int p = 0; p < columns; p++)
                    {
                        randomTable[q, p] = random.Next(0, 9);
                    }
                }
            }
            else
            {
                Print.Error("Массив вышел из допустимого диапазона!");
            }
            return randomTable;
        }

        /// <summary>
        /// Читает рваный массив целых чисел с клавиатуры
        /// </summary>
        /// <returns>Прочитанный массив</returns>
        private static int[][] ReadTable(int[][] readJagged)
        {
            int strings = GetTableSize();
            if (!Check.TableSize(strings))
            {
                Print.Error("Массив вышел из допустимого диапазона!");
            }
            else
            {
                readJagged = new int[strings][];
                for (int p = 0; p < strings; p++)
                {
                    int columns = GetTableSize("Введите количество элементов строки:  ", "Количество столбцов должно быть больше нуля и меньше максимального допустимого значения 100!");
                    if (!Check.TableSize(columns))
                    {
                        Print.Error("Массив вышел из допустимого диапазона!");
                        break;
                    }
                    readJagged[p] = new int[columns];
                    for (int q = 0; q < columns; q++)
                    {
                        readJagged[p][q] = Read.Integer("Введите элемент массива:  ");
                    }
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
            int columns = GetTableSize("Введите количество столбцов таблицы: ", "Количество столбцов должно быть больше нуля и меньше максимального допустимого значения - 100!");
            if (Check.TableSize(strings) && Check.TableSize(columns))
            {
                readMatrix = new int[strings, columns];
                for (int q = 0; q < strings; q++)
                {
                    for (int p = 0; p < columns; p++)
                    {
                        readMatrix[q, p] = Read.Integer("Введите элемент таблицы:  ");
                    }
                }
            }
            else
            {
                Print.Error("Массив вышел из допустимого диапазона!");
            }
            return readMatrix;
        }
        #endregion
    }
} 