using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Dtos.Account
{
    public class AccountDTO
    {
        public Guid AccountID { get; set; }
        public string Name { get; set; }
        public string ProfileImageUrl { get; set; }
    }
}
