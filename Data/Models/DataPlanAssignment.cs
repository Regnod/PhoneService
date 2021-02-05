using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class DataPlanAssignment
    {
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Phone Line")]
        public PhoneLine PhoneLine { get; set; }

        [Display(Name = "Data Plan")]
        public string DataPlanId { get; set; }
        public DataPlan DataPlan { get; set; }

        [Display(Name = "Month")]
        public int Month { get; set; }

        [Display(Name = "Year")]
        public int Year { get; set; }
    }
}