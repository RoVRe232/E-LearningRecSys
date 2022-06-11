using RecSysApi.Application.Dtos.Account;
using RecSysApi.Application.Dtos.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Dtos.Orders
{
    public class OrderDTO
    {
        public Guid OrderID { get; set; }
        public Guid AccountID { get; set; }
        public List<CourseDTO> Courses { get; set; } = new List<CourseDTO>();
        public DateTime Created { get; set; }
        public virtual AccountDTO? Account { get; set; }
        public bool Acknowladged { get; set; }

    }
}
