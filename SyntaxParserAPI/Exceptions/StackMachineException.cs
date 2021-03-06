using System;
using System.Runtime.Serialization;

namespace SyntaxParserAPI
{
    /// <summary>Класс исключений для обратной польской нотации. Построение и Вычисление.</summary>
    [Serializable]
    internal class StackMachineException : Exception
    {
        public StackMachineException()
        {
        }

        public StackMachineException(string? message) : base(message)
        {
        }

        public StackMachineException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected StackMachineException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}