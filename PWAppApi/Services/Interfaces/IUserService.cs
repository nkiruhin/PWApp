using PWAppApi.DataLayer;
using PWAppApi.Models.Dto;
using PWAppApi.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAppApi.Services
{
    public interface IUserService : IDtoServiceInterface<User, string>, IGenericDataAccess<User, string>
    {

    }
}
