using PWAppApi.DataLayer;
using PWAppApi.Models.Enum;
using System;
using System.ComponentModel.DataAnnotations;


namespace PWAppApi.Models.Dto
{
    public class TransactionCreateDto
    {
        public string SenderId { get; set; }
        
        [Required]
        public string RecipientId { get; set; }

        public TransactionType  Type { get; set; }
        
        [Required]
        public decimal Amount { get; set; }
    }
}
