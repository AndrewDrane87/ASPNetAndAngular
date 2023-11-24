using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.data.migrations
{
    /// <inheritdoc />
    public partial class PhotoDataChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemPhotos_PhotoId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_AspNetUsers_AppUserId",
                table: "Photos");

            migrationBuilder.DropTable(
                name: "ItemPhotos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_AppUserId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "Photos");

            migrationBuilder.AddColumn<string>(
                name: "ObjectSubType",
                table: "Photos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ObjectType",
                table: "Photos",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Photos_PhotoId",
                table: "Items",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Photos_PhotoId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ObjectSubType",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "ObjectType",
                table: "Photos");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Photos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "Photos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ItemPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemType = table.Column<string>(type: "text", nullable: true),
                    PublicId = table.Column<string>(type: "text", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPhotos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_AppUserId",
                table: "Photos",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemPhotos_PhotoId",
                table: "Items",
                column: "PhotoId",
                principalTable: "ItemPhotos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_AspNetUsers_AppUserId",
                table: "Photos",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
