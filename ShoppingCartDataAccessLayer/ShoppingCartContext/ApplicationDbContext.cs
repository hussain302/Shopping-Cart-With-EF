using Microsoft.EntityFrameworkCore;
using ShoppingCartModels.DbModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartDataAccessLayer.ShoppingCartContext
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<SetTrends> SetTrends { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.Entity<Category>()
                .HasMany(g => g.Products)
                .WithOne(p => p.Category)
                .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });

                modelBuilder.Entity<OrderProduct>()
                    .HasOne(op => op.Order)
                    .WithMany(o => o.OrderProduct)
                    .HasForeignKey(op => op.OrderId);

                modelBuilder.Entity<OrderProduct>()
                    .HasOne(op => op.Product)
                    .WithMany(p => p.OrderProduct)
                    .HasForeignKey(op => op.ProductId);


            }
            catch(Exception ex)
            {
                throw new Exception("DB LEVEL");
            }
        }
    }
}
