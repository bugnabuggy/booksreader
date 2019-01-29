using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksReader.Web.Migrations
{
    public partial class booksstructurechanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Autor",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "AuthorName",
                table: "Books",
                newName: "Author");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Books",
                newName: "AuthorName");

            migrationBuilder.AddColumn<Guid>(
                name: "Autor",
                table: "Books",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
