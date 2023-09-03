using FinalProject.DAL;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class DisplayAccountModel
    {
       
        public int AccountNum { get; set; }

        public string? AccountType { get; set; }

        public double CurrBalance { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;

        public int CustomerId { get; set; }


        public DisplayAccountModel()
        {

        }
        public DisplayAccountModel(int accountNo, int customerId, string accountType, double currBalance, double minBalance)
        {
            AccountNum = accountNo;
            CustomerId = customerId;
            AccountType = accountType;
            CurrBalance = currBalance;


        }
    }
}