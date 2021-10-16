using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.API.Data.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashRegisters_AspNetUsers_ClosingUserId1",
                table: "CashRegisters");

            migrationBuilder.DropIndex(
                name: "IX_CashRegisters_ClosingUserId1",
                table: "CashRegisters");

            migrationBuilder.DropColumn(
                name: "ClosingUserId",
                table: "CashRegisters");

            migrationBuilder.DropColumn(
                name: "ClosingUserId1",
                table: "CashRegisters");

            migrationBuilder.DropColumn(
                name: "OpeningUserId",
                table: "CashRegisters");

            migrationBuilder.RenameColumn(
                name: "OpeningUser",
                table: "CashRegisters",
                newName: "OpeningUserName");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Losts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Entries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClosingUserName",
                table: "CashRegisters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "CashRegisters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Losts");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "ClosingUserName",
                table: "CashRegisters");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "CashRegisters");

            migrationBuilder.RenameColumn(
                name: "OpeningUserName",
                table: "CashRegisters",
                newName: "OpeningUser");

            migrationBuilder.AddColumn<int>(
                name: "ClosingUserId",
                table: "CashRegisters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ClosingUserId1",
                table: "CashRegisters",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OpeningUserId",
                table: "CashRegisters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CashRegisters_ClosingUserId1",
                table: "CashRegisters",
                column: "ClosingUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CashRegisters_AspNetUsers_ClosingUserId1",
                table: "CashRegisters",
                column: "ClosingUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
