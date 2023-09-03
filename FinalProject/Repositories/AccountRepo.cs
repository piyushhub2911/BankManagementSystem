using AutoMapper;
using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.Utility;

using Microsoft.EntityFrameworkCore;

namespace FinalProject.Repositories
{
    public class AccountRepo:IAccountRepo
    {
        private BankContext _bankContext;
        private readonly Func<string, IAccountType> _accountType;
        private ITransactionRepo _transactionRepo;
        public AccountRepo(BankContext bankContext, Func<string, IAccountType> accountType,ITransactionRepo transactionRepo)
        {
            _accountType = accountType;
            _bankContext=bankContext;
            _transactionRepo=transactionRepo;
        }
       

        public Account? GetAccountById (int id)
        {
            return _bankContext.BankAccounts.FirstOrDefault(x => x.AccountNum == id);
            
        }
        
        public int AddAccount(AccountModel account)
        {
            Mapper m = MyMapper.Mapping();
            Account obj=m.Map<AccountModel,Account>(account);
            obj.CurrBalance = account.InitialBalance;
            Customer customer= _bankContext.BankCustomers.FirstOrDefault(x => x.CustomerId == obj.CustomerId);
            if (customer!=null)
            {
                if(obj.AccountType!=AccounTypes.saving&&obj.AccountType!=AccounTypes.current) {
                    return 48;
                }
                var type = _accountType(obj.AccountType);
                if (obj.AccountType==AccounTypes.saving && type.GetMinBalance()>obj.CurrBalance)
                {
                    return 42;
                }
                _bankContext.BankAccounts.Add(obj);
                _bankContext.SaveChanges();
                
                TransactionModel tran = new TransactionModel(obj.AccountNum, AccounTypes.credit, obj.CurrBalance);
                Transaction t=m.Map<TransactionModel,Transaction>(tran);

               // _transactionRepo.AddTransaction(tran);
                _bankContext.BankTransactions.Add(t);
                _bankContext.SaveChanges();
                return 26;
            }
            return 49;
        }

        public List<DisplayAccountModel> GetAllAccounts()
        {
            List<DisplayAccountModel> list=new List<DisplayAccountModel> ();
            Mapper m=MyMapper.Mapping();

            foreach (Account account in _bankContext.BankAccounts.ToList())
            {
                list.Add(m.Map<Account,DisplayAccountModel>(account));
            }
            return list;
        }

        public List<Account> GetAllAccountsByCustomerId(int customerId)
        {
            return _bankContext.BankAccounts.Where(a => a.CustomerId == customerId).ToList();
        }
    
        public bool RemoveAccountByNumber(int id)
        {
         Account acc=GetAccountById(id);
            if (acc != null)
            {
                if (GetAllAccountsByCustomerId(acc.CustomerId).Count() >1)
                {
                    _bankContext.BankAccounts.Remove(GetAccountById(id));
                    _bankContext.SaveChanges();
                }
                else
                {
                    Customer customer = _bankContext.BankCustomers.FirstOrDefault(x => x.CustomerId == acc.CustomerId);
                    _bankContext.BankCustomers.Remove(customer);
                }
                return true;
            }
            return false;
            
        }

        public bool DepositAmount(int accountNo,double amount,bool DisplayTransaction)
        {
            Account acc = GetAccountById(accountNo);
            if (acc != null)
            {
                acc.CurrBalance += amount;
                if(DisplayTransaction)
                {
                    TransactionModel tran = new TransactionModel(accountNo, AccounTypes.credit, amount);
                    _transactionRepo.AddTransaction(tran);
                }
                _bankContext.SaveChanges();
                return true;
            }
            else
            {
                 return false;
            }
        }

        public int WithdrawAmount(int accountNo,double amount,bool DisplayTransaction)
        {
            Account acc=GetAccountById(accountNo);

            if (acc == null)
            {
                return 41;
            }

            var obj = _accountType(acc.AccountType);
            TransactionDetails transactionDetails = _transactionRepo.GetTransactionsOfAccountByDateRange(accountNo, DateTime.Now, DateTime.Now);
            //check min sufficent balance
            if (acc.CurrBalance - obj.GetMinBalance() < amount)
            {
                return 42;
            }//check maximum number of withdrawls
            else if (transactionDetails.NumOfWithdrawls >= obj.MaxNumberOfWithdrwalPerDay())
            {
                return 43;
            }
            else if (transactionDetails.WithdrawnAmount + amount > obj.GetMaxWithdrawlAmountPerDay())
            {
                return 44;
            }else
            {
                acc.CurrBalance -= amount;
                if (DisplayTransaction)
                {
                    TransactionModel tran = new TransactionModel(accountNo, AccounTypes.debit, amount);
                    _transactionRepo.AddTransaction(tran);
                }
                
                _bankContext.SaveChanges();
                return 21;
            }
        }

        public int TransferMoney(int from ,int to, double amount)
        {
            Account acc1= GetAccountById(from);
            Account acc2= GetAccountById(to);

            if(acc1 == null)
            {
                return 41;
            }
            if(acc2 == null)
            {
                return 41;
            }
            int withdrawlStatus = WithdrawAmount(from, amount, false);
            if (withdrawlStatus!=21)
            {
                return withdrawlStatus;
            }
            int newId = _transactionRepo.GenerateTransactionId();
            
            Transaction transferedTransaction = new Transaction(newId,acc1.AccountNum,AccounTypes.transferred,amount);
         
            _bankContext.BankTransactions.Add(transferedTransaction);

            DepositAmount(to, amount, false);
            Transaction receivedTransaction = new Transaction(newId,acc2.AccountNum, AccounTypes.received, amount);
            
            _bankContext.BankTransactions.Add(receivedTransaction);
            _bankContext.SaveChanges();
            return 25;


        }

        public List<Transaction> GetTransactionSummary(int accountNo,DateTime start,DateTime end)
        {
            return _transactionRepo.GetTransactionsOfAccountByDateRange(accountNo,start,end).Transactions;
        }

        public bool UpdateAccountType(int accountNo,string accountType)
        {
            Account acc= GetAccountById(accountNo); 
           if(accountType.ToLower() == AccounTypes.saving && acc.CurrBalance<_accountType(AccounTypes.saving).GetMinBalance())
            {
                return false;
            }
            else
            {
                acc.AccountType= accountType;
                _bankContext.SaveChanges();
                return true;
            }
        }
    }
}
