using System;
using System.IO;
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
        /// Проверка на исключительные ситуации при загрузки словаря
        /// </summary>
        [TestMethod]
        public void TestDictionaryExceptions()
        {
            HtmlCorrector corrector = new HtmlCorrector();
            //Проверка на ненайденный файл
            corrector.DictLocation = "unknown.txt";
            corrector.LoadDictionary();
            //Проверка на структуру файла
            corrector.DictLocation = "WrongStruct.txt";
            corrector.LoadDictionary();
        }
        /// <summary>
        /// Проверка основного метода класса
        /// </summary>
        [TestMethod]
        public void TestMainFunc()
        {
            HtmlCorrector corrector = new HtmlCorrector();
            corrector.CorrectHtml();

            corrector.InputFileName = "InputFile.txt";
            corrector.CorrectHtml();       
        }
    }
}
