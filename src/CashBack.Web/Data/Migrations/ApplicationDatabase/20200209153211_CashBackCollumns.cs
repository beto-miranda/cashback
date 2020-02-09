using Microsoft.EntityFrameworkCore.Migrations;

namespace CashBack.Web.Data.Migrations.ApplicationDatabase
{
    public partial class CashBackCollumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cashback",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CashbackPercentage",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cashback",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CashbackPercentage",
                table: "Orders");
        }
    }
}
