using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QoolloTaskViewer.Migrations
{
    public partial class UpdateServiceModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Services_Name",
                table: "Services");

            migrationBuilder.DeleteData(
                table: "Domains",
                keyColumn: "Id",
                keyValue: new Guid("754a6861-3fcd-4e84-a458-3f6d5ce898a7"));

            migrationBuilder.DeleteData(
                table: "Domains",
                keyColumn: "Id",
                keyValue: new Guid("cba07488-ebe2-43b0-b87c-b4d60a5c2b5d"));

            migrationBuilder.DeleteData(
                table: "Domains",
                keyColumn: "Id",
                keyValue: new Guid("e73058c2-4dfd-4da9-b683-56ea53857dcd"));

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Services");

            migrationBuilder.AlterColumn<string>(
                name: "InServiceUsername",
                table: "Tokens",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.InsertData(
                table: "Domains",
                columns: new[] { "Id", "Domain" },
                values: new object[,]
                {
                    { new Guid("743b1020-1109-4d6a-8cdc-76005ecafed3"), "github.com" },
                    { new Guid("09754033-2dfb-4060-bce3-82c989539df9"), "gitlab.com" },
                    { new Guid("67ab09b0-b9ad-424b-8d98-eb7bb988167c"), "jira.atlassian.com" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Domains",
                keyColumn: "Id",
                keyValue: new Guid("09754033-2dfb-4060-bce3-82c989539df9"));

            migrationBuilder.DeleteData(
                table: "Domains",
                keyColumn: "Id",
                keyValue: new Guid("67ab09b0-b9ad-424b-8d98-eb7bb988167c"));

            migrationBuilder.DeleteData(
                table: "Domains",
                keyColumn: "Id",
                keyValue: new Guid("743b1020-1109-4d6a-8cdc-76005ecafed3"));

            migrationBuilder.AlterColumn<string>(
                name: "InServiceUsername",
                table: "Tokens",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Services",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Services_Name",
                table: "Services",
                column: "Name");

            migrationBuilder.InsertData(
                table: "Domains",
                columns: new[] { "Id", "Domain" },
                values: new object[,]
                {
                    { new Guid("754a6861-3fcd-4e84-a458-3f6d5ce898a7"), "github.com" },
                    { new Guid("e73058c2-4dfd-4da9-b683-56ea53857dcd"), "gitlab.com" },
                    { new Guid("cba07488-ebe2-43b0-b87c-b4d60a5c2b5d"), "jira.atlassian.com" }
                });
        }
    }
}
