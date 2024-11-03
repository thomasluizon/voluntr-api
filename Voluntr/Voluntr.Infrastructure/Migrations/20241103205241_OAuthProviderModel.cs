using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voluntr.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OAuthProviderModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OAuth",
                table: "User");

            migrationBuilder.DropColumn(
                name: "OAuthProvider",
                table: "User");

            migrationBuilder.AddColumn<Guid>(
                name: "OAuthProviderId",
                table: "User",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OAuthProvider",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserInfoApiUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailProperty = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameProperty = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PictureProperty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAuthProvider", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_OAuthProviderId",
                table: "User",
                column: "OAuthProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_OAuthProvider_OAuthProviderId",
                table: "User",
                column: "OAuthProviderId",
                principalTable: "OAuthProvider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_OAuthProvider_OAuthProviderId",
                table: "User");

            migrationBuilder.DropTable(
                name: "OAuthProvider");

            migrationBuilder.DropIndex(
                name: "IX_User_OAuthProviderId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "OAuthProviderId",
                table: "User");

            migrationBuilder.AddColumn<bool>(
                name: "OAuth",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OAuthProvider",
                table: "User",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
