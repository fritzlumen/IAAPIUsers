using Microsoft.EntityFrameworkCore.Migrations;

namespace IAAPIUsers.Migrations
{
    public partial class NameToNome : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "Nome");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Users",
                newName: "Name");
        }
    }
}
