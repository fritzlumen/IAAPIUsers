using Microsoft.EntityFrameworkCore.Migrations;

namespace IAAPIUsers.Migrations
{
    public partial class AtualizandoPropriedades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Senha",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Idade",
                table: "Users",
                newName: "Age");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "Senha");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Users",
                newName: "Idade");
        }
    }
}
