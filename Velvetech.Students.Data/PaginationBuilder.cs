using System;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Velvetech.Students.Infrastructure.Pagination;

namespace Velvetech.Students.Data
{
    public static class PaginationBuilder
    {
        /// <summary>
        /// Получить отфильтрованный поставщик запросов в EF из базового запроса на постраничный вывод
        /// с использованием одного поля для сортировки и направления сортировки
        /// <remarks>
        /// String - фильтруется по вхождению без учёта регистра
        /// DateTime - фильтруется в интервале дня
        /// Enum, Int, Long - фильтруются по точному вхождению
        /// </remarks>
        /// </summary>
        /// <typeparam name="TEntity">Бизнес сущность</typeparam>
        /// <param name="queryable">Поставщик запросов</param>
        /// <param name="query">Базовый запрос на постраничный вывод</param>
        /// <returns>Фильтрация EF с пагинацией</returns>
        [NotNull]
        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> queryable,
            [NotNull] PaginationQuery query) where TEntity : class
        {
            if (!query.HasFilter)
            {
                return queryable;
            }

            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var property = Expression.Property(parameter, query.FilterBy ?? throw new ArgumentNullException());
            var type = property.Type;

            ConstantExpression constant;
            Expression binaryExpression;

            if (type.IsEnum)
            {
                var filter = Enum.Parse(type, query.Filter, ignoreCase: true);
                var conv = Enum.ToObject(type, filter);
                constant = Expression.Constant(conv, type);
                binaryExpression = Expression.Equal(property, constant);
            }
            else if (type == typeof(DateTime))
            {
                var convertDateTime = DateTime.Parse(query.Filter);
                var date = convertDateTime.Date;

                var leftDateTime = Expression.Constant(date, type);

                date = convertDateTime.Date.AddDays(1);
                var rightDateTime = Expression.Constant(date, type);

                var leftExp = Expression.GreaterThanOrEqual(property, leftDateTime);
                var rightExp = Expression.LessThan(property, rightDateTime);
                binaryExpression = Expression.And(leftExp, rightExp);
            }
            else if (type == typeof(int))
            {
                constant = Expression.Constant(Convert.ToInt32(query.Filter));
                binaryExpression = Expression.Equal(property, constant);
            }
            else if (type == typeof(long))
            {
                constant = Expression.Constant(Convert.ToInt64(query.Filter));
                binaryExpression = Expression.Equal(property, constant);
            }
            else if (type == typeof(string))
            {
                constant = Expression.Constant(query.Filter);
                if (query.IgnoreCase)
                {
                    var propString = Expression.Call(property, "ToLower", Type.EmptyTypes);
                    binaryExpression = Expression.Equal(propString, constant);
                }
                else
                {
                    binaryExpression = Expression.Equal(property, constant);
                }
            }
            else
            {
                throw new NotSupportedException($"Don`t supported type:{type.Name}");
            }

            var lambda = Expression.Lambda<Func<TEntity, bool>>(binaryExpression, parameter);

            return queryable.Where(lambda);
        }
    }
}