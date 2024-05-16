using DATA_ACCESS_LAYER.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA_ACCESS_LAYER.Data_Models
{
    public class PropertyHubDbContext : DbContext
    {

        public PropertyHubDbContext(DbContextOptions<PropertyHubDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ////Configuring Buyer Relationship
            //modelBuilder.Entity<Booking>()
            //   .HasOne(b => b.Buyer)
            //   .WithMany()
            //    .HasForeignKey(b => b.BuyerId)
            //   .IsRequired()
            //   .OnDelete(DeleteBehavior.Restrict);

            // Configure relationships
            modelBuilder.Entity<Property>()
                .HasOne(p => p.Owner)
                .WithMany(u => u.OwnedProperties)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Property)
                .WithMany(p => p.Bookings)
                .HasForeignKey(b => b.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Buyer)
                .WithMany(u => u.BookedProperties)
                .HasForeignKey(b => b.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Property)
                .WithMany(p => p.Feedbacks)
                .HasForeignKey(f => f.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Buyer)
                .WithMany(u => u.Feedbacks)
                .HasForeignKey(f => f.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Wishlist>()
                .HasOne(w => w.Buyer)
                .WithMany(u => u.WishlistItems)
                .HasForeignKey(w => w.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Wishlist>()
                .HasOne(w => w.Property)
                .WithMany(p => p.WishlistedBy)
                .HasForeignKey(w => w.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //  base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=LTIN228415;Initial Catalog=PropHubdb;Integrated Security=True;TrustServerCertificate=True", b => b.MigrationsAssembly("DATA_ACCESS_LAYER"));
        }
    }
}
