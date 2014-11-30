using AbstractHtmlCorrector;
using System.Collections.Generic;

namespace HtmlCorrectorStack
{
    /// <summary>
    /// Программный Stack с поддержкой требуемого интерфейса.
    /// </summary>
    class DictStack : Stack<string>, ICAC<string>
    {
        public void Add(string element)
        {
            base.Push(element);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            HtmlCorrector<DictStack> hc = new HtmlCorrector<DictStack>();
            hc.menu();
        }
    }
}
