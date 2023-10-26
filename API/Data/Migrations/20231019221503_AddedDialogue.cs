using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class AddedDialogue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DialogueCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DialogueCollection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResponseCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponseCollection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NPCCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Caption = table.Column<string>(type: "text", nullable: true),
                    DialogueId = table.Column<int>(type: "integer", nullable: true),
                    LocationId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPCCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NPCCollection_DialogueCollection_DialogueId",
                        column: x => x.DialogueId,
                        principalTable: "DialogueCollection",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NPCCollection_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

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
                name: "IX_NPCCollection_DialogueId",
                table: "NPCCollection",
                column: "DialogueId");

            migrationBuilder.CreateIndex(
                name: "IX_NPCCollection_LocationId",
                table: "NPCCollection",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ResponseDialogLink_ToDialogueId",
                table: "ResponseDialogLink",
                column: "ToDialogueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DialogResponseLink");

            migrationBuilder.DropTable(
                name: "NPCCollection");

            migrationBuilder.DropTable(
                name: "ResponseDialogLink");

            migrationBuilder.DropTable(
                name: "DialogueCollection");

            migrationBuilder.DropTable(
                name: "ResponseCollection");
        }
    }
}
