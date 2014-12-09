using System.Collections;

namespace Corrector
{
    public interface ICorrectorDictionary : ICollection, IEnumerable
    {
        /// <summary>
        /// Удаление всех элементов из словаря.
        /// </summary>
        void Clear();
        /// <summary>
        /// Добавление элемента в словарь.
        /// </summary>
        /// <param name="element">Элемент для добавления.</param>
        void Add(string element);
        /// <summary>
        /// Проверка на наличие элемента.
        /// </summary>
        /// <param name="element">Элемент для проверки</param>
        /// <returns></returns>
        bool Contains(string element);
    }
}
