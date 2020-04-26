using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels
{
    public class InternationalMobilePhoneCall
    {
        [Display(Name = "Employee Id")]
        public int EmployeId { get; set; }
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
        [Display(Name = "Cost Center")]
        public string CostCenter { get; set; }
      //  public int PersonalCode { get; set; }
        [Display(Name = "Phone Number")]
        public int PhoneNumber { get; set; }
        public int Expense { get; set; }
        public float Percent { get; set; }
    }
}