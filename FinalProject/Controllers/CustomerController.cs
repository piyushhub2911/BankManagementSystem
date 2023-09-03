using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.Repositories;
using FinalProject.Utility;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CustomerController : Controller
    {
        private ICustomerRepo _customerRepo;
        private ErrorCodes _errorCodes;

        public CustomerController(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
         _errorCodes = new ErrorCodes();
        }

        [HttpPost("AddCustomer")]
        public IActionResult AddCustomer(AccountCustomerModel customer)
        {
            return Ok(_errorCodes.WithdrawStatusCodes[_customerRepo.AddCustomer(customer)]);
        }

        [HttpGet("GetAllCustomers")]

        public IActionResult GetAllCustomers()
        {
            return Ok(_customerRepo.GetAllCustomers());
        }
        [HttpGet(" GetCustomerById")]


        public IActionResult GetCustomerById(int id)
        {
            return Ok(_customerRepo.GetCustomerById(id));
        }
        [HttpDelete("RemoveCustomerById")]

        public IActionResult RemoveCustomerById(int id)
        {
            if(_customerRepo.RemoveCustomerById(id))
            {
                return Ok("Customer removed successfully");
            }
            else
            {
                return Ok("Customer Id not found");
            }
        }

        [HttpPatch("UpdateCustomer")]
        public IActionResult UpdateCustomer(JsonPatchDocument<Customer> document, int id)
        {
            if(_customerRepo.UpdateCustomer(document, id))
            {
            return Ok("Customer updated successfully");

            }

            return BadRequest("Customer id not found");
        }
    }
}
