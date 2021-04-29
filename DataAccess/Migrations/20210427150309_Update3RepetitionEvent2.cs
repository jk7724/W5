using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Update3RepetitionEvent2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Repetition",
                table: "RepetitionEvents");

            migrationBuilder.RenameColumn(
                name: "SetName",
                table: "RepetitionEvents",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "RepetitionEvents",
                newName: "SetName");

            migrationBuilder.AddColumn<string>(
                name: "Repetition",
                table: "RepetitionEvents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
