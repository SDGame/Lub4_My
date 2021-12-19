using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lub3
{
    class Lub4_11
    {
        public static string puthIN = @"C:\All\C#\Lub3\input.txt";              //Глобальная переменная для пути исходного файла (Для справки)
        public static string puthOUT = @"C:\All\C#\Lub3\output.txt";            //Глобальная переменная для пути выходного файла

        static void ReadText(string[] args, int go, StringBuilder Read, int j)
        {
            try
            {
                char[] arg = args[j].ToCharArray();             //Создание массива из аргумента, для удаления первого элемента
                go = int.Parse(arg[1].ToString());              //Присваивание оставшейся части в переменную шага
            }
            catch
            {
                Console.WriteLine("Нет второго аргумента, или введено не число в качестве этого аргумента.\nПринято стандартное число шага: 1");
                Console.WriteLine("(Второй аргумент записивыется через слэш)\nПример: /2\n");
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
            string ReadText = Read.ToString();                  //Упаковка всё в одну строку
            Console.WriteLine("\n" + ReadText + "\n" + go + "\n");            //Вывод получившейся строки
            Con(ReadText, go);                                  //Передача строки в метод
        }
        
        static void Main(string[] args)
        {
            StringBuilder MyArgs = new();               //Строитель строк для аргументов
            StringBuilder Read = new();                 //Строитель строк для чтения строк
            string AllFile;                             //Массив для чтения файла
            int go = 1;                                 //Переменная 2 аргумента
            foreach (string ar in args)                 //Добавление всех аргументов в строителя строк
            {
                MyArgs.Append(ar);
            }
            string arg = MyArgs.ToString();
            if (args.Length == 0)       //Реализация помощника в консоли 
            {
                Console.WriteLine("Помощь в использовании приложением.");
                Console.WriteLine("Первый аргумент должен быть либо полным путём к .txt файлу, либо '-' вместо пути.");
                Console.WriteLine(@"Пример пути: C:\input.txt" + "\n");
                Console.WriteLine("Второй аргумент должен быть числом большим чем 0, записанным через слэш.\nПример второго аргумента: /2");
                Console.WriteLine("Нажмите любую кнопку...");
                Console.ReadKey();
                Environment.Exit(0);
            }
            else if (args[0] == "/help" || args[0] == "-help")  //Помощник если попросили
            {
                Console.WriteLine("Помощь в использовании приложением.");
                Console.WriteLine("Первый аргумент должен быть либо полным путём к .txt файлу, либо '-' вместо пути.");
                Console.WriteLine(@"Пример пути: C:\input.txt" + "\n");
                Console.WriteLine("Второй аргумент должен быть числом большим чем 0, записанным через слэш.\nПример второго аргумента: /2");
                Environment.Exit(0);
            }
            try
            {
                if (args[0].Contains(":"))
                {
                    try
                    {
                        int j = 0, cout = 0;
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
                        go = int.Parse(arg2[j].ToString());
                    }
                    catch
                    {
                        Console.WriteLine("Нет второго аргумента, или введено не число в качестве этого аргумента.\nПринято стандартное число шага: 1");
                        Console.WriteLine("(Второй аргумент записивыется через слэш)\nПример: /2\n");
                    }
                    try
                    {
                        //Чтение файла из директории
                        string File_1 = "";
                        using (FileStream AllFiles = File.OpenRead(args[0]/*, Encoding.UTF8*/)) //C:\All\C#\Lub3\input.txt
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
                                    ChAll[i+1] = ' ';
                                    i++;
                                }
                            }
                            AllFile = String.Concat<char>(ChAll);
                        }
                        Console.Write("Прочитаный файл: ");
                        Console.WriteLine("\n" + File_1 + "\n\nШаг для использования слов: " + go + "\n\n");     //Вывод прочитанного
                        Fil(AllFile, go);                                         //Передача в метод всех строк
                    }
                    catch
                    {
                        Console.WriteLine("Ошибка чтения файла!");              //Выдать ошибку, если не получилось прочитать файл
                        Environment.Exit(0);                                    //Выход из программы
                    }
                }
                else
                {
                    int j = 0, cout = 0;
                    foreach (string ar in args)
                    {
                        if (ar.Contains("/"))
                        {
                            j = cout;
                            break;
                        }
                        cout++;
                    }
                    ReadText(args, go, Read, j);
                }
            }
            catch
            {
                int j = 0, cout = 0;
                foreach (string ar in args)
                {
                    if (ar.Contains("/"))
                    {
                        j = cout;
                        break;
                    }
                    cout++;
                }
                ReadText(args, go, Read, j);
            }
        }
        static void Fil(string AllFile, int go)
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
                char[] CharArr = subLow.ToCharArray();      //Сделать массив символов из строки
                if (lastLatter == CharArr[0])     //Если последняя буква совпадает с первой, то склеить их
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
                lastLatter = CharArr[^1];                   //Запомнить последнюю букву в слове
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
            for (int i = 0; i < subs.Length; i+=go)       //Цикл скливания и добовление к результату слов
            {
                string sub = subs[i];                   //Скопировать из массива в строку
                string subLow;
                subLow = sub.ToLower();                 //Сделать все символы маленькими
                char[] CharArr = subLow.ToCharArray();  //Сделать массив символов из строки
                if (lastLatter == CharArr[0])  //Если последняя буква совпадает с первой, то склеить их
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
                lastLatter = CharArr[^1];                   //Запомнить последнюю букву в слове
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