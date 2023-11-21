using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class AddingCurrentLocationToAdventureSave : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationSaveId",
                table: "ItemSaves",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentLocationId",
                table: "AdventureSaves",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemSaves_LocationSaveId",
                table: "ItemSaves",
                column: "LocationSaveId");

            migrationBuilder.CreateIndex(
                name: "IX_AdventureSaves_CurrentLocationId",
                table: "AdventureSaves",
                column: "CurrentLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdventureSaves_LocationSaves_CurrentLocationId",
                table: "AdventureSaves",
                column: "CurrentLocationId",
                principalTable: "LocationSaves",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSaves_LocationSaves_LocationSaveId",
                table: "ItemSaves",
                column: "LocationSaveId",
                principalTable: "LocationSaves",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdventureSaves_LocationSaves_CurrentLocationId",
                table: "AdventureSaves");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSaves_LocationSaves_LocationSaveId",
                table: "ItemSaves");

            migrationBuilder.DropIndex(
                name: "IX_ItemSaves_LocationSaveId",
                table: "ItemSaves");

            migrationBuilder.DropIndex(
                name: "IX_AdventureSaves_CurrentLocationId",
                table: "AdventureSaves");

            migrationBuilder.DropColumn(
                name: "LocationSaveId",
                table: "ItemSaves");

            migrationBuilder.DropColumn(
                name: "CurrentLocationId",
                table: "AdventureSaves");
        }
    }
}
