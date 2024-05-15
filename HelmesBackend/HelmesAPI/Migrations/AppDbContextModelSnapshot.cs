﻿// <auto-generated />
using System;
using HelmesAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HelmesAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HelmesAPI.Models.Bag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BagNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BagType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ShipmentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BagNumber")
                        .IsUnique()
                        .HasFilter("[BagNumber] IS NOT NULL");

                    b.HasIndex("ShipmentId");

                    b.ToTable("Bag");

                    b.HasDiscriminator<string>("BagType").HasValue("Bag");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("HelmesAPI.Models.Parcel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BagWithParcelsId")
                        .HasColumnType("int");

                    b.Property<string>("DestinationCountry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParcelNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RecipientName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("BagWithParcelsId");

                    b.HasIndex("ParcelNumber")
                        .IsUnique()
                        .HasFilter("[ParcelNumber] IS NOT NULL");

                    b.ToTable("Parcels");
                });

            modelBuilder.Entity("HelmesAPI.Models.Shipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Airport")
                        .HasColumnType("int");

                    b.Property<DateTime>("FlightDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FlightNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShipmentNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ShipmentNumber")
                        .IsUnique();

                    b.ToTable("Shipments");
                });

            modelBuilder.Entity("HelmesAPI.Models.BagWithLetters", b =>
                {
                    b.HasBaseType("HelmesAPI.Models.Bag");

                    b.Property<int>("CountOfLetters")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18,2)");

                    b.HasDiscriminator().HasValue("Letters");
                });

            modelBuilder.Entity("HelmesAPI.Models.BagWithParcels", b =>
                {
                    b.HasBaseType("HelmesAPI.Models.Bag");

                    b.HasDiscriminator().HasValue("Parcels");
                });

            modelBuilder.Entity("HelmesAPI.Models.Bag", b =>
                {
                    b.HasOne("HelmesAPI.Models.Shipment", null)
                        .WithMany("Bags")
                        .HasForeignKey("ShipmentId");
                });

            modelBuilder.Entity("HelmesAPI.Models.Parcel", b =>
                {
                    b.HasOne("HelmesAPI.Models.BagWithParcels", null)
                        .WithMany("ListOfParcels")
                        .HasForeignKey("BagWithParcelsId");
                });

            modelBuilder.Entity("HelmesAPI.Models.Shipment", b =>
                {
                    b.Navigation("Bags");
                });

            modelBuilder.Entity("HelmesAPI.Models.BagWithParcels", b =>
                {
                    b.Navigation("ListOfParcels");
                });
#pragma warning restore 612, 618
        }
    }
}
