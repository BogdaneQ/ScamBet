using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScamBet.Migrations
{
    /// <inheritdoc />
    public partial class migracja1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    user_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isBanned = table.Column<bool>(type: "bit", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    admin_rank = table.Column<int>(type: "int", nullable: true),
                    acc_balace = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.user_ID);
                });

            migrationBuilder.CreateTable(
                name: "bets",
                columns: table => new
                {
                    betID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_ID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    choosen_team = table.Column<int>(type: "int", nullable: false),
                    matchID = table.Column<int>(type: "int", nullable: false),
                    ratio = table.Column<double>(type: "float", nullable: false),
                    value = table.Column<double>(type: "float", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bets", x => x.betID);
                });

            migrationBuilder.CreateTable(
                name: "roulettes",
                columns: table => new
                {
                    rouletteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    balance = table.Column<double>(type: "float", nullable: false),
                    time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roulettes", x => x.rouletteID);
                });

            migrationBuilder.CreateTable(
                name: "teams",
                columns: table => new
                {
                    teamID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    attack = table.Column<int>(type: "int", nullable: false),
                    middle = table.Column<int>(type: "int", nullable: false),
                    defence = table.Column<int>(type: "int", nullable: false),
                    aggresion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teams", x => x.teamID);
                });

            migrationBuilder.CreateTable(
                name: "teams_results",
                columns: table => new
                {
                    trID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamID = table.Column<int>(type: "int", nullable: false),
                    winner = table.Column<bool>(type: "bit", nullable: false),
                    goals = table.Column<int>(type: "int", nullable: false),
                    fouls = table.Column<int>(type: "int", nullable: false),
                    red_cards = table.Column<int>(type: "int", nullable: false),
                    yellow_cards = table.Column<int>(type: "int", nullable: false),
                    shots = table.Column<int>(type: "int", nullable: false),
                    shots_ontarget = table.Column<int>(type: "int", nullable: false),
                    corners = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teams_results", x => x.trID);
                    table.ForeignKey(
                        name: "FK_teams_results_teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "teams",
                        principalColumn: "teamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "matches",
                columns: table => new
                {
                    matchID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    trID = table.Column<int>(type: "int", nullable: false),
                    time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matches", x => x.matchID);
                    table.ForeignKey(
                        name: "FK_matches_teams_results_trID",
                        column: x => x.trID,
                        principalTable: "teams_results",
                        principalColumn: "trID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_matches_trID",
                table: "matches",
                column: "trID");

            migrationBuilder.CreateIndex(
                name: "IX_teams_results_TeamID",
                table: "teams_results",
                column: "TeamID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "bets");

            migrationBuilder.DropTable(
                name: "matches");

            migrationBuilder.DropTable(
                name: "roulettes");

            migrationBuilder.DropTable(
                name: "teams_results");

            migrationBuilder.DropTable(
                name: "teams");
        }
    }
}
