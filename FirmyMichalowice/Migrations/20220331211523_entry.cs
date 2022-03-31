using Microsoft.EntityFrameworkCore.Migrations;

namespace FirmyMichalowice.Migrations
{
    public partial class entry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EntryCount",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntryCount",
                table: "Users");
        }
    }
}
