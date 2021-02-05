using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Management
    {
        [Key]
        [Display(Name = "ID")]
        public int ManagementId { get; set; }

        [Display(Name = "Gerency")]
        public string Name { get; set; }

        public List<CostCenter> CostCenters { get; set; }
    }
}
