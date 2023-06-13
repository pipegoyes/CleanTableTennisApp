using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanTableTennisApp.Infrastructure.Persistence.Migrations
{
    public partial class AddDoubleMatches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoubleMatch_Players_GuestPlayerLeftId",
                table: "DoubleMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_DoubleMatch_Players_GuestPlayerRightId",
                table: "DoubleMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_DoubleMatch_Players_HostPlayerLeftId",
                table: "DoubleMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_DoubleMatch_Players_HostPlayerRightId",
                table: "DoubleMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_DoubleMatch_TeamMatches_TeamMatchId",
                table: "DoubleMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_DoubleMatchScore_DoubleMatch_DoubleMatchId",
                table: "DoubleMatchScore");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoubleMatch",
                table: "DoubleMatch");

            migrationBuilder.RenameTable(
                name: "DoubleMatch",
                newName: "DoubleMatches");

            migrationBuilder.RenameIndex(
                name: "IX_DoubleMatch_TeamMatchId",
                table: "DoubleMatches",
                newName: "IX_DoubleMatches_TeamMatchId");

            migrationBuilder.RenameIndex(
                name: "IX_DoubleMatch_HostPlayerRightId",
                table: "DoubleMatches",
                newName: "IX_DoubleMatches_HostPlayerRightId");

            migrationBuilder.RenameIndex(
                name: "IX_DoubleMatch_HostPlayerLeftId",
                table: "DoubleMatches",
                newName: "IX_DoubleMatches_HostPlayerLeftId");

            migrationBuilder.RenameIndex(
                name: "IX_DoubleMatch_GuestPlayerRightId",
                table: "DoubleMatches",
                newName: "IX_DoubleMatches_GuestPlayerRightId");

            migrationBuilder.RenameIndex(
                name: "IX_DoubleMatch_GuestPlayerLeftId",
                table: "DoubleMatches",
                newName: "IX_DoubleMatches_GuestPlayerLeftId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoubleMatches",
                table: "DoubleMatches",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoubleMatches_Players_GuestPlayerLeftId",
                table: "DoubleMatches",
                column: "GuestPlayerLeftId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoubleMatches_Players_GuestPlayerRightId",
                table: "DoubleMatches",
                column: "GuestPlayerRightId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoubleMatches_Players_HostPlayerLeftId",
                table: "DoubleMatches",
                column: "HostPlayerLeftId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoubleMatches_Players_HostPlayerRightId",
                table: "DoubleMatches",
                column: "HostPlayerRightId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoubleMatches_TeamMatches_TeamMatchId",
                table: "DoubleMatches",
                column: "TeamMatchId",
                principalTable: "TeamMatches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoubleMatchScore_DoubleMatches_DoubleMatchId",
                table: "DoubleMatchScore",
                column: "DoubleMatchId",
                principalTable: "DoubleMatches",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoubleMatches_Players_GuestPlayerLeftId",
                table: "DoubleMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_DoubleMatches_Players_GuestPlayerRightId",
                table: "DoubleMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_DoubleMatches_Players_HostPlayerLeftId",
                table: "DoubleMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_DoubleMatches_Players_HostPlayerRightId",
                table: "DoubleMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_DoubleMatches_TeamMatches_TeamMatchId",
                table: "DoubleMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_DoubleMatchScore_DoubleMatches_DoubleMatchId",
                table: "DoubleMatchScore");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoubleMatches",
                table: "DoubleMatches");

            migrationBuilder.RenameTable(
                name: "DoubleMatches",
                newName: "DoubleMatch");

            migrationBuilder.RenameIndex(
                name: "IX_DoubleMatches_TeamMatchId",
                table: "DoubleMatch",
                newName: "IX_DoubleMatch_TeamMatchId");

            migrationBuilder.RenameIndex(
                name: "IX_DoubleMatches_HostPlayerRightId",
                table: "DoubleMatch",
                newName: "IX_DoubleMatch_HostPlayerRightId");

            migrationBuilder.RenameIndex(
                name: "IX_DoubleMatches_HostPlayerLeftId",
                table: "DoubleMatch",
                newName: "IX_DoubleMatch_HostPlayerLeftId");

            migrationBuilder.RenameIndex(
                name: "IX_DoubleMatches_GuestPlayerRightId",
                table: "DoubleMatch",
                newName: "IX_DoubleMatch_GuestPlayerRightId");

            migrationBuilder.RenameIndex(
                name: "IX_DoubleMatches_GuestPlayerLeftId",
                table: "DoubleMatch",
                newName: "IX_DoubleMatch_GuestPlayerLeftId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoubleMatch",
                table: "DoubleMatch",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoubleMatch_Players_GuestPlayerLeftId",
                table: "DoubleMatch",
                column: "GuestPlayerLeftId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoubleMatch_Players_GuestPlayerRightId",
                table: "DoubleMatch",
                column: "GuestPlayerRightId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoubleMatch_Players_HostPlayerLeftId",
                table: "DoubleMatch",
                column: "HostPlayerLeftId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoubleMatch_Players_HostPlayerRightId",
                table: "DoubleMatch",
                column: "HostPlayerRightId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoubleMatch_TeamMatches_TeamMatchId",
                table: "DoubleMatch",
                column: "TeamMatchId",
                principalTable: "TeamMatches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoubleMatchScore_DoubleMatch_DoubleMatchId",
                table: "DoubleMatchScore",
                column: "DoubleMatchId",
                principalTable: "DoubleMatch",
                principalColumn: "Id");
        }
    }
}
