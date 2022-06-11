using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAPI.Application.Dtos.Account
{
    public class JwtDTO
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
