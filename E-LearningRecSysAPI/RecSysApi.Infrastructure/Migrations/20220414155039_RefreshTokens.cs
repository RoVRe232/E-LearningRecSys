using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RecSysApi.Infrastructure.Migrations
{
    public partial class RefreshTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActiveRefreshTokenToken",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JwtToken",
                columns: table => new
                {
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JwtToken", x => x.Token);
                    table.ForeignKey(
                        name: "FK_JwtToken_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ActiveRefreshTokenToken",
                table: "Users",
                column: "ActiveRefreshTokenToken");

            migrationBuilder.CreateIndex(
                name: "IX_JwtToken_UserID",
                table: "JwtToken",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_JwtToken_ActiveRefreshTokenToken",
                table: "Users",
                column: "ActiveRefreshTokenToken",
                principalTable: "JwtToken",
                principalColumn: "Token",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_JwtToken_ActiveRefreshTokenToken",
                table: "Users");

            migrationBuilder.DropTable(
                name: "JwtToken");

            migrationBuilder.DropIndex(
                name: "IX_Users_ActiveRefreshTokenToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ActiveRefreshTokenToken",
                table: "Users");
        }
    }
}
