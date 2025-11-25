using System.Diagnostics;

namespace Lab5_6
{
    internal class Program
    {
        static void Main(string[] args)
        {            
            StartWork();
            DoWork();
            FinishWork();
        }

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
                    "Удалить строки начиная с номера Н",
                    "Завершить работу"
            ];

            string end = "Нет";
            int[,] table = new int[0, 0];
            do
            {
                switch (PrintMenu(mainMenu))
                {
                    case 1:
                        {
                            table = CreateTable();
                            break;
                        }
                    case 2:
                        {
                            PrintTable(table);
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
        /// Печатает меню и принимает выбор пользователя
        /// </summary>
        /// <param name="menu">Массив возможных действий</param>
        /// <returns>Выбранное действие</returns>
        private static uint PrintMenu(string[] menu, string message = "Программа реализует следующую функциональность: ")
        {
            uint action;
            string? choice;
            do
            {
                bool isCorrectAction;
                do
                {
                    PrintMessage(message);
                    for (int i = 0; i < menu.Length; i++)
                    {
                        Console.WriteLine($"  {i + 1} " + menu[i]);
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
                Console.WriteLine("Вы уверены в своём выборе? Если уверены, напишите Да(в любом регистре), любой другой ввод будет воспринят как нет");
                choice = Console.ReadLine();

            } while (!string.Equals(choice, "Да", StringComparison.OrdinalIgnoreCase)); // подсказал интернет

            PrintMessage("Приступаю к выполнению команды");

            return action;
        }

        /// <summary>
        /// Время выполнения программы
        /// </summary>
        private static Stopwatch stopwatch = new();

        /// <summary>
        /// Здоровается, начинает работу
        /// </summary>
        private static void StartWork()
        {
            Console.WriteLine("Здравствуйте!");
            PrintMessage("Работа начата", ConsoleColor.Cyan);
            stopwatch.Start();
        }

        /// <summary>
        /// Создаёт массив выбранным способом
        /// </summary>
        /// <returns>Созданный массив</returns>
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
                switch (PrintMenu(arrayMenu, "Выберете способ создания массива:"))
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

        /// <summary>
        /// Уведомляет о завершении, времени выполнения и прощается
        /// </summary>
        private static void FinishWork()
        {
            PrintMessage("Работа закончена", ConsoleColor.Cyan);
            stopwatch.Stop();
            Console.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");
            Console.WriteLine("До свидания!");
        }

        /// <summary>
        /// Датчик случайных чисел
        /// </summary>
        private static Random random = new();

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
        /// Читает массив целых чисел с клавиатуры
        /// </summary>
        /// <returns>Прочитанный массив</returns>
        private static int[,] ReadTable()
        {
            int strings, columns;
            (strings, columns) = GetTableSize();
            int[,] readTable = new int[strings, columns];
            
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
        private static int[,] MakeRandomTable()
        {
            int strings, columns;
            (strings, columns) = GetTableSize();
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
        /// Получает размеры таблицы
        /// </summary>
        /// <returns>Количество строк и столбцов</returns>
        private static (int strings, int columns) GetTableSize()
        {
            bool isCorrect = true;
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

                isCorrect= CheckTableSize(strings, columns);
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
        private static void PrintTable(int[,] table)
        {
            if(CheckEmpty(table))
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
                if (count == length) // напечатали столбец спустились на следующую
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

        // TODO: написать метод который добавляет 1 строку в начало таблицы;
        private static int[,] AddString(int[,] table)
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

        // TODO: написать метод который удаляет К строк начиная с номера Н в рваном массиве;


    }


}
