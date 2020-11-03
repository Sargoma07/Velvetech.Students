using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Velvetech.Students.Domain.Types;

namespace Velvetech.Students.Domain.Model
{
    /// <summary>
    /// Студент - DTO  
    /// </summary>
    [PublicAPI]
    public class StudentDto
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
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Required]
        [MaxLength(40)]
        public string Surname { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [MaxLength(60)]
        public string Patronymic { get; set; }

        /// <summary>
        /// Уникальный идентификатор студента 
        /// </summary>
        [MinLength(6)]
        [MaxLength(16)]
        public string UIS { get; set; }
    }
}