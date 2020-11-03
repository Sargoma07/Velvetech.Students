using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Velvetech.Students.Domain.Entities;
using Velvetech.Students.Domain.Repositories;

namespace Velvetech.Students.Data.Repositories
{
    /// <summary>
    /// Репозиторий для работы с сущностью группа
    /// </summary>
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        private readonly StudentDbContext _dbContext;

        public GroupRepository(StudentDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<Group> GetById(long id)
        {
            var group = await DbSet.FindAsync(id);
            if (group == null)
            {
                return null;
            }
            await IncludeStudents(new[] {group});
            return group;
        }

        /// <inheritdoc />
        public override async Task<Group[]> GetAll()
        {
            var groups = await DbSet
                .OrderBy(t => t)
                .ToArrayAsync();
            
            await IncludeStudents(groups);
            return groups;
        }

        /// <inheritdoc />
        public override async Task<Group[]> GetAll(Expression<Func<Group, bool>> filter)
        {
            var groups = await DbSet
                .Where(filter)
                .OrderBy(t => t)
                .ToArrayAsync();
            
            await IncludeStudents(groups);
            return groups;
        }

        /// <summary>
        /// Включаем в выборку студентов 
        /// </summary>
        /// <param name="groups">Группы</param>
        /// <returns>Группы с загруженными студентами в контекст</returns>
        private async Task IncludeStudents(IEnumerable<Group> groups)
        {
            var groupIds = groups.Select(g => g.Id).ToArray();

            // Подгружаем связанную таблицу для отношения многие-ко-многим
            //
            var studentGroups = await _dbContext
                .StudentGroups
                .Where(x => groupIds.Contains(x.GroupId))
                .ToArrayAsync();

            // Подгружаем студентов в контекст 
            //
            await _dbContext
                .Students
                .Where(x => studentGroups.Select(s => s.StudentId).Contains(x.Id))
                .ToArrayAsync();
        }

        /// <inheritdoc />
        public async Task AddStudent(Group group, Student student)
        {
            var sg = new StudentGroup {Group = group, Student = student};
            await _dbContext.StudentGroups.AddAsync(sg);
            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteStudent(Group @group, Student student)
        {
            var sg = await _dbContext.StudentGroups
                .Where(x => x.GroupId == group.Id && x.StudentId == student.Id)
                .FirstOrDefaultAsync();

            if (sg == null)
            {
                return;
            }

            _dbContext.Remove(sg);
            await _dbContext.SaveChangesAsync();
        }
    }
}