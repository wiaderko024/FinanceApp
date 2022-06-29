using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceApp.Server.Data.Migrations
{
    public partial class AddedNewColumnsToStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address1",
                table: "Stock",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cik",
                table: "Stock",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Stock",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Stock",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HomePageUrl",
                table: "Stock",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "Stock",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ListDate",
                table: "Stock",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
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
                name: "PhoneNumber",
                table: "Stock",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Stock",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryExchange",
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
                name: "State",
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

            migrationBuilder.AddColumn<int>(
                name: "TotalEmployees",
                table: "Stock",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeightedSharesOutstanding",
                table: "Stock",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address1",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "Cik",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "HomePageUrl",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "ListDate",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "MarketCap",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "PrimaryExchange",
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
                name: "State",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "TickerRoot",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "TotalEmployees",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "WeightedSharesOutstanding",
                table: "Stock");
        }
    }
}
