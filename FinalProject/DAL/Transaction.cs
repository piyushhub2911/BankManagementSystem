using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.DAL
{
    public class Transaction
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TransactionId { get; set; }

        public string? TransactionType { get; set; }

        public double Amount { get; set; }

        public DateTime TransactionDate{ get; set; }= DateTime.Now;

        public virtual int AccountNum { get; set; }

        [ForeignKey("AccountNum")]
        public virtual  Account? Accounts {get; set;}
        public Transaction()
        {
            
        }
        public Transaction(int accountNumber, string transactionType, double amount)
        {
           
            AccountNum = accountNumber;
            TransactionType = transactionType;
            Amount = amount;
            
        }
        public Transaction(int id,int accountNumber, string transactionType, double amount)
        {
            TransactionId = id;
            AccountNum = accountNumber;
            TransactionType = transactionType;
            Amount = amount;

        }
    }
}
