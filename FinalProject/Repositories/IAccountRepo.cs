using FinalProject.DAL;
using FinalProject.Models;

namespace FinalProject.Repositories
{
    public interface IAccountRepo
    {
        public int AddAccount(AccountModel account);
        public List<DisplayAccountModel> GetAllAccounts();

        //public void RemoveAccountsByCustomerId(int id);

        public bool RemoveAccountByNumber(int id);

        public Account? GetAccountById(int id);
        public bool DepositAmount(int accountNo, double amount, bool DisplayTransaction);
        public int WithdrawAmount(int accountNo, double amount,  bool DisplayTransaction);
        public int TransferMoney(int from, int to, double amount);
        public List<Transaction> GetTransactionSummary(int accountNo, DateTime start, DateTime end);

        public bool UpdateAccountType(int accountNo, string accountType);
    }
}
