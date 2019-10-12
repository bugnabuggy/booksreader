using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksReader.Infrastructure.Migrations
{
    public partial class domains : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "TypeValues",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TypesLists",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateTable(
                name: "SeoInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MetaDescription = table.Column<string>(maxLength: 256, nullable: true),
                    MetaKeywords = table.Column<string>(maxLength: 256, nullable: true),
                    MetaTitle = table.Column<string>(maxLength: 256, nullable: true),
                    MetaAuthor = table.Column<string>(maxLength: 256, nullable: true),
                    MetaCopyright = table.Column<string>(maxLength: 256, nullable: true),
                    OgTitle = table.Column<string>(maxLength: 256, nullable: true),
                    OgUrl = table.Column<string>(maxLength: 256, nullable: true),
                    OgImage = table.Column<string>(maxLength: 256, nullable: true),
                    OgDescription = table.Column<string>(maxLength: 256, nullable: true),
                    OgType = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeoInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserDomains",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    IsVerified = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    VerificationCode = table.Column<Guid>(nullable: false),
                    VerificationDate = table.Column<DateTime>(nullable: true),
                    VerificationRequested = table.Column<DateTime>(nullable: true),
                    VerificationType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDomains", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDomains_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublicPages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PageType = table.Column<int>(nullable: false),
                    SubjectId = table.Column<Guid>(nullable: true),
                    DomainId = table.Column<Guid>(nullable: false),
                    UrlPath = table.Column<string>(maxLength: 256, nullable: true),
                    Content = table.Column<string>(nullable: true),
                    SeoInfoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicPages_UserDomains_DomainId",
                        column: x => x.DomainId,
                        principalTable: "UserDomains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublicPages_SeoInfos_SeoInfoId",
                        column: x => x.SeoInfoId,
                        principalTable: "SeoInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PublicPages_DomainId",
                table: "PublicPages",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_PublicPages_SeoInfoId",
                table: "PublicPages",
                column: "SeoInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDomains_UserId",
                table: "UserDomains",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublicPages");

            migrationBuilder.DropTable(
                name: "UserDomains");

            migrationBuilder.DropTable(
                name: "SeoInfos");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "TypeValues",
                nullable: false,
                oldClrType: typeof(long))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TypesLists",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        }
    }
}
