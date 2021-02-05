using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Bill
    {
        [Key]
        public string Number { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
