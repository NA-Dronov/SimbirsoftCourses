using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HtmlCorrectorApp;

namespace HtmlCorrectorTests
{

    [TestClass]
    public class HtmlCorrectorAppTests
    {
        [TestMethod]
        public void TestConstructor()
        {
            HtmlCorrector corrector = new HtmlCorrector();
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
        /// Проверка работы программы со словарём
        /// </summary>
        [TestMethod]
        public void TestDictionary()
        {
            HtmlCorrector corrector = new HtmlCorrector();
            corrector.LoadDictionary();
        }
    }
}
