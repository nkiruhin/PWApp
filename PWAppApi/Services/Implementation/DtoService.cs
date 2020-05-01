using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PWAppApi.DataLayer;
using PWAppApi.DataLayer.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PWAppApi.Services
{
    public class DtoService<T,TKey> : GenericDataAccess<T,TKey>, IDtoServiceInterface<T,TKey> where T : class, IEntityBase<TKey>, new()
    {
        private readonly IMapper _mapper;

        public DtoService(IMapper mapper, PWContext context) : base(context) 
        {
            _mapper = mapper;        
        }

        public virtual Dto GetDto<Dto>(object id) => _mapper.Map<Dto>(GetSingle(id));

        public Dto GetDto<Dto>(Expression<Func<T, bool>> predicate) => _mapper.ProjectTo<Dto>(GetQuery().Where(predicate)).FirstOrDefault();
        
        public virtual async Task<Dto> GetDtoAsync<Dto>(object id) => _mapper.Map<Dto>(await GetSingleAsync(id));

        public virtual async Task<Dto> GetDtoAsync<Dto>(Expression<Func<T, bool>> predicate) => await _mapper.ProjectTo<Dto>(GetQuery().Where(predicate)).FirstOrDefaultAsync();

        public virtual List<Dto> GetDtoAll<Dto>() => _mapper.ProjectTo<Dto>(GetQuery()).ToList();

        public List<Dto> GetDtoAll<Dto>(Expression<Func<T, bool>> predicate) => _mapper.ProjectTo<Dto>(GetQuery().Where(predicate)).ToList();

        public virtual async Task<List<Dto>> GetDtoAllAsync<Dto>() => await _mapper.ProjectTo<Dto>(GetQuery()).ToListAsync();
        
        public virtual async Task<List<Dto>> GetDtoAllAsync<Dto>(Expression<Func<T, bool>> predicate) => await _mapper.ProjectTo<Dto>(GetQuery().Where(predicate)).ToListAsync();

        public virtual  T CreteFromDto<Dto>(Dto dto) =>  _mapper.Map<T>(dto);
        
    }
}
