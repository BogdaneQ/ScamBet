﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScamBet.Entities;

#nullable disable

namespace ScamBet.Migrations
{
    [DbContext(typeof(BookmacherDBContext))]
    partial class BookmacherDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ScamBet.Entities.Account", b =>
                {
                    b.Property<int>("user_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("user_ID"));

                    b.Property<double>("acc_balance")
                        .HasColumnType("float");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isBanned")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone_number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("role_ID")
                        .HasColumnType("int");

                    b.Property<string>("surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("user_ID");

                    b.HasIndex("role_ID");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("ScamBet.Entities.Bet", b =>
                {
                    b.Property<int>("bet_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("bet_ID"));

                    b.Property<bool>("active")
                        .HasColumnType("bit");

                    b.Property<int?>("bet_placeruser_ID")
                        .HasColumnType("int");

                    b.Property<int>("match_ID")
                        .HasColumnType("int");

                    b.Property<int?>("match_ID1")
                        .HasColumnType("int");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.Property<double>("ratio")
                        .HasColumnType("float");

                    b.Property<string>("succes")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("user_ID")
                        .HasColumnType("int");

                    b.Property<double>("value")
                        .HasColumnType("float");

                    b.HasKey("bet_ID");

                    b.HasIndex("bet_placeruser_ID");

                    b.HasIndex("match_ID1");

                    b.ToTable("Bets");
                });

            modelBuilder.Entity("ScamBet.Entities.Match", b =>
                {
                    b.Property<int>("match_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("match_ID"));

                    b.Property<int?>("Team1team_ID")
                        .HasColumnType("int");

                    b.Property<int?>("Team2team_ID")
                        .HasColumnType("int");

                    b.Property<int>("team1_ID")
                        .HasColumnType("int");

                    b.Property<int>("team1_goals")
                        .HasColumnType("int");

                    b.Property<int>("team2_ID")
                        .HasColumnType("int");

                    b.Property<int>("team2_goals")
                        .HasColumnType("int");

                    b.Property<DateTime>("time")
                        .HasColumnType("datetime2");

                    b.Property<int>("winner_ID")
                        .HasColumnType("int");

                    b.HasKey("match_ID");

                    b.HasIndex("Team1team_ID");

                    b.HasIndex("Team2team_ID");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("ScamBet.Entities.Roulette", b =>
                {
                    b.Property<int>("roulette_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("roulette_ID"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("roulette_ID");

                    b.ToTable("Roulettes");
                });

            modelBuilder.Entity("ScamBet.Entities.Team", b =>
                {
                    b.Property<int>("team_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("team_ID"));

                    b.Property<int>("draws")
                        .HasColumnType("int");

                    b.Property<int>("loses")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("points")
                        .HasColumnType("int");

                    b.Property<int>("wins")
                        .HasColumnType("int");

                    b.HasKey("team_ID");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("ScamBet.Models.Role", b =>
                {
                    b.Property<int>("role_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("role_ID"));

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("role_ID");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            role_ID = 2,
                            RoleName = "Admin"
                        },
                        new
                        {
                            role_ID = 1,
                            RoleName = "User"
                        });
                });

            modelBuilder.Entity("ScamBet.Entities.Account", b =>
                {
                    b.HasOne("ScamBet.Models.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("role_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ScamBet.Entities.Bet", b =>
                {
                    b.HasOne("ScamBet.Entities.Account", "bet_placer")
                        .WithMany()
                        .HasForeignKey("bet_placeruser_ID");

                    b.HasOne("ScamBet.Entities.Match", "match")
                        .WithMany()
                        .HasForeignKey("match_ID1");

                    b.Navigation("bet_placer");

                    b.Navigation("match");
                });

            modelBuilder.Entity("ScamBet.Entities.Match", b =>
                {
                    b.HasOne("ScamBet.Entities.Team", "Team1")
                        .WithMany()
                        .HasForeignKey("Team1team_ID");

                    b.HasOne("ScamBet.Entities.Team", "Team2")
                        .WithMany()
                        .HasForeignKey("Team2team_ID");

                    b.Navigation("Team1");

                    b.Navigation("Team2");
                });

            modelBuilder.Entity("ScamBet.Models.Role", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
