using System;

namespace Corrector
{
    /// <summary>
    /// Флаги состояния отвечающие за состояние входных (словарь, обрабатываемый файл)
    /// и выходных файлов. Если файл подключен, выставляется соответсвующий флаг.
    /// </summary>
    [Flags]
    public enum CorrectorStatusFlags
    {
        None = 0x0,
        OutLoaded = 0x1,
        InLoaded = 0x2,
        DictLoaded = 0x4
    }

    public static class CorrectorStatusFlagsMethods
    {
        /// <summary>
        /// Добавляет к флагам состояния f1 состояние f2
        /// </summary>
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        public static CorrectorStatusFlags Add(this CorrectorStatusFlags f1, CorrectorStatusFlags f2)
        {
            return f1 |= f2;
        }
        /// <summary>
        /// Убирает из флагов состояния f1 состояние f2
        /// </summary>
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        public static CorrectorStatusFlags Remove(this CorrectorStatusFlags f1, CorrectorStatusFlags f2)
        {
            return f1 &= ~f2;
        }
    }
}
