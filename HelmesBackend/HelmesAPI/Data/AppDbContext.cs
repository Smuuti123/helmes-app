using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HelmesAPI.Models;


namespace HelmesAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Bag> Bag { get; set; }
        public DbSet<BagWithParcels> BagWithParcels { get; set; }
        public DbSet<BagWithLetters> BagWithLetters{ get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<Bag>()
            .HasDiscriminator<string>("BagType")
            .HasValue<BagWithParcels>("Parcels")
            .HasValue<BagWithLetters>("Letters");

            builder.Entity<Bag>()
            .HasIndex(b => b.BagNumber)
            .IsUnique();

            builder.Entity<Shipment>()
            .HasMany(s => s.Bags)
            .WithOne()
            .HasForeignKey("ShipmentId");

            builder.Entity<Parcel>().HasIndex(p => p.ParcelNumber).IsUnique();
            builder.Entity<Shipment>().HasIndex(s => s.ShipmentNumber).IsUnique();
            builder.Entity<BagWithLetters>().HasIndex(b => b.BagNumber).IsUnique();
            builder.Entity<BagWithParcels>().HasIndex(b => b.BagNumber).IsUnique();

            base.OnModelCreating(builder);
            
        }
    }
}