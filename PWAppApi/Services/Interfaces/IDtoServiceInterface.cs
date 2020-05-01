using PWAppApi.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PWAppApi.Services
{
    public interface IDtoServiceInterface<T,TKey> : IGenericDataAccess<T,TKey> where T : class, IEntityBase<TKey>, new()
    {
        /// <summary>
        /// Return DTO
        /// </summary>
        /// <typeparam name="Dto">type DTO</typeparam>
        /// <returns>List DTO object basic on config Automapper</returns>
        List<Dto> GetDtoAll<Dto>();
        List<Dto> GetDtoAll<Dto>(Expression<Func<T, bool>> predicate);
        Task<List<Dto>> GetDtoAllAsync<Dto>();
        Task<List<Dto>> GetDtoAllAsync<Dto>(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Return DTO object by Entity Id
        /// </summary>
        /// <typeparam name="Dto">type DTO</typeparam>
        /// <param name="id">Entyty Id</param>
        /// <returns>DTO object basic on config Automapper</returns>
        Dto GetDto<Dto>(object id);
        Task<Dto> GetDtoAsync<Dto>(object id);

        /// <summary>
        /// Return DTO object by predicate
        /// </summary>
        /// <typeparam name="Dto">type DTO</typeparam>
        /// <param name="predicate">Linq expression</param>
        /// <returns>DTO object basic on config Automapper</returns>
        Dto GetDto<Dto>(Expression<Func<T,bool>> predicate);
        Task<Dto> GetDtoAsync<Dto>(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Create Entity object by  DTO
        /// </summary>
        /// <param name="Dto">dto object</param>
        /// <returns>DTO object basic on config Automapper</returns>
        T CreteFromDto<Dto>(Dto dto);
    }
}
