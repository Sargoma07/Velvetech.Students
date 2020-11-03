using Microsoft.EntityFrameworkCore.Migrations;

namespace Velvetech.Students.Data.Migrations
{
    public partial class StudentGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "fk_student_group_groups_group_id",
                "student_group");

            migrationBuilder.DropForeignKey(
                "fk_student_group_students_student_id",
                "student_group");

            migrationBuilder.DropPrimaryKey(
                "PK_student_group",
                "student_group");

            migrationBuilder.RenameTable(
                "student_group",
                newName: "student_groups");

            migrationBuilder.RenameIndex(
                "ix_student_group_group_id",
                table: "student_groups",
                newName: "ix_student_groups_group_id");

            migrationBuilder.AlterColumn<string>(
                "patronymic",
                "students",
                "VARCHAR",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR",
                oldMaxLength: 60);

            migrationBuilder.AddPrimaryKey(
                "PK_student_groups",
                "student_groups",
                new[] {"student_id", "group_id"});

            migrationBuilder.AddForeignKey(
                "fk_student_groups_groups_group_id",
                "student_groups",
                "group_id",
                "groups",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "fk_student_groups_students_student_id",
                "student_groups",
                "student_id",
                "students",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "fk_student_groups_groups_group_id",
                "student_groups");

            migrationBuilder.DropForeignKey(
                "fk_student_groups_students_student_id",
                "student_groups");

            migrationBuilder.DropPrimaryKey(
                "PK_student_groups",
                "student_groups");

            migrationBuilder.RenameTable(
                "student_groups",
                newName: "student_group");

            migrationBuilder.RenameIndex(
                "ix_student_groups_group_id",
                table: "student_group",
                newName: "ix_student_group_group_id");

            migrationBuilder.AlterColumn<string>(
                "patronymic",
                "students",
                "VARCHAR",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                "PK_student_group",
                "student_group",
                new[] {"student_id", "group_id"});

            migrationBuilder.AddForeignKey(
                "fk_student_group_groups_group_id",
                "student_group",
                "group_id",
                "groups",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "fk_student_group_students_student_id",
                "student_group",
                "student_id",
                "students",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}