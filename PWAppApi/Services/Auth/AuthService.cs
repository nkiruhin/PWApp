using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PWAppApi.DataLayer.DBContext;
using PWAppApi.Models.Dto;
using PWAppApi.Models.Entity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PWAppApi.Services.Auth
{
    public class AuthService : DtoService<User, string>, IAuthService
    {
        private const string _jwtSecret = "U2FsYXJ5Q2FsY3VsYXRvclNlY3JldEtleQ==";
        private const int _jwtLifespan = 2592000;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IBalanceService _balanceService;

        public AuthService(SignInManager<User> signInManager,
                           UserManager<User> userManager,
                           IBalanceService balanceService,
                           PWContext context,
                           IMapper mapper) : base (mapper, context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _balanceService = balanceService;
        }

        /// <summary>
        /// Check present user account
        /// </summary>
        /// <param name="id"></param>
        /// <param name="AccountId">return AccountId if present</param>
        /// <returns></returns>

        private async Task<bool> HasAccount(string email) => await _userManager.FindByEmailAsync(email) != null;
        public async Task<AuthData> GetAuthDataAsync(string username)
        {
            User user = await _userManager.FindByNameAsync(username);
            
            var expirationTime = DateTime.UtcNow.AddSeconds(_jwtLifespan);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
            }),

                Expires = expirationTime,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret)),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            return new AuthData
            {
                Token = token,
                TokenExpirationTime = ((DateTimeOffset)expirationTime).ToUnixTimeSeconds(),
                Name = user.FullUserName
            };
        }
        public async Task<SignInResult> LoginAsync(string username, string password) => await _signInManager.PasswordSignInAsync(username, password, true, true);
        public async Task LogoutAsync() => await _signInManager.SignOutAsync();
        public async Task<IdentityResult> SingUpAsync(User user, string password)
        {
            if (HasAccount(user.Email).Result)
            {
                throw new Exception("Registration canceled! Your email address is already registered.");
            }
            return await _userManager.CreateAsync(user, password);
        }
        public async Task CreateBalance(User user)
        {
            var balance = new Balance
            {
                CurrentBalance = 500,
                User = user,
                DateCreate = DateTime.Now,
                DateUpdate = DateTime.Now
            };
            await _balanceService.AddAsync(balance);
            await _balanceService.CommitAsync();
        }
    }
}
