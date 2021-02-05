using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class SMSPlanAssignment
    {
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Phone Line")]
        public PhoneLine PhoneLine { get; set; }

        [Display(Name = "SMS Plan")]
        public string SMSPlanId { get; set; }
        public SMSPlan SmsPlan { get; set; }

        [Display(Name = "Month")]
        public int Month { get; set; }

        [Display(Name = "Year")]
        public int Year { get; set; }
    }
}