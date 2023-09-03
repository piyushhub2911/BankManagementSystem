using FinalProject.Utility;

namespace FinalProject.Repositories.Utility
{
    public class CurrentAccount:IAccountType
    {
        public double GetMinBalance()
        {
            return 0;
        }

        public double GetMaxWithdrawlAmountPerDay()
        {
            return 100000;
        }

        public int MaxNumberOfWithdrwalPerDay()
        {
            return 10;
        }

        public double GetInterestRate()
        {
            return 0;
        }
    }
}
