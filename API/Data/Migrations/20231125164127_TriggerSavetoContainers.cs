using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.data.migrations
{
    /// <inheritdoc />
    public partial class TriggerSavetoContainers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContainerSaveId",
                table: "TriggerSaves",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TriggerSaves_ContainerSaveId",
                table: "TriggerSaves",
                column: "ContainerSaveId");

            migrationBuilder.AddForeignKey(
                name: "FK_TriggerSaves_ContainerSaves_ContainerSaveId",
                table: "TriggerSaves",
                column: "ContainerSaveId",
                principalTable: "ContainerSaves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TriggerSaves_ContainerSaves_ContainerSaveId",
                table: "TriggerSaves");

            migrationBuilder.DropIndex(
                name: "IX_TriggerSaves_ContainerSaveId",
                table: "TriggerSaves");

            migrationBuilder.DropColumn(
                name: "ContainerSaveId",
                table: "TriggerSaves");
        }
    }
}
