using RecSysApi.Domain.Interfaces;
using RecSysApi.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        protected readonly RecSysApiContext dbContext;

        public Repository(RecSysApiContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<T> GetQuery(Expression<Func<T, bool>> expression)
        {
            return dbContext.Set<T>().Where(expression);
        }

        public T Add(T itemToAdd)
        {
            var entity = dbContext.Add<T>(itemToAdd);
            dbContext.SaveChanges();
            return entity.Entity;
        }

        public T GetById(Guid Id)
        {
            return dbContext.Set<T>()
                            .Find(Id);
        }

        public T GetById(int Id)
        {
            return dbContext.Set<T>()
                .Find(Id);
        }

        public T GetById(string Id)
        {
            return dbContext.Set<T>()
                .Find(Id);
        }

        public bool Delete(T itemToDelete)
        {
            dbContext.Remove<T>(itemToDelete);
            dbContext.SaveChanges();
            return true;

        }

        public IEnumerable<T> GetAll()
        {
            return dbContext.Set<T>().AsEnumerable();
        }

        public T Update(T itemToUpdate)
        {
            var entity = dbContext.Update<T>(itemToUpdate);
            dbContext.SaveChanges();
            return entity.Entity;
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public Task<T> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<T> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        T IRepository<T>.Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

    }
}
