﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VehicleOrganizer.Infrastructure;

#nullable disable

namespace VehicleOrganizer.Infrastructure.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20231107105610_UserWorthy")]
    partial class UserWorthy
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("VehicleOrganizer.Infrastructure.Entities.MileageHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AddDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Mileage")
                        .HasColumnType("INTEGER");

                    b.Property<int>("VehicleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("MileageHistories");
                });

            modelBuilder.Entity("VehicleOrganizer.Infrastructure.Entities.OperationalActivity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDateOperated")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastOperationDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("MileageStep")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MileageWhenPerformed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ReminderDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("VehicleId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("YearsStep")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("OperationalActivities");
                });

            modelBuilder.Entity("VehicleOrganizer.Infrastructure.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ReferenceCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("VehicleOrganizer.Infrastructure.Entities.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("InsuranceConclusion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("InsuranceTermination")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastTechnicalReview")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("NextTechnicalReview")
                        .HasColumnType("TEXT");

                    b.Property<string>("OilType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("SaleDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("VehicleType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("YearOfProduction")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("VehicleOrganizer.Infrastructure.Entities.MileageHistory", b =>
                {
                    b.HasOne("VehicleOrganizer.Infrastructure.Entities.Vehicle", "Vehicle")
                        .WithMany("MileageHistory")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("VehicleOrganizer.Infrastructure.Entities.OperationalActivity", b =>
                {
                    b.HasOne("VehicleOrganizer.Infrastructure.Entities.Vehicle", "Vehicle")
                        .WithMany("OperationalActivities")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("VehicleOrganizer.Infrastructure.Entities.Vehicle", b =>
                {
                    b.HasOne("VehicleOrganizer.Infrastructure.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("VehicleOrganizer.Infrastructure.Entities.Vehicle", b =>
                {
                    b.Navigation("MileageHistory");

                    b.Navigation("OperationalActivities");
                });
#pragma warning restore 612, 618
        }
    }
}
