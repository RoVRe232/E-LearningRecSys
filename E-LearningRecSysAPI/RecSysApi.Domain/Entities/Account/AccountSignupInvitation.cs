using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Domain.Entities.Account
{
    public class AccountSignupInvitation
    {
        public Guid AccountSignupInvitationID { get; set; }
        public Guid AccountID { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Token { get; set; }
    }
}
