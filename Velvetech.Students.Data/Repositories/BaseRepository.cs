using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Velvetech.Students.Domain.Repositories;
using Velvetech.Students.Infrastructure.Pagination;

namespace Velvetech.Students.Data.Repositories
{
    /// <summary>
    /// Базовый репозиторий для сущности типа {TEntity}
    /// </summary>
    /// <typeparam name="TEntity">Сущность, хранящаяся в репозитории</typeparam>
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbContext DbContext { get; }
        protected DbSet<TEntity> DbSet { get; }

        protected BaseRepository(DbContext context)
        {
            DbContext = context;
            DbSet = DbContext.Set<TEntity>();
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> Find(long id)
        {
            return await DbSet.FindAsync(id);
        }

        /// <inheritdoc />
        public virtual async Task<TEntity[]> GetAll()
        {
            var query = DbSet.OrderBy(t => t);

            return await query
                .AsNoTracking()
                .ToArrayAsync();
        }

        /// <inheritdoc />
        public virtual async Task<TEntity[]> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            var query = DbSet.Where(filter).OrderBy(t => t);

            return await query
                .AsNoTracking()
                .ToArrayAsync();
        }

        public virtual async Task<PaginationResult<TEntity>> GetAll(PaginationQuery paginationQuery,
            Expression<Func<TEntity, bool>> filter)
        {
            var query = GetAllQuery(paginationQuery, filter);

            var pagesMetadata = paginationQuery.GetPaginationMetadata(await query.LongCountAsync());

            return new PaginationResult<TEntity>(await query.ToArrayAsync(), pagesMetadata);
        }

        /// <inheritdoc />
        public virtual async Task<PaginationResult<TEntity>> GetAll(PaginationQuery paginationQuery)
        {
            var query = GetAllQuery(paginationQuery, null);

            var pagesMetadata = paginationQuery.GetPaginationMetadata(await query.LongCountAsync());

            return new PaginationResult<TEntity>(await query.ToArrayAsync(), pagesMetadata);
        }

        public IQueryable<TEntity> GetAllQuery(PaginationQuery paginationQuery, Expression<Func<TEntity, bool>> filter)
        {
            var query = DbSet.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (paginationQuery.HasFilter && paginationQuery.HasFilterBy)
            {
                query = query.Where(paginationQuery);
            }

            query = query
                .Skip(paginationQuery.Skip)
                .Take(paginationQuery.Limit);

            return query;
        }

        /// <inheritdoc />
        public virtual async Task Add(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            await DbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public virtual async Task Update(TEntity entity)
        {
            var state = DbContext.Entry(entity).State;
            if (state == EntityState.Detached)
                DbSet.Update(entity);

            await DbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> Delete(long id)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity == null)
                return null;

            DbSet.Remove(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> Delete(TEntity entity)
        {
            DbSet.Attach(entity);
            DbSet.Remove(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<int> Count(Expression<Func<TEntity, bool>> filter = null)
        {
            var query = DbSet.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.CountAsync();
        }
    }
}