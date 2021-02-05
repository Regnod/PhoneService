using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repo;

namespace MVCPhoneServiceWeb.Controllers
{
    public class MobilePhoneExpenseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MobilePhoneExpenseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET
        public async Task<IActionResult> Index(string month, string year)
        {
            int _month = Utils.Parse.IntTryParse(month);
            int _year = Utils.Parse.IntTryParse(year);

            await Task.Delay(TimeSpan.FromSeconds(5));
            var model = ( from pls in _context.PhoneLineSummaries
                join ple in _context.PhoneLineEmployees on pls.PhoneNumber equals ple.PhoneNumber
                join e in _context.Employees on ple.EmployeeId equals e.EmployeeId
                join cc in _context.CostCenters on e.CostCenterCode equals cc.Code
                join dpa in _context.DataPlanAssignments on pls.PhoneNumber equals dpa.PhoneNumber
                join cpa in _context.CallingPlanAssignments on pls.PhoneNumber equals cpa.PhoneNumber
                join dp in _context.DataPlans on dpa.DataPlanId equals dp.DataPlanId
                join cp in _context.CallingPlans on cpa.CallingPlanId equals cp.CallingPlanId
                where pls.Year == _year && pls.Month == _month &&
                      dpa.Year == _year && dpa.Month == _month &&
                      cpa.Year == _year && cpa.Month == _month
                select new MobilePhoneExpense
                {
                    EmployeeName = e.Name,
                    PhoneNumber =pls.PhoneNumber,
                    CosCenterName = cc.Name,
                    CallPlan = cp.CallingPlanId,
                    Minutes = pls.AirTime,
                    SMS = pls.SmsExpenses,
                    LongDistance = pls.LongDistance,
                    GPRS = pls.GprsExpenses + pls.RoamingGprsExpenses,
                    Month =  pls.Month,
                    Year = pls.Year,
                    Total = pls.Total
                });
            
            return View(model);
        }
    }
}