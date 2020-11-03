using System.Collections.Generic;
using JetBrains.Annotations;
using Velvetech.Students.Domain.Exceptions;
using Velvetech.Students.Domain.Model;
using Velvetech.Students.Domain.Types;

namespace Velvetech.Students.Domain.Entities
{
    /// <summary>
    /// Студент
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Минимальное кол-во символов для UIS
        /// </summary>
        private static readonly int _minLenghtUIS = 6;

        protected Student()
        {
        }

        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// Пол
        /// </summary>
        public Gender Gender { get; private set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; private set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; private set; }

        /// <summary>
        /// Уникальный идентификатор студента 
        /// </summary>
        public string UIS { get; private set; }

        /// <summary>
        /// Связующая сущность для отношения  многие ко многим с группами
        /// </summary>
        public ICollection<StudentGroup> StudentGroups { get; set; }

        #region Methods

        /// <summary>
        /// Изменить уникальный идентификатор студента
        /// </summary>
        /// <param name="uis">Уникальный идентификатор студента</param>
        public void ChangeUIS(string uis)
        {
            ValidateUIS(uis);
            UIS = uis;
        }

        /// <summary>
        /// Обновить данные студента 
        /// </summary>
        /// <param name="item">Данные студента</param>
        public void Update([NotNull] StudentDto item)
        {
            Name = item.Name;
            Surname = item.Surname;
            Patronymic = item.Patronymic;
            Gender = item.Gender;
            ChangeUIS(item.UIS);
        }

        /// <summary>
        /// Создать студента
        /// </summary>
        /// <param name="item">Данные студента</param>
        /// <returns></returns>
        public static Student Create([NotNull] StudentDto item)
        {
            ValidateUIS(item.UIS);

            return new Student
            {
                Name = item.Name,
                Surname = item.Surname,
                Patronymic = item.Patronymic,
                Gender = item.Gender,
                UIS = item.UIS,
            };
        }

        /// <summary>
        /// Валидация уникальный идентификатор студента
        /// </summary>
        /// <param name="uis">Уникальный идентификатор</param>
        /// <exception cref="StudentException">Ошибка создания студента</exception>
        private static void ValidateUIS([CanBeNull] string uis)
        {
            if (uis != null && uis.Length < _minLenghtUIS)
            {
                throw new StudentException(
                    $"Уникальный идентификатор студента должен содержать не менее {_minLenghtUIS} символов");
            }
        }

        #endregion
    }
}