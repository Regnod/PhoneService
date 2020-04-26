using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class MobilePhoneCall
    {
        [Display(Name ="Phone Number")]
        public int PhoneNumber { get; set; }
        [Display(Name = "Phone Line")]
        public PhoneLine PhoneLine { get; set; }
        
        public int IMEI { get; set; }
        [Display(Name = "Mobile Phone")]
        public MobilePhone MobilePhone { get; set; }
        
        [Display(Name = "Date Time")]
        public DateTime DateTime { get; set; }
        
        [Display(Name = "Address")]
        public int MobilePhoneCallAddressee { get; set; }
        [Display(Name = "Duration")]
        public int MobilePhoneCallDuration { get; set; }
        [Display(Name = "Cost")]
        public int MobilePhoneCallCost { get; set; }
    }
}