using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Corrector;
using CorrectorUI;
using System.IO;
using CustomTextFileCorrector;

namespace CorrectorUnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestCorrectorCUIConstructor()
        {
            var mockCorrector = MockRepository.GenerateMock<ICorrector>();
            CUI crt = new CUI(mockCorrector);
        }

        [TestMethod]
        public void TestCorrectorCUIShowMenu()
        {
            var mockCorrector = MockRepository.GenerateMock<ICorrector>();
            CUI crt = new CUI(mockCorrector);

            Type crtType = crt.GetType();
            MethodInfo mi = crtType.GetMethod("ShowMenu", BindingFlags.NonPublic | BindingFlags.Instance);
            mi.Invoke(crt, null);
        }

        [TestMethod]
        public void TestCorrectotCUIMenu()
        {
            var mockCorrector = MockRepository.GenerateMock<ICorrector>();
            CUI crt = new CUI(mockCorrector);
            using (var sr = new StringReader("exit"))
            {
                Console.SetIn(sr);
                crt.menu();
            }
        }

        [TestMethod]
        public void TestEnterLengthFailure()
        {
            var mockCorrector = MockRepository.GenerateMock<ICorrector>();
            CUI crt = new CUI(mockCorrector);

            Type crtType = crt.GetType();
            MethodInfo mi = crtType.GetMethod("EnterLength", BindingFlags.NonPublic | BindingFlags.Instance);
            try
            {
                using (var sr = new StringReader("Something to read"))
                {
                    Console.SetIn(sr);
                    mi.Invoke(crt, null);
                }
                Assert.Fail("Length is not implemented into ICorrector interface");
            }
            catch (Exception ex)
            {
                
            }
        }

        [TestMethod]
        public void TestEnterOutFile()
        {
            var mockCorrector = MockRepository.GenerateMock<ICorrector>();
            CUI crt = new CUI(mockCorrector);

            Type crtType = crt.GetType();
            MethodInfo mi = crtType.GetMethod("EnterOutFile", BindingFlags.NonPublic | BindingFlags.Instance);
            try
            {
                using (var sr = new StringReader("Path to file"))
                {
                    Console.SetIn(sr);
                    mi.Invoke(crt, null);
                }
            }
            catch (Exception ex)
            {

            }
        }

        [TestMethod]
        public void TestEnterInFile()
        {
            var mockCorrector = MockRepository.GenerateMock<ICorrector>();
            CUI crt = new CUI(mockCorrector);

            Type crtType = crt.GetType();
            MethodInfo mi = crtType.GetMethod("EnterInFile", BindingFlags.NonPublic | BindingFlags.Instance);
            try
            {
                using (var sr = new StringReader("Path to file"))
                {
                    Console.SetIn(sr);
                    mi.Invoke(crt, null);
                }
            }
            catch (Exception ex)
            {

            }
        }

        [TestMethod]
        public void TestEnterDict()
        {
            var mockCorrector = MockRepository.GenerateMock<ICorrector>();
            CUI crt = new CUI(mockCorrector);

            Type crtType = crt.GetType();
            MethodInfo mi = crtType.GetMethod("EnterDict", BindingFlags.NonPublic | BindingFlags.Instance);
            try
            {
                using (var sr = new StringReader("Path to file"))
                {
                    Console.SetIn(sr);
                    mi.Invoke(crt, null);
                }
            }
            catch (Exception ex)
            {

            }
        }

        [TestMethod]
        public void TestFileCorrectorConstructor()
        {
            var mockCorrectorDictionary = MockRepository.GenerateMock<ICorrectorDictionary>();
            FileCorrector fc = new FileCorrector(mockCorrectorDictionary);
        }

        [TestMethod]
        public void TestLoadDictionarySource()
        {
            var mockCorrectorDictionary = MockRepository.GenerateMock<ICorrectorDictionary>();
            FileCorrector fc = new FileCorrector(mockCorrectorDictionary);
            // Проверка на существующем файле
            fc.LoadDictionarySource("Test.txt");
            // Проверка на несуществующем файле
            fc.LoadDictionarySource("No");
        }

        [TestMethod]
        public void TestLoadInSource()
        {
            var mockCorrectorDictionary = MockRepository.GenerateMock<ICorrectorDictionary>();
            FileCorrector fc = new FileCorrector(mockCorrectorDictionary);
            // Проверка на существующем файле
            fc.LoadInSource("Test.txt");
            // Проверка на несуществующем файле
            fc.LoadInSource("No");
        }

        [TestMethod]
        public void TestLoadOutSource()
        {
            var mockCorrectorDictionary = MockRepository.GenerateMock<ICorrectorDictionary>();
            FileCorrector fc = new FileCorrector(mockCorrectorDictionary);
            fc.LoadOutSource("TestOut.txt");

            fc.Dispose();
        }

        [TestMethod]
        public void TestCorrection()
        {
            var mockCorrectorDictionary = MockRepository.GenerateMock<ICorrectorDictionary>();
            FileCorrector fc = new FileCorrector(mockCorrectorDictionary);
            // Должна быть обработка исключений
            fc.Correct();
            // Подключаем файл
            fc.LoadOutSource("TestOut.txt");
            fc.LoadInSource("Test.txt");
            fc.LoadDictionarySource("Test.txt");

            fc.Correct();
            fc.Dispose();
        }

        [TestMethod]
        public void TestCorrector_ToString()
        {
            var mockCorrectorDictionary = MockRepository.GenerateMock<ICorrectorDictionary>();
            FileCorrector fc = new FileCorrector(mockCorrectorDictionary);
            Console.WriteLine(fc.ToString());
            Console.WriteLine("LoadOut");
            fc.LoadOutSource("TestOut.txt");
            Console.WriteLine("LoadIn");
            fc.LoadInSource("TestOut.txt");
            Console.WriteLine("LoadDictionary");
            fc.LoadDictionarySource("TestOut.txt");

            Console.WriteLine(fc.ToString());
        }
    }
}
