using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Internal;

namespace MVCPhoneServiceWeb.Controllers
{
    public class UserExceededDataPlans : Controller
    {
        private ApplicationDbContext _context;

        public UserExceededDataPlans(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET
        public async Task<IActionResult> Index(string month, string year)
        {
            int _month = Utils.Utils.IntTryParse(month);
            int _year = Utils.Utils.IntTryParse(year);
            _month = 1;
            _year = 2020;
           
            var a = _context.MobilePhoneDataPlanAssignments.First();
            var join = from mdp in _context.MobilePhoneDataPlanAssignments
                join dp in _context.DataPlans
                    on mdp.DataPlanId equals dp.DataPlanId
                join pe in _context.PhoneLineEmployees
                    on mdp.PhoneNumber equals pe.PhoneNumber
                join e in _context.Employees
                    on pe.EmployeeId equals e.EmployeeId
                 where mdp.DataPlanAssignmentDateTime.Month == _month &&
                       mdp.DataPlanAssignmentDateTime.Year == _year &&
                      (mdp.InternationalDataUsage > dp.InternationalData ||
                       mdp.NationalDataUsage > dp.NationalData)
                select new UserExceededDataPlan
                {
                    EmployeeId = e.EmployeeId,
                    EmployeeName = e.EmployeeName,
                    CostCenter = e.CostCenter,
                    PhoneNumber = pe.PhoneNumber,
                    DataPlanId = mdp.DataPlanId,
                    NationalDataExceeded = Math.Max(0,mdp.NationalDataUsage-dp.NationalData),
                    InternationalDataExceeded = Math.Max(0,mdp.InternationalDataUsage-dp.InternationalData)
                };
            IEnumerable<UserExceededDataPlan> model = join;
            return View(model);
        }
    }
}