using System;
using CustomTextFileCorrector;
using Corrector;

namespace CorrectorUI
{
    public class CUI
    {
        public CUI(ICorrector corrector)
        {
            this.corrector = corrector;
        }

        private ICorrector corrector;

        //Вывод меню на консоль
        private void ShowMenu()
        {
            Console.WriteLine("Код команды: Назначение.");
            if (corrector.Status.HasFlag(CorrectorStatusFlags.InLoaded))
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.WriteLine("input      : Ввод названия входного файла.");
            Console.ForegroundColor = ConsoleColor.Gray;
            if (corrector.Status.HasFlag(CorrectorStatusFlags.OutLoaded))
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.WriteLine("output     : Ввод названия выходного файла.");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("length     : Количество строк в выходных файлах (по умолчанию 1000).");
            if (corrector.Status.HasFlag(CorrectorStatusFlags.DictLoaded))
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.WriteLine("dict       : Ввод названия файла словаря и его загрузка в память.");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("info       : Информация о данных программы.");
            if (corrector.Status.HasFlag(CorrectorStatusFlags.DictLoaded | CorrectorStatusFlags.InLoaded | CorrectorStatusFlags.OutLoaded))
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.WriteLine("correct    : Корректировка html.");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("exit       : Выход.");
        }
        /// <summary>
        /// Ввод названия входного файла и проверка на его существование
        /// </summary>
        private void EnterInFile()
        {
            Console.Clear();
            Console.Write("Enter input file name: ");
            string input = Console.ReadLine();
            corrector.LoadInSource(input);
        }
        /// <summary>
        /// Ввод названия выходного файла
        /// </summary>
        private void EnterOutFile()
        {
            Console.Clear();
            Console.Write("Enter output file name: ");
            string output = Console.ReadLine();
            corrector.LoadOutSource(output);
            
        }
        /// <summary>
        /// Ввод количества строк на каждый выходной файл
        /// </summary>
        private void EnterLength()
        {
            Console.Clear();
            Console.Write("Enter number of strings per file: ");
            string temp = Console.ReadLine();
            try
            {
                ((FileCorrector)corrector).N = Convert.ToInt32(temp);
            }
            catch (FormatException)
            {
                Console.WriteLine("WARNING! Input string is not a sequence of digits.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("WARNING! The number cannot fit in an Int32.");
            }
        }
        /// <summary>
        /// Ввод названия файла словаря проверка на его существование и загрузка в память
        /// </summary>
        private void EnterDict()
        {
            Console.Clear();
            Console.Write("Enter dictionary file name: ");
            string dictinput = Console.ReadLine();
            corrector.LoadDictionarySource(dictinput);
        }
        /// <summary>
        /// Метод реализующий работу меню
        /// </summary>
        public void menu()
        {
            string menuCode = null;
            while (menuCode != "exit")
            {
                ShowMenu();
                Console.Write("\nEnter command code: ");
                menuCode = Console.ReadLine();
                switch (menuCode)
                {
                    case "input": //Ввод названия входного файла
                        EnterInFile();
                        Console.ReadLine();
                        break;
                    case "output": //Ввод названия выходного файла
                        EnterOutFile();
                        Console.ReadLine();
                        break;
                    case "length":
                        EnterLength();
                        Console.ReadLine();
                        break;
                    case "dict": //Ввод названия файла словаря и его загрузка в память
                        EnterDict();
                        Console.ReadLine();
                        break;
                    case "info": // Информация о данных программы
                        Console.WriteLine(corrector.ToString());
                        Console.ReadLine();
                        break;
                    case "correct": // Корректировка html
                        if (corrector.Status.HasFlag((CorrectorStatusFlags.DictLoaded | CorrectorStatusFlags.InLoaded | CorrectorStatusFlags.OutLoaded)))
                        {
                            corrector.Correct();
                            Console.ReadLine();
                        }
                        break;
                    default:
                        break;
                }
                Console.Clear();
            }
        }
    }
}
