using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class AddLinkedLocations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adventures_Location_StartingLocationId",
                table: "Adventures");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Adventures_AdventureId",
                table: "Location");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "Locations");

            migrationBuilder.RenameIndex(
                name: "IX_Location_AdventureId",
                table: "Locations",
                newName: "IX_Locations_AdventureId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LocationLink",
                columns: table => new
                {
                    FromId = table.Column<int>(type: "integer", nullable: false),
                    ToId = table.Column<int>(type: "integer", nullable: false),
                    FromLocationId = table.Column<int>(type: "integer", nullable: true),
                    ToLocationId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationLink", x => new { x.FromId, x.ToId });
                    table.ForeignKey(
                        name: "FK_LocationLink_Locations_FromLocationId",
                        column: x => x.FromLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationLink_Locations_ToLocationId",
                        column: x => x.ToLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocationLink_FromLocationId",
                table: "LocationLink",
                column: "FromLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationLink_ToLocationId",
                table: "LocationLink",
                column: "ToLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adventures_Locations_StartingLocationId",
                table: "Adventures",
                column: "StartingLocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Adventures_AdventureId",
                table: "Locations",
                column: "AdventureId",
                principalTable: "Adventures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adventures_Locations_StartingLocationId",
                table: "Adventures");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Adventures_AdventureId",
                table: "Locations");

            migrationBuilder.DropTable(
                name: "LocationLink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_AdventureId",
                table: "Location",
                newName: "IX_Location_AdventureId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Adventures_Location_StartingLocationId",
                table: "Adventures",
                column: "StartingLocationId",
                principalTable: "Location",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Adventures_AdventureId",
                table: "Location",
                column: "AdventureId",
                principalTable: "Adventures",
                principalColumn: "Id");
        }
    }
}
