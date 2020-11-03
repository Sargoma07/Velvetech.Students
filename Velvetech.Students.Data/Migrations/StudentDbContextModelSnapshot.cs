﻿// <auto-generated />

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Velvetech.Students.Data.Migrations
{
    [DbContext(typeof(StudentDbContext))]
    internal partial class StudentDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Velvetech.Students.Domain.Entities.Group", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id")
                    .HasColumnType("bigint")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(25);

                b.HasKey("Id")
                    .HasName("pk_groups");

                b.ToTable("groups");
            });

            modelBuilder.Entity("Velvetech.Students.Domain.Entities.Student", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id")
                    .HasColumnType("bigint")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<int>("Gender")
                    .HasColumnName("gender")
                    .HasColumnType("integer");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(40);

                b.Property<string>("Patronymic")
                    .HasColumnName("patronymic")
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(60);

                b.Property<string>("Surname")
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(40);

                b.Property<string>("UIS")
                    .HasColumnName("uis")
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(16);

                b.HasKey("Id")
                    .HasName("pk_students");

                b.HasIndex("UIS")
                    .IsUnique();

                b.ToTable("students");
            });

            modelBuilder.Entity("Velvetech.Students.Domain.Entities.StudentGroup", b =>
            {
                b.Property<long>("StudentId")
                    .HasColumnName("student_id")
                    .HasColumnType("bigint");

                b.Property<long>("GroupId")
                    .HasColumnName("group_id")
                    .HasColumnType("bigint");

                b.HasKey("StudentId", "GroupId");

                b.HasIndex("GroupId")
                    .HasName("ix_student_groups_group_id");

                b.ToTable("student_groups");
            });

            modelBuilder.Entity("Velvetech.Students.Domain.Entities.StudentGroup", b =>
            {
                b.HasOne("Velvetech.Students.Domain.Entities.Group", "Group")
                    .WithMany("StudentGroup")
                    .HasForeignKey("GroupId")
                    .HasConstraintName("fk_student_groups_groups_group_id")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("Velvetech.Students.Domain.Entities.Student", "Student")
                    .WithMany("StudentGroups")
                    .HasForeignKey("StudentId")
                    .HasConstraintName("fk_student_groups_students_student_id")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });
#pragma warning restore 612, 618
        }
    }
}