using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RecSysApi.Infrastructure.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    PriceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.PriceID);
                });

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

            migrationBuilder.CreateTable(
                name: "VideoSource",
                columns: table => new
                {
                    VideoSourceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Poster = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoSource", x => x.VideoSourceID);
                });

            migrationBuilder.CreateTable(
                name: "Bundles",
                columns: table => new
                {
                    BundleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bundles", x => x.BundleID);
                    table.ForeignKey(
                        name: "FK_Bundles_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoBoxDescription",
                columns: table => new
                {
                    VideoBoxDescriptionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mimetype = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Src = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    VideoSourceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                name: "Courses",
                columns: table => new
                {
                    CourseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BundleID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseID);
                    table.ForeignKey(
                        name: "FK_Courses_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Courses_Bundles_BundleID",
                        column: x => x.BundleID,
                        principalTable: "Bundles",
                        principalColumn: "BundleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Courses_Prices_PriceID",
                        column: x => x.PriceID,
                        principalTable: "Prices",
                        principalColumn: "PriceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdminID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Admins_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    PublisherID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.PublisherID);
                    table.ForeignKey(
                        name: "FK_Publishers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    SectionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.SectionID);
                    table.ForeignKey(
                        name: "FK_Sections_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    VideoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    SourceVideoSourceID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MetadataVideoMetadataID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Thumbnail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Hidden = table.Column<bool>(type: "bit", nullable: false),
                    CanRead = table.Column<bool>(type: "bit", nullable: false),
                    CanWrite = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Repository = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Language = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    HiddenInSearches = table.Column<bool>(type: "bit", nullable: false),
                    SearchPropertiesID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HideSocial = table.Column<bool>(type: "bit", nullable: false),
                    Catalog = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Published = table.Column<bool>(type: "bit", nullable: false),
                    Unprocessed = table.Column<bool>(type: "bit", nullable: false),
                    ProcessSlides = table.Column<bool>(type: "bit", nullable: false),
                    Duration = table.Column<double>(type: "float", nullable: false),
                    Transcription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.VideoID);
                    table.ForeignKey(
                        name: "FK_Videos_SearchProperties_SearchPropertiesID",
                        column: x => x.SearchPropertiesID,
                        principalTable: "SearchProperties",
                        principalColumn: "SearchPropertiesID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Videos_Sections_SectionID",
                        column: x => x.SectionID,
                        principalTable: "Sections",
                        principalColumn: "SectionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Videos_VideoMetadata_MetadataVideoMetadataID",
                        column: x => x.MetadataVideoMetadataID,
                        principalTable: "VideoMetadata",
                        principalColumn: "VideoMetadataID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Videos_VideoSource_SourceVideoSourceID",
                        column: x => x.SourceVideoSourceID,
                        principalTable: "VideoSource",
                        principalColumn: "VideoSourceID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VideoSlides",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    VideoSlidesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mimetype = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Thumb = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Time = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    VideoID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoSlides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoSlides_Videos_VideoID",
                        column: x => x.VideoID,
                        principalTable: "Videos",
                        principalColumn: "VideoID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bundles_AccountID",
                table: "Bundles",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_AccountID",
                table: "Courses",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_BundleID",
                table: "Courses",
                column: "BundleID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_PriceID",
                table: "Courses",
                column: "PriceID");

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_UserID",
                table: "Publishers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_CourseID",
                table: "Sections",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AccountID",
                table: "Users",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_VideoBoxDescription_VideoSourceID",
                table: "VideoBoxDescription",
                column: "VideoSourceID");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_MetadataVideoMetadataID",
                table: "Videos",
                column: "MetadataVideoMetadataID");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_SearchPropertiesID",
                table: "Videos",
                column: "SearchPropertiesID");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_SectionID",
                table: "Videos",
                column: "SectionID");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_SourceVideoSourceID",
                table: "Videos",
                column: "SourceVideoSourceID");

            migrationBuilder.CreateIndex(
                name: "IX_VideoSlides_VideoID",
                table: "VideoSlides",
                column: "VideoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "VideoBoxDescription");

            migrationBuilder.DropTable(
                name: "VideoSlides");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "SearchProperties");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "VideoMetadata");

            migrationBuilder.DropTable(
                name: "VideoSource");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Bundles");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
