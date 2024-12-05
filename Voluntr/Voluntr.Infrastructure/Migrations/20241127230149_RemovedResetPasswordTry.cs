using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voluntr.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedResetPasswordTry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResetPasswordTry");

            migrationBuilder.AddColumn<bool>(
                name: "EmailActivated",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailActivated",
                table: "User");

            migrationBuilder.CreateTable(
                name: "ResetPasswordTry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResetToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResetPasswordTry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResetPasswordTry_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResetPasswordTry_UserId",
                table: "ResetPasswordTry",
                column: "UserId");
        }
    }
}
