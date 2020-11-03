using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace Velvetech.Students.Domain.Model
{
    /// <summary>
    /// Группа - DTO  
    /// </summary>
    [PublicAPI]
    public class GroupDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
    }
}