using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksReader.Infrastructure.Migrations
{
    public partial class PictureForBookAndConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Books",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Books",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "BookChapters",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Number",
                table: "BookChapters",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_PersonalPages_Domain",
                table: "PersonalPages",
                column: "Domain");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalPages_SubjectId",
                table: "PersonalPages",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Author",
                table: "Books",
                column: "Author");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Created",
                table: "Books",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Published",
                table: "Books",
                column: "Published");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Title",
                table: "Books",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_BookChapters_BookId",
                table: "BookChapters",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorProfiles_AuthorName",
                table: "AuthorProfiles",
                column: "AuthorName");

            migrationBuilder.AddForeignKey(
                name: "FK_BookChapters_Books_BookId",
                table: "BookChapters",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookChapters_Books_BookId",
                table: "BookChapters");

            migrationBuilder.DropIndex(
                name: "IX_PersonalPages_Domain",
                table: "PersonalPages");

            migrationBuilder.DropIndex(
                name: "IX_PersonalPages_SubjectId",
                table: "PersonalPages");

            migrationBuilder.DropIndex(
                name: "IX_Books_Author",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_Created",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_Published",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_Title",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_BookChapters_BookId",
                table: "BookChapters");

            migrationBuilder.DropIndex(
                name: "IX_AuthorProfiles_AuthorName",
                table: "AuthorProfiles");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Books",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "BookChapters",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "BookChapters",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
