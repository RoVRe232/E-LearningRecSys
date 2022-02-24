using Microsoft.EntityFrameworkCore;
using RecSysApi.Domain.Interfaces;
using RecSysApi.Infrastructure.Context;
using RecSysApi.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Infrastructure.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private RecSysApiContext _dbContext;

        public IVideoRepository Videos { get; }
        public IQueryRepository Queries { get; }
        public IAccountRepository Accounts { get; }
        public ICourseRepository Courses { get; }
        public IUserRepository Users { get; }
        public IAdminRepository Admins { get; }
        public ISectionRepository Sections { get; }

        public UnitOfWork(RecSysApiContext dbContext)
        {
            Videos = new VideoRepository(dbContext);
            Queries = new QueryRepository(dbContext);
            Accounts = new AccountRepository(dbContext);
            Courses = new CourseRepository(dbContext);
            Users = new UserRepository(dbContext);
            Admins = new AdminRepository(dbContext);
            Sections = new SectionRepository(dbContext);

            _dbContext = dbContext;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async void SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
