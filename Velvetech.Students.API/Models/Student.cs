using System.Collections.Generic;
using JetBrains.Annotations;
using Velvetech.Students.Domain.Model;
using Velvetech.Students.Domain.Types;

namespace Velvetech.Students.API.Models
{
    /// <summary>
    /// Студент
    /// </summary>
    [PublicAPI]
    public class Student
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// Уникальный идентификатор студента 
        /// </summary>
        public string UIS { get; set; }

        /// <summary>
        /// Группы
        /// </summary>
        public ICollection<GroupDto> Groups { get; set; }
    }
}