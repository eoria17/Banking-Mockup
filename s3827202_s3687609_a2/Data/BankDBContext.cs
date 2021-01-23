using Microsoft.EntityFrameworkCore;
using s3827202_s3687609_a2.Models;


namespace s3827202_s3687609_a2.Data
{
    public class BankDBContext : DbContext
    {
        public BankDBContext(DbContextOptions<BankDBContext> options) : base(options)
        {

        }

        public DbSet<Account> Account { get; set; }
        public DbSet<BillPay> BillPay { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<Payee> Payee { get; set; }
        public DbSet<Transaction> Transaction { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Login>().HasCheckConstraint("CH_Login_LoginID", "len(LoginID) = 8").
                HasCheckConstraint("CH_Login_PasswordHash", "len(PasswordHash) = 64");

            builder.Entity<Account>().HasCheckConstraint("CH_Account_Balance", "Balance >= 0");

            builder.Entity<Customer>().HasCheckConstraint("CH_Transaction_Quota", "FreeTransactionQuota >= 0");

            builder.Entity<Transaction>().HasOne(x => x.SourceAccount).WithMany(x => x.Transactions).HasForeignKey(x => x.AccountNumber);
            builder.Entity<Transaction>().HasCheckConstraint("CH_Transaction_Amount", "Amount > 0");
        }

    }
}
