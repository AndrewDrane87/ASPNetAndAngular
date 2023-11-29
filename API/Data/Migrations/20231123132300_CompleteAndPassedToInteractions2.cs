using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.data.migrations
{
    /// <inheritdoc />
    public partial class CompleteAndPassedToInteractions2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionTriggerSaves_InteractionSave_InteractionId",
                table: "ActionTriggerSaves");

            migrationBuilder.DropForeignKey(
                name: "FK_ActionTriggerSaves_LocationSaves_LocationId",
                table: "ActionTriggerSaves");

            migrationBuilder.DropForeignKey(
                name: "FK_ActionTriggerSaves_Triggers_ActionTriggerId",
                table: "ActionTriggerSaves");

            migrationBuilder.DropForeignKey(
                name: "FK_InteractionSave_Interactions_InteractionId",
                table: "InteractionSave");

            migrationBuilder.DropForeignKey(
                name: "FK_InteractionSave_LocationSaves_LocationSaveId",
                table: "InteractionSave");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InteractionSave",
                table: "InteractionSave");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActionTriggerSaves",
                table: "ActionTriggerSaves");

            migrationBuilder.RenameTable(
                name: "InteractionSave",
                newName: "InteractionSaves");

            migrationBuilder.RenameTable(
                name: "ActionTriggerSaves",
                newName: "TriggerSaves");

            migrationBuilder.RenameIndex(
                name: "IX_InteractionSave_LocationSaveId",
                table: "InteractionSaves",
                newName: "IX_InteractionSaves_LocationSaveId");

            migrationBuilder.RenameIndex(
                name: "IX_InteractionSave_InteractionId",
                table: "InteractionSaves",
                newName: "IX_InteractionSaves_InteractionId");

            migrationBuilder.RenameIndex(
                name: "IX_ActionTriggerSaves_LocationId",
                table: "TriggerSaves",
                newName: "IX_TriggerSaves_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_ActionTriggerSaves_InteractionId",
                table: "TriggerSaves",
                newName: "IX_TriggerSaves_InteractionId");

            migrationBuilder.RenameIndex(
                name: "IX_ActionTriggerSaves_ActionTriggerId",
                table: "TriggerSaves",
                newName: "IX_TriggerSaves_ActionTriggerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InteractionSaves",
                table: "InteractionSaves",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TriggerSaves",
                table: "TriggerSaves",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InteractionSaves_Interactions_InteractionId",
                table: "InteractionSaves",
                column: "InteractionId",
                principalTable: "Interactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InteractionSaves_LocationSaves_LocationSaveId",
                table: "InteractionSaves",
                column: "LocationSaveId",
                principalTable: "LocationSaves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TriggerSaves_InteractionSaves_InteractionId",
                table: "TriggerSaves",
                column: "InteractionId",
                principalTable: "InteractionSaves",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TriggerSaves_LocationSaves_LocationId",
                table: "TriggerSaves",
                column: "LocationId",
                principalTable: "LocationSaves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TriggerSaves_Triggers_ActionTriggerId",
                table: "TriggerSaves",
                column: "ActionTriggerId",
                principalTable: "Triggers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InteractionSaves_Interactions_InteractionId",
                table: "InteractionSaves");

            migrationBuilder.DropForeignKey(
                name: "FK_InteractionSaves_LocationSaves_LocationSaveId",
                table: "InteractionSaves");

            migrationBuilder.DropForeignKey(
                name: "FK_TriggerSaves_InteractionSaves_InteractionId",
                table: "TriggerSaves");

            migrationBuilder.DropForeignKey(
                name: "FK_TriggerSaves_LocationSaves_LocationId",
                table: "TriggerSaves");

            migrationBuilder.DropForeignKey(
                name: "FK_TriggerSaves_Triggers_ActionTriggerId",
                table: "TriggerSaves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TriggerSaves",
                table: "TriggerSaves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InteractionSaves",
                table: "InteractionSaves");

            migrationBuilder.RenameTable(
                name: "TriggerSaves",
                newName: "ActionTriggerSaves");

            migrationBuilder.RenameTable(
                name: "InteractionSaves",
                newName: "InteractionSave");

            migrationBuilder.RenameIndex(
                name: "IX_TriggerSaves_LocationId",
                table: "ActionTriggerSaves",
                newName: "IX_ActionTriggerSaves_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_TriggerSaves_InteractionId",
                table: "ActionTriggerSaves",
                newName: "IX_ActionTriggerSaves_InteractionId");

            migrationBuilder.RenameIndex(
                name: "IX_TriggerSaves_ActionTriggerId",
                table: "ActionTriggerSaves",
                newName: "IX_ActionTriggerSaves_ActionTriggerId");

            migrationBuilder.RenameIndex(
                name: "IX_InteractionSaves_LocationSaveId",
                table: "InteractionSave",
                newName: "IX_InteractionSave_LocationSaveId");

            migrationBuilder.RenameIndex(
                name: "IX_InteractionSaves_InteractionId",
                table: "InteractionSave",
                newName: "IX_InteractionSave_InteractionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActionTriggerSaves",
                table: "ActionTriggerSaves",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InteractionSave",
                table: "InteractionSave",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionTriggerSaves_InteractionSave_InteractionId",
                table: "ActionTriggerSaves",
                column: "InteractionId",
                principalTable: "InteractionSave",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionTriggerSaves_LocationSaves_LocationId",
                table: "ActionTriggerSaves",
                column: "LocationId",
                principalTable: "LocationSaves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActionTriggerSaves_Triggers_ActionTriggerId",
                table: "ActionTriggerSaves",
                column: "ActionTriggerId",
                principalTable: "Triggers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InteractionSave_Interactions_InteractionId",
                table: "InteractionSave",
                column: "InteractionId",
                principalTable: "Interactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InteractionSave_LocationSaves_LocationSaveId",
                table: "InteractionSave",
                column: "LocationSaveId",
                principalTable: "LocationSaves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
