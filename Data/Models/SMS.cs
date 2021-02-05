using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class SMS
    {
        [Key]
        public int SMSId { get; set; }
        [Display(Name ="Phone Number")]
        public string PhoneNumber { get; set; }
        public PhoneLine PhoneLine { get; set; }
        
        [Display(Name = "Date Time")]
        public DateTime DateTime { get; set; }
        [Display(Name = "Send Or Received")]
        public string E_R { get; set; }
        public string Location { get; set; }
        public string Destination { get; set; }
        public float Amount { get; set; }
        public float LD { get; set; }
        public float Discount { get; set; }
        public float Charge { get; set; }
        public float Total { get; set; }
        public bool Roaming { get; set; }
    }
}