using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Velvetech.Students.Domain.Entities;
using Velvetech.Students.Domain.Exceptions;
using Velvetech.Students.Domain.Repositories;
using Velvetech.Students.Infrastructure.Pagination;

namespace Velvetech.Students.Data.Repositories
{
    /// <summary>
    /// Репозиторий для работы с сущностью студент 
    /// </summary>
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        private readonly StudentDbContext _dbContext;

        public StudentRepository(StudentDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public override async Task Add(Student item)
        {
            try
            {
                await base.Add(item);
            }
            catch (DbUpdateException e) when(((PostgresException) e.InnerException)?.SqlState == "23505") // Duplicate key
            {
                CheckDuplicateUIS(e);
            }
        }

        /// <inheritdoc />
        public override async Task Update(Student item)
        {
            try
            {
                await base.Update(item);
            }
            catch (DbUpdateException e) when(((PostgresException) e.InnerException)?.SqlState == "23505") // Duplicate key
            {
                CheckDuplicateUIS(e);
            }
        }

        /// <inheritdoc />
        public async Task<Student> GetById(long id)
        {
            var student = await DbSet.FindAsync(id);
            if (student == null)
            {
                return null;
            }
            
            await IncludeGroups(new[] {student});
            return student;
        }

        /// <inheritdoc />
        public override async Task<PaginationResult<Student>> GetAll(PaginationQuery paginationQuery)
        {
            if (paginationQuery.HasFilterBy && paginationQuery.HasFilter && paginationQuery.FilterBy == "groupname")
            {
                return await FilterByGroupName(paginationQuery);
            }

            var result = await base.GetAll(paginationQuery);
            await IncludeGroups(result.Items);

            return result;
        }

        /// <summary>
        /// Фильтрация по имени группы
        /// </summary>
        /// <param name="paginationQuery">Запрос на постраничный вывод</param>
        /// <returns>Данные с постраничным выводом</returns>
        private async Task<PaginationResult<Student>> FilterByGroupName(PaginationQuery paginationQuery)
        {
            var groups = await _dbContext.Groups
                .Where(x => x.Name == paginationQuery.Filter)
                .ToArrayAsync();

            var groupIds = groups.Select(x => x.Id).ToArray();

            // Подгружаем связанную таблицу для отношения многие-ко-многим
            //
            var studentGroups = await _dbContext
                .StudentGroups
                .Where(x => groupIds.Contains(x.GroupId))
                .ToArrayAsync();

            var query = PaginationQuery.Create(paginationQuery.Page, paginationQuery.PageSize);

            var a =  await base.GetAll(query, f =>
                studentGroups.Select(x => x.StudentId)
                    .Distinct()
                    .Contains(f.Id)
            );

            return a;
        }

        /// <inheritdoc />
        public override async Task<PaginationResult<Student>> GetAll(PaginationQuery paginationQuery,
            Expression<Func<Student, bool>> filter)
        {
            var result = await base.GetAll(paginationQuery, filter);
            await IncludeGroups(result.Items);
            return result;
        }

        /// <summary>
        /// Проверка на дубликат UIS
        /// </summary>
        /// <param name="e">Исключение</param>
        /// <exception cref="StudentException">Ошибка при работе с сущностью Student</exception>
        /// <exception cref="Exception">Другие Exception</exception>
        private static void CheckDuplicateUIS([NotNull] Exception e)
        {
            var exception = e.InnerException as PostgresException;
            if (exception?.ConstraintName != null && exception.ConstraintName.ToLower().Contains("uis"))
            {
                throw new StudentException($"Студент с таким {nameof(Student.UIS)} уже существует.");
            }

            throw e;
        }

        /// <summary>
        /// Включаем в выборку группы 
        /// </summary>
        /// <param name="students">Студенты</param>
        /// <returns>Студентами с загруженными группами в контекст</returns>
        private async Task IncludeGroups(IEnumerable<Student> students)
        {
            var studentIds = students.Select(x => x.Id).ToArray();

            // Подгружаем связанную таблицу для отношения многие-ко-многим
            //
            var studentGroups = await _dbContext
                .StudentGroups
                .Where(x => studentIds.Contains(x.StudentId))
                .ToArrayAsync();

            // Подгружаем группы в контекст 
            //
            await _dbContext
                .Groups
                .Where(x => studentGroups.Select(s => s.GroupId).Contains(x.Id))
                .ToArrayAsync();
        }
    }
}