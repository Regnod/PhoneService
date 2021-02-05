using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class PhoneLine
    {
        [Key] [Display(Name = "Phone Number")] 
        public string PhoneNumber { get; set; }
        public int PUK { get; set; }
        public string Contract { get; set; }
        public int PIN { get; set; }
        [Display(Name = "Has Call Details")] 
        public bool CallsDetails { get; set; }
        [Display(Name = "Has SMS Details")] 
        public bool SMSDetails { get; set; }
        [Display(Name = "Has GPRS Details")] 
        public bool GPRSDetails { get; set; }

        public List<SMS> SMSs { get; set; }
        public List<PhoneLineEmployee> PhoneLineEmployees { get; set; }
        public List<MobilePhoneCall> MobilePhoneCalls { get; set; }
        public List<DataPlanAssignment> DataPlanAssignments { get; set; }
        public List<SMSPlanAssignment> SmsPlanAssignments { get; set; }
        public List<CallingPlanAssignment> CallingPlanAssignments { get; set; }
        public List<PhoneLineSummary> PhoneLineSummaries { get; set; }
    }
}