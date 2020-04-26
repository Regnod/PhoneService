using System.ComponentModel.DataAnnotations;

namespace Data.ViewModels
{
    public class TotalCostViewModel
    {
        [Display(Name = "Cost Center")]
        public string CostCenter { get; set; }
        public int Expense { get; set; }
    }
}