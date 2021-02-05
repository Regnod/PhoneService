using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class DataPlan
    {
        [Key]
        [Display(Name ="Data Plan")]
        public string DataPlanId { get; set; }
        [Display(Name ="Data")]
        public int Data { get; set; }
        public float Cost { get; set; }
       
        public List<DataPlanAssignment> DataPlanAssignments { get; set; }
    }
}