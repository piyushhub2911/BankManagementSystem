using FinalProject.Utility;

namespace FinalProject.Repositories.Utility
{
    public class SavingsAccount:IAccountType
    {
        public double GetMinBalance()
        {
            return 1000;
        }

        public double GetMaxWithdrawlAmountPerDay()
        {
            return 45000;
        }

        public int MaxNumberOfWithdrwalPerDay()
        {
            return 5;
        }

        public double GetInterestRate()
        {
            return 4.5;
        }
    }
}
