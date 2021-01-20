using Microsoft.EntityFrameworkCore;
using s3827202_s3687609_a2.Models;


namespace s3827202_s3687609_a2.Data
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {

        }

        public DbSet<Account> Account { get; set; }
    }
}
