using System.ComponentModel.DataAnnotations;

namespace FinalProject.DAL
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        public string? Name { get; set; }

        public int Age { get; set; }

        public string? Location { get; set; }

        public long Mobile { get; set; }

        public DateTime DateOfCreation { get; set; }= DateTime.Now;

        public DateTime DOB { get; set; }

        public Customer()
        {
            
        }
        public Customer(int id,string name, int age, string location, long mobile,  DateTime dOB)
        {
            CustomerId = id;
            Name = name;
            Age = age;
            Location = location;
            Mobile = mobile;
            DateOfCreation = DateTime.Now;
            DOB = dOB;
        }
    }
}
