using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HtmlCorrectorApp_HashTable
{
    public class HtmlCorrector
    {
        /// <summary>
        /// Количество строк (N) в результирующем файле. Если кол-во строк файла превышает данную величину,
        /// то он разбивается на несколько файлов по N строк в каждом 
        /// </summary>
        public int N
        {
            get 
            { 
                return n; 
            }
            set
            {
                if (value < 10)
                    n = 10;
                else if (value > 100000)
                    n = 100000;
                else
                    n = value;
             } 
        }
        private int n;
        /// <summary>
        /// Расположение html файла
        /// </summary>
        public string InputFileName { get; set; }
        /// <summary>
        /// Расположение словаря
        /// </summary>
        public string DictLocation { get; set; }
        /// <summary>
        /// Расположение результирующего файла
        /// </summary>
        public string OutputFileName { get; set; }
        /// <summary>
        /// Словарь для хранения "словаря"
        /// </summary>
        private Hashtable dictionary = new Hashtable();
        /// <summary>
        /// Размер словаря
        /// </summary>
        public int DictSize { get { return dictionary.Count; } }
        /// <summary>
        /// Флаги состояния отвечающие за состояние входных (словарь, обрабатываемый файл)
        /// и выходных файлов. Если файл подключен, выставляется соответсвующий флаг.
        /// </summary>
        [Flags]
        public enum IOStatus
        {
            None = 0x0,
            OutLoaded = 0x1,
            InLoaded = 0x2,
            DictLoaded = 0x4
        }
        private IOStatus status;
        public IOStatus Status { get { return status; } }
        public HtmlCorrector()
        {
            status = IOStatus.None;
            N = 1000;
            OutputFileName = "";
        }

        #if (DEBUG)
        private void ShowDictionary()
        {
            foreach (string word in dictionary.Values)
            {
                Console.WriteLine(word);
            }
        }
        #endif
        /// <summary>
        /// Метод для загрузки словаря
        /// </summary>
        public void LoadDictionary()
        {
            try
            {
                // Открыть файл словаря
                using (StreamReader reader = File.OpenText(DictLocation))
                {
                    //Сбросить текущие значения словаря в null
                    if (dictionary.Count != 0)
                    {
                        dictionary.Clear();
                        if (status.HasFlag(IOStatus.DictLoaded))
                            status = status & ~IOStatus.DictLoaded;
                    }
     
                    string input = null;
                    int numOfLine = 0;
                    while (((input = reader.ReadLine()) != null) && (numOfLine < 100000))
                    {
                        if (input.Split(' ').Length > 1)
                            throw new HtmlCorrectorApp.DictionaryWrongFormatException("WARNING! Dictionary has wrong format.\nLine {0}: {1}.\nThe dictionary is not loaded.", numOfLine + 1, input);
                        dictionary.Add(numOfLine, input);
                        numOfLine++;
                    }
                    status |= IOStatus.DictLoaded;
                }
            }
            catch(FileNotFoundException ex)
            {
                //В случае, если файл не найден выдаётся предупреждение
                Console.WriteLine("WARNING! Dictionary file does not exsists.");
                DictLocation = null;
            }
            catch (HtmlCorrectorApp.DictionaryWrongFormatException ex)
            {
                //В случае, если файл имеет неправильную структуру словарь очищается
                Console.WriteLine(ex.Message);
                dictionary.Clear();
                DictLocation = null;
            }
        }
        /// <summary>
        /// Метод для корректировки содержимого html файла
        /// </summary>
        public void CorrectHtml()
        {
            try
            {
                //Открыть файл для чтения
                using (StreamReader reader = File.OpenText(InputFileName))
                {
                    //Счетчик для добавления в название файла
                    int i = 0;
                    //Создать файл для записи
                    while (!reader.EndOfStream)
                        using (StreamWriter writer = File.CreateText(OutputFileName + (i++) + ".html"))
                        {
                            string input = null;
                            int string_count = 0; // Количество записанных в файл строк
                            while (((input = reader.ReadLine()) != null) && (string_count < N))
                            {
                                Match m = Regex.Match(input, @"\d*\w+\d*"); //Шаблон соответствия
                                int shift = 0; //Сдвиг
                                while (m.Success)
                                {
                                    if (dictionary.ContainsValue(m.Value.ToLower()))
                                    {
                                        input = input.Insert(m.Index + shift, "<b>")
                                            .Insert(m.Index + shift + m.Length + 3, "</b>");
                                        shift += 7; //Количество символов в <b></b>
                                    }
                                    m = m.NextMatch();
                                }
                                writer.WriteLine(input);
                                string_count++;
                            }
                        }
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("WARNING! Input file does not exsists.");
                InputFileName = null;
                if (status.HasFlag(IOStatus.InLoaded))
                    status = status & ~IOStatus.InLoaded;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("WARNING! Output file does not exsists.");
                OutputFileName = null;
                if (status.HasFlag(IOStatus.OutLoaded))
                    status = status & ~IOStatus.OutLoaded;
            }
        }
        public override string ToString()
        {
            Console.Clear();
            return string.Format("Input  file: {0}\n" + 
                "Output file     : {1}\n"  + 
                "Strings per file: {2}"    +
                "Dictionary      :\n"      + 
                " 1.File Name - {3}\n" + 
                " 2.Size      - {4}",
                InputFileName, OutputFileName, N, DictLocation, dictionary.Count);
        }
        /// <summary>
        /// Вывод меню в консоль
        /// </summary>
        private void ShowMenu()
        {
            Console.WriteLine("Код команды: Назначение.");
            if (status.HasFlag(IOStatus.InLoaded))
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.WriteLine("input      : Ввод названия входного файла.");
            Console.ForegroundColor = ConsoleColor.Gray;
            if (status.HasFlag(IOStatus.OutLoaded))
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.WriteLine("output     : Ввод названия выходного файла.");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("length     : Количество строк в выходных файлах (по умолчанию 1000).");
            if (status.HasFlag(IOStatus.DictLoaded))
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.WriteLine("dict       : Ввод названия файла словаря и его загрузка в память.");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("info       : Информация о данных программы.");
            if (status.HasFlag(IOStatus.DictLoaded | IOStatus.InLoaded | IOStatus.OutLoaded))
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
            InputFileName = Console.ReadLine();
            try
            {
                if (!new FileInfo(InputFileName).Exists)
                {
                    Console.WriteLine("WARNING! No such input file.");
                    InputFileName = null;
                    return;
                }
            }
            catch (System.ArgumentException ex)
            {
                Console.WriteLine("WARNING! Invalid input file name or path.");
                InputFileName = null;
                return;
            }
            status |= IOStatus.InLoaded;
        }
        /// <summary>
        /// Ввод названия выходного файла
        /// </summary>
        private void EnterOutFile()
        {
            Console.Clear();
            Console.Write("Enter output file name: ");
            OutputFileName = Console.ReadLine();
            if (OutputFileName.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
            {
                Console.WriteLine("WARNING! Output file name contains invalid symbols.");
                OutputFileName = null;
                return;
            }
            status |= IOStatus.OutLoaded;
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
                N = Convert.ToInt32(temp);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("WARNING! Input string is not a sequence of digits.");
            }
            catch (OverflowException ex)
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
            DictLocation = Console.ReadLine();
            LoadDictionary();
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
                        Console.WriteLine(ToString());
                        Console.ReadLine();
                        break;
                    case "correct": // Корректировка html
                        if (status.HasFlag((IOStatus.DictLoaded | IOStatus.InLoaded | IOStatus.OutLoaded)))
                        { CorrectHtml(); }
                        break;
                    default:
                        break;
                }
                Console.Clear();
            }
        }
    }
}
