using Microsoft.EntityFrameworkCore;

namespace FinalProject.DAL
{
    public class BankContext:DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options) 
        { 
        
        }

        public DbSet<Customer> BankCustomers { get; set; }

        public DbSet<Account> BankAccounts { get; set; }

        public DbSet<Transaction> BankTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>().HasKey(nameof(Transaction.TransactionId),nameof(Transaction.AccountNum ));
        }
    }
}
