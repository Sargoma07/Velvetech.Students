using Microsoft.EntityFrameworkCore.Migrations;

namespace Velvetech.Students.Data.Migrations
{
    public partial class Group : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "name",
                "groups",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "name",
                "groups");
        }
    }
}