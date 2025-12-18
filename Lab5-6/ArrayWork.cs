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
            Work.Start();
            DoWork();
            Work.Finish();
        }

        /// <summary>
        /// Выполняет все требования пользователя
        /// </summary>
        private static void DoWork()
        {
            string end = "Нет";
            string[] mainMenu =
            [
                    "Работать с двумерным массивом",
                    "Работать с рваным массивом",
                    "Завершить работу"
            ];
            do
            {
                switch (Print.Menu(mainMenu, "Это главное меню! Выберете, что вы хотите сделать:"))
                {
                    case 1:
                        { // работа с двумерным массивом
                            string[] matrixMenu =
                            [
                                    "Создать",
                                    "Напечатать",
                                    "Добавить строку в начало",
                                    "Вернуться в главное меню"
                            ];

                            int[,] matrix = new int[0, 0];
                            string main = "Нет";
                            do
                            {
                                switch (Print.Menu(matrixMenu, "Выберете, что вы хотите сделать далее с двумерным массивом"))
                                {
                                    case 1:
                                        {
                                            matrix = Work.CreateTable(matrix);
                                            break;
                                        }
                                    case 2:
                                        {
                                            Print.Table(matrix);
                                            break;
                                        }
                                    case 3:
                                        {
                                            matrix = Work.AddString(matrix);
                                            break;
                                        }
                                    case 4:
                                        {
                                            main = "Да";
                                            break;
                                        }
                                }
                            } while (string.Equals(main, "Нет", StringComparison.OrdinalIgnoreCase));
                            break;
                        } // конец работы с двумерным массивом
                    case 2:
                        { // работа с рваным массивом

                            string[] jaggedMenu =
                            [
                                    "Создать",
                                    "Напечатать",
                                    "Удалить определённое количество строк, начиная с определённого номера",
                                    "Вернуться в главное меню"
                            ];

                            int[][] jagged = [];
                            string main = "Нет";
                            do
                            {
                                switch (Print.Menu(jaggedMenu, "Выберете, что вы хотите сделать далее с рваным массивом"))
                                {
                                    case 1:
                                        {
                                            jagged = Library.Work.CreateTable(jagged);
                                            break;
                                        }
                                    case 2:
                                        {
                                            Print.Table(jagged);
                                            break;
                                        }
                                    case 3:
                                        {
                                            jagged = Library.Work.DeleteStrings(jagged);
                                            break;
                                        }
                                    case 4:
                                        {
                                            main = "Да";
                                            break;
                                        }
                                }
                            } while (string.Equals(main, "Нет", StringComparison.OrdinalIgnoreCase));
                            break;
                        } // конец работы с рваным массивом
                    case 3:
                        {
                            end = "Да";
                            break;
                        }
                }
            } while (string.Equals(end, "Нет", StringComparison.OrdinalIgnoreCase));
        } // DoWork
    }
}
