using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksReader.Infrastructure.Migrations
{
    public partial class SemanticUrls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SemanticUrl",
                table: "Books",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SemanticUrl",
                table: "AuthorProfiles",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SemanticUrl",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "SemanticUrl",
                table: "AuthorProfiles");
        }
    }
}
