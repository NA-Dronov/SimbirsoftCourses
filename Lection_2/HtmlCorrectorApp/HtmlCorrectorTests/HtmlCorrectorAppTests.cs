using System;
using System.Linq;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HtmlCorrectorApp;

namespace HtmlCorrectorTests
{

    [TestClass]
    public class HtmlCorrectorAppTests
    {
        /// <summary>
        /// Проверка конструктора. Флаги состояний должны быть обнулены.
        /// </summary>
        [TestMethod]
        public void TestConstructor()
        {
            HtmlCorrector corrector = new HtmlCorrector();
            Assert.AreEqual(corrector.Status, HtmlCorrector.IOStatus.None);
        }
        /// <summary>
        /// Проверка параметра N на вхождение в заданный интервал
        /// </summary>
        [TestMethod]
        public void N_Value()
        {
            HtmlCorrector corrector = new HtmlCorrector();
            corrector.N = 9;
            Assert.IsTrue(corrector.N >=10 && corrector.N <= 100000, "Значение параметра N должно находится в интервале от 10 до 100000 (включая оба числа).");
        }
        /// <summary>
        /// Проверка на размер словаря (не больше 100000 строк)
        /// </summary>
        [TestMethod]
        public void TestDictionarySize()
        {
            HtmlCorrector corrector = new HtmlCorrector();
            //Загрузка файла Dict.txt содержащего 500000 строк
            corrector.DictLocation = "Dict.txt";
            corrector.LoadDictionary();
            //Программа должна загрузить 100000 строк из словаря
            Assert.AreEqual(corrector.DictSize, 100000);
            //Загрузка словаря
            corrector.LoadDictionary();
            //Размер словаря должен остаться прежним
            Assert.AreEqual(corrector.DictSize, 100000);

        }
        /// <summary>
        /// Проверка на ненайденный файл при загрузки словаря
        /// </summary>
        [TestMethod]
        public void TestDictionaryFileNotFoundException()
        {
            HtmlCorrector corrector = new HtmlCorrector();
            try
            {
                corrector.DictLocation = "unknown.txt";
                corrector.LoadDictionary();
            }
            catch (FileNotFoundException ex)
            {
                Assert.Fail();
            }
        }
        /// <summary>
        /// Проверка на структуру файла при загрузки словаря
        /// </summary>
        [TestMethod]
        public void TestDictionaryWrongFormatException()
        {
            HtmlCorrector corrector = new HtmlCorrector();
            try
            {
                corrector.DictLocation = "WrongStruct.txt";
                corrector.LoadDictionary();
            }
            catch (DictionaryWrongFormatException)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestLoadDictionaryTimeElapsed()
        {
            HtmlCorrector corrector = new HtmlCorrector();

            corrector.DictLocation = "Dict.txt";

            Stopwatch sw = new Stopwatch();

            sw.Start();
            corrector.LoadDictionary();
            sw.Stop();

            Console.WriteLine("Time elapsed: {0} msec", sw.ElapsedMilliseconds);
        }
        /// <summary>
        /// Проверка основного метода класса
        /// </summary>
        [TestMethod]
        public void TestMainFunc()
        {
            
            HtmlCorrector corrector = new HtmlCorrector();

            corrector.InputFileName = "InputFile.html";
            corrector.DictLocation = "Dict.txt";
            corrector.LoadDictionary();
            corrector.OutputFileName = @"Out\out";

            Stopwatch sw = new Stopwatch();

            DirectoryInfo di = new DirectoryInfo("Out");

            long[] result_N = new long[5]; // Результаты для N = 10, 100, 1000, 10000, 100000

            corrector.N = 10;

            var files = from f in di.GetFiles("*.html") where f.Extension == ".html" select f;  

            for (int i = 0; i < result_N.Length; i++)
            {
                 
                sw.Start();

                corrector.CorrectHtml();

                sw.Stop();

                result_N[i] = sw.ElapsedMilliseconds;
                Console.WriteLine("Time elapsed for N = {0}: {1} msec", corrector.N ,result_N[i]); 
                corrector.N *= 10;

                sw.Reset();

                foreach (var f in files)
                    File.Delete(f.FullName);
            }
        }
    }
}
