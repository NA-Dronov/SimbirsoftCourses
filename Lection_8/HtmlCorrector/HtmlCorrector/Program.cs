using CorrectorUI;
using Corrector;
using CustomTextFileCorrector;
using Microsoft.Practices.Unity;

namespace HtmlCorrector
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();
            container.RegisterType<ICorrectorDictionary, DictList>();
            container.RegisterType<ICorrector, FileCorrector>();
            CUI ui = container.Resolve<CUI>();
            ui.menu();
        }
    }
}
