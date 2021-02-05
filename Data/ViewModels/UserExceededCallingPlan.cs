using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels
{
    public class UserExceededCallingPlan
    {
        [Display(Name = "Employee Id")]
        public int EmployeeId { get; set; }
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Calling Plan")]
        public string CallingPlan { get; set; }
        [Display(Name = "Sms Plan")]
        public string SmsPlan { get; set; }
        [Display(Name = "Calling Cost Exceded")]
        public float MinutesExceeded { get; set; }
        [Display(Name = "Sms Cost Exceded")]
        public float MessagesExceeded { get; set; }
        [Display(Name = "Sms %")]
        public float PerCentCalls { get; set; }
        [Display(Name = "Calls %")]
        public float PerCentSms { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}