using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Dtos.Account
{
    public class RefreshedAuthTokensDTO
    {
        public JwtDTO AuthToken { get; set; }
        public JwtDTO RefreshToken { get; set; }
    }
}
