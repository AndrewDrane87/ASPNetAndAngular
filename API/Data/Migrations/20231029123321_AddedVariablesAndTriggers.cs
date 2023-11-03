using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class AddedVariablesAndTriggers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdventureVariable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VariableType = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true),
                    AdventureId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdventureVariable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdventureVariable_Adventures_AdventureId",
                        column: x => x.AdventureId,
                        principalTable: "Adventures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Triggers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventType = table.Column<string>(type: "text", nullable: true),
                    ActionType = table.Column<string>(type: "text", nullable: true),
                    ActionData = table.Column<string>(type: "text", nullable: true),
                    ContainerId = table.Column<int>(type: "integer", nullable: true),
                    DialogueId = table.Column<int>(type: "integer", nullable: true),
                    InteractionId = table.Column<int>(type: "integer", nullable: true),
                    LocationId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Triggers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Triggers_ContainerCollection_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "ContainerCollection",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Triggers_DialogueCollection_DialogueId",
                        column: x => x.DialogueId,
                        principalTable: "DialogueCollection",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Triggers_Interactions_InteractionId",
                        column: x => x.InteractionId,
                        principalTable: "Interactions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Triggers_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdventureVariable_AdventureId",
                table: "AdventureVariable",
                column: "AdventureId");

            migrationBuilder.CreateIndex(
                name: "IX_Triggers_ContainerId",
                table: "Triggers",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Triggers_DialogueId",
                table: "Triggers",
                column: "DialogueId");

            migrationBuilder.CreateIndex(
                name: "IX_Triggers_InteractionId",
                table: "Triggers",
                column: "InteractionId");

            migrationBuilder.CreateIndex(
                name: "IX_Triggers_LocationId",
                table: "Triggers",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdventureVariable");

            migrationBuilder.DropTable(
                name: "Triggers");
        }
    }
}
