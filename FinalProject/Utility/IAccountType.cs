namespace FinalProject.Utility
{
    public interface IAccountType
    {
        public double GetMinBalance();

        public double GetMaxWithdrawlAmountPerDay();

        public int MaxNumberOfWithdrwalPerDay();

        public double GetInterestRate();




    }
}
