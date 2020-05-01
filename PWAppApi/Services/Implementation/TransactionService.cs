using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using PWAppApi.DataLayer;
using PWAppApi.DataLayer.DBContext;
using PWAppApi.Models.Dto;
using PWAppApi.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace PWAppApi.Services
{
    public class TransactionService : DtoService<Transaction, long>, ITransactionService
    {
        private readonly IBalanceService _balanceService;
        private readonly IHubContext<SignalrHub> _hubContext;

        public TransactionService(PWContext context, IMapper mapper, IBalanceService balanceService, IHubContext<SignalrHub> hubContext)
            : base(mapper, context ) 
        {
            _balanceService = balanceService;
            _hubContext = hubContext;
        }
        
        public async Task<Transaction> CreateTransactionAsync(TransactionCreateDto dto)
        {
            var balances = GetCurrentBalances(dto.SenderId, dto.RecipientId);
           
            var senderBalance = balances.Where(x => x.UserId == dto.SenderId).FirstOrDefault();
            if (senderBalance.CurrentBalance < dto.Amount)
            {
                throw new Exception("Cancel! Sum of transaction is greater of balance");
            }
            senderBalance.DateUpdate = DateTime.Now;
            senderBalance.CurrentBalance = senderBalance.CurrentBalance - dto.Amount;

            var recipientBalance = balances.Where(x => x.UserId == dto.RecipientId).FirstOrDefault();
            recipientBalance.DateUpdate = DateTime.Now;
            recipientBalance.CurrentBalance = recipientBalance.CurrentBalance + dto.Amount;
            
            var outcoming = CreteFromDto(dto);
            outcoming.Type = Models.Enum.TransactionType.Credit;
            outcoming.Balance = senderBalance.CurrentBalance;

            var incomming = CreteFromDto(dto);
            incomming.Type = Models.Enum.TransactionType.Debit;
            incomming.Balance = recipientBalance.CurrentBalance;
            
            _balanceService.Update(recipientBalance);
            _balanceService.Update(senderBalance);
            await AddAsync(incomming);
            await AddAsync(outcoming);
            await CommitAsync();
            
            return outcoming;
        }

        public async Task SendRefresh(string user)
        {
            await _hubContext.Clients.User(user).SendAsync("Refresh", false);
        }

        private  IEnumerable<Balance> GetCurrentBalances(string senderId, string recipientId)
        {
            return _balanceService.FindBy(x => x.UserId == recipientId || x.UserId == senderId);
        }
    }   
}
