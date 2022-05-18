﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlanningPoker.Data;

#nullable disable

namespace PlanningPoker.Data.Migrations
{
    [DbContext(typeof(PlanningPokerDbContext))]
    [Migration("20220516145236_Add-CreatedOn")]
    partial class AddCreatedOn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PlanningPoker.Models.GameRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GameRooms");
                });

            modelBuilder.Entity("PlanningPoker.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GameRoomId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameRoomId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("PlanningPoker.Models.PlayerVote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("GameRoomId")
                        .HasColumnType("int");

                    b.Property<string>("PlayerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameRoomId");

                    b.ToTable("PlayerVotes");
                });

            modelBuilder.Entity("PlanningPoker.Models.Player", b =>
                {
                    b.HasOne("PlanningPoker.Models.GameRoom", null)
                        .WithMany("Players")
                        .HasForeignKey("GameRoomId");
                });

            modelBuilder.Entity("PlanningPoker.Models.PlayerVote", b =>
                {
                    b.HasOne("PlanningPoker.Models.GameRoom", null)
                        .WithMany("Votes")
                        .HasForeignKey("GameRoomId");
                });

            modelBuilder.Entity("PlanningPoker.Models.GameRoom", b =>
                {
                    b.Navigation("Players");

                    b.Navigation("Votes");
                });
#pragma warning restore 612, 618
        }
    }
}
