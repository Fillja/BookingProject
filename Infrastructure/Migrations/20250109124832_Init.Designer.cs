﻿// <auto-generated />
using System;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20250109124832_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Infrastructure.Entities.BookingEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BookerEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BookerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BookerPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SeatingId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("SeatingId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("Infrastructure.Entities.ChairEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Eggs")
                        .HasColumnType("bit");

                    b.Property<bool>("Gluten")
                        .HasColumnType("bit");

                    b.Property<bool>("Milk")
                        .HasColumnType("bit");

                    b.Property<bool>("Vegan")
                        .HasColumnType("bit");

                    b.Property<bool>("Vegetarian")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Chairs");
                });

            modelBuilder.Entity("Infrastructure.Entities.RestaurantEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("Infrastructure.Entities.SeatingEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RestaurantId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TableChairId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("TableChairId");

                    b.ToTable("Seatings");
                });

            modelBuilder.Entity("Infrastructure.Entities.TableChairEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ChairId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TableId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ChairId");

                    b.HasIndex("TableId");

                    b.ToTable("TablesChairs");
                });

            modelBuilder.Entity("Infrastructure.Entities.TableEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsBooked")
                        .HasColumnType("bit");

                    b.Property<string>("RestaurantId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("Infrastructure.Entities.BookingEntity", b =>
                {
                    b.HasOne("Infrastructure.Entities.SeatingEntity", "Seating")
                        .WithMany("Bookings")
                        .HasForeignKey("SeatingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Seating");
                });

            modelBuilder.Entity("Infrastructure.Entities.SeatingEntity", b =>
                {
                    b.HasOne("Infrastructure.Entities.RestaurantEntity", "Restaurant")
                        .WithMany("Seatings")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Entities.TableChairEntity", "TableChair")
                        .WithMany("Seatings")
                        .HasForeignKey("TableChairId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");

                    b.Navigation("TableChair");
                });

            modelBuilder.Entity("Infrastructure.Entities.TableChairEntity", b =>
                {
                    b.HasOne("Infrastructure.Entities.ChairEntity", "Chair")
                        .WithMany("TablesChairs")
                        .HasForeignKey("ChairId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Entities.TableEntity", "Table")
                        .WithMany("TablesChairs")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chair");

                    b.Navigation("Table");
                });

            modelBuilder.Entity("Infrastructure.Entities.TableEntity", b =>
                {
                    b.HasOne("Infrastructure.Entities.RestaurantEntity", "Restaurant")
                        .WithMany("Tables")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("Infrastructure.Entities.ChairEntity", b =>
                {
                    b.Navigation("TablesChairs");
                });

            modelBuilder.Entity("Infrastructure.Entities.RestaurantEntity", b =>
                {
                    b.Navigation("Seatings");

                    b.Navigation("Tables");
                });

            modelBuilder.Entity("Infrastructure.Entities.SeatingEntity", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("Infrastructure.Entities.TableChairEntity", b =>
                {
                    b.Navigation("Seatings");
                });

            modelBuilder.Entity("Infrastructure.Entities.TableEntity", b =>
                {
                    b.Navigation("TablesChairs");
                });
#pragma warning restore 612, 618
        }
    }
}
