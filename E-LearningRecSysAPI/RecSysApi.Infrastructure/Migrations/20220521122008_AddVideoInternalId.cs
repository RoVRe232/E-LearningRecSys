using Microsoft.EntityFrameworkCore.Migrations;

namespace RecSysApi.Infrastructure.Migrations
{
    public partial class AddVideoInternalId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InternalId",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InternalId",
                table: "Videos");
        }
    }
}
