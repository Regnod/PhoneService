using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    // TODO: This model does not have any controller
    public class CostCenter
    {
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Gerency")]
        public int ManagementId { get; set; }
        public Management Management { get; set; }

        public List<Employee> Employees { get; set; }
    }
}