using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.WebUI.Migrations
{
    public partial class abc2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DotUsername",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DotUsername",
                table: "Users");
        }
    }
}
