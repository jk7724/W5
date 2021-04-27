using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class updateSetTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Repeat1",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "Repeat1Flag",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "Repeat2",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "Repeat2Flag",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "Repeat3",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "Repeat3Flag",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "Repeat4",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "Repeat4Flag",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "Repeat5",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "Repeat5Flag",
                table: "Sets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Repeat1",
                table: "Sets",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Repeat1Flag",
                table: "Sets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Repeat2",
                table: "Sets",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Repeat2Flag",
                table: "Sets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Repeat3",
                table: "Sets",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Repeat3Flag",
                table: "Sets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Repeat4",
                table: "Sets",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Repeat4Flag",
                table: "Sets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Repeat5",
                table: "Sets",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Repeat5Flag",
                table: "Sets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
