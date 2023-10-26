using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class FixingDialogue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DialogResponseLink");

            migrationBuilder.DropTable(
                name: "ResponseDialogLink");

            migrationBuilder.CreateTable(
                name: "DialogueResponseLink",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FromDialogueId = table.Column<int>(type: "integer", nullable: true),
                    ToResponseId = table.Column<int>(type: "integer", nullable: true),
                    ToDialogueId = table.Column<int>(type: "integer", nullable: true),
                    FromResponseId = table.Column<int>(type: "integer", nullable: true)
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
                column: "FromResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_DialogueResponseLink_ToDialogueId",
                table: "DialogueResponseLink",
                column: "ToDialogueId");

            migrationBuilder.CreateIndex(
                name: "IX_DialogueResponseLink_ToResponseId",
                table: "DialogueResponseLink",
                column: "ToResponseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DialogueResponseLink");

            migrationBuilder.CreateTable(
                name: "DialogResponseLink",
                columns: table => new
                {
                    FromDialogId = table.Column<int>(type: "integer", nullable: false),
                    ToResponseId = table.Column<int>(type: "integer", nullable: false),
                    FromDialogueId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DialogResponseLink", x => new { x.FromDialogId, x.ToResponseId });
                    table.ForeignKey(
                        name: "FK_DialogResponseLink_DialogueCollection_FromDialogueId",
                        column: x => x.FromDialogueId,
                        principalTable: "DialogueCollection",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DialogResponseLink_ResponseCollection_ToResponseId",
                        column: x => x.ToResponseId,
                        principalTable: "ResponseCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResponseDialogLink",
                columns: table => new
                {
                    FromResponseId = table.Column<int>(type: "integer", nullable: false),
                    ToDialogueId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponseDialogLink", x => new { x.FromResponseId, x.ToDialogueId });
                    table.ForeignKey(
                        name: "FK_ResponseDialogLink_DialogueCollection_ToDialogueId",
                        column: x => x.ToDialogueId,
                        principalTable: "DialogueCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResponseDialogLink_ResponseCollection_FromResponseId",
                        column: x => x.FromResponseId,
                        principalTable: "ResponseCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DialogResponseLink_FromDialogueId",
                table: "DialogResponseLink",
                column: "FromDialogueId");

            migrationBuilder.CreateIndex(
                name: "IX_DialogResponseLink_ToResponseId",
                table: "DialogResponseLink",
                column: "ToResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_ResponseDialogLink_ToDialogueId",
                table: "ResponseDialogLink",
                column: "ToDialogueId");
        }
    }
}
