using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Domain.Entities.Account
{
    public class Publisher
    {
        public Guid PublisherID { get; set; }
        public Guid UserID { get; set; }

        public User User { get; set; }
    }
}
