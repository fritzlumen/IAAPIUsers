using Microsoft.EntityFrameworkCore.Migrations;

namespace IAAPIUsers.Migrations
{
    public partial class AtualizandoCampo3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Password", "Role", "Username" },
                values: new object[] { 1, 0, "senha123", "manager", "administrador" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
