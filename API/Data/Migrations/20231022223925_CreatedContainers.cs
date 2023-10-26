using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class CreatedContainers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContainerId",
                table: "ItemCollection",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContainerCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    LocationId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContainerCollection_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemCollection_ContainerId",
                table: "ItemCollection",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerCollection_LocationId",
                table: "ContainerCollection",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCollection_ContainerCollection_ContainerId",
                table: "ItemCollection",
                column: "ContainerId",
                principalTable: "ContainerCollection",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemCollection_ContainerCollection_ContainerId",
                table: "ItemCollection");

            migrationBuilder.DropTable(
                name: "ContainerCollection");

            migrationBuilder.DropIndex(
                name: "IX_ItemCollection_ContainerId",
                table: "ItemCollection");

            migrationBuilder.DropColumn(
                name: "ContainerId",
                table: "ItemCollection");
        }
    }
}
