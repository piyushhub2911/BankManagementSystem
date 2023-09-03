using AutoMapper;
using FinalProject.DAL;
using FinalProject.Models;

namespace FinalProject.Utility
{
    public class MyMapper
    {
        public static Mapper Mapping()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountCustomerModel, Customer>();
                cfg.CreateMap<AccountCustomerModel, Account>();
                cfg.CreateMap<AccountCustomerModel, AccountModel>();
                cfg.CreateMap<AccountModel, Account>();
                cfg.CreateMap<TransactionModel, Transaction>();
                cfg.CreateMap<Account, DisplayAccountModel>();
                cfg.CreateMap<Transaction, DisplayTransactionModel>();
                cfg.CreateMap<Customer, DisplayCustomerModel>();
            });
            Mapper m1 = new Mapper(config);
            return m1;
        }
    }
}
