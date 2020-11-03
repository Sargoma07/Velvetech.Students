using System;
using System.Runtime.Serialization;

namespace Velvetech.Students.Domain.Exceptions
{
    /// <summary>
    /// Ошибка создания студента
    /// </summary>
    [Serializable]
    public class StudentCreateException : Exception
    {
        public StudentCreateException()
        {
        }

        public StudentCreateException(string message) : base(message)
        {
        }

        public StudentCreateException(string message, Exception inner) : base(message, inner)
        {
        }

        protected StudentCreateException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}