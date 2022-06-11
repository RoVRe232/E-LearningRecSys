using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAPI.Application.Dtos.Courses
{
    public class PriceDTO
    {
        public double Amount { get; set; }
        public string Currency { get; set; }
    }
}
