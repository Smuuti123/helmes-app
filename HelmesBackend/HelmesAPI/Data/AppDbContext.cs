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
        public DbSet<BagWithParcels> BagWithParcels { get; set; }
        public DbSet<BagWithLetters> BagWithLetters{ get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Parcel>().HasIndex(p => p.ParcelNumber);
            builder.Entity<Shipment>().HasIndex(s => s.ShipmentNumber);
            builder.Entity<BagWithLetters>().HasIndex(b => b.BagNumber);
            builder.Entity<BagWithParcels>().HasIndex(b => b.BagNumber);
            
        }
    }
}