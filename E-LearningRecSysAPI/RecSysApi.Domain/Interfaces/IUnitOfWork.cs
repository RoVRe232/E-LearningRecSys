﻿using RecSysApi.Domain.Entities.Licenses;
using RecSysApi.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        public IVideoRepository Videos { get; }
        public IQueryRepository Queries{ get; }
        public IAccountRepository Accounts { get; }
        public ICourseRepository Courses { get; }
        public IUserRepository Users { get; }
        public ISectionRepository Sections { get; }
        public IRepository<Order> Orders { get; }
        public IRepository<CourseLicense> CourseLicenses { get; }

        public void SaveChanges();
        public Task SaveChangesAsync();
        
    }
}
