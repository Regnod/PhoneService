using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class MobilePhoneCall
    {
        [Key]
        public int MobilePhoneCallId { get; set; }//this 
        
        [Display(Name ="Phone Number")]
        public string PhoneNumber { get; set; }//this
        [Display(Name = "Phone Line")]
        public PhoneLine PhoneLine { get; set; }
        
        [Display(Name = "Date Time")]
        public DateTime DateTime { get; set; }//this
        
        [Display(Name = "Address")]
        public string Addressee { get; set; }//this
        [Display(Name = "Duration")]
        public float Duration { get; set; }//this
        public float TA { get; set; }
        public float LD { get; set; }
        public float Discount { get; set; }
        public float Charge { get; set; }
        public float TotalCost { get; set; }//this
        public bool RoamingCall { get; set; } //this
        
    }
}