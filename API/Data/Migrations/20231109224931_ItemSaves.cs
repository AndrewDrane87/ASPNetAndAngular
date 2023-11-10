using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class ItemSaves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContainerItem");

            migrationBuilder.CreateTable(
                name: "ItemContainerLink",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    ContainerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemContainerLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemContainerLink_ContainerCollection_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "ContainerCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemContainerLink_ItemCollection_ItemId",
                        column: x => x.ItemId,
                        principalTable: "ItemCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemSave",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContainerSaveId = table.Column<int>(type: "integer", nullable: false),
                    ItemId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSave", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemSave_ContainerSaves_ContainerSaveId",
                        column: x => x.ContainerSaveId,
                        principalTable: "ContainerSaves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemSave_ItemCollection_ItemId",
                        column: x => x.ItemId,
                        principalTable: "ItemCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemContainerLink_ContainerId",
                table: "ItemContainerLink",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemContainerLink_ItemId",
                table: "ItemContainerLink",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSave_ContainerSaveId",
                table: "ItemSave",
                column: "ContainerSaveId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSave_ItemId",
                table: "ItemSave",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemContainerLink");

            migrationBuilder.DropTable(
                name: "ItemSave");

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
    }
}
