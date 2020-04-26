using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class MobilePhone
    {
        [Key] public int IMEI { get; set; }

        public string Modelo { get; set; }

        public List<MobilePhoneEmployee> MobilePhoneEmployee { get; set; }
        public  List<MobilePhoneCall> MobilePhoneCalls { get; set; }
        public List<MobilePhoneDataPlanAssignment> MobilePhoneDataPlanAssignments { get; set; }
    }
}