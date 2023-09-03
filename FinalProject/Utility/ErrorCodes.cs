namespace FinalProject.Utility
{
    public class ErrorCodes
    {
        public  Dictionary<int,string> WithdrawStatusCodes = new Dictionary<int,string>();
        public ErrorCodes() {
            WithdrawStatusCodes.Add(41, "Account ID doesnt exist");
            WithdrawStatusCodes.Add(42, "not sufficient balance");
            WithdrawStatusCodes.Add(43, "Maximum number of transactions limit reached");
            WithdrawStatusCodes.Add(44, "Maximum withdrawal amount for the day exceeded");
            WithdrawStatusCodes.Add(45, "Account number doesn't exist");
            WithdrawStatusCodes.Add(46, "New account type is same as old account type");
            WithdrawStatusCodes.Add(47, "Transaction id is not present");
            WithdrawStatusCodes.Add(48, "Enter correct account type");
            WithdrawStatusCodes.Add(49, "Customer Id is not present");
            WithdrawStatusCodes.Add(50, "Age should be greater than 18");

            
            WithdrawStatusCodes.Add(21, "Withdraw successfull");
            WithdrawStatusCodes.Add(22, "Transaction type updated successfully");
            WithdrawStatusCodes.Add(23, "Transaction Added successfully");
            WithdrawStatusCodes.Add(24, "Transaction amount updated successfully");
            WithdrawStatusCodes.Add(25, "Money transferred successfully");
            WithdrawStatusCodes.Add(26, "Account created successfully");
            WithdrawStatusCodes.Add(27, "Customer added successfully");
        }
        
    }
}
