using Library;
using System;

namespace StringWork
{
    internal class StringWork
    {
        static void Main(string[] args)
        {
            Work.Start();
            DoWork();
            Work.Finish();
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
            switch (Print.Menu(mainMenu, "Выберете, как вы хотите создать строку?"))
            {
                case 1:
                    {
                        Print.Message("Введите строку:  ", ConsoleColor.White);
                        newString = Read.Data();
                        break;
                    }
                case 2:
                    {
                        newString = testMenu[Print.Menu(testMenu, "Выберете нужную строку", "Вы выбрали строку:  ") - 1];
                        break;
                    }
            }
            string finalString = SwapWords(newString);

            if (!string.IsNullOrEmpty(finalString))
            {
                Print.Message("Изменённая строка:" + finalString + '\n', ConsoleColor.White);
            }
        }

        /// <summary>
        /// Меняет первое и последнее слово в строке местами
        /// </summary>
        /// <param name="newString">Строка для смены</param>
        private static string SwapWords(string input)
        {
            string newString = input.Replace('\t', ' ');
            
            string finalString = "";
            if (!string.IsNullOrEmpty(newString))
            {
                if (Check.String(newString))
                {
                    string[] prepareString = PrepareString(newString);
                    Swap(prepareString, 0, prepareString.Length - 1);
                    finalString = string.Join(" ", prepareString);
                }
            }
            else
            {
                Print.Error("Строка пустая!");
            }
            return finalString;
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
    }
}