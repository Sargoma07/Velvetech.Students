using System;
using JetBrains.Annotations;

namespace Velvetech.Students.Infrastructure.Pagination
{
    /// <summary>
    /// Метаданные страничного представления списков
    /// </summary>
    public class PaginationMetadata
    {
        /// <summary>
        /// Номер страницы
        /// </summary>
        public int Page { get; }

        /// <summary>
        /// Размер страницы
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int PageSize { get; }

        /// <summary>
        /// Общее количество страниц
        /// </summary>
        public int TotalPage { get; }

        /// <summary>
        /// Общее количество элементов
        /// </summary>
        public long TotalItem { get; }

        /// <summary>
        /// Строка поиска
        /// <remarks> Опционально</remarks>
        /// </summary>
        [CanBeNull]
        public string Filter { get; }

        /// <summary>
        /// Поиск по полю
        /// <remarks> Опционально</remarks>
        /// </summary>
        [CanBeNull]
        public string FilterBy { get; }

        /// <summary>
        /// Сортировка по полю
        /// <remarks> Опционально</remarks>
        /// </summary>
        [CanBeNull]
        public string OrderBy { get; }

        /// <summary>
        /// Порядок сортировки
        /// <remarks> Опционально</remarks>
        /// </summary>
        [CanBeNull]
        public SortDirection? Sort { get; }

        /// <summary>
        /// Закрытый конструктор
        /// </summary>
        private PaginationMetadata(int page, int pageSize, long totalItem, [CanBeNull] string filter = null,
            [CanBeNull] string filterBy = null, [CanBeNull] string orderBy = null,
            [CanBeNull] SortDirection? sort = null)
        {
            Page = page;
            PageSize = pageSize;
            TotalItem = totalItem;
            Filter = filter;
            FilterBy = filterBy;
            OrderBy = orderBy;
            Sort = sort;
            TotalPage = (int) Math.Ceiling((double) TotalItem / PageSize);
        }

        /// <summary>
        /// Статический фабричный метод для создания экземпляра PaginationMetadata
        /// </summary>
        /// <returns>PaginationMetadata</returns>
        public static PaginationMetadata Create(int page, int pageSize, long totalItem,
            [CanBeNull] string filter = null,
            [CanBeNull] string filterBy = null, [CanBeNull] string orderBy = null,
            [CanBeNull] SortDirection? sort = null)
        {
            return new PaginationMetadata(page, pageSize, totalItem, filter, filterBy, orderBy, sort);
        }
    }
}