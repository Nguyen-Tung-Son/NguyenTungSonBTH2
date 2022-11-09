using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoMVC2.Migrations
{
    public partial class Khachhang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Khachhangs",
                columns: table => new
                {
                    KhachhangID = table.Column<string>(type: "TEXT", nullable: false),
                    KhachhangName = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khachhangs", x => x.KhachhangID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Khachhangs");
        }
    }
}
