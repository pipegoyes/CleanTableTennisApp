using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanTableTennisApp.Infrastructure.Persistence.Migrations
{
    public partial class SeparateDoubleScores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Score_DoubleMatch_DoubleMatchId",
                table: "Score");

            migrationBuilder.DropForeignKey(
                name: "FK_Score_Matches_MatchId",
                table: "Score");

            migrationBuilder.DropIndex(
                name: "IX_Score_DoubleMatchId",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "DoubleMatchId",
                table: "Score");

            migrationBuilder.AlterColumn<int>(
                name: "MatchId",
                table: "Score",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "DoubleMatchScore",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GamePointsHost = table.Column<int>(type: "int", nullable: false),
                    GamePointsGuest = table.Column<int>(type: "int", nullable: false),
                    DoubleMatchId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoubleMatchScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoubleMatchScore_DoubleMatch_DoubleMatchId",
                        column: x => x.DoubleMatchId,
                        principalTable: "DoubleMatch",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoubleMatchScore_DoubleMatchId",
                table: "DoubleMatchScore",
                column: "DoubleMatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Score_Matches_MatchId",
                table: "Score",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Score_Matches_MatchId",
                table: "Score");

            migrationBuilder.DropTable(
                name: "DoubleMatchScore");

            migrationBuilder.AlterColumn<int>(
                name: "MatchId",
                table: "Score",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
                name: "FK_Score_DoubleMatch_DoubleMatchId",
                table: "Score",
                column: "DoubleMatchId",
                principalTable: "DoubleMatch",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Score_Matches_MatchId",
                table: "Score",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
