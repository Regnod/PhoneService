using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class PhoneLineEmployee
    {
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public  PhoneLine PhoneLine { get; set; }

        [Display(Name = "Employee Id")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}