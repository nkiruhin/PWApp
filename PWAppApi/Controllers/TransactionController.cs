using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PWAppApi.Models.Dto;
using PWAppApi.Models.Entity;
using PWAppApi.Models.Enum;
using PWAppApi.Services;

namespace PWAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _service;

        public TransactionController(ITransactionService service)
        {
            _service = service;
        }

        // GET: api/Transaction/
        [HttpGet]
        public async Task<ActionResult<List<TransactionListDto>>> GetAsync()
        {
            var userId = HttpContext.User.Identity.Name;

            if (string.IsNullOrEmpty(userId))
            {
                return NotFound("user not found");
            }

            return await _service.GetDtoAllAsync<TransactionListDto>(
                x => (x.Sender.Id == userId && x.Type == TransactionType.Credit)
                || (x.RecipientId == userId && x.Type == TransactionType.Debit));
        }

        // GET: api/Transaction/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionFormDto>> GetAsync(long id) => await _service.GetDtoAsync<TransactionFormDto>(x => x.Id == id);
     

        // POST: api/Transaction/
        [HttpPost(Name = "Create")]
        public async Task<ActionResult> CreateAsync([FromForm] TransactionCreateDto dto)
        {
            var userId = HttpContext.User.Identity.Name;

            if (string.IsNullOrEmpty(userId))
            {
                return NotFound("user not found");
            }
            dto.SenderId = userId;
            var transaction = await _service.CreateTransactionAsync(dto);
            await _service.SendRefresh(dto.RecipientId);
            return CreatedAtAction("Create", new { id = transaction.Id }, transaction);
        }
    }
}