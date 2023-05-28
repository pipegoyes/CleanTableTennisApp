using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanTableTennisApp.Infrastructure.Persistence.Migrations
{
    public partial class DoubleWithScores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoubleMatch_Players_GuestPlayerRigthId",
                table: "DoubleMatch");

            migrationBuilder.RenameColumn(
                name: "GuestPlayerRigthId",
                table: "DoubleMatch",
                newName: "GuestPlayerRightId");

            migrationBuilder.RenameIndex(
                name: "IX_DoubleMatch_GuestPlayerRigthId",
                table: "DoubleMatch",
                newName: "IX_DoubleMatch_GuestPlayerRightId");

            migrationBuilder.AddColumn<int>(
                name: "DoubleMatchId",
                table: "Score",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Score_DoubleMatchId",
                table: "Score",
                column: "DoubleMatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoubleMatch_Players_GuestPlayerRightId",
                table: "DoubleMatch",
                column: "GuestPlayerRightId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Score_DoubleMatch_DoubleMatchId",
                table: "Score",
                column: "DoubleMatchId",
                principalTable: "DoubleMatch",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoubleMatch_Players_GuestPlayerRightId",
                table: "DoubleMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_Score_DoubleMatch_DoubleMatchId",
                table: "Score");

            migrationBuilder.DropIndex(
                name: "IX_Score_DoubleMatchId",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "DoubleMatchId",
                table: "Score");

            migrationBuilder.RenameColumn(
                name: "GuestPlayerRightId",
                table: "DoubleMatch",
                newName: "GuestPlayerRigthId");

            migrationBuilder.RenameIndex(
                name: "IX_DoubleMatch_GuestPlayerRightId",
                table: "DoubleMatch",
                newName: "IX_DoubleMatch_GuestPlayerRigthId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoubleMatch_Players_GuestPlayerRigthId",
                table: "DoubleMatch",
                column: "GuestPlayerRigthId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
