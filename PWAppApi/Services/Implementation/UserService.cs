using AutoMapper;
using PWAppApi.DataLayer;
using PWAppApi.DataLayer.DBContext;
using PWAppApi.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace PWAppApi.Services
{
    public class UserService : DtoService<User, string>, IUserService
    {
        public UserService(PWContext context, IMapper mapper) : base(mapper, context ) { }
    }   
}
