using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class BackPack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemSave_ContainerSaves_ContainerSaveId",
                table: "ItemSave");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSave_ItemCollection_ItemId",
                table: "ItemSave");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacters_ItemCollection_BodyId",
                table: "PlayerCharacters");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacters_ItemCollection_FeetId",
                table: "PlayerCharacters");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacters_ItemCollection_HelmetId",
                table: "PlayerCharacters");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacters_ItemCollection_LeftHandId",
                table: "PlayerCharacters");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacters_ItemCollection_RightHandId",
                table: "PlayerCharacters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemSave",
                table: "ItemSave");

            migrationBuilder.RenameTable(
                name: "ItemSave",
                newName: "ItemSaves");

            migrationBuilder.RenameColumn(
                name: "RightHandId",
                table: "PlayerCharacters",
                newName: "RightHandItemSaveId");

            migrationBuilder.RenameColumn(
                name: "LeftHandId",
                table: "PlayerCharacters",
                newName: "LeftHandItemSaveId");

            migrationBuilder.RenameColumn(
                name: "HelmetId",
                table: "PlayerCharacters",
                newName: "HelmetItemSaveId");

            migrationBuilder.RenameColumn(
                name: "FeetId",
                table: "PlayerCharacters",
                newName: "FeetItemSaveId");

            migrationBuilder.RenameColumn(
                name: "BodyId",
                table: "PlayerCharacters",
                newName: "BodyItemSaveId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerCharacters_RightHandId",
                table: "PlayerCharacters",
                newName: "IX_PlayerCharacters_RightHandItemSaveId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerCharacters_LeftHandId",
                table: "PlayerCharacters",
                newName: "IX_PlayerCharacters_LeftHandItemSaveId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerCharacters_HelmetId",
                table: "PlayerCharacters",
                newName: "IX_PlayerCharacters_HelmetItemSaveId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerCharacters_FeetId",
                table: "PlayerCharacters",
                newName: "IX_PlayerCharacters_FeetItemSaveId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerCharacters_BodyId",
                table: "PlayerCharacters",
                newName: "IX_PlayerCharacters_BodyItemSaveId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemSave_ItemId",
                table: "ItemSaves",
                newName: "IX_ItemSaves_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemSave_ContainerSaveId",
                table: "ItemSaves",
                newName: "IX_ItemSaves_ContainerSaveId");

            migrationBuilder.AlterColumn<int>(
                name: "ContainerSaveId",
                table: "ItemSaves",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "PlayerCharacterId",
                table: "ItemSaves",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemSaves",
                table: "ItemSaves",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSaves_PlayerCharacterId",
                table: "ItemSaves",
                column: "PlayerCharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSaves_ContainerSaves_ContainerSaveId",
                table: "ItemSaves",
                column: "ContainerSaveId",
                principalTable: "ContainerSaves",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSaves_ItemCollection_ItemId",
                table: "ItemSaves",
                column: "ItemId",
                principalTable: "ItemCollection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSaves_PlayerCharacters_PlayerCharacterId",
                table: "ItemSaves",
                column: "PlayerCharacterId",
                principalTable: "PlayerCharacters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacters_ItemSaves_BodyItemSaveId",
                table: "PlayerCharacters",
                column: "BodyItemSaveId",
                principalTable: "ItemSaves",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacters_ItemSaves_FeetItemSaveId",
                table: "PlayerCharacters",
                column: "FeetItemSaveId",
                principalTable: "ItemSaves",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacters_ItemSaves_HelmetItemSaveId",
                table: "PlayerCharacters",
                column: "HelmetItemSaveId",
                principalTable: "ItemSaves",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacters_ItemSaves_LeftHandItemSaveId",
                table: "PlayerCharacters",
                column: "LeftHandItemSaveId",
                principalTable: "ItemSaves",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacters_ItemSaves_RightHandItemSaveId",
                table: "PlayerCharacters",
                column: "RightHandItemSaveId",
                principalTable: "ItemSaves",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemSaves_ContainerSaves_ContainerSaveId",
                table: "ItemSaves");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSaves_ItemCollection_ItemId",
                table: "ItemSaves");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSaves_PlayerCharacters_PlayerCharacterId",
                table: "ItemSaves");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacters_ItemSaves_BodyItemSaveId",
                table: "PlayerCharacters");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacters_ItemSaves_FeetItemSaveId",
                table: "PlayerCharacters");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacters_ItemSaves_HelmetItemSaveId",
                table: "PlayerCharacters");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacters_ItemSaves_LeftHandItemSaveId",
                table: "PlayerCharacters");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerCharacters_ItemSaves_RightHandItemSaveId",
                table: "PlayerCharacters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemSaves",
                table: "ItemSaves");

            migrationBuilder.DropIndex(
                name: "IX_ItemSaves_PlayerCharacterId",
                table: "ItemSaves");

            migrationBuilder.DropColumn(
                name: "PlayerCharacterId",
                table: "ItemSaves");

            migrationBuilder.RenameTable(
                name: "ItemSaves",
                newName: "ItemSave");

            migrationBuilder.RenameColumn(
                name: "RightHandItemSaveId",
                table: "PlayerCharacters",
                newName: "RightHandId");

            migrationBuilder.RenameColumn(
                name: "LeftHandItemSaveId",
                table: "PlayerCharacters",
                newName: "LeftHandId");

            migrationBuilder.RenameColumn(
                name: "HelmetItemSaveId",
                table: "PlayerCharacters",
                newName: "HelmetId");

            migrationBuilder.RenameColumn(
                name: "FeetItemSaveId",
                table: "PlayerCharacters",
                newName: "FeetId");

            migrationBuilder.RenameColumn(
                name: "BodyItemSaveId",
                table: "PlayerCharacters",
                newName: "BodyId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerCharacters_RightHandItemSaveId",
                table: "PlayerCharacters",
                newName: "IX_PlayerCharacters_RightHandId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerCharacters_LeftHandItemSaveId",
                table: "PlayerCharacters",
                newName: "IX_PlayerCharacters_LeftHandId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerCharacters_HelmetItemSaveId",
                table: "PlayerCharacters",
                newName: "IX_PlayerCharacters_HelmetId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerCharacters_FeetItemSaveId",
                table: "PlayerCharacters",
                newName: "IX_PlayerCharacters_FeetId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerCharacters_BodyItemSaveId",
                table: "PlayerCharacters",
                newName: "IX_PlayerCharacters_BodyId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemSaves_ItemId",
                table: "ItemSave",
                newName: "IX_ItemSave_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemSaves_ContainerSaveId",
                table: "ItemSave",
                newName: "IX_ItemSave_ContainerSaveId");

            migrationBuilder.AlterColumn<int>(
                name: "ContainerSaveId",
                table: "ItemSave",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemSave",
                table: "ItemSave",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSave_ContainerSaves_ContainerSaveId",
                table: "ItemSave",
                column: "ContainerSaveId",
                principalTable: "ContainerSaves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSave_ItemCollection_ItemId",
                table: "ItemSave",
                column: "ItemId",
                principalTable: "ItemCollection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacters_ItemCollection_BodyId",
                table: "PlayerCharacters",
                column: "BodyId",
                principalTable: "ItemCollection",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacters_ItemCollection_FeetId",
                table: "PlayerCharacters",
                column: "FeetId",
                principalTable: "ItemCollection",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacters_ItemCollection_HelmetId",
                table: "PlayerCharacters",
                column: "HelmetId",
                principalTable: "ItemCollection",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacters_ItemCollection_LeftHandId",
                table: "PlayerCharacters",
                column: "LeftHandId",
                principalTable: "ItemCollection",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerCharacters_ItemCollection_RightHandId",
                table: "PlayerCharacters",
                column: "RightHandId",
                principalTable: "ItemCollection",
                principalColumn: "Id");
        }
    }
}
