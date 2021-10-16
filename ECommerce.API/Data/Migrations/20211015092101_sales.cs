using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.API.Data.Migrations
{
    public partial class sales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_CashRegisters_CashRegisterId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_CashRegisterId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "CashRegisterId",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "DetailSales",
                newName: "Price");

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Sales",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "DetailSales",
                newName: "UnitPrice");

            migrationBuilder.AddColumn<int>(
                name: "CashRegisterId",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CashRegisterId",
                table: "Sales",
                column: "CashRegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_CashRegisters_CashRegisterId",
                table: "Sales",
                column: "CashRegisterId",
                principalTable: "CashRegisters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
