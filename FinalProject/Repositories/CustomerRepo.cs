using FinalProject.DAL;
using AutoMapper;
using FinalProject.Models;
using Microsoft.AspNetCore.JsonPatch;
using FinalProject.Utility;

namespace FinalProject.Repositories
{
    public class CustomerRepo:ICustomerRepo
    {
        private readonly BankContext _bankContext;
        private readonly Func<string, IAccountType> _accountType;
        IAccountRepo _accountRepo;
        public CustomerRepo(BankContext bankContext,IAccountRepo accountRepo, Func<string, IAccountType> accountType)
        {
            _bankContext = bankContext;
            _accountRepo = accountRepo;
            _accountType = accountType;
        }
        
        public int AddCustomer(AccountCustomerModel customer)
        {
            Mapper m = MyMapper.Mapping();
            Customer obj = m.Map<AccountCustomerModel, Customer>(customer);
            
            if (DateTime.Now.Year - customer.DOB.Year < 18)
            {
                return 50; //returns age invalid error
            }
            if (customer.AccountType != AccounTypes.saving && customer.AccountType != AccounTypes.current)
            {
                return 48;// return error if account type has spelling mistake
            }
            var type = _accountType(customer.AccountType);
            if (type.GetMinBalance() > customer.InitialBalance)
            {
                return 42; //initial balance is less than minimum balance 
            }
            obj.Age=DateTime.Now.Year-obj.DOB.Year;
            _bankContext.BankCustomers.Add(obj);
            _bankContext.SaveChanges();

            AccountModel acc = m.Map<AccountCustomerModel, AccountModel>(customer);
            acc.CustomerId=obj.CustomerId;
            _accountRepo.AddAccount(acc);
            //not directly calling aaddAccount in if to check code because it is called twice and hence in second call not able to find cu
            //customer ID and showing error customer id not found
            
            _bankContext.SaveChanges();
            return 27;
        }

        public Customer? GetCustomerById(int id)
        {
            return _bankContext.BankCustomers.FirstOrDefault(x => x.CustomerId == id);
        }
        public List<Customer> GetAllCustomers()
        {
            return _bankContext.BankCustomers.ToList();
        }

        public bool RemoveCustomerById(int id)
        {
            if(GetCustomerById(id) != null)
            {
                
                _bankContext.BankCustomers.Remove(GetCustomerById(id));
                _bankContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateCustomer(JsonPatchDocument<Customer> document, int id)
        {
            Customer model = GetCustomerById(id);
            if (model != null)
            {
                document.ApplyTo(model);
                _bankContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
