using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanTableTennisApp.Infrastructure.Persistence.Migrations
{
    public partial class playingOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "SingleMatches");

            migrationBuilder.AddColumn<int>(
                name: "PlayingOrder",
                table: "SingleMatches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlayingOrder",
                table: "DoubleMatches",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayingOrder",
                table: "SingleMatches");

            migrationBuilder.DropColumn(
                name: "PlayingOrder",
                table: "DoubleMatches");

            migrationBuilder.AddColumn<short>(
                name: "Order",
                table: "SingleMatches",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
