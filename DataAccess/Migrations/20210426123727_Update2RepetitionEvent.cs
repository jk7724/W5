using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Update2RepetitionEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RepetitionEvents_Sets_SetId",
                table: "RepetitionEvents");

            migrationBuilder.DropIndex(
                name: "IX_RepetitionEvents_SetId",
                table: "RepetitionEvents");

            migrationBuilder.DropColumn(
                name: "SetId",
                table: "RepetitionEvents");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Repetition",
                table: "RepetitionEvents");

            migrationBuilder.RenameColumn(
                name: "SetName",
                table: "RepetitionEvents",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "SetId",
                table: "RepetitionEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RepetitionEvents_SetId",
                table: "RepetitionEvents",
                column: "SetId");

            migrationBuilder.AddForeignKey(
                name: "FK_RepetitionEvents_Sets_SetId",
                table: "RepetitionEvents",
                column: "SetId",
                principalTable: "Sets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
