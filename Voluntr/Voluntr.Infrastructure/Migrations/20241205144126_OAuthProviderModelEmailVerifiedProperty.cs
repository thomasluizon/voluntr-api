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

            migrationBuilder.AlterColumn<string>(
                name: "PictureProperty",
                table: "OAuthProvider",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "EmailVerifiedProperty",
                table: "OAuthProvider",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
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

            migrationBuilder.AlterColumn<string>(
                name: "PictureProperty",
                table: "OAuthProvider",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
