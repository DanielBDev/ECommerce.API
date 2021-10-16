using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.API.Data.Migrations
{
    public partial class v21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "CashRegisters");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "CashRegisters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
