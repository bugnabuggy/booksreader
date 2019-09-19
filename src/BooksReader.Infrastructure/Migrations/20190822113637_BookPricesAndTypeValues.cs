using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksReader.Infrastructure.Migrations
{
    public partial class BookPricesAndTypeValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsForSale",
                table: "Books",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "TypesLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 120, nullable: true),
                    LocalizationKey = table.Column<string>(maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeValues",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 120, nullable: false),
                    LocalizationKey = table.Column<string>(maxLength: 60, nullable: true),
                    Value = table.Column<string>(nullable: true),
                    TypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypeValues_TypesLists_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TypesLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BooksPrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OwnerId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    BookId = table.Column<Guid>(nullable: false),
                    CurrencyId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BooksPrices_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BooksPrices_TypeValues_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "TypeValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "IX_BooksPrices_BookId",
                table: "BooksPrices",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BooksPrices_CurrencyId",
                table: "BooksPrices",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeValues_TypeId",
                table: "TypeValues",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BooksPrices");

            migrationBuilder.DropTable(
                name: "TypeValues");

            migrationBuilder.DropTable(
                name: "TypesLists");

            migrationBuilder.DropColumn(
                name: "IsForSale",
                table: "Books");
        }
    }
}
