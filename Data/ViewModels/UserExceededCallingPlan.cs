namespace Data.ViewModels
{
    public class UserExceededCallingPlan
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int PhoneNumber { get; set; }
        public int PlanId { get; set; }
        public int MinutesExceeded { get; set; }
        public int MessagesExceeded { get; set; }
    }
}