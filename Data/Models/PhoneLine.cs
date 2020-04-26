using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class PhoneLine
    {
        [Key]
        [Display(Name = "Phone Number")]
        public int PhoneNumber { get; set; }
        public int PUK { get; set; }
        public int PIN { get; set; }
        
        public List<PhoneLineEmployee> PhoneLineEmployees { get; set; }
        public List<MobilePhoneCall> MobilePhoneCalls { get; set; }
        public List<MobilePhoneDataPlanAssignment> MobilePhoneDataPlanAssignments { get; set; }
        public List<MobilePhoneCallingPlanAssignment> MobilePhoneCallingPlanAssignments { get; set; }
        
    }
}