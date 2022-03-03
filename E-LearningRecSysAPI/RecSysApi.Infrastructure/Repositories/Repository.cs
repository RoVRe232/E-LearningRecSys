using Microsoft.EntityFrameworkCore;
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
        protected readonly DbContext dbContext;
        protected readonly DbSet<T> dbSet;

        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = this.dbContext.Set<T>();
        }

        public IQueryable<T> GetQuery(Expression<Func<T, bool>> expression)
        {
            return dbSet.Where(expression);
        }

        public T Add(T itemToAdd)
        {
            var entity = dbSet.Add(itemToAdd);
            return entity.Entity;
        }

        public T GetById(Guid Id)
        {
            return dbSet.Find(Id);
        }

        public T GetById(int Id)
        {
            return dbSet.Find(Id);
        }

        public T GetById(string Id)
        {
            return dbSet.Find(Id);
        }

        public T Delete(T itemToDelete)
        {
            return dbSet.Remove(itemToDelete).Entity;
        }

        public ICollection<T> GetAll()
        {
            return dbSet.AsEnumerable().ToList();
        }

        public T Update(T itemToUpdate)
        {
            var entity = dbSet.Update(itemToUpdate);
            dbContext.SaveChanges();
            return entity.Entity;
        }

        public async Task<T> GetByIdAsync(Guid Id)
        {
            return await dbSet.FindAsync(Id);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await dbSet.AsQueryable().FirstOrDefaultAsync(expression);
        }

        public async Task<T> FirstAsync(Expression<Func<T, bool>> expression)
        {
            return await dbSet.AsQueryable().FirstAsync(expression);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await dbSet.AsQueryable().FirstOrDefaultAsync(expression);
        }

        public async Task<T> AddAsync(T entity)
        {
            return (await dbSet.AddAsync(entity)).Entity;
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
