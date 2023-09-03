namespace FinalProject.Models
{
    public class AccountCustomerModel
    {
        
        public string Name { get; set; }

        public string Location { get; set; }

        public DateTime DOB { get; set; }
        public long Mobile { get; set; }
        public string AccountType { get; set; }

        public double InitialBalance { get; set; }


        public AccountCustomerModel(string name, string location, long mobile, string accountType, double initialBalance, DateTime dOB)
        {
            Name = name;
            Location = location;
            Mobile = mobile;
            AccountType = accountType;
            InitialBalance = initialBalance;

            DOB = dOB;

        }
    }
}
