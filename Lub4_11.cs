using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lub3
{
    class Lub4_11
    {
        //C:\All\C#\Lub3\input.txt             //Путь исходного файла (Для справки)
        //C:\All\C#\Lub3\output.txt            //Путь выходного файла (Для справки)

        static void Main(string[] args)
        {
            StringBuilder Read = new();                 //Строитель строк для чтения строк
            string AllFile;                             //Массив для чтения файла
            int go = 1;                                 //Переменная 2 аргумента
            string puthOUT;
            string puthIN;
            if (args.Length == 0)                       //Реализация помощи в консоли 
            {
                Console.WriteLine("Помощь в использовании приложением.\n\n");
                Console.WriteLine("-help /help для подсказки.\n");
                Console.WriteLine("Первый аргумент - входной файл. Он должен быть либо путём к .txt файлу, либо пустым для ввода строк в ручном режиме.");
                Console.WriteLine(@"Пример пути: C:\input.txt" + "\n");
                Console.WriteLine("Второй аргумент - выходной файл. Он должен быть либо путём к .txt файлу, либо пустым для ввода строк в ручном режиме.");
                Console.WriteLine(@"Пример пути: C:\output.txt" + "\n");
                Console.WriteLine("Третий аргумент должен быть числом большим чем 0, записанным через слэш.\nПример третьего аргумента: /2\n");
                Console.WriteLine("Нажмите любую кнопку...");
                Console.ReadKey();
                Environment.Exit(0);
            }
            else if (args[0] == "/help" || args[0] == "-help")      //Помощь если попросят из командной строки
            {
                Console.WriteLine("Помощь в использовании приложением.\n\n");
                Console.WriteLine("-help /help для подсказки.\n");
                Console.WriteLine("Первый аргумент - входной файл. Он должен быть либо путём к .txt файлу, либо пустым для ввода строк в ручном режиме.");
                Console.WriteLine(@"Пример пути: C:\input.txt" + "\n");
                Console.WriteLine("Второй аргумент - выходной файл. Он должен быть либо путём к .txt файлу, либо пустым для ввода строк в ручном режиме.");
                Console.WriteLine(@"Пример пути: C:\output.txt" + "\n");
                Console.WriteLine("Третий аргумент должен быть числом большим чем 0, записанным через слэш.\nПример третьего аргумента: /2");
                Environment.Exit(0);
            }
            try
            {
                puthIN = args[0];
                puthOUT = args[1];
                try
                {
                    char[] arg2 = args[2].ToCharArray();
                    go = int.Parse(arg2[1].ToString());
                }
                catch
                {
                    Console.WriteLine("Нет третьего аргумента, или введено не число в качестве этого аргумента.\nПринято стандартное число шага: 1");
                    Console.WriteLine("(Третий аргумент записивыется через слэш)\nПример: /2\n");
                }
                try
                {
                    //Чтение файла из директории
                    string File_1 = "";
                    using (FileStream AllFiles = File.OpenRead(puthIN/*, Encoding.UTF8*/)) //C:\All\C#\Lub3\input.txt
                    {
                        byte[] array = new byte[AllFiles.Length];
                        AllFiles.Read(array, 0, array.Length);                   // считываем данные
                        File_1 = System.Text.Encoding.Default.GetString(array);  // декодируем байты в строку
                        char[] ChAll = File_1.ToCharArray();
                        for (int i = 0; i < ChAll.Length; i++)
                        {
                            if (ChAll[i] == '\r')
                            {
                                ChAll[i] = ' ';
                                ChAll[i + 1] = ' ';
                                i++;
                            }
                        }
                        AllFile = String.Concat<char>(ChAll);
                    }
                    Console.Write("Прочитаный файл: ");
                    Console.WriteLine("\n" + File_1 + "\n\nШаг для использования слов: " + go + "\n\n");     //Вывод прочитанного
                    Fil(AllFile, go, puthOUT);                                         //Передача в метод всех строк
                }
                catch
                {
                    Console.WriteLine("Ошибка чтения файла!");              //Выдать ошибку, если не получилось прочитать файл
                    Environment.Exit(0);                                    //Выход из программы
                }
            }
            catch
            {
                int j = 0, cout = 0;
                try
                {
                    foreach (string ar in args)
                    {
                        if (ar.Contains("/"))
                        {
                            j = cout;
                            break;
                        }
                        cout++;
                    }
                    char[] arg2 = args[j].ToCharArray();
                    go = int.Parse(arg2[j + 1].ToString());
                }
                catch
                {
                    Console.WriteLine("Нет третьего аргумента, или введено не число в качестве этого аргумента.\nПринято стандартное число шага: 1");
                    Console.WriteLine("(Третий аргумент записивыется через слэш)\nПример: /2\n");
                }
                Console.Write("Введите количество строк: ");        //Опрос о кол-ве строк, если не дан путь к файлу
                int count = 1;
                try
                {
                    count = int.Parse(Console.ReadLine());          //Получение переменной кол-ва строк
                }
                catch
                {
                    Console.WriteLine("Введено не число. Принято стандартное значение строк - 1");
                }
                for (int i = 1; i <= count; i++)                    //Цикл опроса построчно
                {
                    Console.Write($"Введите строчку {i}: ");
                    Read.Append(Console.ReadLine().Trim());
                    Read.Append(' ');
                }
                string ReadText = Read.ToString();                          //Упаковка всё в одну строку
                Console.WriteLine("\n" + ReadText + "\n" + go + "\n");      //Вывод получившейся строки
                Con(ReadText, go);
            }
        }

        static void Fil(string AllFile, int go, string puthOUT)
        {
            char lastLatter = ' ';                      //Переменная последнего символа
            string lastStr = "";                        //Переменная последней строки
            var OUT = new List<string>();               //Лист конечной строки
            string Cat = "";
            string[] subs = AllFile.Split(' ', '.');    //Разбитие строки на слова
            var temp = new List<string>();
            foreach (var s in subs)
            {
                if (!string.IsNullOrEmpty(s))
                    temp.Add(s);
            }
            subs = temp.ToArray();
            for (int i = 0; i < subs.Length; i += go)       //Цикл скливания и добовление к результату слов
            {
                string sub = subs[i];                       //Скопировать из массива в строку
                string subLow = sub.ToLower();              //Сделать все символы маленькими
                if (lastLatter == subLow[0])     //Если последняя буква совпадает с первой, то склеить их
                {
                    OUT.RemoveAt(OUT.Count - 1);               //Вычитаем последнее слово
                    OUT.Add(string.Concat(lastStr, sub));      //Склеиваем последнее и следующее
                    Cat = string.Concat(lastStr, sub);         //Переменная последней фразы
                }
                else        //Иначе добавить следующее слово в следующую строку
                {
                    OUT.Add(sub);
                    Cat = sub;                              //Переменная последней фразы
                }
                lastLatter = subLow[^1];                   //Запомнить последнюю букву в слове
                lastStr = Cat;                              //Запомнить последнее слово
            }
            try
            {
                StreamWriter output = new(puthOUT);         //Объявление записи в файл
                foreach (var data in OUT)                   //Цикл записи
                {
                    output.WriteLine(data);
                }
                output.Close();                             //Закрытие файла
                Console.WriteLine($"Запись в файл успешна. Путь к нему - {puthOUT}");       //Вывод в консоль информации об успешной операции
            }
            catch
            {
                Console.WriteLine($"Ошибка записи в файл по пути - {puthOUT}");             //Вывод в консоль информации об ошибки в операции
            }

        }
        static void Con(string ReadText, int go)                //Метод склеивания и вывода строки в консоль
        {
            char lastLatter = ' ';                      //Переменная последнего символа
            string lastStr = "";                        //Переменная последней строки
            var OUT = new List<string>();               //Список конечной строки
            string Cat = "";
            string[] subs = ReadText.Split(' ', '.');   //Разбитие строки на слова
            var temp = new List<string>();              //Создание списка для копирования массива слов
            foreach (var s in subs)                     //Удаление всех пустых символов в массиве
            {
                if (!string.IsNullOrEmpty(s))
                    temp.Add(s);
            }
            subs = temp.ToArray();
            for (int i = 0; i < subs.Length; i += go)       //Цикл скливания и добовление к результату слов
            {
                string sub = subs[i];                   //Скопировать из массива в строку
                string subLow;
                subLow = sub.ToLower();                 //Сделать все символы маленькими
                if (lastLatter == subLow[0])  //Если последняя буква совпадает с первой, то склеить их
                {
                    OUT.RemoveAt(OUT.Count - 1);                //Вычитаем последнее слово
                    OUT.Add(string.Concat(lastStr, sub));   //Склеиваем последнее и следующее
                    Cat = string.Concat(lastStr, sub);
                }
                else        //Иначе добавить следующее слово через пробел
                {
                    OUT.Add(sub);
                    Cat = sub;
                }
                lastLatter = subLow[^1];                   //Запомнить последнюю букву в слове
                lastStr = Cat;                              //Запомнить последнее слово
            }
            Console.WriteLine("Вывод получившихся слов:\n");
            for (int i = 0; i < OUT.Count; i++)              //Вывод в консоль всех слов
            {
                Console.WriteLine(OUT[i]);
            }
        }
    }
}