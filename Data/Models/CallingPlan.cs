using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class CallingPlan
    {
        [Display(Name = "Calling Plan")]
        public string CallingPlanId { get; set; }

        [Display(Name = "Minutes")]
        public float Minutes { get; set; }

        [Display(Name = "Cost")]
        public float Cost { get; set; }

        public List<CallingPlanAssignment> CallingPlanAssignments { get; set; }
    }
}