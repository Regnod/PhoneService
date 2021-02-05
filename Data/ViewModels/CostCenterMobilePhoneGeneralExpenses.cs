using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.ViewModels
{
    public class CostCenterMobilePhoneGeneralExpenses
    {
        [Display(Name = "Employee")]
        public string EmployeeName { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Calling Plan")]
        public string CallingPlanId { get; set; }
        [Display(Name = "CC")]
        public string CostCenterCode { get; set; }
        [Display(Name = "Cost Center")]
        public string CostCenterName { get; set; }
        [Display(Name = "Calls")]
        public float Minutes { get; set; }
        [Display(Name = "Messages")]
        public float SMS { get; set; }
        public float Gprs { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public float Total { get; set; }
    }
}
