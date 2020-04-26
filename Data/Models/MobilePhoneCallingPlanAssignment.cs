using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class MobilePhoneCallingPlanAssignment
    {
        [Display(Name = "Phone Number")]
        public int PhoneNumber { get; set; }
        [Display(Name = "Phone Line")]
        public PhoneLine PhoneLine { get; set; }
        [Display(Name = "Date Time")]
        public DateTime CallingPlanAssignmentDateTime { get; set; }
        [Display(Name = "Calling Plan")]
        public int CallingPlanId { get; set; }
        public CallingPlan CallingPlan { get; set; }
        [Display(Name = "Minutes Consumed")]
        public int MinutesConsumed { get; set; }
        [Display(Name = "Messages Sent")]
        public int MessagesSent { get; set; }
        
    }
}