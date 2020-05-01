using PWAppApi.DataLayer;
using PWAppApi.Models.Enum;
using System;
using System.ComponentModel.DataAnnotations;


namespace PWAppApi.Models.Dto
{
    public class TransactionFormDto
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public decimal Amount { get; set; }
    }
}
