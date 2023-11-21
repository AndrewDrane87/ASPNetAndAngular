using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class BackAtTheDialogue2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DialogueResponseLinkId",
                table: "ResponseCollection",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResponseCollection_DialogueResponseLinkId",
                table: "ResponseCollection",
                column: "DialogueResponseLinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseCollection_DialogueResponseLinkCollection_DialogueR~",
                table: "ResponseCollection",
                column: "DialogueResponseLinkId",
                principalTable: "DialogueResponseLinkCollection",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResponseCollection_DialogueResponseLinkCollection_DialogueR~",
                table: "ResponseCollection");

            migrationBuilder.DropIndex(
                name: "IX_ResponseCollection_DialogueResponseLinkId",
                table: "ResponseCollection");

            migrationBuilder.DropColumn(
                name: "DialogueResponseLinkId",
                table: "ResponseCollection");
        }
    }
}
