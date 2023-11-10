using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class ContainerSaves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModifierDiceSides",
                table: "EnemyCollection",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ContainerSaves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContainerId = table.Column<int>(type: "integer", nullable: false),
                    Complete = table.Column<bool>(type: "boolean", nullable: false),
                    LocationSaveId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerSaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContainerSaves_ContainerCollection_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "ContainerCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContainerSaves_LocationSaves_LocationSaveId",
                        column: x => x.LocationSaveId,
                        principalTable: "LocationSaves",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContainerSaves_ContainerId",
                table: "ContainerSaves",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerSaves_LocationSaveId",
                table: "ContainerSaves",
                column: "LocationSaveId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContainerSaves");

            migrationBuilder.DropColumn(
                name: "ModifierDiceSides",
                table: "EnemyCollection");
        }
    }
}
