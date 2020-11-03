using JetBrains.Annotations;

namespace Velvetech.Students.API.Models
{
    /// <summary>
    /// Пагинация
    /// </summary>
    [PublicAPI]
    public class Pagination
    {
        /// <summary>
        /// Текущая страница
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Количество элементов на странице 
        /// </summary>
        public int PageSize { get; set; } = 25;

        /// <summary>
        /// Фильтр
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Поле для фильтрации
        /// </summary>
        public string FilterBy { get; set; }
    }
}