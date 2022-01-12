using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    /// <summary>
    /// Base of CRUD Operations
    /// </summary>
    /// <typeparam name="T">Your entity</typeparam>
    public interface IEntityRepository<T>
    {
        Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null);
        Task<T> Get(Expression<Func<T, bool>> filter);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);

        Task<int> Count(Expression<Func<T, bool>> filter = null);
        Task<bool> Any(Expression<Func<T, bool>> filter);
    }
}