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
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace MVCPhoneServiceWeb.Controllers
{
    public class UserExceededDataPlans : Controller
    {
        private ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnviroment;

        public UserExceededDataPlans(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnviroment = hostingEnvironment;
        }

        // GET
        public async Task<IActionResult> Index(int cpage, string phoneNumber, string employeeName, string dataPlan, string ccc, string ccName, string month, string year,
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
            bool[] mask = { phoneNumberCheck != null, employeeNameCheck != null, dataPlanCheck != null, cccCheck != null, ccNameCheck != null, monthCheck != null, yearCheck != null, DataExcCheck != null, false, PercentCheck != null, false };
            string csv = CSVStringConstructor(show, mask, result.Item1);
            //ViewData["csv"] = ss;
            string httpSessionName = SD.HttpSessionString(new List<string> { "UserExceededDataPlan", result.Item4.ToString(), phoneNumber, employeeName, dataPlan, ccc, ccName, month, year, minDataExc, maxDataExc, minPercent, maxPercent,
                (phoneNumberCheck != null).ToString(), (employeeNameCheck != null).ToString(), (dataPlanCheck != null).ToString(), (cccCheck != null).ToString(), (ccNameCheck != null).ToString(), (monthCheck != null).ToString(), (yearCheck != null).ToString(), (DataExcCheck != null).ToString(), (PercentCheck != null).ToString() });

            HttpContext.Session.SetString(httpSessionName, csv);
            return View(result.Item1);
        }

        public string CSVStringConstructor(Tuple<bool, string>[] show, bool[] mask, List<UserExceededDataPlan> data)
        {
            List<string> headers = new List<string>();

            int t = 0;

            for (int j = 0; j < mask.Length; j++)
            {
                if (mask[j])
                {
                    headers.Add(SD.userExceededDataPlan[j]);
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
                    row.Add(item.DataPlanId);
                }
                if (show[3].Item1)
                {
                    row.Add(item.CC);
                }
                if (show[4].Item1)
                {
                    row.Add(item.CostCenter);
                }
                if (show[5].Item1)
                {
                    row.Add(SD.Months[item.Month]);
                }
                if (show[6].Item1)
                {
                    row.Add(item.Year.ToString());
                }
                if (show[7].Item1)
                {
                    row.Add(item.DataExceeded.ToString());
                }
                if (show[9].Item1)
                {
                    row.Add(item.PerCent.ToString());
                }
                datas.Add(row);
            }

            string csv = SD.csvString(headers, datas);
            return csv;
        }

        public async Task<IActionResult> Export(int page, string phoneNumber, string employeeName, string dataPlan, string ccc, string ccName, string month, string year,
            string minDataExc, string maxDataExc, string minPercent, string maxPercent,
            string phoneNumberCheck, string employeeNameCheck, string dataPlanCheck, string cccCheck, string ccNameCheck, string monthCheck, string yearCheck,
            string DataExcCheck, string PercentCheck)
        {

            string webRootPath = _hostingEnviroment.WebRootPath;
            var uploads = Path.Combine(webRootPath, "ExportFiles");
            string time = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second;
            var path = Path.Combine(uploads, "userExceededDataPlan " + time + ".csv");
            using (var filesStream = new FileStream(path, FileMode.Create))
            {

            }
            StreamWriter stw = new StreamWriter(Path.Combine(uploads, "userExceededDataPlan " + time + ".csv"));
            string httpSessionName = SD.HttpSessionString(new List<string> { "UserExceededDataPlan", page.ToString(), phoneNumber, employeeName, dataPlan, ccc, ccName, month, year, minDataExc, maxDataExc, minPercent, maxPercent,
                phoneNumberCheck.ToString(), employeeNameCheck.ToString(), dataPlanCheck.ToString(), cccCheck.ToString(), ccNameCheck.ToString(), monthCheck.ToString(), yearCheck.ToString(), DataExcCheck.ToString(), PercentCheck.ToString() });

            string csv = HttpContext.Session.GetString(httpSessionName);
            stw.Write(csv);
            stw.Dispose();
            return RedirectToAction(nameof(Index), new
            {
                phoneNumber = phoneNumber,
                month = month,
                year = year,
                employeeName = employeeName,
                dataPlan = dataPlan,
                ccc = ccc,
                ccName = ccName,
                minDataExc = minDataExc,
                maxDataExc = maxDataExc,
                minPercent = minPercent,
                maxPercent = maxPercent,
                phoneNumberCheck = phoneNumberCheck == "True" ? "True" : null,
                employeeNameCheck = employeeNameCheck == "True" ? "True" : null,
                dataPlanCheck = dataPlanCheck == "True" ? "True" : null,
                cccCheck = cccCheck == "True" ? "True" : null,
                ccNameCheck = ccNameCheck == "True" ? "True" : null,
                DataExcCheck = DataExcCheck == "True" ? "True" : null,
                PercentCheck = PercentCheck == "True" ? "True" : null,
                monthCheck = monthCheck == "True" ? "True" : null,
                yearCheck = yearCheck == "True" ? "True" : null,
                cpage = page
            });
        }
    }
}