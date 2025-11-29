using Library;
using System;

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
        /// Выполняет все требования пользователя
        /// </summary>
        private static void DoWork()
        {
            string[] mainMenu =
            [
                    "Создать массив",
                    "Напечатать массив",
                    "Добавить строку в начало двумерного массива",
                    "Удалить K строк начиная с номера N в рваном массиве",
                    "Завершить работу"
            ];

            string end = "Нет";
            int[,] matrix = new int[0, 0];
            int[][] jagged = [];
            // TODO: придумать как сделать удобное меню для работы с массивами
            do
            {
                switch (Helper.PrintMenu(mainMenu))
                {
                    case 1:
                        {
                            matrix = Helper.CreateTable(matrix);
                            break;
                        }
                    case 2:
                        {
                            Helper.PrintTable(matrix);
                            break;
                        }
                    case 3:
                        {
                            matrix = Helper.AddString(matrix);
                            break;
                        }
                    case 4:
                        {
                            Helper.DeleteStrings(jagged);
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
    }
}
