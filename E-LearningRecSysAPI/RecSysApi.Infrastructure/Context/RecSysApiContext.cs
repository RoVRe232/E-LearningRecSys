using Microsoft.EntityFrameworkCore;
using RecSysApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Infrastructure.Context
{
    public class RecSysApiContext : DbContext
    {
        public RecSysApiContext(DbContextOptions<RecSysApiContext> options) : base(options) { }
        public DbSet<Video> Videos { get; set; }
    }
}
