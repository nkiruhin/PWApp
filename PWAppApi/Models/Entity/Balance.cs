using PWAppApi.DataLayer;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PWAppApi.Models.Entity
{
    public class Balance : EntityBase<long>
    {
        [Display(Name = "User")]
        public string UserId { get; set; }
        public User User { get; set; }

        [Display(Name ="Balance")]
        [Column(TypeName = "decimal(9, 2)")]
        public decimal CurrentBalance { get; set; }

        [Required]
        [Display(Name = "DateUpdate")]
        public DateTime DateUpdate{ get; set; }

        [Required]
        [Display(Name = "DateCreate")]
        public DateTime DateCreate { get; set; }
    }
}
