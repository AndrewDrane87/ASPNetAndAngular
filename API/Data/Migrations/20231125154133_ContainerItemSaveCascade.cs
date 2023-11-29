using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.data.migrations
{
    /// <inheritdoc />
    public partial class ContainerItemSaveCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemSaves_ContainerSaves_ContainerSaveId",
                table: "ItemSaves");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSaves_ContainerSaves_ContainerSaveId",
                table: "ItemSaves",
                column: "ContainerSaveId",
                principalTable: "ContainerSaves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemSaves_ContainerSaves_ContainerSaveId",
                table: "ItemSaves");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSaves_ContainerSaves_ContainerSaveId",
                table: "ItemSaves",
                column: "ContainerSaveId",
                principalTable: "ContainerSaves",
                principalColumn: "Id");
        }
    }
}
