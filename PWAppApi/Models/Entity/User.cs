using Microsoft.AspNetCore.Identity;
using PWAppApi.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAppApi.Models.Entity
{
    public class User : IdentityUser, IEntityBase<string>
    {
        public string FullUserName { get; set; }
    }
}
