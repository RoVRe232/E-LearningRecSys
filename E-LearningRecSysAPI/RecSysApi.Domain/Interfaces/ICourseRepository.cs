using Microsoft.EntityFrameworkCore.Query;
using RecSysApi.Domain.Entities.Account;
using RecSysApi.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Domain.Interfaces
{
    public interface ICourseRepository : IRepository<Course>
    {
        public IIncludableQueryable<Course, Account> GetCoursesWithAccountByExpressionAsync(Expression<Func<Course, bool>> expression);
    }
}
