using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoMVC2.Migrations
{
    public partial class Create_Table_Faculty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Faculty",
                columns: table => new
                {
                    FacultyID = table.Column<string>(type: "TEXT", nullable: false),
                    FacultyName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculty", x => x.FacultyID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Faculty");
        }
    }
}
