using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceApp.Server.Data.Migrations
{
    public partial class DeletedSomeColumnsFromStockModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cik",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "CompositeFigi",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "Market",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "MarketCap",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "ShareClassFigi",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "ShareClassSharesOutstanding",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "SicCode",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "SicDescription",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "TickerRoot",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "WeightedSharesOutstanding",
                table: "Stock");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cik",
                table: "Stock",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompositeFigi",
                table: "Stock",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Market",
                table: "Stock",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "MarketCap",
                table: "Stock",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ShareClassFigi",
                table: "Stock",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ShareClassSharesOutstanding",
                table: "Stock",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SicCode",
                table: "Stock",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SicDescription",
                table: "Stock",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TickerRoot",
                table: "Stock",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Stock",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WeightedSharesOutstanding",
                table: "Stock",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
