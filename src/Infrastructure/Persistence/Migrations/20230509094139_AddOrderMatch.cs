using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanTableTennisApp.Infrastructure.Persistence.Migrations
{
    public partial class AddOrderMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Order",
                table: "Matches",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Matches");
        }
    }
}
