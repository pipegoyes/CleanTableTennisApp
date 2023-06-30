using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanTableTennisApp.Infrastructure.Persistence.Migrations
{
    public partial class renameSingleMatchSecondTry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_GuestPlayerId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_HostPlayerId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_TeamMatches_TeamMatchId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Score_Matches_MatchId",
                table: "Score");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matches",
                table: "Matches");

            migrationBuilder.RenameTable(
                name: "Matches",
                newName: "SingleMatches");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_TeamMatchId",
                table: "SingleMatches",
                newName: "IX_SingleMatches_TeamMatchId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_HostPlayerId",
                table: "SingleMatches",
                newName: "IX_SingleMatches_HostPlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_GuestPlayerId",
                table: "SingleMatches",
                newName: "IX_SingleMatches_GuestPlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SingleMatches",
                table: "SingleMatches",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Score_SingleMatches_MatchId",
                table: "Score",
                column: "MatchId",
                principalTable: "SingleMatches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SingleMatches_Players_GuestPlayerId",
                table: "SingleMatches",
                column: "GuestPlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_SingleMatches_Players_HostPlayerId",
                table: "SingleMatches",
                column: "HostPlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_SingleMatches_TeamMatches_TeamMatchId",
                table: "SingleMatches",
                column: "TeamMatchId",
                principalTable: "TeamMatches",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Score_SingleMatches_MatchId",
                table: "Score");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleMatches_Players_GuestPlayerId",
                table: "SingleMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleMatches_Players_HostPlayerId",
                table: "SingleMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleMatches_TeamMatches_TeamMatchId",
                table: "SingleMatches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SingleMatches",
                table: "SingleMatches");

            migrationBuilder.RenameTable(
                name: "SingleMatches",
                newName: "Matches");

            migrationBuilder.RenameIndex(
                name: "IX_SingleMatches_TeamMatchId",
                table: "Matches",
                newName: "IX_Matches_TeamMatchId");

            migrationBuilder.RenameIndex(
                name: "IX_SingleMatches_HostPlayerId",
                table: "Matches",
                newName: "IX_Matches_HostPlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_SingleMatches_GuestPlayerId",
                table: "Matches",
                newName: "IX_Matches_GuestPlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matches",
                table: "Matches",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_GuestPlayerId",
                table: "Matches",
                column: "GuestPlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_HostPlayerId",
                table: "Matches",
                column: "HostPlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_TeamMatches_TeamMatchId",
                table: "Matches",
                column: "TeamMatchId",
                principalTable: "TeamMatches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Score_Matches_MatchId",
                table: "Score",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id");
        }
    }
}
