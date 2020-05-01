using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PWAppApi.Models.Dto;
using PWAppApi.Services;

namespace PWAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        // GET: api/User/5
        [HttpGet(Name = "GetSelectList")]
        public async Task<ActionResult<List<UserSelectListDto>>> GetAsync(string term)
        {
            var userId = HttpContext.User.Identity.Name;
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound("user not found");
            }
            return await _service.GetDtoAllAsync<UserSelectListDto>(x => x.Id != userId && (string.IsNullOrEmpty(term) || x.Email.Contains(term)));
        }

    }
}
