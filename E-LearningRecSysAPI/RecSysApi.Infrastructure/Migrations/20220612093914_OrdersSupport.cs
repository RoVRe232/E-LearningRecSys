using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RecSysApi.Infrastructure.Migrations
{
    public partial class OrdersSupport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderID",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileImageUrl",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CourseLicenses",
                columns: table => new
                {
                    CourseLicenseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourseID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseLicenses", x => x.CourseLicenseID);
                    table.ForeignKey(
                        name: "FK_CourseLicenses_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseLicenses_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Acknowladged = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_OrderID",
                table: "Courses",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseLicenses_AccountID",
                table: "CourseLicenses",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseLicenses_CourseID",
                table: "CourseLicenses",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AccountID",
                table: "Orders",
                column: "AccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Orders_OrderID",
                table: "Courses",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Orders_OrderID",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "CourseLicenses");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Courses_OrderID",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ProfileImageUrl",
                table: "Accounts");
        }
    }
}
