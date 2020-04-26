using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class CallingPlan
    {
        [Display(Name ="Calling Plan Id")]
        public  int CallingPlanId { get; set; }
        public int Minutes { get; set; }
        public int Messages { get; set; }
        public  List<MobilePhoneCallingPlanAssignment> MobilePhoneCallingPlanAssignments { get; set; }
    }
}