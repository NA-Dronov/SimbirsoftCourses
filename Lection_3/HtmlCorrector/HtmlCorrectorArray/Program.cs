using AbstractHtmlCorrector;
using System;
using System.Collections;
using System.Linq;

namespace HtmlCorrectorArray
{
    /// <summary>
    /// Программный Array с поддержкой требуемых интерфейсов.
    /// </summary>
    class DictArray: ICollection, IEnumerable, ICAC<string>
    {

        private string[] container = new string[100000];

        private int index = 0;

        public int Index
        {
            get
            {
                return index;
            }
            set
            {
                if (value < 0)
                    index = 0;
                else if (value > container.GetLength(0) - 1)
                    index = container.GetLength(0) - 1;
                else
                    index = value;
            }
        }

        public DictArray()
        {
            Clear();
        }

        public void Add(string element)
        {
            container[Index] = element;
            Index++;
        }

        public void Clear()
        {
            Array.Clear(container, container.GetLowerBound(0), container.GetUpperBound(0));
            index = 0;
        }

        public bool Contains(string element)
        {
            return container.Contains(element);
        }

        public IEnumerator GetEnumerator()
        {
            return container.GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            container.CopyTo(array, index);
        }

        public int Count
        {
            get {
                    return index;
                }
        }

        public bool IsSynchronized
        {
            get 
            { return container.IsSynchronized; }
        }

        public object SyncRoot
        {
            get { return container.SyncRoot; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //DictArray d = new DictArray();
            //Console.WriteLine(d.Index);
            //d.Index++;
            //Console.WriteLine(d.Index);
            HtmlCorrector<DictArray> hc = new HtmlCorrector<DictArray>();
            hc.menu();
        }
    }
}
