using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class MobilePhoneEmployee
    {
        public int IMEI { get; set; }
        [Display(Name = "Mobile Phone")]
        public  MobilePhone MobilePhone { get; set; }
        
        [Display(Name = "Employee Id")]
        public  int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}