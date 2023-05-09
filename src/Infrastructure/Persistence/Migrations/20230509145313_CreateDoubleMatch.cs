using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanTableTennisApp.Infrastructure.Persistence.Migrations
{
    public partial class CreateDoubleMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMatches_Teams_GuestTeamId",
                table: "TeamMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMatches_Teams_HostTeamId",
                table: "TeamMatches");

            migrationBuilder.AlterColumn<int>(
                name: "HostTeamId",
                table: "TeamMatches",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GuestTeamId",
                table: "TeamMatches",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DoubleMatch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HostPlayerRightId = table.Column<int>(type: "int", nullable: false),
                    HostPlayerLeftId = table.Column<int>(type: "int", nullable: false),
                    GuestPlayerRigthId = table.Column<int>(type: "int", nullable: false),
                    GuestPlayerLeftId = table.Column<int>(type: "int", nullable: false),
                    TeamMatchId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoubleMatch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoubleMatch_Players_GuestPlayerLeftId",
                        column: x => x.GuestPlayerLeftId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoubleMatch_Players_GuestPlayerRigthId",
                        column: x => x.GuestPlayerRigthId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoubleMatch_Players_HostPlayerLeftId",
                        column: x => x.HostPlayerLeftId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoubleMatch_Players_HostPlayerRightId",
                        column: x => x.HostPlayerRightId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoubleMatch_TeamMatches_TeamMatchId",
                        column: x => x.TeamMatchId,
                        principalTable: "TeamMatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoubleMatch_GuestPlayerLeftId",
                table: "DoubleMatch",
                column: "GuestPlayerLeftId");

            migrationBuilder.CreateIndex(
                name: "IX_DoubleMatch_GuestPlayerRigthId",
                table: "DoubleMatch",
                column: "GuestPlayerRigthId");

            migrationBuilder.CreateIndex(
                name: "IX_DoubleMatch_HostPlayerLeftId",
                table: "DoubleMatch",
                column: "HostPlayerLeftId");

            migrationBuilder.CreateIndex(
                name: "IX_DoubleMatch_HostPlayerRightId",
                table: "DoubleMatch",
                column: "HostPlayerRightId");

            migrationBuilder.CreateIndex(
                name: "IX_DoubleMatch_TeamMatchId",
                table: "DoubleMatch",
                column: "TeamMatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMatches_Teams_GuestTeamId",
                table: "TeamMatches",
                column: "GuestTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMatches_Teams_HostTeamId",
                table: "TeamMatches",
                column: "HostTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMatches_Teams_GuestTeamId",
                table: "TeamMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMatches_Teams_HostTeamId",
                table: "TeamMatches");

            migrationBuilder.DropTable(
                name: "DoubleMatch");

            migrationBuilder.AlterColumn<int>(
                name: "HostTeamId",
                table: "TeamMatches",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GuestTeamId",
                table: "TeamMatches",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMatches_Teams_GuestTeamId",
                table: "TeamMatches",
                column: "GuestTeamId",
                principalTable: "Teams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMatches_Teams_HostTeamId",
                table: "TeamMatches",
                column: "HostTeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }
    }
}
