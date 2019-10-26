using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksReader.Infrastructure.Migrations
{
    public partial class Histories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Verified",
                table: "Books",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "Books",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "BooksHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OwnerId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 1000, nullable: true),
                    Author = table.Column<string>(maxLength: 1000, nullable: true),
                    Picture = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 3000, nullable: true),
                    Version = table.Column<long>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChaptersHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OwnerId = table.Column<Guid>(nullable: false),
                    ChapterId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Version = table.Column<long>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Title = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChaptersHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChaptersHistory_BookChapters_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "BookChapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChaptersHistory_ChapterId",
                table: "ChaptersHistory",
                column: "ChapterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BooksHistory");

            migrationBuilder.DropTable(
                name: "ChaptersHistory");

            migrationBuilder.DropColumn(
                name: "Verified",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Books");
        }
    }
}
