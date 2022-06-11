using RecSysApi.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Domain.Entities.Orders
{
    public class Order
    {
        public Guid OrderID { get; set; }
        public Guid? AccountID { get; set; }
        public DateTime Created { get; set; }
        public bool Acknowladged { get; set; }
        public virtual Account.Account Account { get; set; }
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
