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
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map models to table names
            modelBuilder.Entity<UserModel>().ToTable("users");
            modelBuilder.Entity<BagModel>().ToTable("bags");
            modelBuilder.Entity<CategoryModel>().ToTable("categories");
            modelBuilder.Entity<VendorModel>().ToTable("vendors");
            

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


            base.OnModelCreating(modelBuilder);
        }
    }
}
