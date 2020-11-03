using System;
using System.Runtime.Serialization;

namespace Velvetech.Students.Domain.Exceptions
{
    /// <summary>
    /// Ошибка при работе с сущностью студента
    /// </summary>
    [Serializable]
    public class StudentException : Exception
    {
        public StudentException()
        {
        }

        public StudentException(string message) : base(message)
        {
        }

        public StudentException(string message, Exception inner) : base(message, inner)
        {
        }

        protected StudentException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}