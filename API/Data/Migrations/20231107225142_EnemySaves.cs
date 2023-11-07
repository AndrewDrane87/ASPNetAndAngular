using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class EnemySaves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnemyCollection_Attack_MeleeAttackId",
                table: "EnemyCollection");

            migrationBuilder.DropForeignKey(
                name: "FK_EnemyCollection_Attack_RangedAttackId",
                table: "EnemyCollection");

            migrationBuilder.DropTable(
                name: "Attack");

            migrationBuilder.DropIndex(
                name: "IX_EnemyCollection_MeleeAttackId",
                table: "EnemyCollection");

            migrationBuilder.DropIndex(
                name: "IX_EnemyCollection_RangedAttackId",
                table: "EnemyCollection");

            migrationBuilder.DropColumn(
                name: "RangedAttackId",
                table: "EnemyCollection");

            migrationBuilder.AddColumn<int>(
                name: "Attack1BaseDamage",
                table: "EnemyCollection",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Attack1Name",
                table: "EnemyCollection",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Attack1Range",
                table: "EnemyCollection",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Attack2BaseDamage",
                table: "EnemyCollection",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Attack2Name",
                table: "EnemyCollection",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Attack2Range",
                table: "EnemyCollection",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EnemySave",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EnemyId = table.Column<int>(type: "integer", nullable: false),
                    CurrentHp = table.Column<int>(type: "integer", nullable: false),
                    LocationSaveId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnemySave", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnemySave_EnemyCollection_EnemyId",
                        column: x => x.EnemyId,
                        principalTable: "EnemyCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnemySave_LocationSaves_LocationSaveId",
                        column: x => x.LocationSaveId,
                        principalTable: "LocationSaves",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnemySave_EnemyId",
                table: "EnemySave",
                column: "EnemyId");

            migrationBuilder.CreateIndex(
                name: "IX_EnemySave_LocationSaveId",
                table: "EnemySave",
                column: "LocationSaveId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnemySave");

            migrationBuilder.DropColumn(
                name: "Attack1BaseDamage",
                table: "EnemyCollection");

            migrationBuilder.DropColumn(
                name: "Attack1Name",
                table: "EnemyCollection");

            migrationBuilder.DropColumn(
                name: "Attack1Range",
                table: "EnemyCollection");

            migrationBuilder.DropColumn(
                name: "Attack2BaseDamage",
                table: "EnemyCollection");

            migrationBuilder.DropColumn(
                name: "Attack2Name",
                table: "EnemyCollection");

            migrationBuilder.DropColumn(
                name: "Attack2Range",
                table: "EnemyCollection");

            migrationBuilder.AddColumn<int>(
                name: "RangedAttackId",
                table: "EnemyCollection",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Attack",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BaseDamage = table.Column<int>(type: "integer", nullable: false),
                    Range = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attack", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnemyCollection_MeleeAttackId",
                table: "EnemyCollection",
                column: "MeleeAttackId");

            migrationBuilder.CreateIndex(
                name: "IX_EnemyCollection_RangedAttackId",
                table: "EnemyCollection",
                column: "RangedAttackId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnemyCollection_Attack_MeleeAttackId",
                table: "EnemyCollection",
                column: "MeleeAttackId",
                principalTable: "Attack",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EnemyCollection_Attack_RangedAttackId",
                table: "EnemyCollection",
                column: "RangedAttackId",
                principalTable: "Attack",
                principalColumn: "Id");
        }
    }
}
