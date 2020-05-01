using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PWAppApi.DataLayer.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PWAppApi.DataLayer
{
    public class GenericDataAccess<T, TKey> : IGenericDataAccess<T,TKey> where T : class, IEntityBase<TKey>, new()
    {
        private readonly PWContext _context;

        public GenericDataAccess(PWContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Base query for custom query and extention
        /// </summary>
        /// <returns></returns>
        private protected IQueryable<T> GetQuery() => _context.Set<T>().AsNoTracking();
        
        public virtual void Add(T entity) => _context.Set<T>().Add(entity);

        public virtual async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);

        public virtual List<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.AsNoTracking().Include(includeProperty);
            }
            return query.ToList();
        }
        
        public virtual async Task<List<T>> AllIncludingAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.AsNoTracking().Include(includeProperty);
            }
            return await query.ToListAsync();
        }

        public virtual void Commit() => _context.SaveChanges();
        
        public virtual async Task CommitAsync() => await _context.SaveChangesAsync();

        public virtual void Delete(T entity) => _context.Set<T>().Remove(entity);

        public virtual void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> entities = _context.Set<T>().Where(predicate);
            _context.Set<T>().RemoveRange(entities);
        }

        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate) => _context.Set<T>().Where(predicate);
        
        public virtual List<T> GetAll() => _context.Set<T>().AsNoTracking().ToList();

        public virtual async Task<List<T>> GetAllAsync() => await _context.Set<T>().AsNoTracking().ToListAsync();

        public T GetSingle(object id) => _context.Set<T>().Find(id);
        
        public async Task<T> GetSingleAsync(object id) => await _context.Set<T>().FindAsync(id);

        public virtual void Update(T entity)
        {
            var local = _context.Set<T>()
            .Local
            .FirstOrDefault(entry => entry.Id.Equals(entity.Id));

            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
            
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }
    }
}
