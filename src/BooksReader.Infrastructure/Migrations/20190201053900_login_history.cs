using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksReader.Web.Migrations
{
    public partial class login_history : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogHistory");

            migrationBuilder.CreateTable(
                name: "LoginHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    IpAddress = table.Column<string>(nullable: true),
                    Browser = table.Column<string>(nullable: true),
                    Screen = table.Column<string>(nullable: true),
                    Geolocation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginHistory", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginHistory");

            migrationBuilder.CreateTable(
                name: "LogHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Browser = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Geolocation = table.Column<string>(nullable: true),
                    IpAddress = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogHistory", x => x.Id);
                });
        }
    }
}
