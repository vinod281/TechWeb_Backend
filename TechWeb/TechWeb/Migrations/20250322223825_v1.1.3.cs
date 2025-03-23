using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechWeb.Migrations
{
    public partial class v113 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PID",
                table: "P_Images",
                newName: "ProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "P_Images",
                newName: "PID");
        }
    }
}
