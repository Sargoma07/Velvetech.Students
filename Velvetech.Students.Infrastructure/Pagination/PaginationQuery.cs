using System;
using JetBrains.Annotations;

namespace Velvetech.Students.Infrastructure.Pagination
{
    /// <summary>
    /// Базовый запрос на постраничный вывод
    /// </summary>
    public class PaginationQuery
    {
        /// <summary>
        /// Номер страницы по умолчанию
        /// </summary>
        public static int PageNumberDefault = 1;

        /// <summary>
        /// Размер страницы по умолчанию
        /// </summary>
        public static int PageSizeDefault = 25;

        /// <summary>
        /// Максимальный размер страницы
        /// </summary>
        public static int PageSizeMax = 100;

        public int Page { get; }

        // ReSharper disable once InconsistentNaming
        public int PageSize { get; }

        public int Skip => (Page - 1) * PageSize;

        public int Limit => PageSize;

        [CanBeNull] public string Filter { get; }

        [CanBeNull] public string FilterBy { get; }

        /// <summary>
        /// Сортировка по полю
        /// <remarks>Работает пока только по полю string</remarks>
        /// </summary>
        [CanBeNull]
        public string OrderBy { get; }

        [CanBeNull] public SortDirection? Sort { get; }

        /// <summary>
        /// Задана ли строка фильтрации
        /// </summary>
        /// <returns>True если задано поле для фильтрации</returns>
        public bool HasFilter => !string.IsNullOrWhiteSpace(Filter);

        /// <summary>
        /// Задано ли поле фильтрации
        /// </summary>
        /// <returns>True если задано поле для фильтрации</returns>
        public bool HasFilterBy => !string.IsNullOrWhiteSpace(FilterBy);

        /// <summary>
        /// Задано ли поле для сортировки
        /// </summary>
        /// <returns>True если задано поле для сортировки</returns>
        public bool HasOrderBy => !string.IsNullOrWhiteSpace(OrderBy);

        /// <summary>
        /// Игнорировать регистр при поиске
        /// <remarks>Внимание! При включении возможно игнорирование ключей при поиске</remarks>
        /// </summary>
        /// <returns>True если игнорируется регистр</returns>
        public bool IgnoreCase { get; }

        /// <summary>
        /// Использовать частичный поиск (используя Contains)
        /// <remarks>Внимание! При включении возможно игнорирование ключей при поиске</remarks>
        /// </summary>
        /// <returns>True если используется частичный поиск</returns>
        public bool PartialSearch { get; }

        /// <summary>
        /// Закрытый конструктор
        /// </summary>
        private PaginationQuery(int? page, int? pageSize, [CanBeNull] string filter = null,
            [CanBeNull] string filterBy = null,
            [CanBeNull] string orderBy = null, [CanBeNull] string sort = null, bool ignoreCase = false,
            bool partialSearch = false)
        {
            var pageNumberInt = page ?? PageNumberDefault;
            var pageSizeInt = pageSize ?? PageSizeDefault;

            Filter = ignoreCase ? filter?.ToLower() : filter;
            FilterBy = filterBy?.ToLower();
            Page = pageNumberInt <= 0 ? PageNumberDefault : pageNumberInt;
            PageSize = pageSizeInt <= 0 ? PageSizeDefault : pageSizeInt;
            PageSize = PageSize > PageSizeMax ? PageSizeMax : PageSize;
            OrderBy = orderBy?.ToLower();
            IgnoreCase = ignoreCase;
            PartialSearch = partialSearch;

            if (Enum.TryParse(sort, true, out SortDirection parsedSortDirection))
                Sort = parsedSortDirection;
        }

        /// <summary>
        /// Статический фабричный метод для создания экземпляра PaginationQuery
        /// </summary>
        /// <returns>PaginationQuery</returns>
        public static PaginationQuery Create(int? page, int? pageSize, [CanBeNull] string filter = null,
            [CanBeNull] string filterBy = null,
            [CanBeNull] string orderBy = null, [CanBeNull] string sortDirection = null, bool ignoreCase = false,
            bool partialSearch = false)
        {
            return new PaginationQuery(page, pageSize, filter, filterBy, orderBy, sortDirection, ignoreCase,
                partialSearch);
        }

        /// <summary>
        /// Получить метаданные страничного представления на основе запроса
        /// </summary>
        /// <param name="totalItem">Общее количество элементов</param>
        /// <returns>Метаданные страничного представления списков</returns>
        public PaginationMetadata GetPaginationMetadata(long totalItem)
        {
            return PaginationMetadata.Create(Page, PageSize, totalItem, Filter, FilterBy, OrderBy, Sort);
        }
    }
}