using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class AddedTriggerSaves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialogueResponseSave_DialogueSave_DialogueSaveId",
                table: "DialogueResponseSave");

            migrationBuilder.DropTable(
                name: "ActionTriggerSave");

            migrationBuilder.DropTable(
                name: "NPCSave");

            migrationBuilder.DropTable(
                name: "DialogueSave");

            migrationBuilder.DropTable(
                name: "DialogueResponseSave");

            migrationBuilder.CreateTable(
                name: "ActionTriggerSaves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActionTriggerId = table.Column<int>(type: "integer", nullable: false),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    Complete = table.Column<bool>(type: "boolean", nullable: false),
                    Result = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionTriggerSaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionTriggerSaves_LocationSaves_LocationId",
                        column: x => x.LocationId,
                        principalTable: "LocationSaves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionTriggerSaves_Triggers_ActionTriggerId",
                        column: x => x.ActionTriggerId,
                        principalTable: "Triggers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionTriggerSaves_ActionTriggerId",
                table: "ActionTriggerSaves",
                column: "ActionTriggerId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionTriggerSaves_LocationId",
                table: "ActionTriggerSaves",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionTriggerSaves");

            migrationBuilder.CreateTable(
                name: "ActionTriggerSave",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActionTriggerId = table.Column<int>(type: "integer", nullable: false),
                    DialogueSaveId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionTriggerSave", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionTriggerSave_Triggers_ActionTriggerId",
                        column: x => x.ActionTriggerId,
                        principalTable: "Triggers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DialogueResponseSave",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DialogueResponseId = table.Column<int>(type: "integer", nullable: false),
                    DialogueSaveId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DialogueResponseSave", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DialogueResponseSave_ResponseCollection_DialogueResponseId",
                        column: x => x.DialogueResponseId,
                        principalTable: "ResponseCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DialogueSave",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DialogueId = table.Column<int>(type: "integer", nullable: false),
                    ParentResponseId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DialogueSave", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DialogueSave_DialogueCollection_DialogueId",
                        column: x => x.DialogueId,
                        principalTable: "DialogueCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DialogueSave_DialogueResponseSave_ParentResponseId",
                        column: x => x.ParentResponseId,
                        principalTable: "DialogueResponseSave",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NPCSave",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DialogueId = table.Column<int>(type: "integer", nullable: true),
                    NpcId = table.Column<int>(type: "integer", nullable: false),
                    LocationSaveId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPCSave", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NPCSave_DialogueSave_DialogueId",
                        column: x => x.DialogueId,
                        principalTable: "DialogueSave",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NPCSave_LocationSaves_LocationSaveId",
                        column: x => x.LocationSaveId,
                        principalTable: "LocationSaves",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NPCSave_NPCCollection_NpcId",
                        column: x => x.NpcId,
                        principalTable: "NPCCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionTriggerSave_ActionTriggerId",
                table: "ActionTriggerSave",
                column: "ActionTriggerId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionTriggerSave_DialogueSaveId",
                table: "ActionTriggerSave",
                column: "DialogueSaveId");

            migrationBuilder.CreateIndex(
                name: "IX_DialogueResponseSave_DialogueResponseId",
                table: "DialogueResponseSave",
                column: "DialogueResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_DialogueResponseSave_DialogueSaveId",
                table: "DialogueResponseSave",
                column: "DialogueSaveId");

            migrationBuilder.CreateIndex(
                name: "IX_DialogueSave_DialogueId",
                table: "DialogueSave",
                column: "DialogueId");

            migrationBuilder.CreateIndex(
                name: "IX_DialogueSave_ParentResponseId",
                table: "DialogueSave",
                column: "ParentResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_NPCSave_DialogueId",
                table: "NPCSave",
                column: "DialogueId");

            migrationBuilder.CreateIndex(
                name: "IX_NPCSave_LocationSaveId",
                table: "NPCSave",
                column: "LocationSaveId");

            migrationBuilder.CreateIndex(
                name: "IX_NPCSave_NpcId",
                table: "NPCSave",
                column: "NpcId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionTriggerSave_DialogueSave_DialogueSaveId",
                table: "ActionTriggerSave",
                column: "DialogueSaveId",
                principalTable: "DialogueSave",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DialogueResponseSave_DialogueSave_DialogueSaveId",
                table: "DialogueResponseSave",
                column: "DialogueSaveId",
                principalTable: "DialogueSave",
                principalColumn: "Id");
        }
    }
}
