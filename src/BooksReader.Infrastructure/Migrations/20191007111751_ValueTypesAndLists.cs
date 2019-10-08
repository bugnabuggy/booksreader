using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksReader.Infrastructure.Migrations
{
    public partial class ValueTypesAndLists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_TypeValues_TypeId",
                table: "TypeValues",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TypeValues");

            migrationBuilder.DropTable(
                name: "TypesLists");
        }
    }
}
