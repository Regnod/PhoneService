using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels
{
    public class CostCenterMobilePhoneExpense
    {
        [Display(Name ="Code")]
        public string CostCenterCode { get; set; }
        [Display(Name = "Name")]
        public string CostCenterName { get; set; }
        public float Calls { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        [Display(Name = "Messages")]
        public float SMS { get; set; }
        public float GPRS { get; set; }
        public float Total { get; set; }
        [Display(Name = "%")]
        public float Percent { get; set; }

    }
}