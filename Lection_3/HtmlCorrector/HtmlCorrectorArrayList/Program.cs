using AbstractHtmlCorrector;
using System.Collections;

namespace HtmlCorrectorArrayList
{
    /// <summary>
    /// Программный ArrayList с поддержкой требуемого интерфейса. Метод Clear уже реализован
    /// стандартным контейнером.
    /// </summary>
    class DictArrayList : ArrayList, ICAC<string>
    {
        public void Add(string element)
        {
            base.Add(element);
        }

        public bool Contains(string element)
        {
            return base.Contains(element);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            HtmlCorrector<DictArrayList> hc = new HtmlCorrector<DictArrayList>();
            hc.menu();
        }
    }
}
