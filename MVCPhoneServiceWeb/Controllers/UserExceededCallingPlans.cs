using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPhoneServiceWeb.Utils;
using Repo;

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
        public async Task<IActionResult> Index(int cpage, string phoneNumber, string employeeName, string callingPlan,string smsPlan, 
            string minMinExc, string maxMinExc, string minSmsExc, string maxSmsExc, string minMinPercent, string maxMinPercent, string minSmsPercent, string maxSmsPercent,
            string phoneNumberCheck, string employeeNameCheck, string callingPlanCheck, string smsPlanCheck, string minExcCheck, string smsExcCheck, string minPercentCheck, string smsPercentCheck, string monthCheck, string yearCheck,
            string month, string year, string page, string next, string previous)
        {
            var _month = (month != null && SD.months.ContainsKey(month)) ? SD.months[month] : -1;
            if (_month == -1)
                _month = Parse.IntTryParse(month);
            var _year = Parse.IntTryParse(year);
            var _minMinExc = Parse.FloatTryParse(minMinExc);
            var _maxMinExc = Parse.FloatTryParse(maxMinExc);
            var _minSmsExc = Parse.FloatTryParse(minSmsExc);
            var _maxSmsExc = Parse.FloatTryParse(maxSmsExc);
            var _minMinPercent = Parse.FloatTryParse(minMinPercent);
            var _maxMinPercent = Parse.FloatTryParse(maxMinPercent);
            var _minSmsPercent = Parse.FloatTryParse(minSmsPercent);
            var _maxSmsPercent = Parse.FloatTryParse(maxSmsPercent);

            var query = (from pls in _context.PhoneLineSummaries
                        join ple in _context.PhoneLineEmployees on pls.PhoneNumber equals ple.PhoneNumber
                        join e in _context.Employees on ple.EmployeeId equals e.EmployeeId
                        join mpcpa in _context.CallingPlanAssignments on pls.PhoneNumber equals mpcpa.PhoneNumber
                        join cp in _context.CallingPlans on mpcpa.CallingPlanId equals cp.CallingPlanId
                        join spa in _context.SmsPlanAssignments on ple.PhoneNumber equals spa.PhoneNumber
                        join sp in _context.SmsPlans on spa.SMSPlanId equals sp.SMSPlanId
                        where (pls.AirTime + pls.RoamingExpenses > cp.Cost || pls.SmsExpenses + pls.RoamingSmsExpenses > sp.Cost)
                        select new 
                        {
                            EmployeeName = e.Name,
                            EmployeeId = e.EmployeeId,
                            PhoneNumber = pls.PhoneNumber,
                            CallingPlan = cp.CallingPlanId,
                            MinutesExceeded = Math.Max(pls.AirTime + pls.RoamingExpenses - cp.Cost,0),
                            Month = pls.Month,
                            Year = pls.Year,
                            cpaMonth = mpcpa.Month,
                            cpaYear = mpcpa.Year,
                            spaMonth = spa.Month,
                            spaYear = spa.Year,
                            SmsPlan = sp.SMSPlanId,
                            MessagesExceeded = Math.Max( pls.SmsExpenses + pls.RoamingSmsExpenses - sp.Cost, 0),
                            PerCentCalls= 0,
                            PerCentSms = 0
                        }).ToList().Where(a=> SD.DateFilter(_month, _year, new int[] { a.Month, a.cpaMonth, a.spaMonth}, new int[] { a.Year, a.cpaYear, a.spaYear }, false));

            float totalCallCost = query.Sum(a => a.MinutesExceeded);
            float totalMessagesCost = query.Sum(a => a.MessagesExceeded);
            var models = new List<UserExceededCallingPlan>();
            foreach (var item in query)
            {
                models.Add(new UserExceededCallingPlan()
                {
                    EmployeeId = item.EmployeeId,
                    EmployeeName = item.EmployeeName,
                    PhoneNumber = item.PhoneNumber,
                    CallingPlan = item.CallingPlan,
                    MinutesExceeded = item.MinutesExceeded,
                    Month = item.Month,
                    Year = item.Year,
                    SmsPlan = item.SmsPlan,
                    MessagesExceeded = item.MessagesExceeded,
                    PerCentCalls = item.PerCentCalls,
                    PerCentSms = item.PerCentSms
                });
            }
            foreach (var userExceededCallingPlan in models)
            {
                userExceededCallingPlan.PerCentCalls = 100 * (userExceededCallingPlan.MinutesExceeded / totalCallCost);
                userExceededCallingPlan.PerCentSms = 100 * (userExceededCallingPlan.MessagesExceeded / totalMessagesCost);
            }
            //
            Tuple<bool, string>[] show = SD.Show(new List<string>() { phoneNumberCheck, employeeNameCheck, callingPlanCheck, smsPlanCheck, minExcCheck, minExcCheck, smsExcCheck, smsExcCheck, monthCheck, yearCheck, minPercentCheck, minPercentCheck, smsPercentCheck, smsPercentCheck },
                new List<string>() { phoneNumber, employeeName, callingPlan, smsPlan, minMinExc, maxMinExc, minSmsExc, maxSmsExc, month, year, minMinPercent, maxMinPercent, minSmsPercent, maxSmsPercent });
            ViewData["columns"] = show;
            //

            models = DataFilter<UserExceededCallingPlan>.Filter(phoneNumber, (m) => m.PhoneNumber, models).ToList();
            models = DataFilter<UserExceededCallingPlan>.Filter(employeeName, (m) => m.EmployeeName, models).ToList();
            models = DataFilter<UserExceededCallingPlan>.Filter(callingPlan, (m) => m.CallingPlan, models).ToList();
            models = DataFilter<UserExceededCallingPlan>.Filter(smsPlan, (m) => m.SmsPlan, models).ToList();
            models = DataFilter<UserExceededCallingPlan>.Filter(_minMinExc, _maxMinExc, (m) => m.MinutesExceeded, models).ToList();
            models = DataFilter<UserExceededCallingPlan>.Filter(_minSmsExc, _maxSmsExc, (m) => m.MessagesExceeded, models).ToList();
            models = DataFilter<UserExceededCallingPlan>.Filter(_minMinPercent, _maxMinPercent, (m) => m.PerCentCalls, models).ToList();
            models = DataFilter<UserExceededCallingPlan>.Filter(_minSmsPercent, _maxSmsPercent, (m) => m.PerCentSms, models).ToList();

            //separar en paginas
            models = models.OrderBy(m => m.EmployeeName).ToList();
            var result = Paging<UserExceededCallingPlan>.Pages(models, page, cpage, (next != null), (previous != null));

            ViewData["top"] = result.Item2;
            ViewData["mult"] = result.Item3;
            ViewData["page"] = result.Item4;

            return View(result.Item1);
        }
    }
}