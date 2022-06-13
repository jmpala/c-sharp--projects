using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkRelations.Migrations
{
    public partial class AvailableAmountField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailableAmount",
                table: "BrickAvailabilities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableAmount",
                table: "BrickAvailabilities");
        }
    }
}
