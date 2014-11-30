using AbstractHtmlCorrector;
using System.Collections.Generic;

namespace HtmlCorrectorDictionary
{
    /// <summary>
    /// Программный Dictionary с поддержкой требуемого интерфейса. Метод Clear уже реализован
    /// стандартным контейнером.
    /// </summary>
    class DictDict : Dictionary<int, string>, ICAC<string>
    {
        public void Add(string element)
        {
            base.Add(base.Count, element);
        }

        public bool Contains(string element)
        {
            return base.ContainsValue(element);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            HtmlCorrector<DictDict> hc = new HtmlCorrector<DictDict>();
            hc.menu();
        }
    }
}
