using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Velvetech.Students.Infrastructure.Pagination;

namespace Velvetech.Students.Domain.Repositories
{
    /// <summary>
    /// Репозиторий
    /// </summary>
    /// <typeparam name="TEntity">Сущность, за которую отвечает репозиторий</typeparam>
    public interface IRepository<TEntity>
    {
        /// <summary>
        /// Получить все записи
        /// </summary>
        /// <returns>Коллекция сущностей</returns>
        Task<TEntity[]> GetAll();

        /// <summary>
        /// Получить все записи, удовлетворяющие фильтру
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <returns>Коллекция сущностей</returns>
        Task<TEntity[]> GetAll(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Получить записи с постраничным выводом 
        /// </summary>
        /// <param name="paginationQuery">Запрос на постраничное отображение данных</param>
        /// <returns>Постраничное отображение данных</returns>
        Task<PaginationResult<TEntity>> GetAll(PaginationQuery paginationQuery);

        /// <summary>
        /// Получить записи с постраничным выводом, удовлетворяющие фильтру 
        /// </summary>
        /// <param name="paginationQuery">Запрос на постраничное отображение данных</param>
        /// <param name="filter">Фильтр</param>
        /// <returns>Постраничное отображение данных</returns>
        IQueryable<TEntity> GetAllQuery(PaginationQuery paginationQuery, Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Поиск по Id 
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Найденная сущность</returns>
        Task<TEntity> Find(long id);

        /// <summary>
        /// Добавить сущность в репозиторий
        /// </summary>
        /// <param name="item">Сущность</param>
        /// <returns></returns>
        Task Add(TEntity item);

        /// <summary>
        /// Обновить сущность в репозитории 
        /// </summary>
        /// <param name="item">Сущность для обновления</param>
        /// <returns></returns>
        Task Update(TEntity item);

        /// <summary>
        /// Удалить сущность из репозитория по Id 
        /// </summary>
        /// <param name="id">Id сущности</param>
        /// <returns>Удаленная сущность</returns>
        Task<TEntity> Delete(long id);

        /// <summary>
        /// Удалить сущность из репозитория 
        /// </summary>
        /// <param name="item">Сущность для удаления</param>
        /// <returns>Удаленная сущность</returns>
        Task<TEntity> Delete(TEntity item);
    }
}