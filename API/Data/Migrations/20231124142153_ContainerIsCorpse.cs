using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.data.migrations
{
    /// <inheritdoc />
    public partial class ContainerIsCorpse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCorpse",
                table: "Containers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCorpse",
                table: "Containers");
        }
    }
}
