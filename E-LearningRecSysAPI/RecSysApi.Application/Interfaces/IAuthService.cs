using RecSysApi.Application.Dtos.Account;
using RecSysApi.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Interfaces
{
    public interface IAuthService
    {
        JwtDTO GenerateToken(User user);
    }
}
