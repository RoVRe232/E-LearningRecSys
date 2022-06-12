using Microsoft.EntityFrameworkCore;
using RecSysApi.Domain.Entities;
using RecSysApi.Domain.Entities.Account;
using RecSysApi.Domain.Entities.Licenses;
using RecSysApi.Domain.Entities.Orders;
using RecSysApi.Domain.Entities.Products;

namespace RecSysApi.Infrastructure.Context
{
    public class RecSysApiContext : DbContext
    {
        public RecSysApiContext(DbContextOptions<RecSysApiContext> options) : base(options)
        {

        }
        public DbSet<Video> Videos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Bundle> Bundles { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CourseLicense> CourseLicenses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Video>().ToTable("Videos");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Account>().ToTable("Accounts");
            modelBuilder.Entity<Admin>().ToTable("Admins");
            modelBuilder.Entity<Publisher>().ToTable("Publishers");
            modelBuilder.Entity<Course>().ToTable("Courses");
            modelBuilder.Entity<Bundle>().ToTable("Bundles");
            modelBuilder.Entity<Price>().ToTable("Prices");
            modelBuilder.Entity<Section>().ToTable("Sections");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<CourseLicense>().ToTable("CourseLicenses");
        }
    }
}
