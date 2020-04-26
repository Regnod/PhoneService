namespace Data.ViewModels
{
    public class UserExceededDataPlan
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string CostCenter { get; set; }        
        public int PhoneNumber { get; set; }
        public int DataPlanId { get; set; }
        public float NationalDataExceeded { get; set; }
        public float InternationalDataExceeded { get; set; }
    }
}