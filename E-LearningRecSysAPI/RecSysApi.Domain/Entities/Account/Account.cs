using RecSysApi.Domain.Entities.Products;
using RecSysApi.Domain.Entities.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Domain.Entities.Account
{
    public class Account
    {
        public Guid AccountID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
        public virtual ICollection<Bundle> Bundles { get; set; } = new List<Bundle>();
        public virtual ICollection<AccountSignupInvitation> AccountSignupInvitations { get; set; } 
            = new List<AccountSignupInvitation>();
    }
}
