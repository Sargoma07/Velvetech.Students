using System;
using Marques.EFCore.SnakeCase;
using Microsoft.EntityFrameworkCore;
using Velvetech.Students.Domain.Entities;

namespace Velvetech.Students.Data
{
    /// <summary>
    /// DbContext
    /// </summary>
    public class StudentDbContext : DbContext
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Студенты
        /// </summary>
        public DbSet<Student> Students { get; set; }

        /// <summary>
        /// Группы
        /// </summary>
        public DbSet<Group> Groups { get; set; }

        /// <summary>
        /// Связующая таблица для отношения многие-ко-многим (Student,Group)
        /// </summary>
        public DbSet<StudentGroup> StudentGroups { get; set; }

        /// <summary>
        /// Конфигурация БД
        /// </summary>
        /// <param name="modelBuilder">Builder модели для конфигурации БД</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ToSnakeCase();
            modelBuilder.SetDefaultDateTimeKind(DateTimeKind.Utc);

            ConfigureEntities(modelBuilder);
        }

        private static void ConfigureEntities(ModelBuilder modelBuilder)
        {
            ConfigureStudent(modelBuilder);
            ConfigureGroup(modelBuilder);

            ConfigureStudentGroup(modelBuilder);
        }

        private static void ConfigureStudentGroup(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentGroup>()
                .HasKey(pc => new {pc.StudentId, pc.GroupId});

            modelBuilder.Entity<StudentGroup>()
                .HasOne(x => x.Student)
                .WithMany(x => x.StudentGroups)
                .HasForeignKey(x => x.StudentId);

            modelBuilder.Entity<StudentGroup>()
                .HasOne(x => x.Group)
                .WithMany(x => x.StudentGroups)
                .HasForeignKey(x => x.GroupId);
        }

        private static void ConfigureStudent(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().Property(x => x.Name)
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(40);

            modelBuilder.Entity<Student>().Property(x => x.Surname)
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(40);
            modelBuilder.Entity<Student>().Property(x => x.Patronymic)
                .HasColumnType("VARCHAR")
                .HasMaxLength(60);

            modelBuilder.Entity<Student>().HasIndex(x => x.UIS).IsUnique();
            modelBuilder.Entity<Student>().Property(x => x.UIS)
                .HasColumnType("VARCHAR")
                .HasMaxLength(16);
        }

        private static void ConfigureGroup(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>().Property(x => x.Name)
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(25);
        }
    }
}