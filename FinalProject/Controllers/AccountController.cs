using FinalProject.Models;
using FinalProject.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using FinalProject.Utility;
namespace FinalProject.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AccountController : Controller
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly IAccountRepo _accountRepo;
        private readonly ITransactionRepo _transactionRepo;
        private ErrorCodes _errorCodes;
        public AccountController(IAccountRepo accountRepo,ICustomerRepo customerRepo,ITransactionRepo transactionRepo)
        {
           _accountRepo = accountRepo;
            _customerRepo = customerRepo;
            _transactionRepo = transactionRepo;
            _errorCodes = new ErrorCodes();
        }
        [HttpPost("AddAccount")]
        public IActionResult AddAccount(AccountModel account)
        {
          
            return Ok(_errorCodes.WithdrawStatusCodes[_accountRepo.AddAccount(account)]);
        }

        [HttpGet("GetAllAccounts")]

        public IActionResult GetAllAccounts()
        {
            return Ok(_accountRepo.GetAllAccounts());
        }

        [HttpGet("GetAccountById")]
        public IActionResult GetAccountById(int id) {
           if(_accountRepo.GetAccountById(id) == null)
            {
                return Ok("No account with given id exists");
            }
            return Ok(_accountRepo.GetAccountById(id));
        }
        [HttpGet("WithdrawAmount")]
        public IActionResult WithdrawAmount(int accountNo, int amount)
        {
            int ans= _accountRepo.WithdrawAmount(accountNo, amount, true);
            return Ok(_errorCodes.WithdrawStatusCodes[ans]);
        }

      
            [HttpGet("DepositAmount")]
        public IActionResult DepositAmount(int accountNo, int amount)
        {
            if(_accountRepo.DepositAmount(accountNo, amount, true))
            {
               return Ok("Amount deposited successfully");
                
            }
           return Ok("Account number doesn't exist");
        }

        [HttpDelete("RemoveAccountByNumber")]

        
        public IActionResult RemoveAccountByNumber(int AccountNum)
        {
            if (_accountRepo.RemoveAccountByNumber(AccountNum))
            {
            return Ok("Account deleted successfully");

            }
            else
            {
                return BadRequest("Account id is incorrect");
            }
        }

        [HttpGet("TransferMoney")]

        public IActionResult TransferMoney(int fromAccount, int toAccount, double amount)
        {
           
            return Ok(_errorCodes.WithdrawStatusCodes[_accountRepo.TransferMoney(fromAccount, toAccount, amount)]);
        }

        [HttpGet("GetTransactionSummary")]
        public IActionResult GetTransactionSummary(int accountNo, DateTime start, DateTime end)
        {
            return Ok(_accountRepo.GetTransactionSummary(accountNo,start,end));
        }

        [HttpPut("UpdateAccountType")]

        public IActionResult UpdateAccountType(int accountNo, string accountType)
        {
            if (_accountRepo.UpdateAccountType(accountNo, accountType))
            {
                return Ok("Account type updated successfully");
            }
            return BadRequest("Unsuccessfull");
        }
    }
}
