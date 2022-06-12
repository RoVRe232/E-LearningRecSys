using Microsoft.EntityFrameworkCore.Migrations;

namespace RecSysApi.Infrastructure.Migrations
{
    public partial class RemoveCourseFromLicense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseLicenses_Courses_CourseID",
                table: "CourseLicenses");

            migrationBuilder.DropIndex(
                name: "IX_CourseLicenses_CourseID",
                table: "CourseLicenses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CourseLicenses_CourseID",
                table: "CourseLicenses",
                column: "CourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseLicenses_Courses_CourseID",
                table: "CourseLicenses",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
