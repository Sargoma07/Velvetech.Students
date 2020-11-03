using System.Collections.Generic;
using JetBrains.Annotations;
using Velvetech.Students.Domain.Model;

namespace Velvetech.Students.API.Models
{
    /// <summary>
    /// Группа
    /// </summary>
    [PublicAPI]
    public class Group
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Количество студентов
        /// </summary>
        /// <returns></returns>
        public int StudentCount => Students.Count;

        /// <summary>
        /// Студенты
        /// </summary>
        public ICollection<StudentDto> Students { get; set; }
    }
}