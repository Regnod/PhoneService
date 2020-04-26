using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class MobilePhoneDataPlanAssignment
    {
        [Display(Name = "Phone Number")]
        public int PhoneNumber { get; set; }
        [Display(Name = "Phone Line")]
        public PhoneLine PhoneLine { get; set; }
        [Display(Name = "Date Time")]
        public DateTime DataPlanAssignmentDateTime { get; set; }
        [Display(Name = "Data Plan Id")]
        public int DataPlanId { get; set; }
        public DataPlan DataPlan { get; set; }
        [Display(Name = "National Data Usage")]
        public int NationalDataUsage { get; set; }
        [Display(Name = "International Data Usage")]
        public int InternationalDataUsage { get; set; }
    }
}