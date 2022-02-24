using Microsoft.EntityFrameworkCore;
using RecSysApi.Domain.Entities.Products;
using RecSysApi.Domain.Interfaces;
using RecSysApi.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Infrastructure.Repositories
{
    public class SectionRepository : Repository<Section>, ISectionRepository
    {
        public SectionRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
