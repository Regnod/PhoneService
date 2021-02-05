using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        
        [Display(Name ="Employee Name")]
        public string Name { get; set; }
        
        [Display(Name ="Cost Center")]
        public string CostCenterCode { get; set; }
        public CostCenter CostCenter { get; set; }
        
        [Display(Name ="Personal Code")]
        public string PersonalCode { get; set; }
        public string Email { get; set; }
        public string Extension { get; set; }

        public  List<MobilePhoneEmployee> MobilePhoneEmployees { get; set; }
        public  List<PhoneLineEmployee> PhoneLineEmployees { get; set; }
        public List<LandlinePhoneCall> LandlinePhoneCalls { get; set; }
    }
}