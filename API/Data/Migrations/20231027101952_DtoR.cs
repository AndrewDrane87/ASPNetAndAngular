using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class DtoR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialogueResponseLink_DialogueCollection_FromDialogueId",
                table: "DialogueResponseLink");

            migrationBuilder.DropForeignKey(
                name: "FK_DialogueResponseLink_ResponseCollection_FromResponseId",
                table: "DialogueResponseLink");

            migrationBuilder.AddForeignKey(
                name: "FK_DialogueResponseLink_DialogueCollection_FromDialogueId",
                table: "DialogueResponseLink",
                column: "FromDialogueId",
                principalTable: "DialogueCollection",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DialogueResponseLink_ResponseCollection_FromResponseId",
                table: "DialogueResponseLink",
                column: "FromResponseId",
                principalTable: "ResponseCollection",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialogueResponseLink_DialogueCollection_FromDialogueId",
                table: "DialogueResponseLink");

            migrationBuilder.DropForeignKey(
                name: "FK_DialogueResponseLink_ResponseCollection_FromResponseId",
                table: "DialogueResponseLink");

            migrationBuilder.AddForeignKey(
                name: "FK_DialogueResponseLink_DialogueCollection_FromDialogueId",
                table: "DialogueResponseLink",
                column: "FromDialogueId",
                principalTable: "DialogueCollection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DialogueResponseLink_ResponseCollection_FromResponseId",
                table: "DialogueResponseLink",
                column: "FromResponseId",
                principalTable: "ResponseCollection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
