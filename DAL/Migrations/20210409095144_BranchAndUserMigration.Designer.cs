﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(MonopolyDbContext))]
    [Migration("20210409095144_BranchAndUserMigration")]
    partial class BranchAndUserMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DAL.Entities.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BranchTypeId")
                        .HasColumnType("int");

                    b.Property<int>("Buyout")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Pledge")
                        .HasColumnType("int");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int?>("RentSettingId")
                        .HasColumnType("int");

                    b.Property<int>("Upgrade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BranchTypeId");

                    b.HasIndex("RentSettingId");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("DAL.Entities.BranchType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("BranchTypes");
                });

            modelBuilder.Entity("DAL.Entities.RentSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ByBranchQuantity")
                        .HasColumnType("bit");

                    b.Property<float>("FirstCoef")
                        .HasColumnType("real");

                    b.Property<int>("SecondCoef")
                        .HasColumnType("int");

                    b.Property<int>("StartCost")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RentSettings");
                });

            modelBuilder.Entity("DAL.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Money")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Login")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DAL.Entities.Branch", b =>
                {
                    b.HasOne("DAL.Entities.BranchType", "BranchType")
                        .WithMany("Branches")
                        .HasForeignKey("BranchTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.RentSetting", "RentSetting")
                        .WithMany("Branches")
                        .HasForeignKey("RentSettingId");

                    b.Navigation("BranchType");

                    b.Navigation("RentSetting");
                });

            modelBuilder.Entity("DAL.User", b =>
                {
                    b.HasOne("DAL.User", null)
                        .WithMany("Friends")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("DAL.Entities.BranchType", b =>
                {
                    b.Navigation("Branches");
                });

            modelBuilder.Entity("DAL.Entities.RentSetting", b =>
                {
                    b.Navigation("Branches");
                });

            modelBuilder.Entity("DAL.User", b =>
                {
                    b.Navigation("Friends");
                });
#pragma warning restore 612, 618
        }
    }
}