using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAPI.Application.Dtos.Account
{
    public class RefreshedAuthTokensDTO
    {
        public JwtDTO AuthToken { get; set; }
        public JwtDTO RefreshToken { get; set; }
    }
}
