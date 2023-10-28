using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class CreatedLocationInteractionsAndKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interactions_Locations_LocationId",
                table: "Interactions");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Interactions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Interactions_Locations_LocationId",
                table: "Interactions",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interactions_Locations_LocationId",
                table: "Interactions");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Interactions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Interactions_Locations_LocationId",
                table: "Interactions",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }
    }
}
