﻿// <auto-generated />
using System;
using AviatoDDD.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AviatoDDD.Repository.Migrations
{
    [DbContext(typeof(AviatoDbContext))]
    [Migration("20230502140515_AddCreatedAtToBooking")]
    partial class AddCreatedAtToBooking
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.3.23174.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AviatoDDD.Domain.Enums.Airplane", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BusinessClassCapacity")
                        .HasColumnType("int");

                    b.Property<int>("EconomyClassCapacity")
                        .HasColumnType("int");

                    b.Property<int>("FirstClassCapacity")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Airplanes");
                });

            modelBuilder.Entity("AviatoDDD.Domain.Models.Booking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BookingStatus")
                        .HasColumnType("int");

                    b.Property<int>("ClassType")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FlightId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PointsToUse")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("FlightId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("AviatoDDD.Domain.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CustomerType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("AviatoDDD.Domain.Models.Flight", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AirplaneId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ArrivalAirport")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("BasePrice")
                        .HasColumnType("real");

                    b.Property<DateTime>("DateAndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DepartureAirport")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AirplaneId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("AviatoDDD.Domain.Models.Booking", b =>
                {
                    b.HasOne("AviatoDDD.Domain.Models.Customer", "Customer")
                        .WithMany("Bookings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AviatoDDD.Domain.Models.Flight", "Flight")
                        .WithMany("Bookings")
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("AviatoDDD.Domain.Models.Flight", b =>
                {
                    b.HasOne("AviatoDDD.Domain.Enums.Airplane", "Airplane")
                        .WithMany()
                        .HasForeignKey("AirplaneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Airplane");
                });

            modelBuilder.Entity("AviatoDDD.Domain.Models.Customer", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("AviatoDDD.Domain.Models.Flight", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
