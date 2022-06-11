using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAPI.Application.Dtos.Account
{
    public class AuthenticatedUserDTO
    {
        public Guid UserID { get; set; }
        public Guid AccountID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public JwtDTO AuthToken { get; set; }
        public JwtDTO RefreshToken { get; set; }
    }
}
