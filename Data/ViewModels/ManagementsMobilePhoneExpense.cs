using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.ViewModels
{
    public class ManagementsMobilePhoneExpense
    {
        [Display(Name = "Gerency")]
        public string Management { get; set; }
        public float Calls { get; set; }
        [Display(Name ="Messages")]
        public float SMS { get; set; }
        public float GPRS { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public float Total { get; set; }
        [Display(Name = "%")]
        public float Percent { get; set; }
    }
}