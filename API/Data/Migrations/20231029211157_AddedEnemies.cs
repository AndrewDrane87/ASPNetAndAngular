using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class AddedEnemies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attack",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Range = table.Column<int>(type: "integer", nullable: false),
                    BaseDamage = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attack", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnemyCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    PhotoId = table.Column<int>(type: "integer", nullable: true),
                    MaxHp = table.Column<int>(type: "integer", nullable: false),
                    ArmorValue = table.Column<int>(type: "integer", nullable: false),
                    MeleeAttackId = table.Column<int>(type: "integer", nullable: true),
                    RangedAttackId = table.Column<int>(type: "integer", nullable: true),
                    LocationId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnemyCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnemyCollection_Attack_MeleeAttackId",
                        column: x => x.MeleeAttackId,
                        principalTable: "Attack",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EnemyCollection_Attack_RangedAttackId",
                        column: x => x.RangedAttackId,
                        principalTable: "Attack",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EnemyCollection_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EnemyCollection_Photos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnemyCollection_LocationId",
                table: "EnemyCollection",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_EnemyCollection_MeleeAttackId",
                table: "EnemyCollection",
                column: "MeleeAttackId");

            migrationBuilder.CreateIndex(
                name: "IX_EnemyCollection_PhotoId",
                table: "EnemyCollection",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_EnemyCollection_RangedAttackId",
                table: "EnemyCollection",
                column: "RangedAttackId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnemyCollection");

            migrationBuilder.DropTable(
                name: "Attack");
        }
    }
}
