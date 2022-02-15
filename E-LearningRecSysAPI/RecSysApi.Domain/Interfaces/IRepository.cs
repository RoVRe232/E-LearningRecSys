using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Domain.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        IQueryable<T> GetQuery(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(Guid Id);
        T GetById(int Id);
        T GetById(string Id);
        T Add(T entity);
        Task<T> AddAsync(T entity);
        T Update(T entity);
        T Delete(T entity);
        Task DeleteAsync(T entity);
        Task<int> SaveChangesAsync();
        void Save();
    }
}
