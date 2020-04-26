using Data.Models;
using Repo;

namespace MVCPhoneServiceWeb.Utils
{
    public static class Seed
    {
        public static void PopulateDatabase (ApplicationDbContext db)
        {
            db.Employees.Add(new Employee() {EmployeeId = 123455, CostCenter = "123", EmployeeName = "Juan"});
            db.Employees.Add(new Employee() {EmployeeId = 123456, CostCenter = "123", EmployeeName = "Juana"});
            db.Employees.Add(new Employee() {EmployeeId = 123457, CostCenter = "123", EmployeeName = "Juanita"});
            db.Employees.Add(new Employee() {EmployeeId = 123458, CostCenter = "123", EmployeeName = "Juanota"});
            db.Employees.Add(new Employee() {EmployeeId = 123459, CostCenter = "123", EmployeeName = "Juanima"});

            db.CallingPlans.Add(new CallingPlan() {Messages = 100, Minutes = 100, CallingPlanId = 1});
            db.CallingPlans.Add(new CallingPlan() {Messages = 100, Minutes = 400, CallingPlanId = 2});
            db.CallingPlans.Add(new CallingPlan() {Messages = 300, Minutes = 100, CallingPlanId = 3});
            db.CallingPlans.Add(new CallingPlan() {Messages = 50, Minutes = 100, CallingPlanId = 4});
            db.CallingPlans.Add(new CallingPlan() {Messages = 100, Minutes = 200, CallingPlanId = 5});

            db.DataPlans.Add(new DataPlan() {DataPlanId = 1, InternationalData = 1000, NationalData = 500});
            db.DataPlans.Add(new DataPlan() {DataPlanId = 2, InternationalData = 2000, NationalData = 500});
            db.DataPlans.Add(new DataPlan() {DataPlanId = 3, InternationalData = 3000, NationalData = 500});
            db.DataPlans.Add(new DataPlan() {DataPlanId = 4, InternationalData = 10000, NationalData = 500});

            db.MobilePhones.Add(new MobilePhone() {IMEI = 12648765, Modelo = "Samsung S7 Edge"});
            db.MobilePhones.Add(new MobilePhone() {IMEI = 1234975, Modelo = "Samsung S8"});
            db.MobilePhones.Add(new MobilePhone() {IMEI = 492133275, Modelo = "Samsung S6 Edge"});
            db.MobilePhones.Add(new MobilePhone() {IMEI = 13496425, Modelo = "Samsung S9"});

            db.PhoneLines.Add(new PhoneLine() {PhoneNumber = 55160363, PIN = 1580, PUK = 14534});

        }
    }
}