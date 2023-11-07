using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class AddedLocationVisibility : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VariableType",
                table: "AdventureVariable",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "AdventureVariable",
                newName: "InitialValue");

            migrationBuilder.AddColumn<string>(
                name: "VisibilityRequirement",
                table: "LocationSaves",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisibilityRequirements",
                table: "Locations",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AdventureVariableSave",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdventureVariableId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true),
                    AdventureSaveId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdventureVariableSave", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdventureVariableSave_AdventureSaves_AdventureSaveId",
                        column: x => x.AdventureSaveId,
                        principalTable: "AdventureSaves",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdventureVariableSave_AdventureVariable_AdventureVariableId",
                        column: x => x.AdventureVariableId,
                        principalTable: "AdventureVariable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdventureVariableSave_AdventureSaveId",
                table: "AdventureVariableSave",
                column: "AdventureSaveId");

            migrationBuilder.CreateIndex(
                name: "IX_AdventureVariableSave_AdventureVariableId",
                table: "AdventureVariableSave",
                column: "AdventureVariableId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdventureVariableSave");

            migrationBuilder.DropColumn(
                name: "VisibilityRequirement",
                table: "LocationSaves");

            migrationBuilder.DropColumn(
                name: "VisibilityRequirements",
                table: "Locations");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AdventureVariable",
                newName: "VariableType");

            migrationBuilder.RenameColumn(
                name: "InitialValue",
                table: "AdventureVariable",
                newName: "Value");
        }
    }
}
