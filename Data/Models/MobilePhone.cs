using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class MobilePhone
    {
        [Key]
        public string IMEI { get; set; }

        public string Model { get; set; }

        public List<MobilePhoneEmployee> MobilePhoneEmployee { get; set; }
       
    }
}