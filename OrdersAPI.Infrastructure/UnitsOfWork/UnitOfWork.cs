using Microsoft.EntityFrameworkCore;
using OrdersAPI.Infrastructure.Context;
using RecSysApi.Domain.Entities;
using RecSysApi.Domain.Entities.Account;
using RecSysApi.Domain.Entities.Licenses;
using RecSysApi.Domain.Entities.Orders;
using RecSysApi.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAPI.Infrastructure.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private RecSysApiContext _dbContext;

        public DbSet<Video> Videos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Bundle> Bundles { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<CourseLicense> CourseLicenses { get; set; }
        public DbSet<Order> Orders { get; set; }

        public UnitOfWork(RecSysApiContext dbContext)
        {
            _dbContext = dbContext;

            Videos = _dbContext.Set<Video>();
            Users = _dbContext.Set<User>();
            Accounts = _dbContext.Set<Account>();
            Admins = _dbContext.Set<Admin>();
            Courses = _dbContext.Set<Course>();
            Sections = _dbContext.Set<Section>();
            Bundles = _dbContext.Set<Bundle>();
            Prices = _dbContext.Set<Price>();
            CourseLicenses = _dbContext.Set<CourseLicense>();
            Orders = _dbContext.Set<Order>();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
