using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.data.migrations
{
    /// <inheritdoc />
    public partial class InteractioTriggerSaves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "ActionTriggerSaves",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "InteractionId",
                table: "ActionTriggerSaves",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InteractionSave",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Complete = table.Column<bool>(type: "boolean", nullable: false),
                    InteractionId = table.Column<int>(type: "integer", nullable: false),
                    LocationSaveId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteractionSave", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InteractionSave_Interactions_InteractionId",
                        column: x => x.InteractionId,
                        principalTable: "Interactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InteractionSave_LocationSaves_LocationSaveId",
                        column: x => x.LocationSaveId,
                        principalTable: "LocationSaves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionTriggerSaves_InteractionId",
                table: "ActionTriggerSaves",
                column: "InteractionId");

            migrationBuilder.CreateIndex(
                name: "IX_InteractionSave_InteractionId",
                table: "InteractionSave",
                column: "InteractionId");

            migrationBuilder.CreateIndex(
                name: "IX_InteractionSave_LocationSaveId",
                table: "InteractionSave",
                column: "LocationSaveId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionTriggerSaves_InteractionSave_InteractionId",
                table: "ActionTriggerSaves",
                column: "InteractionId",
                principalTable: "InteractionSave",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionTriggerSaves_InteractionSave_InteractionId",
                table: "ActionTriggerSaves");

            migrationBuilder.DropTable(
                name: "InteractionSave");

            migrationBuilder.DropIndex(
                name: "IX_ActionTriggerSaves_InteractionId",
                table: "ActionTriggerSaves");

            migrationBuilder.DropColumn(
                name: "InteractionId",
                table: "ActionTriggerSaves");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "ActionTriggerSaves",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
