using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Employee
    {
        [Key]
        [Display(Name ="Employee Id")]
        public int EmployeeId { get; set; }
        [Display(Name ="Employee Name")]
        public string EmployeeName { get; set; }
        [Display(Name ="Cost Center")]
        public string CostCenter { get; set; }
        [Display(Name ="Personal Code")]
        public int PersonalCode { get; set; }
        
        public  List<MobilePhoneEmployee> MobilePhoneEmployees { get; set; }
        public  List<PhoneLineEmployee> PhoneLineEmployees { get; set; }
        public List<LandlinePhoneCall> LandlinePhoneCalls { get; set; }
    }
}