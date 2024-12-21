
using Microsoft.EntityFrameworkCore;
using Bag_E_Commerce.Models;
using System.Data;

namespace Bag_E_Commerce.Data
{
    public class BagDbContext : DbContext
    {
        public BagDbContext(DbContextOptions<BagDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<BagModel> Bags { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().ToTable("users");
            modelBuilder.Entity<BagModel>().ToTable("bags");

        }
  
    }
}

