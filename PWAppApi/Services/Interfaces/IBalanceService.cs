using PWAppApi.DataLayer;
using PWAppApi.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAppApi.Services
{
    public interface IBalanceService : IDtoServiceInterface<Balance, long>, IGenericDataAccess<Balance, long>
    {

    }
}
