using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.data.migrations
{
    /// <inheritdoc />
    public partial class CompleteAndPassedToInteractions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Information",
                table: "Interactions",
                newName: "PassedText");

            migrationBuilder.AddColumn<bool>(
                name: "Passed",
                table: "InteractionSave",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DefaultText",
                table: "Interactions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FailedText",
                table: "Interactions",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Passed",
                table: "InteractionSave");

            migrationBuilder.DropColumn(
                name: "DefaultText",
                table: "Interactions");

            migrationBuilder.DropColumn(
                name: "FailedText",
                table: "Interactions");

            migrationBuilder.RenameColumn(
                name: "PassedText",
                table: "Interactions",
                newName: "Information");
        }
    }
}
