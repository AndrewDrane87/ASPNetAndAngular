using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class SingleItemRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacters_ArmorCollection_BodyId",
                table: "PlayerCharacters");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacters_BootCollection_FeetId",
                table: "PlayerCharacters");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacters_HandItemCollection_LeftHandId",
                table: "PlayerCharacters");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacters_HandItemCollection_RightHandId",
                table: "PlayerCharacters");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacters_HelmetCollection_HelmetId",
                table: "PlayerCharacters");

            migrationBuilder.DropTable(
                name: "ArmorCollection");

            migrationBuilder.DropTable(
                name: "BootCollection");

            migrationBuilder.DropTable(
                name: "HandItemCollection");

            migrationBuilder.DropTable(
                name: "HelmetCollection");

            migrationBuilder.CreateTable(
                name: "ItemCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    PhotoId = table.Column<int>(type: "integer", nullable: true),
                    AttackValue = table.Column<int>(type: "integer", nullable: false),
                    ArmorValue = table.Column<int>(type: "integer", nullable: false),
                    Modifiers = table.Column<string>(type: "text", nullable: true),
                    ItemType = table.Column<int>(type: "integer", nullable: false),
                    DamageType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemCollection_ItemPhotos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "ItemPhotos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemCollection_PhotoId",
                table: "ItemCollection",
                column: "PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacters_ItemCollection_BodyId",
                table: "PlayerCharacters",
                column: "BodyId",
                principalTable: "ItemCollection",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacters_ItemCollection_FeetId",
                table: "PlayerCharacters",
                column: "FeetId",
                principalTable: "ItemCollection",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacters_ItemCollection_HelmetId",
                table: "PlayerCharacters",
                column: "HelmetId",
                principalTable: "ItemCollection",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacters_ItemCollection_LeftHandId",
                table: "PlayerCharacters",
                column: "LeftHandId",
                principalTable: "ItemCollection",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacters_ItemCollection_RightHandId",
                table: "PlayerCharacters",
                column: "RightHandId",
                principalTable: "ItemCollection",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacters_ItemCollection_BodyId",
                table: "PlayerCharacters");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacters_ItemCollection_FeetId",
                table: "PlayerCharacters");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacters_ItemCollection_HelmetId",
                table: "PlayerCharacters");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacters_ItemCollection_LeftHandId",
                table: "PlayerCharacters");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacters_ItemCollection_RightHandId",
                table: "PlayerCharacters");

            migrationBuilder.DropTable(
                name: "ItemCollection");

            migrationBuilder.CreateTable(
                name: "ArmorCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PhotoId = table.Column<int>(type: "integer", nullable: true),
                    ArmorValue = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArmorCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArmorCollection_ItemPhotos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "ItemPhotos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BootCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PhotoId = table.Column<int>(type: "integer", nullable: true),
                    ArmorValue = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BootCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BootCollection_ItemPhotos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "ItemPhotos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HandItemCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PhotoId = table.Column<int>(type: "integer", nullable: true),
                    ArmorValue = table.Column<int>(type: "integer", nullable: false),
                    AttackValue = table.Column<int>(type: "integer", nullable: false),
                    DamageType = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HandItemCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HandItemCollection_ItemPhotos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "ItemPhotos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HelmetCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PhotoId = table.Column<int>(type: "integer", nullable: true),
                    ArmorValue = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelmetCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HelmetCollection_ItemPhotos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "ItemPhotos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArmorCollection_PhotoId",
                table: "ArmorCollection",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_BootCollection_PhotoId",
                table: "BootCollection",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_HandItemCollection_PhotoId",
                table: "HandItemCollection",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_HelmetCollection_PhotoId",
                table: "HelmetCollection",
                column: "PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacters_ArmorCollection_BodyId",
                table: "PlayerCharacters",
                column: "BodyId",
                principalTable: "ArmorCollection",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacters_BootCollection_FeetId",
                table: "PlayerCharacters",
                column: "FeetId",
                principalTable: "BootCollection",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacters_HandItemCollection_LeftHandId",
                table: "PlayerCharacters",
                column: "LeftHandId",
                principalTable: "HandItemCollection",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacters_HandItemCollection_RightHandId",
                table: "PlayerCharacters",
                column: "RightHandId",
                principalTable: "HandItemCollection",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacters_HelmetCollection_HelmetId",
                table: "PlayerCharacters",
                column: "HelmetId",
                principalTable: "HelmetCollection",
                principalColumn: "Id");
        }
    }
}
