using OrdersAPI.Application.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAPI.Application.Interfaces
{
    public interface IOrdersService
    {
        public Task<bool> CreateOrder(OrderDTO order);
    }
}
