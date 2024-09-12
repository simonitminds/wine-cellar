﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WineCellar.Persistence;

#nullable disable

namespace WineCellar.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240910104359_StorageRelations")]
    partial class StorageRelations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("WineCellar.Domain.Storage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<uint>("Capacity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<double>("Temperature")
                        .HasColumnType("REAL");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Storages");
                });

            modelBuilder.Entity("WineCellar.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WineCellar.Domain.Wine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("StorageId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Description");

                    b.HasIndex("Name");

                    b.HasIndex("Quantity");

                    b.HasIndex("StorageId");

                    b.HasIndex("Type");

                    b.HasIndex("UserId");

                    b.HasIndex("Year");

                    b.ToTable("Wines");
                });

            modelBuilder.Entity("WineCellar.Domain.Storage", b =>
                {
                    b.HasOne("WineCellar.Domain.User", "User")
                        .WithMany("Storage")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WineCellar.Domain.Wine", b =>
                {
                    b.HasOne("WineCellar.Domain.Storage", "Storage")
                        .WithMany("Wines")
                        .HasForeignKey("StorageId");

                    b.HasOne("WineCellar.Domain.User", "User")
                        .WithMany("Wines")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Storage");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WineCellar.Domain.Storage", b =>
                {
                    b.Navigation("Wines");
                });

            modelBuilder.Entity("WineCellar.Domain.User", b =>
                {
                    b.Navigation("Storage");

                    b.Navigation("Wines");
                });
#pragma warning restore 612, 618
        }
    }
}
