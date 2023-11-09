using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class AddedEnemyMovement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeleeAttackId",
                table: "EnemyCollection");

            migrationBuilder.AddColumn<string>(
                name: "AttackStrategy",
                table: "EnemyCollection",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovementRange",
                table: "EnemyCollection",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttackStrategy",
                table: "EnemyCollection");

            migrationBuilder.DropColumn(
                name: "MovementRange",
                table: "EnemyCollection");

            migrationBuilder.AddColumn<int>(
                name: "MeleeAttackId",
                table: "EnemyCollection",
                type: "integer",
                nullable: true);
        }
    }
}
