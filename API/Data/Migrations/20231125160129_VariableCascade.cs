using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.data.migrations
{
    /// <inheritdoc />
    public partial class VariableCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdventureVariableSave_AdventureSaves_AdventureSaveId",
                table: "AdventureVariableSave");

            migrationBuilder.AlterColumn<int>(
                name: "AdventureSaveId",
                table: "AdventureVariableSave",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AdventureVariableSave_AdventureSaves_AdventureSaveId",
                table: "AdventureVariableSave",
                column: "AdventureSaveId",
                principalTable: "AdventureSaves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdventureVariableSave_AdventureSaves_AdventureSaveId",
                table: "AdventureVariableSave");

            migrationBuilder.AlterColumn<int>(
                name: "AdventureSaveId",
                table: "AdventureVariableSave",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_AdventureVariableSave_AdventureSaves_AdventureSaveId",
                table: "AdventureVariableSave",
                column: "AdventureSaveId",
                principalTable: "AdventureSaves",
                principalColumn: "Id");
        }
    }
}
