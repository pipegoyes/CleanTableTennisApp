using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanTableTennisApp.Infrastructure.Persistence.Migrations
{
    public partial class AddPlayersToMatches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GuestPlayerId",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HostPlayerId",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_GuestPlayerId",
                table: "Matches",
                column: "GuestPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_HostPlayerId",
                table: "Matches",
                column: "HostPlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_GuestPlayerId",
                table: "Matches",
                column: "GuestPlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_HostPlayerId",
                table: "Matches",
                column: "HostPlayerId",
                principalTable: "Players",
                principalColumn: "Id", 
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_GuestPlayerId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_HostPlayerId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_GuestPlayerId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_HostPlayerId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "GuestPlayerId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "HostPlayerId",
                table: "Matches");
        }
    }
}
