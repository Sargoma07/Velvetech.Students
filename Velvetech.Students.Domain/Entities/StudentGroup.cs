namespace Velvetech.Students.Domain.Entities
{
    /// <summary>
    /// Связующая сущность для отношения  многие ко многим (Student - Group)
    /// </summary>
    public class StudentGroup
    {
        /// <summary>
        /// Id студента
        /// </summary>
        public long StudentId { get; set; }

        /// <summary>
        /// Студент
        /// </summary>
        public Student Student { get; set; }

        /// <summary>
        /// Id группы
        /// </summary>
        public long GroupId { get; set; }

        /// <summary>
        /// Группа 
        /// </summary>
        public Group Group { get; set; }
    }
}