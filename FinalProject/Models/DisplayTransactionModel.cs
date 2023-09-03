using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class DisplayTransactionModel
    {
        public int TransactionId { get; set; }

        public string? TransactionType { get; set; }

        public double Amount { get; set; }

        public DateTime TransactionDate { get; set; } 

        public int AccountNum { get; set; }

        public DisplayTransactionModel()
        {

        }
        public DisplayTransactionModel(int accountNumber, string transactionType, double amount)
        {
            AccountNum = accountNumber;
            TransactionType = transactionType;
            Amount = amount;

        }
    }
}
