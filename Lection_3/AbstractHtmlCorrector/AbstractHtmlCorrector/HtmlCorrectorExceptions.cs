using System;
using System.Runtime.Serialization;

namespace AbstractHtmlCorrector
{
    [Serializable]
    public class DictionaryWrongFormatException: Exception
    {
        public DictionaryWrongFormatException() 
        : base() { }

        public DictionaryWrongFormatException(string message) 
        : base(message) { }

        public DictionaryWrongFormatException(string format, params object[] args)
        : base(string.Format(format, args)) { }
    
        public DictionaryWrongFormatException(string message, Exception innerException)
        : base(message, innerException) { }
    
        public DictionaryWrongFormatException(string format, Exception innerException, params object[] args)
        : base(string.Format(format, args), innerException) { }

        protected DictionaryWrongFormatException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
    }
}

