using Microsoft.EntityFrameworkCore.Migrations;

namespace PCPF.Infra.Data.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Perfil",
                table: "Utilizador",
                type: "tinyint",
                maxLength: 1,
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Perfil",
                table: "Utilizador");
        }
    }
}
