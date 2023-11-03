using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class AddedResultDataToTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResultData",
                table: "Triggers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultData",
                table: "Triggers");
        }
    }
}
