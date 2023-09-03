using AutoMapper;
using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.Utility;
using System.Collections.Generic;

namespace FinalProject.Repositories
{
    public class TransactionRepo:ITransactionRepo
    {
       private BankContext _bankContext;
       // private IAccountRepo _accountRepo;
        private Mapper m;
        public TransactionRepo(BankContext bankContext)
        {
            _bankContext = bankContext;
            m = MyMapper.Mapping();
        }

        public TransactionRepo()
        {
        }

        public int GenerateTransactionId()
        {
            Transaction tran=_bankContext.BankTransactions.OrderBy(t=>t.TransactionId).LastOrDefault();

            if(tran == null)
            {
                return 30000;
            }

            return tran.TransactionId+1;
        }
        public int AddTransaction(TransactionModel transaction)
        {
            Account acc = _bankContext.BankAccounts.FirstOrDefault(x => x.AccountNum == transaction.AccountNum);
            if (acc != null)
            {
                
                Transaction obj= new Transaction();
                obj = m.Map<TransactionModel, Transaction>(transaction);

                obj.TransactionId=GenerateTransactionId();
                _bankContext.BankTransactions.Add(obj);
                _bankContext.SaveChanges();
                return 23;
            }
            return 45;
           
        }

        public Transaction GetTransactionById(int id)
        {
           return _bankContext.BankTransactions.FirstOrDefault(x => x.TransactionId == id);
        }
        public bool RemoveTransactionById(int id)
        {
            if(GetTransactionById(id) != null)
            {

            _bankContext.Remove(GetTransactionById(id));
            
             _bankContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<DisplayTransactionModel> GetBankTransactions()
        {
            List<DisplayTransactionModel> list=new List<DisplayTransactionModel>();
            Mapper m = MyMapper.Mapping();
           foreach( Transaction t in _bankContext.BankTransactions.ToList())
            {
                list.Add(m.Map<DisplayTransactionModel>(t));
            } 
           return list;
        }

        public TransactionDetails GetTransactionsOfAccountByDateRange(int accNo, DateTime startDate, DateTime endDate)
        {
            List<Transaction> list = new List<Transaction>();
            int numOfWithdrawls = 0;
            double amountWithdrawn = 0;
            foreach(Transaction t in _bankContext.BankTransactions)
            {
                if(t.AccountNum== accNo && startDate.Date <= t.TransactionDate.Date && endDate.Date >= t.TransactionDate.Date)
                {
                    if (t.TransactionType == AccounTypes.debit)
                    {
                        numOfWithdrawls++;
                        amountWithdrawn += t.Amount;
                    }
                    list.Add(t);
                }
            }
           return new TransactionDetails(list,numOfWithdrawls,amountWithdrawn);
        }

        

        public int UpdateTransactionType(int id, string newType, ITransactionRepo transactionRepo, IAccountRepo accountRepo)
        {
            Transaction tran = GetTransactionById(id);
            if (tran != null)
            {
                if (tran.TransactionType == newType)
                {
                    return 45;
                }
                if (tran.TransactionType == AccounTypes.transferred || tran.TransactionType == AccounTypes.received)
                {
                  if(newType== AccounTypes.received) { 
                      
                    }
                }
                if (newType == AccounTypes.debit)
                {
                    //check if withdrawl is successfull
                    int withdrawStatus = accountRepo.WithdrawAmount(tran.AccountNum, tran.Amount * 2, false);
                    if (withdrawStatus != 21)
                    {
                        return withdrawStatus;
                    }
                    tran.TransactionType = newType;
                    _bankContext.SaveChanges();
                    return 22;
                }
                else if(newType==AccounTypes.credit)//do not check for deposit because account id is already checked if present
                {
                    accountRepo.DepositAmount(tran.AccountNum, tran.Amount * 2,  false);
                    tran.TransactionType = newType;
                    _bankContext.SaveChanges();
                    return 22;
                }
                else
                {
                    return 22;
                }

            }
            else
            {
                return 47;
            }
        }

        public int UpdateTransactionAmount(int id,double  amount,IAccountRepo accountRepo,ITransactionRepo transactionRepo)
        {
            Transaction tran = GetTransactionById(id);
            double newAmt = amount;
            double oldAmt=tran.Amount;
            if(tran != null)
            {
                
                if (tran.TransactionType == AccounTypes.credit)
                {
                    if(newAmt-oldAmt > 0)
                    {
                       accountRepo.DepositAmount(tran.AccountNum,newAmt-oldAmt, false);
                        tran.Amount = amount;
                        _bankContext.SaveChanges();
                        return 24;
                    }else {
                      if(accountRepo.WithdrawAmount(tran.AccountNum,oldAmt-newAmt, false) != 21)
                        {
                            return accountRepo.WithdrawAmount(tran.AccountNum, oldAmt - newAmt, false);

                        }
                        tran.Amount = amount;
                        _bankContext.SaveChanges();
                        return 24;
                    }
                }else // for debit
                {
                    if (newAmt - oldAmt > 0)
                    {
                        if (accountRepo.WithdrawAmount(tran.AccountNum,newAmt-oldAmt, false) != 21)
                        {
                            return accountRepo.WithdrawAmount(tran.AccountNum, newAmt-oldAmt,false);
                        }
                        tran.Amount = amount;
                        _bankContext.SaveChanges();
                        return 24;
                    }
                    else
                    {
                        accountRepo.DepositAmount(tran.AccountNum, oldAmt - newAmt, false);
                       
                        tran.Amount = amount;
                        _bankContext.SaveChanges();
                        return 24;
                    }
                }

            }
            else
            {
                return 47;
            }
        }
    }
}
