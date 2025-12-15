using Library;
using System;
using System.Drawing;

namespace StringWork
{
    internal class StringWork
    {
        static void Main(string[] args)
        {
            Helper.StartWork();
            DoWork();
            Helper.FinishWork();
        }

        private static void DoWork()
        {
            string[] testMenu =
[
                    "   Некрасивая девочка   ",
                    "Среди других играющих детей",
                    "Она напоминает лягушонка.",
                    "Заправлена в трусы худая рубашонка,",
                    "Колечки рыжеватые кудрей",
                    "Рассыпаны, рот длинен, зубки кривы,",
                    "Черты лица остры и некрасивы.",
                    "Двум мальчуганам, сверстникам её,",
                    "Отцы купили по велосипеду.",
                    "Сегодня мальчики, не торопясь к обеду,",
                    "Гоняют по двору, забывши про неё,",
                    "Она ж за ними бегает по следу.",
                    "Чужая радость так же, как своя,",
                    "Томит её и вон из сердца рвётся,",
                    "И девочка ликует и смеётся,",
                    "Охваченная счастьем бытия.",
                    "Ни тени зависти, ни умысла худого",
                    "Ещё не знает это существо.",
                    "Ей всё на свете так безмерно ново,",
                    "Так живо всё, что для иных мертво!",
                    "И не хочу я думать, наблюдая,",
                    "Что будет день, когда она, рыдая,",
                    "Увидит с ужасом, что посреди подруг",
                    "Она всего лишь бедная дурнушка!",
                    "Мне верить хочется, что сердце не игрушка,",
                    "Сломать его едва ли можно вдруг!",
                    "Мне верить хочется, что чистый этот пламень,",
                    "Который в глубине её горит,",
                    "Всю боль свою один переболит",
                    "И перетопит самый тяжкий камень!",
                    "И пусть черты её нехороши",
                    "И нечем ей прельстить воображенье, —",
                    "Младенческая грация души",
                    "Уже сквозит в любом её движенье.",
                    "А если это так, то что есть красота",
                    "И почему её обожествляют люди?",
                    "Сосуд она, в котором пустота,",
                    "Или огонь, мерцающий в сосуде?",
                    "                  Николай Алексеевич Заболоцкий     "
            ];

            string[] mainMenu =
            [
                    "Создать самостоятельно",
                    "Выбрать из готового тестового массива"
            ];

            string newString = "";
            switch (Helper.PrintMenu(mainMenu, "Выберете, как вы хотите создать строку?"))
            {
                case 1:
                    {
                        Helper.PrintMessage("Введите строку:  ", ConsoleColor.White);
                        newString = Helper.ReadData();
                        break;
                    }
                case 2:
                    {
                        newString = testMenu[Helper.PrintMenu(testMenu, "Выберете нужную строку", "Вы выбрали строку:  ") - 1];
                        break;
                    }
            }
            _ = SwapWords(newString);
        }

        /// <summary>
        /// Меняет первое и последнее слово в строке местами
        /// </summary>
        /// <param name="newString">Строка для смены</param>
        private static string SwapWords(string newString)
        {
            if (!string.IsNullOrEmpty(newString))
            {
                if (CheckPunctuation(newString))
                {
                    string[] prepareString = PrepareString(newString);
                    Swap(prepareString, 0, prepareString.Length - 1);

                    string finalString = string.Join(" ", prepareString);
                    Helper.PrintMessage("Изменённая строка: ", ConsoleColor.White);
                    Helper.PrintMessage(finalString + '\n', ConsoleColor.White);
                }
            }
            else
            {
                Helper.PrintError("Строка пустая");
            }
            return newString;
        }
        
        /// <summary>
        /// Меняет местами два слова
        /// </summary>
        /// <param name="str">Там где меняем слова</param>
        /// <param name="indexFirst">Индекс первого слова</param>
        /// <param name="indexSecond">Индекс второго слова</param>        
        private static void Swap(string[] str, int indexFirst, int indexSecond)
        {
            string temp = str[indexFirst];
            str[indexFirst] = str[indexSecond];
            str[indexSecond] = temp;
        }

        /// <summary>
        /// Готовит строку к проверке на ошибки и дальнейшей работе
        /// </summary>
        /// <param name="str">Строка для подготовки</param>
        /// <returns></returns>
        private static string[] PrepareString(string str)
        {
            string stringTrim = str.Trim();
            string[] stringSprlit = stringTrim.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return stringSprlit;
        }

        /// <summary>
        /// Проверяет строку на соответствие требованиям задания
        /// </summary>
        /// <param name="str">Проверяемая строка</param>
        /// <returns>True если строка правильная</returns>
        private static bool CheckPunctuation(string str)
        {
            bool result = false;
            char[] strArray = str.Trim().ToCharArray();
            uint points = 0; // точки
            uint commas = 0; // запятые
            uint semicolons = 0; // точки с запятой
            uint colons = 0; // двоеточия
            uint exclamations = 0; // восклицательные знаки
            uint questions = 0; // вопросительные знаки
            for (uint p = 0; p < strArray.Length; p++)
            {
                if (strArray[p] == ' ')
                {
                    continue;
                }
                if (strArray[p] == ',' && (commas == 1 || p == 0))
                {
                    commas++;
                    Helper.PrintError("Постановка запятых неверная");
                }
                else if (strArray[p] == '.' && ( points == 1 || p == 0))
                {
                    Helper.PrintError("Постановка точек неверная");
                    points++;
                }
                else if (strArray[p] == ':' && (colons == 1 || p == 0))
                {
                    Helper.PrintError("Постановка двоеточий неверная");
                    colons++;
                }
                else if (strArray[p] == ';' && (semicolons == 1 || p == 0))
                {
                    Helper.PrintError("Постановка точек с запятой неверная");
                    semicolons++;
                }
                else if (strArray[p] == '!' && (exclamations == 1 || p == 0))
                {
                    Helper.PrintError("Постановка восклицательных знаков неверная");
                    exclamations++;
                }
                else if (strArray[p] == '?' && (questions == 1 || p == 0))
                {
                    Helper.PrintError("Постановка вопроситьльных знаков неверная");
                    questions++;
                }
                else
                {
                    commas = 0;
                    points = 0;
                    colons = 0;
                    semicolons = 0;
                    exclamations = 0;
                    questions = 0;
                    result = true;
                }
            }
            return result;
        }
    }
}