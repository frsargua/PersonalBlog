using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace PersonalBlog.Data.Base
{
	public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext _context;
        public EntityBaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();
        }



        public async Task<T> GetByIdAsync(int id)
        {
            var result = await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
            return result;
        }

        public async Task<T> GetByIdAsync(int id, int skip, int take, params Expression<Func<T, object>>[] includeProperties)
        {

            var query = _context.Set<T>().Where(o => o.Id == id);
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            var result = await query.FirstOrDefaultAsync();

            //if (result != null)
            //{
            //    // Sort comments by date (or any other criteria if needed)
            //    result.Comments = result.Comments.OrderBy(c => c.Date).Skip(skip).Take(take).ToList();
            //}

            return result;
        }


        public async Task UpdateAsync(int id, T newEntity)
        {
            EntityEntry entityEntry = _context.Entry<T>(newEntity);
            entityEntry.State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}

