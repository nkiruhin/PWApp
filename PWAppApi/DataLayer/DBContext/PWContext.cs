using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PWAppApi.Models.Entity;


namespace PWAppApi.DataLayer.DBContext
{
    public class PWContext : IdentityDbContext<User>
    {
        public PWContext(DbContextOptions<PWContext> options)
           : base(options)
        { }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Balance> Balances { get; set; }
    }
}
