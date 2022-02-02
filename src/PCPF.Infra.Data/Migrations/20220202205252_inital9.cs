using Microsoft.EntityFrameworkCore.Migrations;

namespace PCPF.Infra.Data.Migrations
{
    public partial class inital9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "bit",
                table: "Pagamento",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
                name: "Comprovativo",
                table: "Pagamento",
                type: "varchar(150)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comprovativo",
                table: "Pagamento");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Pagamento",
                newName: "bit");
        }
    }
}
