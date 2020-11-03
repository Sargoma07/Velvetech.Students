using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Velvetech.Students.Domain.Model;

namespace Velvetech.Students.Domain.Entities
{
    /// <summary>
    /// Группа студентов
    /// </summary>
    public class Group
    {
        public Group(long id, string name):this()
        {
            Id = id;
            Name = name;
        }

        protected Group()
        {
        }

        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; protected set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Связующая сущность для отношения  многие ко многим со студентами
        /// </summary>
        public ICollection<StudentGroup> StudentGroups { get; set; }

        /// <summary>
        /// Изменить имя группы
        /// </summary>
        /// <param name="item">Новое имя группы</param>
        public void Update([NotNull] GroupDto item)
        {
            Name = item.Name;
        }

        /// <summary>
        /// Создать группу
        /// </summary>
        /// <param name="item">Данные для создания группы</param>
        /// <returns>Новая группа</returns>
        public static Group Create([NotNull] GroupDto item)
        {
            return new Group
            {
                Name = item.Name
            };
        }
    }
}