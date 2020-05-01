using Microsoft.AspNetCore.Identity;
using PWAppApi.DataLayer;
using PWAppApi.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAppApi.Services.Auth
{

    public interface IAuthService: IDtoServiceInterface<User, string>, IGenericDataAccess<User, string>
    {
        Task<AuthData> GetAuthDataAsync(string username);
        Task<SignInResult> LoginAsync(string username, string password);
        Task LogoutAsync();
        Task<IdentityResult> SingUpAsync(User user, string password);
        Task CreateBalance(User user);
    }
}
