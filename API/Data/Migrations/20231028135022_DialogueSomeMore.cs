using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class DialogueSomeMore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DialogueResponseLink");

            migrationBuilder.AddColumn<int>(
                name: "DialogueId",
                table: "ResponseCollection",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentResponseId",
                table: "DialogueCollection",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResponseCollection_DialogueId",
                table: "ResponseCollection",
                column: "DialogueId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialogueCollection_ResponseCollection_ParentResponseId",
                table: "DialogueCollection");

            migrationBuilder.DropForeignKey(
                name: "FK_ResponseCollection_DialogueCollection_DialogueId",
                table: "ResponseCollection");

            migrationBuilder.DropIndex(
                name: "IX_ResponseCollection_DialogueId",
                table: "ResponseCollection");

            migrationBuilder.DropIndex(
                name: "IX_DialogueCollection_ParentResponseId",
                table: "DialogueCollection");

            migrationBuilder.DropColumn(
                name: "DialogueId",
                table: "ResponseCollection");

            migrationBuilder.DropColumn(
                name: "ParentResponseId",
                table: "DialogueCollection");

            migrationBuilder.CreateTable(
                name: "DialogueResponseLink",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FromDialogueId = table.Column<int>(type: "integer", nullable: true),
                    FromResponseId = table.Column<int>(type: "integer", nullable: true),
                    ToDialogueId = table.Column<int>(type: "integer", nullable: true),
                    ToResponseId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DialogueResponseLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DialogueResponseLink_DialogueCollection_FromDialogueId",
                        column: x => x.FromDialogueId,
                        principalTable: "DialogueCollection",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DialogueResponseLink_DialogueCollection_ToDialogueId",
                        column: x => x.ToDialogueId,
                        principalTable: "DialogueCollection",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DialogueResponseLink_ResponseCollection_FromResponseId",
                        column: x => x.FromResponseId,
                        principalTable: "ResponseCollection",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DialogueResponseLink_ResponseCollection_ToResponseId",
                        column: x => x.ToResponseId,
                        principalTable: "ResponseCollection",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DialogueResponseLink_FromDialogueId",
                table: "DialogueResponseLink",
                column: "FromDialogueId");

            migrationBuilder.CreateIndex(
                name: "IX_DialogueResponseLink_FromResponseId",
                table: "DialogueResponseLink",
                column: "FromResponseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DialogueResponseLink_ToDialogueId",
                table: "DialogueResponseLink",
                column: "ToDialogueId");

            migrationBuilder.CreateIndex(
                name: "IX_DialogueResponseLink_ToResponseId",
                table: "DialogueResponseLink",
                column: "ToResponseId");
        }
    }
}
