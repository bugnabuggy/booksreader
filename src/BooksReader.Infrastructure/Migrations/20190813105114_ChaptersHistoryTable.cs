using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksReader.Infrastructure.Migrations
{
    public partial class ChaptersHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "BookChapters",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "BookChaptersHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OwnerId = table.Column<Guid>(nullable: false),
                    ChapterId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Title = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookChaptersHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookChaptersHistory_BookChapters_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "BookChapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_OwnerId",
                table: "Books",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_BookChapters_Created",
                table: "BookChapters",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_BookChapters_Number",
                table: "BookChapters",
                column: "Number");

            migrationBuilder.CreateIndex(
                name: "IX_BookChapters_OwnerId",
                table: "BookChapters",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_BookChapters_Title",
                table: "BookChapters",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_BookChapters_Version",
                table: "BookChapters",
                column: "Version");

            migrationBuilder.CreateIndex(
                name: "IX_BookChaptersHistory_ChapterId",
                table: "BookChaptersHistory",
                column: "ChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_BookChaptersHistory_Date",
                table: "BookChaptersHistory",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_BookChaptersHistory_OwnerId",
                table: "BookChaptersHistory",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookChaptersHistory");

            migrationBuilder.DropIndex(
                name: "IX_Books_OwnerId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_BookChapters_Created",
                table: "BookChapters");

            migrationBuilder.DropIndex(
                name: "IX_BookChapters_Number",
                table: "BookChapters");

            migrationBuilder.DropIndex(
                name: "IX_BookChapters_OwnerId",
                table: "BookChapters");

            migrationBuilder.DropIndex(
                name: "IX_BookChapters_Title",
                table: "BookChapters");

            migrationBuilder.DropIndex(
                name: "IX_BookChapters_Version",
                table: "BookChapters");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "BookChapters");
        }
    }
}
