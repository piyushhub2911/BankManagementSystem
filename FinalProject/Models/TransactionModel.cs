namespace FinalProject.Models
{
    public class TransactionModel
    {
        

        public int AccountNum { get; set; }

        public string TransactionType { get; set; }

        public double Amount { get; set; }

        public DateTime TransactionDate { get; set; } 

        public TransactionModel()
        {
            
        }

        public TransactionModel(int accountNumber,string transactionType,double amount)
        {
            AccountNum = accountNumber;
            TransactionType = transactionType.ToLower();
            Amount = amount;
            TransactionDate = DateTime.Now;
        }
    }
}
