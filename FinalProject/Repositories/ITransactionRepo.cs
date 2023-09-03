using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.Utility;

namespace FinalProject.Repositories
{
    public interface ITransactionRepo
    {
        public int AddTransaction(TransactionModel transaction);

        public int GenerateTransactionId();

        public Transaction GetTransactionById(int id);
        public List<DisplayTransactionModel> GetBankTransactions();

        public bool RemoveTransactionById(int id);
        public TransactionDetails GetTransactionsOfAccountByDateRange(int accNo, DateTime startDate, DateTime endDate);
        //public double GetTotalWithdrawnAmountOfAccountByDateRange(int accNo, DateTime startDate, DateTime endDate);

        public int UpdateTransactionType(int id, string newType, ITransactionRepo transactionRepo, IAccountRepo accountRepo);

        public int UpdateTransactionAmount(int id, double amount, IAccountRepo accountRepo, ITransactionRepo transactionRepo);
    }
}
