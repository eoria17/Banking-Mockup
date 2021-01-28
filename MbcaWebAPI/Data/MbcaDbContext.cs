using Microsoft.EntityFrameworkCore;
using MbcaWebAPI.Models;

namespace MbcaWebAPI.Data
{
    public class MbcaDbContext : DbContext
    {
        public MbcaDbContext(DbContextOptions<MbcaDbContext> options) : base(options)
        {

        }

        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<BillPay> BillPay { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<Payee> Payee { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Login>().HasCheckConstraint("CH_Login_LoginID", "len(LoginID) = 8").
                HasCheckConstraint("CH_Login_PasswordHash", "len(PasswordHash) = 64");

            builder.Entity<Account>().HasCheckConstraint("CH_Account_Balance", "Balance >= 0").
                HasCheckConstraint("CH_Transaction_Quota", "FreeTransaction >= 0"); ;

            builder.Entity<Transaction>().HasOne(x => x.SourceAccount).WithMany(x => x.Transactions).HasForeignKey(x => x.AccountNumber);
            builder.Entity<Transaction>().HasCheckConstraint("CH_Transaction_Amount", "Amount > 0");
        }
    }
}
