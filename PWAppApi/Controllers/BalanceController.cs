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
    public class BalanceController : ControllerBase
    {
        private readonly IBalanceService _service;

        public BalanceController(IBalanceService service)
        {
            _service = service;
        }

        // GET: api/Balance/
        [HttpGet]
        public async Task<ActionResult<BalanceDto>> GetAsync()
        {
            var userId = HttpContext.User.Identity.Name;
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound("user not found");
            }
            return await _service.GetDtoAsync<BalanceDto>(x => x.UserId == userId);
        }
    }
}