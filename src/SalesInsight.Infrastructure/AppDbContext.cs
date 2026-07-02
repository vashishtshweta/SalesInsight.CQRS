using Microsoft.EntityFrameworkCore;
using SalesInsight.Application.Interfaces;
using SalesInsight.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInsight.Infrastructure
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

         public DbSet<Customer> Customers { get; set; } = default!; // why to use Default here for setting null 

         public DbSet<Product> Products { get; set; } = default!;

        public DbSet<Order> Orders { get; set; } = default!;

        public DbSet<OrderItem> OrderItems { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure relationships and constraints if needed
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.CompanyName)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(c => c.ContactName)
                    .IsRequired()
                    .HasMaxLength(150);
                entity.Property(c => c.Email)
                    .IsRequired()
                    .HasMaxLength(250);
            });
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(p => p.Sku)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(p => p.UnitPrice)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");
            });
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.Property(o => o.OrderNumber)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(o => o.TotalAmount)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");
                entity.HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);
            });
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(oi => oi.Id);
                entity.Property(oi => oi.Quantity)
                    .IsRequired();
                entity.Property(oi => oi.UnitPrice)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");
                entity.Property(oi => oi.LineTotal)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");
                entity.HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId);
                entity.HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);
            });

        }


    }
}
