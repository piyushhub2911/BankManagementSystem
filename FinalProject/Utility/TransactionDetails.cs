

using FinalProject.DAL;

namespace FinalProject.Utility
{
    public class TransactionDetails
    {
        public List<Transaction> Transactions { get; set; }

        public int NumOfWithdrawls { get; set; }

        public double WithdrawnAmount { get; set; }

        public TransactionDetails(List<Transaction> list, int num, double amt)
        {
            Transactions = list;
            NumOfWithdrawls = num;
            WithdrawnAmount = amt;
        }
    }
}
