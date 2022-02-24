using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Domain.Entities.Account
{
    public class Admin : User
    {
        public Guid AdminID { get; set; }
    }
}
