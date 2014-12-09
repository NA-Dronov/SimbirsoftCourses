    namespace Corrector
    {
        public interface ICorrector
        {
            /// <summary>
            /// Статус корректора.
            /// </summary>
            CorrectorStatusFlags Status { get; }
            /// <summary>
            /// Источник словаря.
            /// </summary>
            string DictSource { get; }
            /// <summary>
            /// Источник входных данных.
            /// </summary>
            string InSource { get; }
            /// <summary>
            /// Результирующие данные.
            /// </summary>
            string OutSource { get; }
            /// <summary>
            /// Метод для загрузки словаря из источника.
            /// </summary>
            void LoadDictionarySource(string dictionarySource);
            /// <summary>
            /// Метод для загрузки/подключения источника данных.
            /// </summary>
            void LoadInSource(string inSource);
            /// <summary>
            /// Метод для указания результирующих данных
            /// </summary>
            void LoadOutSource(string outSource);

            /// <summary>
            /// Метод корректировobr содержимого источника данных.
            /// </summary>
            void Correct();
            /// <summary>
            /// Метод, который записывает статус объекта в строку.
            /// </summary>
            /// <returns>Информация об объекте.</returns>
            string ToString();
        }
    }

