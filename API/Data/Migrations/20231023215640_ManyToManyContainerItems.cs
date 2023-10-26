using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class ManyToManyContainerItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemCollection_ContainerCollection_ContainerId",
                table: "ItemCollection");

            migrationBuilder.DropIndex(
                name: "IX_ItemCollection_ContainerId",
                table: "ItemCollection");

            migrationBuilder.DropColumn(
                name: "ContainerId",
                table: "ItemCollection");

            migrationBuilder.CreateTable(
                name: "ContainerItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContainerId = table.Column<int>(type: "integer", nullable: false),
                    ItemId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContainerItem_ContainerCollection_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "ContainerCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContainerItem_ItemCollection_ItemId",
                        column: x => x.ItemId,
                        principalTable: "ItemCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContainerItem_ContainerId",
                table: "ContainerItem",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerItem_ItemId",
                table: "ContainerItem",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContainerItem");

            migrationBuilder.AddColumn<int>(
                name: "ContainerId",
                table: "ItemCollection",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemCollection_ContainerId",
                table: "ItemCollection",
                column: "ContainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCollection_ContainerCollection_ContainerId",
                table: "ItemCollection",
                column: "ContainerId",
                principalTable: "ContainerCollection",
                principalColumn: "Id");
        }
    }
}
