using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PWAppApi.DataLayer
{
    public interface IGenericDataAccess<T, in TKey> where T : class, IEntityBase<TKey>, new()
    {
        /// <summary>
        /// Return list record whith included 
        /// </summary>
        /// <param name="includeProperties">Included expression</param>
        /// <returns>record list</returns>
        List<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<List<T>> AllIncludingAsync(params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Return all record
        /// </summary>
        /// <returns></returns>
        List<T> GetAll();
        Task<List<T>> GetAllAsync();
       
        /// <summary>
        /// Return record by Id (primaryKey)
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        T GetSingle(object id);
        Task<T> GetSingleAsync(object id);

        /// <summary>
        /// Find record by expression
        /// </summary>
        /// <param name="predicate">expression (Where)</param>
        /// <returns></returns>
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
    
        /// <summary>
        /// Add record 
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);
        Task AddAsync(T entity);
        
        /// <summary>
        /// Update entity record, no commit
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Delete entity, no commit
        /// </summary>
        /// <param name="entity"></param>
        
        void Delete(T entity);

        // <summary>
        /// Удаляет записи из Entity по условию, не коммитит
        /// </summary>
        /// <param name="predicate">условие (Where)</param>
        /// 
        void DeleteWhere(Expression<Func<T, bool>> predicate);
        
        /// <summary>
        /// Commit database
        /// </summary>
        void Commit();
        Task CommitAsync();
    }
}

