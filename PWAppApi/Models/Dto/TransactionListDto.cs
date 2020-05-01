using PWAppApi.Models.Enum;
using System;

namespace PWAppApi.Models.Dto
{
    public class TransactionListDto 
    {
        public long Id { get; set; }
       
        public DateTime Timestamp { get; set; }
 
        public string Sender { get; set; }

        public string Recipient { get; set; }

        public TransactionType  Type { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }
    }
}
