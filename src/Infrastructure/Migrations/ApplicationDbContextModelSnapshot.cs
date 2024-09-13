﻿// <auto-generated />
using System;
using CleanTableTennisApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CleanTableTennisApp.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.28");

            modelBuilder.Entity("CleanTableTennisApp.Domain.Entities.DoubleMatch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<int>("GuestPlayerLeftId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GuestPlayerRightId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HostPlayerLeftId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HostPlayerRightId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<int>("PlayingOrder")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TeamMatchId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GuestPlayerLeftId");

                    b.HasIndex("GuestPlayerRightId");

                    b.HasIndex("HostPlayerLeftId");

                    b.HasIndex("HostPlayerRightId");

                    b.HasIndex("TeamMatchId");

                    b.ToTable("DoubleMatches");
                });

            modelBuilder.Entity("CleanTableTennisApp.Domain.Entities.DoubleMatchScore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DoubleMatchId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GamePointsGuest")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GamePointsHost")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DoubleMatchId");

                    b.ToTable("DoubleMatchScore");
                });

            modelBuilder.Entity("CleanTableTennisApp.Domain.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TeamId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("CleanTableTennisApp.Domain.Entities.SingleMatch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<int>("GuestPlayerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HostPlayerId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<int>("PlayingOrder")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TeamMatchId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GuestPlayerId");

                    b.HasIndex("HostPlayerId");

                    b.HasIndex("TeamMatchId");

                    b.ToTable("SingleMatches");
                });

            modelBuilder.Entity("CleanTableTennisApp.Domain.Entities.SingleMatchScore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("GamePointsGuest")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GamePointsHost")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MatchId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.ToTable("SingleMatchScore");
                });

            modelBuilder.Entity("CleanTableTennisApp.Domain.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("CleanTableTennisApp.Domain.Entities.TeamMatch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FinishedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("GuestTeamId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HostTeamId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GuestTeamId");

                    b.HasIndex("HostTeamId");

                    b.ToTable("TeamMatches");
                });

            modelBuilder.Entity("CleanTableTennisApp.Domain.Entities.DoubleMatch", b =>
                {
                    b.HasOne("CleanTableTennisApp.Domain.Entities.Player", "GuestPlayerLeft")
                        .WithMany()
                        .HasForeignKey("GuestPlayerLeftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleanTableTennisApp.Domain.Entities.Player", "GuestPlayerRight")
                        .WithMany()
                        .HasForeignKey("GuestPlayerRightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleanTableTennisApp.Domain.Entities.Player", "HostPlayerLeft")
                        .WithMany()
                        .HasForeignKey("HostPlayerLeftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleanTableTennisApp.Domain.Entities.Player", "HostPlayerRight")
                        .WithMany()
                        .HasForeignKey("HostPlayerRightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleanTableTennisApp.Domain.Entities.TeamMatch", null)
                        .WithMany("DoubleMatches")
                        .HasForeignKey("TeamMatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GuestPlayerLeft");

                    b.Navigation("GuestPlayerRight");

                    b.Navigation("HostPlayerLeft");

                    b.Navigation("HostPlayerRight");
                });

            modelBuilder.Entity("CleanTableTennisApp.Domain.Entities.DoubleMatchScore", b =>
                {
                    b.HasOne("CleanTableTennisApp.Domain.Entities.DoubleMatch", "DoubleMatch")
                        .WithMany("Scores")
                        .HasForeignKey("DoubleMatchId");

                    b.Navigation("DoubleMatch");
                });

            modelBuilder.Entity("CleanTableTennisApp.Domain.Entities.Player", b =>
                {
                    b.HasOne("CleanTableTennisApp.Domain.Entities.Team", null)
                        .WithMany("Players")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CleanTableTennisApp.Domain.Entities.SingleMatch", b =>
                {
                    b.HasOne("CleanTableTennisApp.Domain.Entities.Player", "GuestPlayer")
                        .WithMany()
                        .HasForeignKey("GuestPlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleanTableTennisApp.Domain.Entities.Player", "HostPlayer")
                        .WithMany()
                        .HasForeignKey("HostPlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleanTableTennisApp.Domain.Entities.TeamMatch", "TeamMatch")
                        .WithMany("SingleMatches")
                        .HasForeignKey("TeamMatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GuestPlayer");

                    b.Navigation("HostPlayer");

                    b.Navigation("TeamMatch");
                });

            modelBuilder.Entity("CleanTableTennisApp.Domain.Entities.SingleMatchScore", b =>
                {
                    b.HasOne("CleanTableTennisApp.Domain.Entities.SingleMatch", "Match")
                        .WithMany("Scores")
                        .HasForeignKey("MatchId");

                    b.Navigation("Match");
                });

            modelBuilder.Entity("CleanTableTennisApp.Domain.Entities.TeamMatch", b =>
                {
                    b.HasOne("CleanTableTennisApp.Domain.Entities.Team", "GuestTeam")
                        .WithMany()
                        .HasForeignKey("GuestTeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleanTableTennisApp.Domain.Entities.Team", "HostTeam")
                        .WithMany()
                        .HasForeignKey("HostTeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GuestTeam");

                    b.Navigation("HostTeam");
                });

            modelBuilder.Entity("CleanTableTennisApp.Domain.Entities.DoubleMatch", b =>
                {
                    b.Navigation("Scores");
                });

            modelBuilder.Entity("CleanTableTennisApp.Domain.Entities.SingleMatch", b =>
                {
                    b.Navigation("Scores");
                });

            modelBuilder.Entity("CleanTableTennisApp.Domain.Entities.Team", b =>
                {
                    b.Navigation("Players");
                });

            modelBuilder.Entity("CleanTableTennisApp.Domain.Entities.TeamMatch", b =>
                {
                    b.Navigation("DoubleMatches");

                    b.Navigation("SingleMatches");
                });
#pragma warning restore 612, 618
        }
    }
}
