using PWAppApi.DataLayer;
using PWAppApi.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PWAppApi.Models.Entity
{
    public class Transaction : EntityBase<long>
    {
        //public long Id { get; set; }
        [Required]
        [Display(Name = "Date and time")]
        public DateTime Timestamp { get; set; }
        
        [Display(Name = "Sender")]
        [ForeignKey("SenderForeignKey")]
        public string SenderId { get; set; }
        public User Sender { get; set; }

        [Display(Name = "Recipient")]
        [ForeignKey("RecipientForeignKey")]
        public string RecipientId { get; set; }
        public User Recipient { get; set; }      

        [Required]
        [Display(Name = "Type")]
        public TransactionType  Type { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(9, 2)")]
        [Display(Name = "Ammount")]
        public decimal Amount { get; set; }
        
        [Required]
        [Display(Name = "Balance")]
        [Column(TypeName = "decimal(9, 2)")]
        public decimal Balance { get; set; }
        
    }
}
