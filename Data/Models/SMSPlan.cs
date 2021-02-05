using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class SMSPlan
    {
        [Display(Name = "SMS Plan")]
        public string SMSPlanId { get; set; }

        [Display(Name = "Messages")]
        public int Messages { get; set; }

        [Display(Name = "Cost")]
        public float Cost { get; set; }

        public List<SMSPlanAssignment> SmsPlanAssignments { get; set; }
    }
}