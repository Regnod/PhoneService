using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPhoneServiceWeb.Utils;
using Repo;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace MVCPhoneServiceWeb.Controllers
{
    public class UserExceededCallingPlans : Controller
    {
        private ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnviroment;

        public UserExceededCallingPlans(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnviroment = hostingEnvironment;
        }

        // GET
        public async Task<IActionResult> Index(int cpage, string phoneNumber, string employeeName, string callingPlan, string smsPlan,
            string minMinExc, string maxMinExc, string minSmsExc, string maxSmsExc, string minMinPercent, string maxMinPercent, string minSmsPercent, string maxSmsPercent,
            string phoneNumberCheck, string employeeNameCheck, string callingPlanCheck, string smsPlanCheck, string minExcCheck, string smsExcCheck, string minPercentCheck,
            string smsPercentCheck, string monthCheck, string yearCheck,
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
                             MinutesExceeded = Math.Max(pls.AirTime + pls.RoamingExpenses - cp.Cost, 0),
                             Month = pls.Month,
                             Year = pls.Year,
                             cpaMonth = mpcpa.Month,
                             cpaYear = mpcpa.Year,
                             spaMonth = spa.Month,
                             spaYear = spa.Year,
                             SmsPlan = sp.SMSPlanId,
                             MessagesExceeded = Math.Max(pls.SmsExpenses + pls.RoamingSmsExpenses - sp.Cost, 0),
                             PerCentCalls = 0,
                             PerCentSms = 0
                         }).ToList().Where(a => SD.DateFilter(_month, _year, new int[] { a.Month, a.cpaMonth, a.spaMonth }, new int[] { a.Year, a.cpaYear, a.spaYear }, false));

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
            bool[] mask = { phoneNumberCheck != null, employeeNameCheck != null, callingPlanCheck != null, smsPlanCheck != null, minExcCheck != null, false, smsExcCheck != null, false, monthCheck != null, yearCheck != null, minPercentCheck != null, false, smsPercentCheck != null, false };
            string csv = CSVStringConstructor(show, mask, result.Item1);
            //ViewData["csv"] = ss;
            HttpContext.Session.SetString(SD.csv, csv);
            return View(result.Item1);
        }

        public string CSVStringConstructor(Tuple<bool, string>[] show, bool[] mask, List<UserExceededCallingPlan> data)
        {
            List<string> headers = new List<string>();

            int t = 0;

            for (int j = 0; j < mask.Length; j++)
            {
                if (mask[j])
                {
                    headers.Add(SD.usersExceededCallingPlan[j]);
                }
            }

            List<List<string>> datas = new List<List<string>>();

            foreach (var item in data)
            {
                List<string> row = new List<string>();

                if (show[0].Item1)
                {
                    row.Add(item.PhoneNumber);
                }
                if (show[1].Item1)
                {
                    row.Add(item.EmployeeName);
                }
                if (show[2].Item1)
                {
                    row.Add(item.CallingPlan);
                }
                if (show[3].Item1)
                {
                    row.Add(item.SmsPlan);
                }
                if (show[4].Item1)
                {
                    row.Add(item.MinutesExceeded.ToString());
                }
                if (show[6].Item1)
                {
                    row.Add(item.MessagesExceeded.ToString());
                }
                if (show[8].Item1)
                {
                    row.Add(SD.Months[item.Month]);
                }
                if (show[9].Item1)
                {
                    row.Add(item.Year.ToString());
                }
                if (show[10].Item1)
                {
                    row.Add(item.PerCentCalls.ToString());
                }
                if (show[12].Item1)
                {
                    row.Add(item.PerCentSms.ToString());
                }
                datas.Add(row);
            }

            string csv = SD.csvString(headers, datas);
            return csv;
        }

        public async Task<IActionResult> Export(int page, string phoneNumber, string employeeName, string callingPlan, string smsPlan,
            string minMinExc, string maxMinExc, string minSmsExc, string maxSmsExc, string minMinPercent, string maxMinPercent, string minSmsPercent, string maxSmsPercent,
            string phoneNumberCheck, string employeeNameCheck, string callingPlanCheck, string smsPlanCheck, string minExcCheck, string smsExcCheck,
            string minPercentCheck, string smsPercentCheck, string monthCheck, string yearCheck,
            string month, string year)
        {

            string webRootPath = _hostingEnviroment.WebRootPath;
            var uploads = Path.Combine(webRootPath, "ExportFiles");
            var path = Path.Combine(uploads, "userExceededCallingPlan.csv");
            using (var filesStream = new FileStream(path, FileMode.Create))
            {

            }
            StreamWriter stw = new StreamWriter(Path.Combine(uploads, "userExceededCallingPlan.csv"));
            string csv = HttpContext.Session.GetString(SD.csv);
            stw.Write(csv);
            stw.Dispose();
            return RedirectToAction(nameof(Index), new
            {
                smsPercentCheck = smsPercentCheck,
                phoneNumber = phoneNumber,
                month = month,
                year = year,
                employeeName = employeeName,
                callingPlan = callingPlan,
                smsPlan = smsPlan,
                minMinExc = minMinExc,
                maxMinExc = maxMinExc,
                minSmsExc = minSmsExc,
                maxSmsExc = maxSmsExc,
                minMinPercent = minMinPercent,
                maxMinPercent = maxMinPercent,
                minSmsPercent = minSmsPercent,
                maxSmsPercent = maxSmsPercent,
                phoneNumberCheck = phoneNumberCheck,
                employeeNameCheck = employeeNameCheck,
                callingPlanCheck = callingPlanCheck,
                smsPlanCheck = smsPlanCheck,
                minExcCheck = minExcCheck,
                smsExcCheck = smsExcCheck,
                minPercentCheck = minPercentCheck,
                monthCheck = monthCheck,
                yearCheck = yearCheck,
                cpage = page
            });
        }
    }
}