using Microsoft.EntityFrameworkCore;
using Bag_E_Commerce.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public DbSet<ShoppingCartModel> Carts { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderDetailsModel> OrderDetails { get; set; }
        public DbSet<PaymentModel> Payment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map models to table names
            modelBuilder.Entity<UserModel>().ToTable("users");
            modelBuilder.Entity<BagModel>().ToTable("bags");
            modelBuilder.Entity<CategoryModel>().ToTable("categories");
            modelBuilder.Entity<VendorModel>().ToTable("vendors");
            modelBuilder.Entity<ReviewModel>().ToTable("reviews");
            modelBuilder.Entity<ShoppingCartModel>().ToTable("shopping_carts");
            modelBuilder.Entity<OrderModel>().ToTable("orders");

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

            // Configure ShoppingCartModel with composite key
            modelBuilder.Entity<ShoppingCartModel>()
            .HasKey(cart => new { cart.CartId, cart.ProductId });

            

            modelBuilder.Entity<ShoppingCartModel>()
                .HasOne<BagModel>()
                .WithMany()
                .HasForeignKey(cart => cart.ProductId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete for ProductId

            modelBuilder.Entity<ShoppingCartModel>()
                .HasOne<UserModel>()
                .WithMany()
                .HasForeignKey(cart => cart.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete for UserId

            // Configure OrderModel relationships without navigation properties
            modelBuilder.Entity<OrderModel>()
                .HasKey(order => order.OrderId);

            modelBuilder.Entity<OrderModel>()
                .HasOne<UserModel>()
                .WithMany()  // No navigation property in UserModel
                .HasForeignKey(order => order.UserId)  // Foreign key pointing to UserId in Users table
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete for UserId

            //ORDER DETAILS

            modelBuilder.Entity<OrderDetailsModel>()
                .HasKey(od => new { od.OrderItemId, od.ProductId });

            // Foreign key relationships
            modelBuilder.Entity<OrderDetailsModel>()
                .HasOne<OrderModel>()
                .WithMany()
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetailsModel>()
                .HasOne<BagModel>()
                .WithMany()
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderModel>()
                .Property(order => order.OrderStatus)
                .HasConversion<int>(); // Stores enum as integer in the database


            base.OnModelCreating(modelBuilder);
        }
    }
}
