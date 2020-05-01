using PWAppApi.DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PWAppApi.Models.Dto
{
    public class BalanceDto
    {
        public string UserName { get; set; }
        
        public string UserEmail { get; set; }

        public string UserId { get; set; }
        
        public decimal CurrentBalance { get; set; }

        public DateTime DateUpdate{ get; set; }

        public DateTime DateCreate { get; set; }
    }
}
