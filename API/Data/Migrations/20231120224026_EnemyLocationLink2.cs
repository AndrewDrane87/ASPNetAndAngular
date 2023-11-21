using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class EnemyLocationLink2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnemyLocationLinkId",
                table: "EnemySaves",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EnemySaves_EnemyLocationLinkId",
                table: "EnemySaves",
                column: "EnemyLocationLinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnemySaves_EnemyLocationLink_EnemyLocationLinkId",
                table: "EnemySaves",
                column: "EnemyLocationLinkId",
                principalTable: "EnemyLocationLink",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnemySaves_EnemyLocationLink_EnemyLocationLinkId",
                table: "EnemySaves");

            migrationBuilder.DropIndex(
                name: "IX_EnemySaves_EnemyLocationLinkId",
                table: "EnemySaves");

            migrationBuilder.DropColumn(
                name: "EnemyLocationLinkId",
                table: "EnemySaves");
        }
    }
}
