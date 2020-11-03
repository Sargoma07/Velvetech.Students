using System.Threading.Tasks;
using Velvetech.Students.Domain.Entities;

namespace Velvetech.Students.Domain.Repositories
{
    /// <summary>
    /// Репозиторий для работы с сущностью группа
    /// </summary>
    public interface IGroupRepository : IRepository<Group>
    {
        /// <summary>
        /// Получить группу по id
        /// </summary>
        /// <param name="id">Id группы</param>
        /// <returns>Группа</returns>
        Task<Group> GetById(long id);

        /// <summary>
        /// Добавить студента в группу 
        /// </summary>
        /// <param name="group">Группа</param>
        /// <param name="student">Студент для добавления</param>
        /// <returns></returns>
        Task AddStudent(Group group, Student student);

        /// <summary>
        /// Удалить студента из группы
        /// </summary>
        /// <param name="group">Группа</param>
        /// <param name="student">Студент, которого нужно удалить</param>
        /// <returns></returns>
        Task DeleteStudent(Group group, Student student);
    }
}