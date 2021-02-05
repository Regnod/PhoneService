using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class PhoneLineSummary
    {
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public PhoneLine PhoneLine { get; set; }

        [Display(Name = "Month")]
        public int Month { get; set; }

        [Display(Name = "Year")]
        public int Year { get; set; }

        [Display(Name = "Import By Details")]
        public float ImportByDetails { get; set; }

        [Display(Name = "Rent")]
        public float Rent { get; set; }

        [Display(Name = "AirTime")]
        public float AirTime { get; set; }

        [Display(Name = "LongDistance")]
        public float LongDistance { get; set; }

        [Display(Name = "Discount TA")]
        public float DiscountTA { get; set; }

        [Display(Name = "Discount LD")]
        public float DiscountLD { get; set; }

        [Display(Name = "ExtraCharges")]
        public float ExtraCharges { get; set; }

        [Display(Name = "TotalCalls")]
        public int TotalCalls { get; set; }

        [Display(Name = "Day Of Use")]
        public int DayOfUse { get; set; }
        [Display(Name = "SubTotal")]

        public float SubTotal { get; set; }

        [Display(Name = "Sms Expenses")]
        public float SmsExpenses { get; set; }

        [Display(Name = "Gprs Expenses")]
        public float GprsExpenses { get; set; }

        [Display(Name = "Roaming Expenses")]
        public float RoamingExpenses { get; set; }

        [Display(Name = "Roaming Sms Expenses")]
        public float RoamingSmsExpenses { get; set; }

        [Display(Name = "Roaming Gprs Expenses")]
        public float RoamingGprsExpenses { get; set; }

        [Display(Name = "Total")]
        public float Total { get; set; }
    }
}