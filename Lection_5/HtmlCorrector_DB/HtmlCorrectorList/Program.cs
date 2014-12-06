using System.Collections.Generic;
using AbstractHtmlCorrector;

namespace HtmlCorrectorList
{
    /// <summary>
    /// Программный List с поддержкой требуемого интерфейса. Так как методы интерфейса входят в List по
    /// умолчанию, то определять их не надо.
    /// </summary>
    class DictList : List<string>, ICAC<string>
    { }

    class Program
    {
        static void Main(string[] args)
        {
            HtmlCorrector<DictList> hc = new HtmlCorrector<DictList>();
            hc.menu();
        }
    }
}
