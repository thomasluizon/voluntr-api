using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voluntr.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserModelPauseProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Paused",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paused",
                table: "User");
        }
    }
}
