namespace FinalProject.Models
{
    public class AccountModel
    {
        public int CustomerId { get; set; }

        public string AccountType { get; set; }

        public double InitialBalance { get; set; }

     

        public AccountModel()
        {
            
        }

       
        public AccountModel(int customerId,string accountType, double initialBalance, double minBalance)
        {
            CustomerId = customerId;
            AccountType = accountType;
            InitialBalance = initialBalance;
           
        }
    }
}
