using FinalProject.Models;
using FinalProject.Repositories;
using FinalProject.Utility;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{

    [ApiController]
    [Route("[Controller]")]
    public class TransactionController : Controller
    {

       private ITransactionRepo _transactionRepo;
        private IAccountRepo _accountRepo;
        private ErrorCodes _errorCodes;
        public TransactionController(ITransactionRepo transactionRepo,IAccountRepo accountRepo)
        {
            _transactionRepo = transactionRepo;
            _accountRepo = accountRepo;
            _errorCodes = new ErrorCodes();
        }

      
        [HttpGet("GetAllTransactions")]

        public IActionResult GetAllTransactions() { 
              return Ok(_transactionRepo.GetBankTransactions());
        }

        [HttpGet("GetTransactionById")]

        public IActionResult GetTransactionById(int id) {
            if (_transactionRepo.GetTransactionById(id) == null)
            {
                return Ok("No transaction with given id is present");
            }
            return Ok(_transactionRepo.GetTransactionById(id));
        }

        [HttpDelete("DeleteTransaction")]
        public IActionResult RemoveTransactionById(int transactionId)
        {
            if (_transactionRepo.RemoveTransactionById(transactionId))
            {
                return Ok("Transaction removed successfully");

            }
            return BadRequest("Transaction Id not found");
        }

        [HttpPut("UpdateTransactionType")]

        public IActionResult UpdateTransactionType(int transactionId,string newType)
        {
            return Ok(_errorCodes.WithdrawStatusCodes[_transactionRepo.UpdateTransactionType(transactionId, newType, _transactionRepo, _accountRepo)]);
        }

        [HttpPut("UpdateTransactionAmount")]
        public IActionResult UpdateTransactionAmount(int transactionId,double amount) {
            return Ok(_errorCodes.WithdrawStatusCodes[_transactionRepo.UpdateTransactionAmount(transactionId, amount, _accountRepo, _transactionRepo)]);
        }
    }
}
