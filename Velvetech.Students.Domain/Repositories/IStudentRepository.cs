using System.Threading.Tasks;
using Velvetech.Students.Domain.Entities;

namespace Velvetech.Students.Domain.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        /// <summary>
        /// Получить студента по id
        /// </summary>
        /// <param name="id">Id студента</param>
        /// <returns>Студент</returns>
        Task<Student> GetById(long id);
    }
}