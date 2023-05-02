using System;
using System.Linq.Expressions;

namespace PersonalBlog.Data.Base
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(params Expression<Func<T, Object>>[] includeProperties);
        Task<T> GetByIdAsync(int id);
        void Add(T entity);
        Task DeleteAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(int id, T newEntity);
    }
}