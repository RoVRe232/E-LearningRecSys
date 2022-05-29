using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RecSysApi.Domain.Entities.Account;
using RecSysApi.Domain.Entities.Products;
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
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(DbContext dbContext) : base(dbContext)
        {

        }

        public IIncludableQueryable<Course, Account> GetCoursesWithAccountByExpressionAsync(Expression<Func<Course, bool>> expression)
        {
            return dbContext.Set<Course>()
                .Where(expression)
                .Include(e => e.Price)
                .Include(e => e.Account);
        }
    }
}
