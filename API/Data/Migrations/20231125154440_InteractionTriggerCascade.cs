using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.data.migrations
{
    /// <inheritdoc />
    public partial class InteractionTriggerCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TriggerSaves_InteractionSaves_InteractionId",
                table: "TriggerSaves");

            migrationBuilder.AddForeignKey(
                name: "FK_TriggerSaves_InteractionSaves_InteractionId",
                table: "TriggerSaves",
                column: "InteractionId",
                principalTable: "InteractionSaves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TriggerSaves_InteractionSaves_InteractionId",
                table: "TriggerSaves");

            migrationBuilder.AddForeignKey(
                name: "FK_TriggerSaves_InteractionSaves_InteractionId",
                table: "TriggerSaves",
                column: "InteractionId",
                principalTable: "InteractionSaves",
                principalColumn: "Id");
        }
    }
}
