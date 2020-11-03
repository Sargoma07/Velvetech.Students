using System;

namespace Velvetech.Students.Infrastructure.Pagination
{
    /// <summary>
    /// Базовый класс для работы с постраничным выводом
    /// </summary>
    public class PaginationResult
    {
        public PaginationMetadata PageInfo { get; protected set; }
    }

    /// <summary>
    /// Базовый ответ с постраничным выводом
    /// </summary>
    public class PaginationResult<TEntity> : PaginationResult
    {
        /// <summary>
        /// Коллекция сущностей
        /// </summary>
        public TEntity[] Items { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="items">Коллекция сущностей</param>
        /// <param name="pageInfo">Метаданные</param>
        public PaginationResult(TEntity[] items, PaginationMetadata pageInfo)
        {
            Items = items;
            PageInfo = pageInfo;
        }

        /// <summary>
        /// Создать новый результат на основе метаданных
        /// </summary>
        /// <typeparam name="REntity">Новый тип сущности</typeparam>
        /// <param name="items">Коллекция сущностей</param>
        public PaginationResult<REntity> Create<REntity>(REntity[] items)
        {
            return new PaginationResult<REntity>(items, PageInfo);
        }

        /// <summary>
        /// Создать новый результат на основе метаданных
        /// </summary>
        /// <typeparam name="Result">Новый тип сущности</typeparam>
        /// <param name="lambda">Функция конвертации коллекции текущей сущности в новую</param>
        public PaginationResult<Result> Create<Result>(Func<TEntity[], Result[]> lambda)
        {
            return new PaginationResult<Result>(lambda(Items), PageInfo);
        }
    }
}