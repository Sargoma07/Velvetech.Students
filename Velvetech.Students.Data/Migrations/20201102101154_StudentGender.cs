using Microsoft.EntityFrameworkCore.Migrations;

namespace Velvetech.Students.Data.Migrations
{
    public partial class StudentGender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "gender",
                "students",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "gender",
                "students");
        }
    }
}