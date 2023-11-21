using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class BackAtTheDialogue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialogueCollection_ResponseCollection_ParentResponseId",
                table: "DialogueCollection");

            migrationBuilder.DropForeignKey(
                name: "FK_ResponseCollection_DialogueCollection_DialogueId",
                table: "ResponseCollection");

            migrationBuilder.DropIndex(
                name: "IX_DialogueCollection_ParentResponseId",
                table: "DialogueCollection");

            migrationBuilder.DropColumn(
                name: "ParentResponseId",
                table: "DialogueCollection");

            migrationBuilder.RenameColumn(
                name: "DialogueId",
                table: "ResponseCollection",
                newName: "ParentDialogueId");

            migrationBuilder.RenameIndex(
                name: "IX_ResponseCollection_DialogueId",
                table: "ResponseCollection",
                newName: "IX_ResponseCollection_ParentDialogueId");

            migrationBuilder.CreateTable(
                name: "DialogueResponseLinkCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ResponseId = table.Column<int>(type: "integer", nullable: true),
                    ChildDialogueId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DialogueResponseLinkCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DialogueResponseLinkCollection_DialogueCollection_ChildDial~",
                        column: x => x.ChildDialogueId,
                        principalTable: "DialogueCollection",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DialogueResponseLinkCollection_ResponseCollection_ResponseId",
                        column: x => x.ResponseId,
                        principalTable: "ResponseCollection",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DialogueResponseLinkCollection_ChildDialogueId",
                table: "DialogueResponseLinkCollection",
                column: "ChildDialogueId");

            migrationBuilder.CreateIndex(
                name: "IX_DialogueResponseLinkCollection_ResponseId",
                table: "DialogueResponseLinkCollection",
                column: "ResponseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseCollection_DialogueCollection_ParentDialogueId",
                table: "ResponseCollection",
                column: "ParentDialogueId",
                principalTable: "DialogueCollection",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResponseCollection_DialogueCollection_ParentDialogueId",
                table: "ResponseCollection");

            migrationBuilder.DropTable(
                name: "DialogueResponseLinkCollection");

            migrationBuilder.RenameColumn(
                name: "ParentDialogueId",
                table: "ResponseCollection",
                newName: "DialogueId");

            migrationBuilder.RenameIndex(
                name: "IX_ResponseCollection_ParentDialogueId",
                table: "ResponseCollection",
                newName: "IX_ResponseCollection_DialogueId");

            migrationBuilder.AddColumn<int>(
                name: "ParentResponseId",
                table: "DialogueCollection",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DialogueCollection_ParentResponseId",
                table: "DialogueCollection",
                column: "ParentResponseId");

            migrationBuilder.AddForeignKey(
                name: "FK_DialogueCollection_ResponseCollection_ParentResponseId",
                table: "DialogueCollection",
                column: "ParentResponseId",
                principalTable: "ResponseCollection",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResponseCollection_DialogueCollection_DialogueId",
                table: "ResponseCollection",
                column: "DialogueId",
                principalTable: "DialogueCollection",
                principalColumn: "Id");
        }
    }
}
