using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels
{
    public class MobilePhoneExpense
    {   
        [Display(Name="Employee Name")]
        public string EmployeeName { get; set; }
        [Display(Name="Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name= "Cost Center Name")]
        public string CosCenterName { get; set; }
        [Display(Name= "Call Plan")]
        public string CallPlan { get; set; }
        public float Minutes { get; set; }
        public float SMS { get; set; }
        [Display(Name= "Long Distance")]
        public float  LongDistance { get; set; }
        public float GPRS { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public float Total { get; set; }
    }
}