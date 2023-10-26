using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class AttemptToFixLocationLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationLink_Locations_ToLocationId",
                table: "LocationLink");

            migrationBuilder.RenameColumn(
                name: "ToLocationId",
                table: "LocationLink",
                newName: "LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_LocationLink_ToLocationId",
                table: "LocationLink",
                newName: "IX_LocationLink_LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationLink_Locations_LocationId",
                table: "LocationLink",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationLink_Locations_LocationId",
                table: "LocationLink");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "LocationLink",
                newName: "ToLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_LocationLink_LocationId",
                table: "LocationLink",
                newName: "IX_LocationLink_ToLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationLink_Locations_ToLocationId",
                table: "LocationLink",
                column: "ToLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
