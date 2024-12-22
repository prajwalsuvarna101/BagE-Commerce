using Bag_E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Bag_E_Commerce.Data
{
    public class BagDbContext : DbContext
    {
        public BagDbContext(DbContextOptions<BagDbContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<BagModel> Bags { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<VendorModel> Vendors { get; set; }
        public DbSet<ReviewModel> Reviews { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderDetailsModel> OrderDetails { get; set; }
        public DbSet<ShoppingCartModel> ShoppingCart { get; set; }
        public DbSet<PaymentModel> Payments { get; set; }
        public DbSet<InventoryLogModel> InventoryLogs { get; set; }
        public DbSet<ShippingModel> Shippings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map models to table names
            modelBuilder.Entity<UserModel>().ToTable("users");
            modelBuilder.Entity<BagModel>().ToTable("bags");
            modelBuilder.Entity<CategoryModel>().ToTable("categories");
            modelBuilder.Entity<VendorModel>().ToTable("vendors");
            modelBuilder.Entity<ReviewModel>().ToTable("reviews");
            modelBuilder.Entity<OrderModel>().ToTable("orders");
            modelBuilder.Entity<OrderDetailsModel>().ToTable("order_details");
            modelBuilder.Entity<ShoppingCartModel>().ToTable("shopping_cart");
            modelBuilder.Entity<PaymentModel>().ToTable("payments");
            modelBuilder.Entity<InventoryLogModel>().ToTable("inventory_log");
            modelBuilder.Entity<ShippingModel>().ToTable("shipping");

            // Configure UserModel
            modelBuilder.Entity<UserModel>()
                .HasIndex(u => u.email)
                .IsUnique();

            // Configure BagModel relationships
            modelBuilder.Entity<BagModel>()
                .HasOne<CategoryModel>()
                .WithMany()
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BagModel>()
                .HasOne<VendorModel>()
                .WithMany()
                .HasForeignKey(b => b.VendorId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure ReviewModel relationships
            modelBuilder.Entity<ReviewModel>()
                .HasOne<BagModel>()
                .WithMany()
                .HasForeignKey(r => r.BagId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReviewModel>()
                .HasOne<UserModel>()
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure OrderDetailsModel relationships
            modelBuilder.Entity<OrderDetailsModel>()
                .HasKey(od => new { od.OrderId, od.BagId });

            modelBuilder.Entity<OrderDetailsModel>()
                .HasOne<OrderModel>()
                .WithMany()
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetailsModel>()
                .HasOne<BagModel>()
                .WithMany()
                .HasForeignKey(od => od.BagId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure ShoppingCartModel relationships
            modelBuilder.Entity<ShoppingCartModel>()
                .HasKey(sc => new { sc.CartId, sc.BagId });

            modelBuilder.Entity<ShoppingCartModel>()
                .HasOne<UserModel>()
                .WithMany()
                .HasForeignKey(sc => sc.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShoppingCartModel>()
                .HasOne<BagModel>()
                .WithMany()
                .HasForeignKey(sc => sc.BagId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure PaymentModel relationships
            modelBuilder.Entity<PaymentModel>()
                .HasOne<OrderModel>()
                .WithMany()
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure ShippingModel relationships
            modelBuilder.Entity<ShippingModel>()
                .HasOne<OrderModel>()
                .WithMany()
                .HasForeignKey(s => s.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
