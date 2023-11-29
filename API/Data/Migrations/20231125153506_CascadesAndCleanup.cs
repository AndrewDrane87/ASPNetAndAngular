using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.data.migrations
{
    /// <inheritdoc />
    public partial class CascadesAndCleanup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnemySaves_LocationSaves_LocationSaveId",
                table: "EnemySaves");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSaves_LocationSaves_LocationSaveId",
                table: "ItemSaves");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationLink_Locations_FromLocationId",
                table: "LocationLink");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationLink_Locations_LocationId",
                table: "LocationLink");

            migrationBuilder.DropIndex(
                name: "IX_LocationLink_FromLocationId",
                table: "LocationLink");

            migrationBuilder.DropIndex(
                name: "IX_LocationLink_LocationId",
                table: "LocationLink");

            migrationBuilder.DropColumn(
                name: "FromLocationId",
                table: "LocationLink");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "LocationLink");

            migrationBuilder.AlterColumn<int>(
                name: "LocationSaveId",
                table: "EnemySaves",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocationLink_ToId",
                table: "LocationLink",
                column: "ToId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnemySaves_LocationSaves_LocationSaveId",
                table: "EnemySaves",
                column: "LocationSaveId",
                principalTable: "LocationSaves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSaves_LocationSaves_LocationSaveId",
                table: "ItemSaves",
                column: "LocationSaveId",
                principalTable: "LocationSaves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationLink_Locations_FromId",
                table: "LocationLink",
                column: "FromId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationLink_Locations_ToId",
                table: "LocationLink",
                column: "ToId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnemySaves_LocationSaves_LocationSaveId",
                table: "EnemySaves");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSaves_LocationSaves_LocationSaveId",
                table: "ItemSaves");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationLink_Locations_FromId",
                table: "LocationLink");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationLink_Locations_ToId",
                table: "LocationLink");

            migrationBuilder.DropIndex(
                name: "IX_LocationLink_ToId",
                table: "LocationLink");

            migrationBuilder.AddColumn<int>(
                name: "FromLocationId",
                table: "LocationLink",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "LocationLink",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LocationSaveId",
                table: "EnemySaves",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_LocationLink_FromLocationId",
                table: "LocationLink",
                column: "FromLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationLink_LocationId",
                table: "LocationLink",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnemySaves_LocationSaves_LocationSaveId",
                table: "EnemySaves",
                column: "LocationSaveId",
                principalTable: "LocationSaves",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSaves_LocationSaves_LocationSaveId",
                table: "ItemSaves",
                column: "LocationSaveId",
                principalTable: "LocationSaves",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationLink_Locations_FromLocationId",
                table: "LocationLink",
                column: "FromLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationLink_Locations_LocationId",
                table: "LocationLink",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }
    }
}
