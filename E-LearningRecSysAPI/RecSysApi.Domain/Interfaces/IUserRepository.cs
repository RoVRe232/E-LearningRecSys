using RecSysApi.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetUserWithAccountByExpression(Expression<Func<User, bool>> expression);
    }
}
