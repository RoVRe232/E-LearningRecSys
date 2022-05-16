using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RecSysApi.Infrastructure.Migrations
{
    public partial class VideoRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_SearchProperties_SearchPropertiesID",
                table: "Videos");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_VideoMetadata_MetadataVideoMetadataID",
                table: "Videos");

            migrationBuilder.DropTable(
                name: "SearchProperties");

            migrationBuilder.DropTable(
                name: "VideoBoxDescription");

            migrationBuilder.DropTable(
                name: "VideoMetadata");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VideoSlides",
                table: "VideoSlides");

            migrationBuilder.DropIndex(
                name: "IX_Videos_MetadataVideoMetadataID",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_SearchPropertiesID",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Poster",
                table: "VideoSource");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "VideoSlides");

            migrationBuilder.DropColumn(
                name: "CanRead",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "CanWrite",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Catalog",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "HideSocial",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "MetadataVideoMetadataID",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "ProcessSlides",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Repository",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "SearchPropertiesID",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Unprocessed",
                table: "Videos");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Courses",
                newName: "ThumbnailImage");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "VideoSource",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Videos",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddColumn<string>(
                name: "Keywords",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Sections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailImage",
                table: "Sections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Sections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Hours",
                table: "Courses",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "LargeDescription",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SmallDescription",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VideoSlides",
                table: "VideoSlides",
                column: "VideoSlidesID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VideoSlides",
                table: "VideoSlides");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "VideoSource");

            migrationBuilder.DropColumn(
                name: "Keywords",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "ThumbnailImage",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "Hours",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "LargeDescription",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "SmallDescription",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "ThumbnailImage",
                table: "Courses",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "Poster",
                table: "VideoSource",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "VideoSlides",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Videos",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CanRead",
                table: "Videos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanWrite",
                table: "Videos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Catalog",
                table: "Videos",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HideSocial",
                table: "Videos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "MetadataVideoMetadataID",
                table: "Videos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ProcessSlides",
                table: "Videos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Repository",
                table: "Videos",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SearchPropertiesID",
                table: "Videos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Unprocessed",
                table: "Videos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VideoSlides",
                table: "VideoSlides",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SearchProperties",
                columns: table => new
                {
                    SearchPropertiesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Transcription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchProperties", x => x.SearchPropertiesID);
                });

            migrationBuilder.CreateTable(
                name: "VideoBoxDescription",
                columns: table => new
                {
                    VideoBoxDescriptionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Mimetype = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Src = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    VideoSourceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Width = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoBoxDescription", x => x.VideoBoxDescriptionID);
                    table.ForeignKey(
                        name: "FK_VideoBoxDescription_VideoSource_VideoSourceID",
                        column: x => x.VideoSourceID,
                        principalTable: "VideoSource",
                        principalColumn: "VideoSourceID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VideoMetadata",
                columns: table => new
                {
                    VideoMetadataID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoMetadata", x => x.VideoMetadataID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Videos_MetadataVideoMetadataID",
                table: "Videos",
                column: "MetadataVideoMetadataID");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_SearchPropertiesID",
                table: "Videos",
                column: "SearchPropertiesID");

            migrationBuilder.CreateIndex(
                name: "IX_VideoBoxDescription_VideoSourceID",
                table: "VideoBoxDescription",
                column: "VideoSourceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_SearchProperties_SearchPropertiesID",
                table: "Videos",
                column: "SearchPropertiesID",
                principalTable: "SearchProperties",
                principalColumn: "SearchPropertiesID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_VideoMetadata_MetadataVideoMetadataID",
                table: "Videos",
                column: "MetadataVideoMetadataID",
                principalTable: "VideoMetadata",
                principalColumn: "VideoMetadataID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
