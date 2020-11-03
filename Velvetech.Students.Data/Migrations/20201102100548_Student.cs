using Microsoft.EntityFrameworkCore.Migrations;

namespace Velvetech.Students.Data.Migrations
{
    public partial class Student : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "name",
                "students",
                "VARCHAR",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                "patronymic",
                "students",
                "VARCHAR",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                "surname",
                "students",
                "VARCHAR",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                "uis",
                "students",
                "VARCHAR",
                maxLength: 16,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                "name",
                "groups",
                "VARCHAR",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                "student_group",
                table => new
                {
                    student_id = table.Column<long>(nullable: false),
                    group_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student_group", x => new {x.student_id, x.group_id});
                    table.ForeignKey(
                        "fk_student_group_groups_group_id",
                        x => x.group_id,
                        "groups",
                        "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "fk_student_group_students_student_id",
                        x => x.student_id,
                        "students",
                        "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_students_uis",
                "students",
                "uis",
                unique: true);

            migrationBuilder.CreateIndex(
                "ix_student_group_group_id",
                "student_group",
                "group_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "student_group");

            migrationBuilder.DropIndex(
                "IX_students_uis",
                "students");

            migrationBuilder.DropColumn(
                "name",
                "students");

            migrationBuilder.DropColumn(
                "patronymic",
                "students");

            migrationBuilder.DropColumn(
                "surname",
                "students");

            migrationBuilder.DropColumn(
                "uis",
                "students");

            migrationBuilder.AlterColumn<string>(
                "name",
                "groups",
                "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR",
                oldMaxLength: 25);
        }
    }
}