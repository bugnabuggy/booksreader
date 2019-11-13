using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksReader.Infrastructure.Migrations
{
    public partial class nullableEndDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "BookSubscriptions",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.CreateIndex(
                name: "IX_BookSubscriptions_BookId",
                table: "BookSubscriptions",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookSubscriptions_Books_BookId",
                table: "BookSubscriptions",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookSubscriptions_Books_BookId",
                table: "BookSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_BookSubscriptions_BookId",
                table: "BookSubscriptions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "BookSubscriptions",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
