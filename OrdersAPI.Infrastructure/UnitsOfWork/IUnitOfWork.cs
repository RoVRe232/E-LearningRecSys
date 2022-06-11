using Microsoft.EntityFrameworkCore;
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
    public interface IUnitOfWork
    {
        DbSet<Video> Videos { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Account> Accounts { get; set; }
        DbSet<Admin> Admins { get; set; }
        DbSet<Course> Courses { get; set; }
        DbSet<Bundle> Bundles { get; set; }
        DbSet<Price> Prices { get; set; }
        DbSet<Section> Sections { get; set; }
        DbSet<CourseLicense> CourseLicenses { get; set; }
        DbSet<Order> Orders { get; set; }

        public void SaveChanges();
        public Task SaveChangesAsync();
    }
}
