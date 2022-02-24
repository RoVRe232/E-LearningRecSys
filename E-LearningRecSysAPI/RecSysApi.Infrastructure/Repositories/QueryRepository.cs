using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecSysApi.Domain.Entities.Common;
using RecSysApi.Domain.Interfaces;
using RecSysApi.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Infrastructure.Repositories
{
    public class QueryRepository : Repository<Query>, IQueryRepository
    {
        private readonly ILogger<QueryRepository> _logger;
        public QueryRepository(DbContext dbContext, ILogger<QueryRepository> logger) : base(dbContext)
        {
            _logger = logger;
        }
        public QueryRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
