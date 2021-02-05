using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels
{
    public class InternationalMobilePhoneCall
    {
        [Display(Name = "Employee Id")]
        public int EmployeId { get; set; }
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
        [Display(Name = "Cost Center Code")]
        public string CostCenterCode { get; set; }
        [Display(Name = "Cost Center Name")]
        public string CostCenterName { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string Addresse { get; set; }
        public float Expense { get; set; }
        [Display(Name = "%")]
        public float PerCent { get; set; }
    }
}