using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class ItemModifiersandValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Modifiers",
                table: "ItemCollection",
                newName: "StatModifiers");

            migrationBuilder.AddColumn<string>(
                name: "DamageModifiers",
                table: "ItemCollection",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResistanceModifiers",
                table: "ItemCollection",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "ItemCollection",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DamageModifiers",
                table: "ItemCollection");

            migrationBuilder.DropColumn(
                name: "ResistanceModifiers",
                table: "ItemCollection");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "ItemCollection");

            migrationBuilder.RenameColumn(
                name: "StatModifiers",
                table: "ItemCollection",
                newName: "Modifiers");
        }
    }
}
