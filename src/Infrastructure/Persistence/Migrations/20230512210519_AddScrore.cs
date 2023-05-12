using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanTableTennisApp.Infrastructure.Persistence.Migrations
{
    public partial class AddScrore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_TeamMatches_TeamMatchId",
                table: "Matches");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TeamMatchId",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Score",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GamePointsHost = table.Column<int>(type: "int", nullable: false),
                    GamePointsGuest = table.Column<int>(type: "int", nullable: false),
                    MatchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Score", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Score_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Score_MatchId",
                table: "Score",
                column: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_TeamMatches_TeamMatchId",
                table: "Matches",
                column: "TeamMatchId",
                principalTable: "TeamMatches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_TeamMatches_TeamMatchId",
                table: "Matches");

            migrationBuilder.DropTable(
                name: "Score");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "TeamMatchId",
                table: "Matches",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_TeamMatches_TeamMatchId",
                table: "Matches",
                column: "TeamMatchId",
                principalTable: "TeamMatches",
                principalColumn: "Id");
        }
    }
}
