using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RecSysApi.Infrastructure.Migrations
{
    public partial class OrderCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Orders_OrderID",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_OrderID",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "Courses");

            migrationBuilder.CreateTable(
                name: "CourseOrder",
                columns: table => new
                {
                    CoursesCourseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrdersOrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseOrder", x => new { x.CoursesCourseID, x.OrdersOrderID });
                    table.ForeignKey(
                        name: "FK_CourseOrder_Courses_CoursesCourseID",
                        column: x => x.CoursesCourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseOrder_Orders_OrdersOrderID",
                        column: x => x.OrdersOrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseOrder_OrdersOrderID",
                table: "CourseOrder",
                column: "OrdersOrderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseOrder");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderID",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_OrderID",
                table: "Courses",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Orders_OrderID",
                table: "Courses",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
