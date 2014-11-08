﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HtmlCorrectorApp
{
    class HtmlCorrector
    {
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
        /// Список для хранения словаря
        /// </summary>
        private List<string> dictionary = new List<string>();
        //Флаги состояния
        private bool isOutLoaded; 
        private bool isInLoaded;
        private bool isDictLoaded;

        public HtmlCorrector()
        {
            isOutLoaded = false;
            isInLoaded = false;
            isDictLoaded = false;
        }

        private void ShowDictionary()
        {
            foreach (string word in dictionary)
            {
                Console.WriteLine(word);
            }
        }
        /// <summary>
        /// Метод для загрузки словаря
        /// </summary>
        public void LoadDictionary()
        {
            // Открыть файл словаря
            using (StreamReader reader = File.OpenText(DictLocation))
            {
                string input = null;
                while (((input = reader.ReadLine()) != null) && (dictionary.Count < 100000))
                { dictionary.Add(input); }
            }           
        }
        /// <summary>
        /// Метод для корректировки содержимого html файла
        /// </summary>
        public void CorrectHtml() 
        {
            //Открыть файл для чтения
            using (StreamReader reader = File.OpenText(InputFileName))
            {
                //Создать файл для записи
                using (StreamWriter writer = File.CreateText(OutputFileName))
                {
                    string input = null;
                    while ((input = reader.ReadLine()) != null)
                    {
                        Match m = Regex.Match(input, @"\w+"); //Шаблон соответствия
                        int shift = 0; //Сдвиг
                        while (m.Success)
                        {
                            if (dictionary.Contains(m.Value.ToLower()))
                            {
                                input = input.Insert(m.Index + shift, "<b>")
                                    .Insert(m.Index + shift + m.Length + 3, "</b>");
                                shift += 7; //Количество символов в <b></b>
                                Console.WriteLine(m.Value);
                            }
                            m = m.NextMatch();
                        }
                        writer.WriteLine(input);
                    }
                }
            }
        }
        public override string ToString()
        {
            return string.Format("+++++ HtmlCorrectorInfo +++++\nInput file: {0}\nOutput file: {1}\nDictionary:\n 1.File Name: {2}\n 2.Size: {3}",
                InputFileName, OutputFileName, DictLocation, dictionary.Count);
        }
        /// <summary>
        /// Вывод меню в консоль
        /// </summary>
        private void ShowMenu() 
        {
            Console.WriteLine("Код команды: Назначение.");
            if (isInLoaded) 
            {
                Console.ForegroundColor = ConsoleColor.Green;
            } 
            Console.WriteLine("input: Ввод названия входного файла.");
            Console.ForegroundColor = ConsoleColor.Gray;
            if (isOutLoaded)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.WriteLine("output: Ввод названия выходного файла.");
            Console.ForegroundColor = ConsoleColor.Gray;
            if (isDictLoaded)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.WriteLine("dict: Ввод названия файла словаря и его загрузка в память.");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("info: Информация о данных программы.");
            if (isDictLoaded & isInLoaded & isOutLoaded)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.WriteLine("correct: Корректировка html.");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("exit: Выход.");
        }
        /// <summary>
        /// Ввод названия входного файла и проверка на его существование
        /// </summary>
        /// <returns></returns>
        private bool EnterInFile() 
        {
            Console.Write("Enter input file name: ");
            InputFileName = Console.ReadLine();
            try
            {
                if (!new FileInfo(InputFileName).Exists)
                {
                    Console.WriteLine("Warning! No such input file.");
                    InputFileName = null;
                    return false;
                }
            }
            catch (System.ArgumentException ex)
            {
                Console.WriteLine("Warning! Invalid input file name or path.");
                InputFileName = null;
                return false;
            }
            return true;
        }
        /// <summary>
        /// Ввод названия выходного файла
        /// </summary>
        /// <returns></returns>
        private bool EnterOutFile()
        {
            Console.Write("Enter output file name: ");
            OutputFileName = Console.ReadLine();
            if (OutputFileName.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
            {
                Console.WriteLine("Warning! Output file name contains invalid symbols.");
                OutputFileName = null;
                return false;
            }
            return true;
        }
        /// <summary>
        /// Ввод названия файла словаря проверка на его существование и загрузка в память
        /// </summary>
        /// <returns></returns>
        private bool EnterDict() 
        {
            bool status = false;
            Console.Write("Enter dictionary file name: ");
            DictLocation = Console.ReadLine(); 
            if (new FileInfo(DictLocation).Exists == false)
            {
                Console.WriteLine("Warning! No such dictionary file");
                InputFileName = null;
                return status;
            }
            using (StreamReader reader = File.OpenText(DictLocation))
            {
                string input = null;
                while (((input = reader.ReadLine()) != null) && (dictionary.Count < 100000)) //загружаем словарь
                { dictionary.Add(input); }
                status = true;
            }
            return status;
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
                Console.Write("Enter command code: ");
                menuCode = Console.ReadLine();
                switch (menuCode)
                {
                    case "input": //Ввод названия входного файла
                        isInLoaded = EnterInFile();
                        Console.ReadLine();
                        break;
                    case "output": //Ввод названия выходного файла
                        isOutLoaded = EnterOutFile();
                        Console.ReadLine();
                        break;
                    case "dict": //Ввод названия файла словаря и его загрузка в память
                        isDictLoaded = EnterDict();
                        Console.ReadLine();
                        break;
                    case "info": // Информация о данных программы
                        Console.WriteLine(ToString());
                        Console.ReadLine();
                        break;
                    case "correct": // Корректировка html
                        if (isDictLoaded & isInLoaded & isOutLoaded)
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