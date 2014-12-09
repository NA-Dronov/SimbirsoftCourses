using System;
using System.IO;
using System.Text.RegularExpressions;
using Corrector;

using CSF = Corrector.CorrectorStatusFlags;

namespace CustomTextFileCorrector
{
    public class FileCorrector : ICorrector, IDisposable
    {
        public FileCorrector(ICorrectorDictionary dictionary)
        {
            this.dictionary = dictionary;
            status = CorrectorStatusFlags.None;
            N = 1000;
            outSource = "";
            inSourceReader = null;
        }
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

        private string dictSource;
        public string DictSource
        {
            get { return dictSource; }
        }

        private string inSource;
        public string InSource
        {
            get { return inSource; }
        }

        private string outSource;
        public string OutSource
        {
            get { return outSource; }
        }

        private ICorrectorDictionary dictionary;
        /// <summary>
        /// Размер словаря
        /// </summary>
        public int DictSize { get { return dictionary.Count; } }

        private CorrectorStatusFlags status;
        public CorrectorStatusFlags Status { get { return status; } }

        private StreamReader inSourceReader;

        public void LoadDictionarySource(string dictionarySource)
        {
            try
            {
                // Создаем поток для чтения из файла словаря
                using (StreamReader reader = File.OpenText(dictionarySource))
                {
                    //Очистить словарь и сбросить соответствующий флаг, если он уже существует
                    if (dictionary.Count != 0)
                    {
                        dictSource = null;
                        dictionary.Clear();
                        if (status.HasFlag(CSF.DictLoaded))
                            status = status.Remove(CSF.DictLoaded);
                    }

                    string input = null;

                    // Считываем словарь и записываем его в контейнер
                    while (((input = reader.ReadLine()) != null) && (dictionary.Count <= 100000))
                    {
                        // Если в строке больше одного слова, то она игнорируется
                        if (input.Split(' ').Length > 1)
                            continue;
                        dictionary.Add(input);
                    }
                    dictSource = dictionarySource;
                    status = status.Add(CSF.DictLoaded);
                }
            }
            catch (Exception ex)
            {
                //В случае ошибки при обработке источника словаря, контейнер очищается
                Console.WriteLine(ex.Message);
                dictSource = null;
                dictionary.Clear();
            }
        }

        public void LoadInSource(string inSource)
        {
            try
            {
                //Закрываем поток, если он уже подсоединён к источнику данных.
                if (inSourceReader != null)
                {
                    inSourceReader.Close();
                    inSourceReader = null;
                    this.inSource = null;
                    status = status.Remove(CSF.InLoaded);
                }

                //Подключаем поток к источнику данных
                this.inSource = inSource;
                inSourceReader = File.OpenText(inSource);
                status = status.Add(CSF.InLoaded);
            }
            catch (Exception ex)
            {
                // В случае возникновения ошибки выводим сообщение
                Console.WriteLine(ex.Message);
                this.inSource = null;
                inSourceReader = null;
            }
        }

        public void LoadOutSource(string outSource)
        {
            try
            {
                //Обнуляем вывод данных, если он уже существет.
                if (this.outSource != null)
                {
                    this.outSource = null;
                    status = status.Remove(CSF.InLoaded);
                }
                // Создаём файл. Если файл создан успешно, то путь валиден
                this.outSource = outSource;
                FileStream f = File.Create(outSource);
                f.Close();
                status = status.Add(CSF.InLoaded);
            }
            catch (Exception ex)
            {
                //Обнуляем путь к результирующим данным.
                Console.WriteLine(ex.Message);
                this.outSource = null;
            }
        }

        public void Correct()
        {
            try
            {
                int i = 0;
                string ext; // Расширение файла
                string path; // Путь к файлу
                // Разделяем строку пути на подстроки при помощи точки, чтобы узнать разрешение файла
                string[] outPutSource = outSource.Split('.');
                // Если разрешение обнаружено, сохраняем его.
                if (outSource.Length > 1)
                {
                    ext = "." + outPutSource[outPutSource.GetUpperBound(0)];
                    Array.Resize(ref outPutSource, outPutSource.Length - 1);
                    path = string.Join(".", outPutSource);
                }
                else // Иначе присваиваем расширение по умолчанию
                {
                    ext = ".txt";
                    path = outPutSource[0];
                }
                    
                while(!inSourceReader.EndOfStream)
                    using(StreamWriter writer = File.CreateText(path + (i++) + ext))
                    {
                        string input = null;
                        int string_count = 0; // Количество записанных в файл строк
                        while (((input = inSourceReader.ReadLine()) != null) && (string_count < N))
                        {
                            Match m = Regex.Match(input, @"\d*\w+\d*"); //Шаблон соответствия
                            int shift = 0; //Сдвиг
                            while (m.Success)
                            {
                                if (dictionary.Contains(m.Value.ToLower()))
                                {
                                    input = input.Insert(m.Index + shift, "<b>")
                                        .Insert(m.Index + shift + m.Length + 3, "</b>");
                                    shift += 7; //Количество символов в <b></b>
                                }
                                m = m.NextMatch();
                            }
                            string_count++;
                        }
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public override string ToString()
        {
            Console.Clear();
            return string.Format("Input  file: {0}\n" +
                "Output file     : {1}\n" +
                "Strings per file: {2}\n" +
                "Dictionary      :\n" +
                " 1.File Name - {3}\n" +
                " 2.Size      - {4}",
                InSource, OutSource, N, DictSource, dictionary.Count);
        }

        public void Dispose()
        {
            try
            {
                inSourceReader.Close();
            }
            catch(Exception)
            {

            }
        }
    }
}
