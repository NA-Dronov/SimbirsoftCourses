using AbstractHtmlCorrector;
using System.Collections;

namespace HtmlCorrectorHashTable
{
    /// <summary>
    /// Программный Hashtable с поддержкой требуемого интерфейса.
    /// </summary>
    class DictHashTable : Hashtable, ICAC<string>
    {
        public int Index { get; set; }

        public void Add(string element)
        {
            base.Add(Index, element);
            Index++;
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
            HtmlCorrector<DictHashTable> hc = new HtmlCorrector<DictHashTable>();
            hc.menu();
        }
    }
}
