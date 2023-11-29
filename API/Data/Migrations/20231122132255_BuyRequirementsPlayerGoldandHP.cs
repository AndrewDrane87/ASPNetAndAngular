using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.data.migrations
{
    /// <inheritdoc />
    public partial class BuyRequirementsPlayerGoldandHP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentHitpoints",
                table: "PlayerCharacters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Gold",
                table: "PlayerCharacters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "PlayerCharacters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxHitpoints",
                table: "PlayerCharacters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "ItemsRequirePurchase",
                table: "Locations",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentHitpoints",
                table: "PlayerCharacters");

            migrationBuilder.DropColumn(
                name: "Gold",
                table: "PlayerCharacters");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "PlayerCharacters");

            migrationBuilder.DropColumn(
                name: "MaxHitpoints",
                table: "PlayerCharacters");

            migrationBuilder.DropColumn(
                name: "ItemsRequirePurchase",
                table: "Locations");
        }
    }
}
