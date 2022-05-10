using System.Runtime.Serialization;

namespace SyntaxParser
{
    [Serializable]
    internal class LangException : Exception
    {
        private object p;

        public LangException()
        {
        }

        public LangException(object p)
        {
            this.p = p;
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