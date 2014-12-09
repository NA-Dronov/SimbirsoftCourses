using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Corrector
{
    /// <summary>
    /// Источник словарь Array с поддержкой требуемых интерфейсов.
    /// </summary>
    public class DictArray : ICollection, IEnumerable, ICorrectorDictionary
    {

        private string[] container;

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
            container = new string[100000];
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
            get
            {
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
    /// <summary>
    /// Источник словарь ArrayList с поддержкой требуемого интерфейса. Метод Clear уже реализован
    /// стандартным контейнером.
    /// </summary>
    public class DictArrayList : ArrayList, ICorrectorDictionary
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
    /// <summary>
    /// Источник словарь Dictionary с поддержкой требуемого интерфейса. Метод Clear уже реализован
    /// стандартным контейнером.
    /// </summary>
    public class DictDict : Dictionary<int, string>, ICorrectorDictionary
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
    /// <summary>
    /// Источник словарь Hashtable с поддержкой требуемого интерфейса.
    /// </summary>
    public class DictHashTable : Hashtable, ICorrectorDictionary
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
    /// <summary>
    /// Источник словарь List с поддержкой требуемого интерфейса. Так как методы интерфейса входят в List по
    /// умолчанию, то определять их не надо.
    /// </summary>
    public class DictList : List<string>, ICorrectorDictionary
    { }
    /// <summary>
    /// Источник словарь Stack с поддержкой требуемого интерфейса.
    /// </summary>
    public class DictStack : Stack<string>, ICorrectorDictionary
    {
        public void Add(string element)
        {
            base.Push(element);
        }
    }
}
