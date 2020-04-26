using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class LandlinePhoneCall
    {
        public int Extension { get; set; }
        [Display(Name ="Date Time")]
        public DateTime LandlinePhoneCallDateTime { get; set; }

        [Display(Name ="Employee Id")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Display(Name ="Destination")]
        public string Destination { get; set; }
        [Display(Name ="Cost")]
        public int LandlinePhoneCallCost { get; set; }
        [Display(Name ="Duration")]
        public int LandlinePhoneCallDuration { get; set; }
        [Display(Name ="Address")]
        public int LandlinePhoneCallAddressee { get; set; }
    }
}