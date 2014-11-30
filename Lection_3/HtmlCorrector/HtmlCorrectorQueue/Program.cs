using AbstractHtmlCorrector;
using System.Collections.Generic;

namespace HtmlCorrectorQueue
{
    /// <summary>
    /// Программный Queue с поддержкой требуемого интерфейса.
    /// </summary>
    class DictQueue : Queue<string>, ICAC<string>
    {
        public void Add(string element)
        {
            base.Enqueue(element);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            HtmlCorrector<DictQueue> hc = new HtmlCorrector<DictQueue>();
            hc.menu();
        }
    }
}
