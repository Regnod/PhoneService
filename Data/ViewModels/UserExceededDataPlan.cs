using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels
{
    public class UserExceededDataPlan
    {
        [Display(Name = "Employee Id")]
        public int EmployeeId { get; set; }
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
        [Display(Name = "Cost Center")]
        public string CostCenter { get; set; }
        [Display(Name = "Cost Center Code")]
        public string CC { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Data Plan")]
        public string DataPlanId { get; set; }
        [Display(Name = "Data Cost Exceded")]
        public float DataExceeded { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public float PerCent { get; set; }
    }
}