using FinalProject.DAL;
using FinalProject.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace FinalProject.Repositories
{
    public interface ICustomerRepo
    {
        public int AddCustomer(AccountCustomerModel customer);
        public List<Customer> GetAllCustomers();

        public Customer? GetCustomerById(int id);
        public bool RemoveCustomerById(int id);

        public bool UpdateCustomer(JsonPatchDocument<Customer> document, int id);
    }
}
