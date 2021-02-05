using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels
{
    public class CostCenterMobilePhoneExpensesDetails
    {
        [Display(Name ="Employee")]
        public string EmployeeName { get; set; }
        [Display(Name ="Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name ="Calling Plan")]
        public string CallingPlanId { get; set; }
        [Display(Name ="Calls")]
        public float Minutes { get; set; }
        [Display(Name = "Messages")]
        public float SMS { get; set; }
        public float Gprs { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public float Total { get; set; }
        
    }
}