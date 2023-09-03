using FinalProject.DAL;

namespace FinalProject.Models
{
    public class CustomerModel
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public long Mobile { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;

        public CustomerModel()
        {
            
        }

        public CustomerModel(string name,string location,long mobile,DateTime dob)
        {
            Name = name;
            Location = location;
            Mobile = mobile;
            DOB = dob;
            
        }
    }
}
