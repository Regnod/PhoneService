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
using MVCPhoneServiceWeb.Utils;
using Repo;

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
        public async Task<IActionResult> Index( int cpage, string phoneNumber, string employeeName, string dataPlan, string ccc, string ccName, string month, string year,
            string minDataExc, string maxDataExc, string minPercent, string maxPercent,
            string phoneNumberCheck, string employeeNameCheck, string dataPlanCheck, string cccCheck, string ccNameCheck, string monthCheck, string yearCheck,
            string DataExcCheck, string PercentCheck, string page, string next, string previous)
        {
            var _month = (month != null && SD.months.ContainsKey(month)) ? SD.months[month] : -1;
            if (_month == -1)
                _month = Parse.IntTryParse(month);
            var _year = Parse.IntTryParse(year);
            var _minDataExc = Parse.FloatTryParse(minDataExc);
            var _maxDataExc = Parse.FloatTryParse(maxDataExc);
            var _minPercent = Parse.FloatTryParse(minPercent);
            var _maxPercent = Parse.FloatTryParse(maxPercent);
            //
            var query1 = (from pls in _context.PhoneLineSummaries
                         join pe in _context.PhoneLineEmployees on pls.PhoneNumber equals pe.PhoneNumber
                         join e in _context.Employees on pe.EmployeeId equals e.EmployeeId
                         join dpa in _context.DataPlanAssignments on pls.PhoneNumber equals dpa.PhoneNumber
                         join dp in _context.DataPlans on dpa.DataPlanId equals dp.DataPlanId
                         join cc in _context.CostCenters on e.CostCenterCode equals cc.Code
                         where (pls.GprsExpenses > dp.Cost)
                         select new 
                         {
                             EmployeeId = e.EmployeeId,
                             EmployeeName = e.Name,
                             CostCenter = cc.Name,
                             CC = cc.Code,
                             PhoneNumber = pls.PhoneNumber,
                             Month = pls.Month,
                             Year = pls.Year,
                             dpaMonth = dpa.Month,
                             dpaYear = dpa.Year,
                             DataPlanId = dp.DataPlanId,
                             DataExceeded = pls.GprsExpenses - dp.Cost,
                             PerCent = 0
                         }).ToList();
            var query = query1.Where(a => SD.DateFilter(_month, _year, new int[] { a.Month, a.dpaMonth }, new int[] { a.Year, a.dpaYear }, true));
            float total = query.Sum(a => a.DataExceeded);
            var models = new List<UserExceededDataPlan>();
            foreach (var item in query)
            {
                models.Add(new UserExceededDataPlan()
                {
                    EmployeeId = item.EmployeeId,
                    EmployeeName = item.EmployeeName,
                    PhoneNumber = item.PhoneNumber,
                    CC = item.CC,
                    CostCenter = item.CostCenter,
                    Month = item.Month,
                    Year = item.Year,
                    DataPlanId = item.DataPlanId,
                    DataExceeded = item.DataExceeded,
                    PerCent = item.PerCent
                });
            }
            foreach (var userExceededDataPlan in models)
            {
                userExceededDataPlan.PerCent = 100 * (userExceededDataPlan.DataExceeded / total);
            }
            //
            Tuple<bool, string>[] show = SD.Show(new List<string>() { phoneNumberCheck, employeeNameCheck, dataPlanCheck, cccCheck, ccNameCheck, monthCheck, yearCheck, DataExcCheck, DataExcCheck, PercentCheck, PercentCheck },
                new List<string>() { phoneNumber, employeeName, dataPlan, ccc, ccName, month, year, minDataExc, maxDataExc, minPercent, maxPercent });
            ViewData["columns"] = show;
            //

            models = DataFilter<UserExceededDataPlan>.Filter(phoneNumber, (m) => m.PhoneNumber, models).ToList();
            models = DataFilter<UserExceededDataPlan>.Filter(employeeName, (m) => m.EmployeeName, models).ToList();
            models = DataFilter<UserExceededDataPlan>.Filter(dataPlan, (m) => m.DataPlanId, models).ToList();
            models = DataFilter<UserExceededDataPlan>.Filter(_minDataExc, _maxDataExc, (m) => m.DataExceeded, models).ToList();
            models = DataFilter<UserExceededDataPlan>.Filter(_minPercent, _maxPercent, (m) => m.PerCent, models).ToList();

            //separar en paginas
            models = models.OrderBy(m => m.EmployeeName).ToList();
            var result = Paging<UserExceededDataPlan>.Pages(models, page, cpage, (next != null), (previous != null));

            ViewData["top"] = result.Item2;
            ViewData["mult"] = result.Item3;
            ViewData["page"] = result.Item4;

            return View(result.Item1);
        }
    }
}