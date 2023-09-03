using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class DisplayCustomerModel
    { 
        public int CustomerId { get; set; }

        public string? Name { get; set; }

        public int Age { get; set; }

        public string? Location { get; set; }

        public long Mobile { get; set; }

        public DateTime DateOfCreation { get; set; } = DateTime.Now;

        public DateTime DOB { get; set; }

        public DisplayCustomerModel()
        {
            
        }

    }
}
