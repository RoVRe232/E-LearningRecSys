﻿using Microsoft.EntityFrameworkCore;
using RecSysApi.Domain.Entities.Account;
using RecSysApi.Domain.Interfaces;
using RecSysApi.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {

        }

        public async Task<User> GetUserWithAccountByExpressionAsync(Expression<Func<User, bool>> expression)
        {
            return await dbContext.Set<User>()
                .Where(expression)
                .Include(e => e.Account)
                .FirstAsync();
        }

        public async Task<User> GetUserWithTokensFamilyAsync(Expression<Func<User, bool>> expression)
        {
            return await dbContext.Set<User>()
                .Include(e => e.ActiveRefreshToken)
                .Include(e => e.UsedRefreshTokensFamily)
                .Where(expression).Include(e => e.Account)
                .FirstAsync();
        }
    }
}
