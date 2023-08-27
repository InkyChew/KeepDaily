﻿// <auto-generated />
using System;
using DomainLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DomainLayer.Migrations
{
    [DbContext(typeof(KeepDailyContext))]
    [Migration("20230826114254_update_category2")]
    partial class update_category2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DomainLayer.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("DomainLayer.Models.Day", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Date")
                        .HasColumnType("int");

                    b.Property<string>("ImgName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ImgType")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<int>("PlanId")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlanId");

                    b.ToTable("Day");
                });

            modelBuilder.Entity("DomainLayer.Models.Friend", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("FriendId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "FriendId");

                    b.ToTable("Friend");
                });

            modelBuilder.Entity("DomainLayer.Models.Plan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("StartFrom")
                        .IsRequired()
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Plan");
                });

            modelBuilder.Entity("DomainLayer.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("EmailNotify")
                        .HasColumnType("bit");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LineAccessToken")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LineId")
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LineNotify")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserLevel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("LineId")
                        .IsUnique()
                        .HasFilter("[LineId] IS NOT NULL");

                    b.ToTable("AppUser");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "a@a",
                            EmailConfirmed = false,
                            EmailNotify = true,
                            IsActive = false,
                            LineNotify = false,
                            Name = "Inky",
                            Password = "AQAAAAEAACcQAAAAEHAKdeCaobahtanlK2qe0Pcu6GOAdwkcTcr/ZBeyKozN+Okzqofk+sXcvwkgh/TRNQ==",
                            UserLevel = 1
                        });
                });

            modelBuilder.Entity("DomainLayer.Models.Day", b =>
                {
                    b.HasOne("DomainLayer.Models.Plan", null)
                        .WithMany("Days")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DomainLayer.Models.Plan", b =>
                {
                    b.HasOne("DomainLayer.Models.Category", "Category")
                        .WithMany("Plans")
                        .HasForeignKey("CategoryId");

                    b.HasOne("DomainLayer.Models.User", "User")
                        .WithMany("Plans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DomainLayer.Models.Category", b =>
                {
                    b.Navigation("Plans");
                });

            modelBuilder.Entity("DomainLayer.Models.Plan", b =>
                {
                    b.Navigation("Days");
                });

            modelBuilder.Entity("DomainLayer.Models.User", b =>
                {
                    b.Navigation("Plans");
                });
#pragma warning restore 612, 618
        }
    }
}
