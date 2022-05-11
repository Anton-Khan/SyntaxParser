using System;
using System.Runtime.Serialization;

namespace SyntaxParserAPI
{
    /// <summary>Класс исключений языка.</summary>
    [Serializable]
    internal class LangException : Exception
    {
        public LangException()
        {
        }

        public LangException(string? message) : base(message)
        {
        }

        public LangException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected LangException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}