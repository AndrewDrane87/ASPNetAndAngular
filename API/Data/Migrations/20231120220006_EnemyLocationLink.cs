using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class EnemyLocationLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnemyCollection_Locations_LocationId",
                table: "EnemyCollection");

            migrationBuilder.DropIndex(
                name: "IX_EnemyCollection_LocationId",
                table: "EnemyCollection");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "EnemyCollection");

            migrationBuilder.CreateTable(
                name: "EnemyLocationLink",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    EnemyId = table.Column<int>(type: "integer", nullable: false),
                    RequiredPlayerCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnemyLocationLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnemyLocationLink_EnemyCollection_EnemyId",
                        column: x => x.EnemyId,
                        principalTable: "EnemyCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnemyLocationLink_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnemyLocationLink_EnemyId",
                table: "EnemyLocationLink",
                column: "EnemyId");

            migrationBuilder.CreateIndex(
                name: "IX_EnemyLocationLink_LocationId",
                table: "EnemyLocationLink",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnemyLocationLink");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "EnemyCollection",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnemyCollection_LocationId",
                table: "EnemyCollection",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnemyCollection_Locations_LocationId",
                table: "EnemyCollection",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }
    }
}
