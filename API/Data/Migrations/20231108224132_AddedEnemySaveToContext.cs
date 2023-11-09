using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class AddedEnemySaveToContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnemySave_EnemyCollection_EnemyId",
                table: "EnemySave");

            migrationBuilder.DropForeignKey(
                name: "FK_EnemySave_LocationSaves_LocationSaveId",
                table: "EnemySave");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnemySave",
                table: "EnemySave");

            migrationBuilder.RenameTable(
                name: "EnemySave",
                newName: "EnemySaves");

            migrationBuilder.RenameIndex(
                name: "IX_EnemySave_LocationSaveId",
                table: "EnemySaves",
                newName: "IX_EnemySaves_LocationSaveId");

            migrationBuilder.RenameIndex(
                name: "IX_EnemySave_EnemyId",
                table: "EnemySaves",
                newName: "IX_EnemySaves_EnemyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnemySaves",
                table: "EnemySaves",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EnemySaves_EnemyCollection_EnemyId",
                table: "EnemySaves",
                column: "EnemyId",
                principalTable: "EnemyCollection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnemySaves_LocationSaves_LocationSaveId",
                table: "EnemySaves",
                column: "LocationSaveId",
                principalTable: "LocationSaves",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnemySaves_EnemyCollection_EnemyId",
                table: "EnemySaves");

            migrationBuilder.DropForeignKey(
                name: "FK_EnemySaves_LocationSaves_LocationSaveId",
                table: "EnemySaves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnemySaves",
                table: "EnemySaves");

            migrationBuilder.RenameTable(
                name: "EnemySaves",
                newName: "EnemySave");

            migrationBuilder.RenameIndex(
                name: "IX_EnemySaves_LocationSaveId",
                table: "EnemySave",
                newName: "IX_EnemySave_LocationSaveId");

            migrationBuilder.RenameIndex(
                name: "IX_EnemySaves_EnemyId",
                table: "EnemySave",
                newName: "IX_EnemySave_EnemyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnemySave",
                table: "EnemySave",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EnemySave_EnemyCollection_EnemyId",
                table: "EnemySave",
                column: "EnemyId",
                principalTable: "EnemyCollection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnemySave_LocationSaves_LocationSaveId",
                table: "EnemySave",
                column: "LocationSaveId",
                principalTable: "LocationSaves",
                principalColumn: "Id");
        }
    }
}
