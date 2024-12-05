using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voluntr.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OAuthProviderModelEmailVerifiedProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailActivated",
                table: "User",
                newName: "EmailVerified");

            migrationBuilder.AddColumn<bool>(
                name: "EmailVerifiedProperty",
                table: "OAuthProvider",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailVerifiedProperty",
                table: "OAuthProvider");

            migrationBuilder.RenameColumn(
                name: "EmailVerified",
                table: "User",
                newName: "EmailActivated");
        }
    }
}
