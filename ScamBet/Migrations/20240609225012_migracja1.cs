using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ScamBet.Migrations
{
    /// <inheritdoc />
    public partial class migracja1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coinflip",
                columns: table => new
                {
                    bet_ID_cf = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_ID = table.Column<int>(type: "int", nullable: false),
                    BetAmount_cf = table.Column<double>(type: "float", nullable: false),
                    Choice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BetTime_cf = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsWin_cf = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coinflip", x => x.bet_ID_cf);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    role_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.role_ID);
                });

            migrationBuilder.CreateTable(
                name: "Roulette",
                columns: table => new
                {
                    bet_ID_r = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_ID = table.Column<int>(type: "int", nullable: false),
                    betType_r = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    betValue_r = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    betAmount_r = table.Column<double>(type: "float", nullable: false),
                    betTime_r = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isWin_r = table.Column<bool>(type: "bit", nullable: false),
                    result_r = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roulette", x => x.bet_ID_r);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    team_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    wins = table.Column<int>(type: "int", nullable: false),
                    draws = table.Column<int>(type: "int", nullable: false),
                    loses = table.Column<int>(type: "int", nullable: false),
                    points = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.team_ID);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    user_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_ID = table.Column<int>(type: "int", nullable: false),
                    username = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isBanned = table.Column<bool>(type: "bit", nullable: false),
                    acc_balance = table.Column<double>(type: "float", nullable: false),
                    AvatarPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalWinnings = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.user_ID);
                    table.ForeignKey(
                        name: "FK_Accounts_Roles_role_ID",
                        column: x => x.role_ID,
                        principalTable: "Roles",
                        principalColumn: "role_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    match_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    team1_ID = table.Column<int>(type: "int", nullable: false),
                    Team1team_ID = table.Column<int>(type: "int", nullable: true),
                    team2_ID = table.Column<int>(type: "int", nullable: false),
                    Team2team_ID = table.Column<int>(type: "int", nullable: true),
                    team1_goals = table.Column<int>(type: "int", nullable: false),
                    team2_goals = table.Column<int>(type: "int", nullable: false),
                    time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    winner_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.match_ID);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_Team1team_ID",
                        column: x => x.Team1team_ID,
                        principalTable: "Teams",
                        principalColumn: "team_ID");
                    table.ForeignKey(
                        name: "FK_Matches_Teams_Team2team_ID",
                        column: x => x.Team2team_ID,
                        principalTable: "Teams",
                        principalColumn: "team_ID");
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "user_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bets",
                columns: table => new
                {
                    bet_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_ID = table.Column<int>(type: "int", nullable: false),
                    bet_placeruser_ID = table.Column<int>(type: "int", nullable: true),
                    succes = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    match_ID = table.Column<int>(type: "int", nullable: false),
                    match_ID1 = table.Column<int>(type: "int", nullable: true),
                    ratio = table.Column<double>(type: "float", nullable: false),
                    value = table.Column<double>(type: "float", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bets", x => x.bet_ID);
                    table.ForeignKey(
                        name: "FK_Bets_Accounts_bet_placeruser_ID",
                        column: x => x.bet_placeruser_ID,
                        principalTable: "Accounts",
                        principalColumn: "user_ID");
                    table.ForeignKey(
                        name: "FK_Bets_Matches_match_ID1",
                        column: x => x.match_ID1,
                        principalTable: "Matches",
                        principalColumn: "match_ID");
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "role_ID", "RoleName" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_role_ID",
                table: "Accounts",
                column: "role_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_bet_placeruser_ID",
                table: "Bets",
                column: "bet_placeruser_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_match_ID1",
                table: "Bets",
                column: "match_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Team1team_ID",
                table: "Matches",
                column: "Team1team_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Team2team_ID",
                table: "Matches",
                column: "Team2team_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bets");

            migrationBuilder.DropTable(
                name: "Coinflip");

            migrationBuilder.DropTable(
                name: "Roulette");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
