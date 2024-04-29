﻿using System;
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
                    username = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isBanned = table.Column<bool>(type: "bit", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    admin_rank = table.Column<int>(type: "int", nullable: true),
                    acc_balance = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.user_ID);
                });

            migrationBuilder.CreateTable(
                name: "roulettes",
                columns: table => new
                {
                    roulette_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    difficulty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roulettes", x => x.roulette_ID);
                });

            migrationBuilder.CreateTable(
                name: "teams",
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
                    table.PrimaryKey("PK_teams", x => x.team_ID);
                });

            migrationBuilder.CreateTable(
                name: "matches",
                columns: table => new
                {
                    match_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    team1_ID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    team1team_ID = table.Column<int>(type: "int", nullable: true),
                    team2_ID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    team2team_ID = table.Column<int>(type: "int", nullable: true),
                    winnerteam_ID = table.Column<int>(type: "int", nullable: true),
                    team1_goals = table.Column<int>(type: "int", nullable: false),
                    team2_goals = table.Column<int>(type: "int", nullable: false),
                    team1_fouls = table.Column<int>(type: "int", nullable: false),
                    team2_fouls = table.Column<int>(type: "int", nullable: false),
                    team1_red_cards = table.Column<int>(type: "int", nullable: false),
                    team2_red_cards = table.Column<int>(type: "int", nullable: false),
                    team1_yellow_cards = table.Column<int>(type: "int", nullable: false),
                    team2_yellow_cards = table.Column<int>(type: "int", nullable: false),
                    team1_shots = table.Column<int>(type: "int", nullable: false),
                    team2_shots = table.Column<int>(type: "int", nullable: false),
                    team1_shots_ontarget = table.Column<int>(type: "int", nullable: false),
                    team2_shots_ontarget = table.Column<int>(type: "int", nullable: false),
                    team1_corners = table.Column<int>(type: "int", nullable: false),
                    team2_corners = table.Column<int>(type: "int", nullable: false),
                    time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matches", x => x.match_ID);
                    table.ForeignKey(
                        name: "FK_matches_teams_team1team_ID",
                        column: x => x.team1team_ID,
                        principalTable: "teams",
                        principalColumn: "team_ID");
                    table.ForeignKey(
                        name: "FK_matches_teams_team2team_ID",
                        column: x => x.team2team_ID,
                        principalTable: "teams",
                        principalColumn: "team_ID");
                    table.ForeignKey(
                        name: "FK_matches_teams_winnerteam_ID",
                        column: x => x.winnerteam_ID,
                        principalTable: "teams",
                        principalColumn: "team_ID");
                });

            migrationBuilder.CreateTable(
                name: "bets",
                columns: table => new
                {
                    bet_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_ID = table.Column<int>(type: "int", nullable: false),
                    bet_placeruser_ID = table.Column<int>(type: "int", nullable: true),
                    winnner = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    match_ID = table.Column<int>(type: "int", nullable: false),
                    match_ID1 = table.Column<int>(type: "int", nullable: true),
                    ratio = table.Column<double>(type: "float", nullable: false),
                    value = table.Column<double>(type: "float", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bets", x => x.bet_ID);
                    table.ForeignKey(
                        name: "FK_bets_accounts_bet_placeruser_ID",
                        column: x => x.bet_placeruser_ID,
                        principalTable: "accounts",
                        principalColumn: "user_ID");
                    table.ForeignKey(
                        name: "FK_bets_matches_match_ID1",
                        column: x => x.match_ID1,
                        principalTable: "matches",
                        principalColumn: "match_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_bets_bet_placeruser_ID",
                table: "bets",
                column: "bet_placeruser_ID");

            migrationBuilder.CreateIndex(
                name: "IX_bets_match_ID1",
                table: "bets",
                column: "match_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_matches_team1team_ID",
                table: "matches",
                column: "team1team_ID");

            migrationBuilder.CreateIndex(
                name: "IX_matches_team2team_ID",
                table: "matches",
                column: "team2team_ID");

            migrationBuilder.CreateIndex(
                name: "IX_matches_winnerteam_ID",
                table: "matches",
                column: "winnerteam_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bets");

            migrationBuilder.DropTable(
                name: "roulettes");

            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "matches");

            migrationBuilder.DropTable(
                name: "teams");
        }
    }
}