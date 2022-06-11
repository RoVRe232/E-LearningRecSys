using AutoMapper;
using OrdersAPI.Application.Dtos.Orders;
using OrdersAPI.Application.Interfaces;
using OrdersAPI.Infrastructure.UnitsOfWork;
using RecSysApi.Domain.Entities.Licenses;
using RecSysApi.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAPI.Infrastructure.Services
{
    public class OrdersService : IOrdersService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public OrdersService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateOrder(OrderDTO order)
        {
            var mappedOrder = _mapper.Map<Order>(order);

            foreach(var course in order.Courses)
            {
                var newLicense = new CourseLicense
                {
                    AccountID = order.AccountID,
                    CourseID = course.CourseID
                };
                await _unitOfWork.CourseLicenses.AddAsync(newLicense);
            }

            var result = await _unitOfWork.Orders.AddAsync(mappedOrder);

            await _unitOfWork.SaveChangesAsync();
            if (result.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                return true;

            return false;
        }
    }
}
