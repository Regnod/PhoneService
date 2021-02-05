using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    //TODO: This model does not any controller
    public class GPRS
    {
        [Key]
        public int GPRSId { get; set; }
        public string PhoneNumber { get; set; }
        public PhoneLine PhoneLine { get; set; }
        
        public DateTime DateTime { get; set; }
        public string Location { get; set; }
        public string APN { get; set; }
        public string Volume { get; set; }
        public string VolFact { get; set; }
        public float Amount { get; set; }
        public float Discount { get; set; }
        public float Charge { get; set; }
        public float Total { get; set; }
        public bool Roaming { get; set; }
    }
}