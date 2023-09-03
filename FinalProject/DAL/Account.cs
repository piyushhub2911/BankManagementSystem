using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.DAL
{
    public class Account
    {
        [Key]
        public int AccountNum { get; set; }

        public string? AccountType { get; set; }

        public double CurrBalance { get; set; }
        public DateTime DateOfCreation { get; set; }= DateTime.Now;
        [ForeignKey("CustomerId")]
        public virtual int CustomerId { get; set; }
        public virtual Customer? Customers { get; set; }

        public Account()
        {
            
        }
        public Account(int accountNo,int customerId, string accountType, double currBalance, double minBalance)
        {
            AccountNum = accountNo;
            CustomerId = customerId;
            AccountType = accountType;
            CurrBalance = currBalance;

           
        }
    }
}
