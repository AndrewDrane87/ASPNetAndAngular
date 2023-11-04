using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class LocationSaveDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationSave_AdventureSaves_AdventureSaveId",
                table: "LocationSave");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationSave_Locations_LocationId",
                table: "LocationSave");

            migrationBuilder.DropForeignKey(
                name: "FK_NPCSave_LocationSave_LocationSaveId",
                table: "NPCSave");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationSave",
                table: "LocationSave");

            migrationBuilder.RenameTable(
                name: "LocationSave",
                newName: "LocationSaves");

            migrationBuilder.RenameIndex(
                name: "IX_LocationSave_LocationId",
                table: "LocationSaves",
                newName: "IX_LocationSaves_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_LocationSave_AdventureSaveId",
                table: "LocationSaves",
                newName: "IX_LocationSaves_AdventureSaveId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocationSaves",
                table: "LocationSaves",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationSaves_AdventureSaves_AdventureSaveId",
                table: "LocationSaves",
                column: "AdventureSaveId",
                principalTable: "AdventureSaves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationSaves_Locations_LocationId",
                table: "LocationSaves",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NPCSave_LocationSaves_LocationSaveId",
                table: "NPCSave",
                column: "LocationSaveId",
                principalTable: "LocationSaves",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationSaves_AdventureSaves_AdventureSaveId",
                table: "LocationSaves");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationSaves_Locations_LocationId",
                table: "LocationSaves");

            migrationBuilder.DropForeignKey(
                name: "FK_NPCSave_LocationSaves_LocationSaveId",
                table: "NPCSave");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationSaves",
                table: "LocationSaves");

            migrationBuilder.RenameTable(
                name: "LocationSaves",
                newName: "LocationSave");

            migrationBuilder.RenameIndex(
                name: "IX_LocationSaves_LocationId",
                table: "LocationSave",
                newName: "IX_LocationSave_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_LocationSaves_AdventureSaveId",
                table: "LocationSave",
                newName: "IX_LocationSave_AdventureSaveId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocationSave",
                table: "LocationSave",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationSave_AdventureSaves_AdventureSaveId",
                table: "LocationSave",
                column: "AdventureSaveId",
                principalTable: "AdventureSaves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationSave_Locations_LocationId",
                table: "LocationSave",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NPCSave_LocationSave_LocationSaveId",
                table: "NPCSave",
                column: "LocationSaveId",
                principalTable: "LocationSave",
                principalColumn: "Id");
        }
    }
}
