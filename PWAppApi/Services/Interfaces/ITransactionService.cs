using PWAppApi.DataLayer;
using PWAppApi.Models.Dto;
using PWAppApi.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAppApi.Services
{
    public interface ITransactionService : IDtoServiceInterface<Transaction, long>, IGenericDataAccess<Transaction, long>
    {
        /// <summary>
        /// Create debit and credit trnsaction and update balances
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<Transaction> CreateTransactionAsync(TransactionCreateDto dto);
        /// <summary>
        /// Send action to user throw SignalR
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task SendRefresh(string user);
    }
}
