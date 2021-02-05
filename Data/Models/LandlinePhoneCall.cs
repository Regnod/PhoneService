using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class LandlinePhoneCall
    {
        public int Extension { get; set; }

        [Display(Name ="Date Time")]
        public DateTime DateTime { get; set; }

        [Display(Name ="Employee Id")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Display(Name ="Destination")]
        public string Destination { get; set; }

        [Display(Name ="Cost")]
        public float Cost { get; set; }

        [Display(Name ="Duration")]
        public float Duration { get; set; }

        [Display(Name ="Addressee")]
        public string Addressee { get; set; }
    }
}