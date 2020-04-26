using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class DataPlan
    {
        [Display(Name ="Data Plan Id")]
        public int DataPlanId { get; set; }
        [Display(Name ="National Data")]
        public int NationalData { get; set; }
        [Display(Name = "International Data")]
        public int InternationalData { get; set; }
        public List<MobilePhoneDataPlanAssignment> MobilePhoneDataPlanAssignments { get; set; }
    }
}