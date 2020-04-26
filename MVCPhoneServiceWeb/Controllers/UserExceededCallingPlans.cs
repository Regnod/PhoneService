using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MVCPhoneServiceWeb.Controllers
{
    public class UserExceededCallingPlans : Controller
    {
        private ApplicationDbContext _context;

        public UserExceededCallingPlans(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET
        public async Task<IActionResult> Index(string month, string year)
        {
            int _month = Utils.Utils.IntTryParse(month);
            int _year = Utils.Utils.IntTryParse(year);

            var join1 = await _context.CallingPlans.Join(
                _context.MobilePhoneCallingPlanAssignments,
                a => a.CallingPlanId,
                a => a.CallingPlanId,
                (a, b) => new
                {
                    PhoneNumber = b.PhoneNumber,
                    MinutesExceeded = (a.Minutes >= b.MinutesConsumed) ? 0 : b.MinutesConsumed - a.Minutes,
                    MessagesExceeded = (a.Messages >= b.MessagesSent) ? 0 : b.MessagesSent - a.Messages,
                    DateTime = b.CallingPlanAssignmentDateTime,
                    PlanId = b.CallingPlanId
                }
            ).ToListAsync();


            var join2 = join1.Join(
                await _context.PhoneLineEmployees.ToListAsync(),
                a => a.PhoneNumber,
                a => a.PhoneNumber,
                (a, b) => new
                {
                    EmployeeId = b.EmployeeId,
                    PhoneNumber = a.PhoneNumber,
                    DateTime = a.DateTime,
                    PlanId = a.PlanId,
                    MinutesExceeded = a.MinutesExceeded,
                    MessagesExceeded = a.MinutesExceeded
                }
            );
            if (_month != -1)
            {
                join2 = join2.Where(a => a.DateTime.Month == _month);
            }

            if (_year != -1)
            {
                join2 = join2.Where(a => a.DateTime.Year == _year);
            }

            join2 = join2.Where(a => a.MessagesExceeded > 0 || a.MinutesExceeded > 0);

            var join3 = join2.Join(
                await _context.Employees.ToListAsync(),
                a => a.EmployeeId,
                a => a.EmployeeId,
                (a, b) => new
                {
                    EmployeeName = b.EmployeeName,
                    EmployeId = a.EmployeeId,
                    PhoneNumber = a.PhoneNumber,
                    PlanId = a.PlanId,
                    MinutesExceeded = a.MinutesExceeded,
                    MessagesExceeded = a.MinutesExceeded
                });

            List<UserExceededCallingPlan> model = new List<UserExceededCallingPlan>();
            foreach (var item in join3)
            {
                model.Add(new UserExceededCallingPlan()
                {
                    EmployeeId = item.EmployeId,
                    EmployeeName = item.EmployeeName,
                    PhoneNumber = item.PhoneNumber,
                    PlanId = item.PlanId,
                    MessagesExceeded = item.MessagesExceeded,
                    MinutesExceeded = item.MinutesExceeded
                });
            }

            return View(model);
        }
    }
}