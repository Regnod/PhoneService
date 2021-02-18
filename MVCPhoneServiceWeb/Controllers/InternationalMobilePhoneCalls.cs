using System;
using System.Linq;
using System.Threading.Tasks;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Repo;
using MVCPhoneServiceWeb.Utils;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace MVCPhoneServiceWeb.Controllers
{
    public class InternationalMobilePhoneCalls : Controller
    {
        private ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnviroment;

        public InternationalMobilePhoneCalls(ApplicationDbContext context, IHostingEnvironment hostingEnviroment)
        {
            _context = context;
            _hostingEnviroment = hostingEnviroment;
        }

        // GET
        public async Task<IActionResult> Index(int cpage, string phoneNumber, string employeeName, string costCenterName, string costCenterCode,
            string addresse, string month, string year, string minExpense, string maxExpense, string minPercent, string maxPercent,
            string phoneNumberCheck, string employeeNameCheck, string costCenterNameCheck, string costCenterCodeCheck,
            string addresseCheck, string expenseCheck, string percentCheck, string monthCheck, string yearCheck, string page, string previous, string next)
        {
            var _month = (month != null && SD.months.ContainsKey(month)) ? SD.months[month] : -1;
            if (_month == -1)
                _month = Parse.IntTryParse(month);
            var _year = Parse.IntTryParse(year);
            var _minExpense = Parse.FloatTryParse(minExpense);
            var _maxExpense = Parse.FloatTryParse(maxExpense);
            var _minPercent = Parse.FloatTryParse(minPercent);
            var _maxPercent = Parse.FloatTryParse(maxPercent);

            var query = (from mpc in _context.MobilePhoneCalls
                         join ple in _context.PhoneLineEmployees on mpc.PhoneNumber equals ple.PhoneNumber
                         join e in _context.Employees on ple.EmployeeId equals e.EmployeeId
                         join cc in _context.CostCenters on e.CostCenterCode equals cc.Code
                         select new InternationalMobilePhoneCall()
                         {
                             PhoneNumber = mpc.PhoneNumber,
                             EmployeeName = e.Name,
                             CostCenterName = cc.Name,
                             CostCenterCode = cc.Code,
                             Month = mpc.DateTime.Month,
                             Year = mpc.DateTime.Year,
                             Addresse = mpc.Addressee,
                             Expense = mpc.TotalCost,
                             PerCent = 0
                         }).ToList();
            var query1 = query.Where(a => Checking(a.Addresse, _month, _year, a.Month, a.Year));
          //(!((a.Addresse[0] == '5' && a.Addresse.Length == 8) && !ProvinceFilter(a.Addresse))) && SD.DateFilter(_month, _year, a.Month, a.Year, false));

            float total = query1.Sum(a => a.Expense);
            var models = query1.ToList();
            foreach (var internationalMobilePhoneCall in query)
            {
                internationalMobilePhoneCall.PerCent = (internationalMobilePhoneCall.Expense / total) * 100;
            }
            //
            Tuple<bool, string>[] show = SD.Show(new List<string>() { phoneNumberCheck, employeeNameCheck, costCenterNameCheck, costCenterCodeCheck, addresseCheck, monthCheck, yearCheck, expenseCheck, expenseCheck, percentCheck, percentCheck },
                new List<string>() { phoneNumber, employeeName, costCenterName, costCenterCode, addresse, month, year, minExpense, maxExpense, minPercent, maxPercent });
            ViewData["columns"] = show;
            //
            List<InternationalMobilePhoneCall> final_result = new List<InternationalMobilePhoneCall>();

            models = DataFilter<InternationalMobilePhoneCall>.Filter(phoneNumber, (m) => m.PhoneNumber, models).ToList();
            models = DataFilter<InternationalMobilePhoneCall>.Filter(employeeName, (m) => m.EmployeeName, models).ToList();
            models = DataFilter<InternationalMobilePhoneCall>.Filter(costCenterName, (m) => m.CostCenterName, models).ToList();
            models = DataFilter<InternationalMobilePhoneCall>.Filter(costCenterCode, (m) => m.CostCenterCode, models).ToList();
            models = DataFilter<InternationalMobilePhoneCall>.Filter(addresse, (m) => m.Addresse, models).ToList();
            models = DataFilter<InternationalMobilePhoneCall>.Filter(_minExpense, _maxExpense, (m) => m.Expense, models).ToList();
            models = DataFilter<InternationalMobilePhoneCall>.Filter(_minPercent, _maxPercent, (m) => m.PerCent, models).ToList();

            //separar en paginas
            models = models.OrderBy(m => m.CostCenterCode).ThenBy(m => m.EmployeeName).ToList();
            var result = Paging<InternationalMobilePhoneCall>.Pages(models, page, cpage, (next != null), (previous != null));

            ViewData["top"] = result.Item2;
            ViewData["mult"] = result.Item3;
            ViewData["page"] = result.Item4;
            bool[] mask = { phoneNumberCheck != null, employeeNameCheck != null, costCenterNameCheck != null, costCenterCodeCheck != null, addresseCheck != null, monthCheck != null, yearCheck != null, expenseCheck != null, false, percentCheck != null, false };
            string csv = CSVStringConstructor(show, mask, result.Item1);
            string HttpSessionName = SD.HttpSessionString(new List<string> { "InternationalMobilePhoneCall", result.Item4.ToString(), phoneNumber, employeeName, costCenterName, costCenterCode, addresse, month, year, minExpense, maxExpense, minPercent, maxPercent,
                                                                               (phoneNumberCheck != null).ToString(), (employeeNameCheck != null).ToString(), (costCenterNameCheck != null).ToString(), (costCenterCodeCheck != null).ToString(), (addresseCheck != null).ToString(), (monthCheck !=null).ToString(), (yearCheck != null).ToString(), (expenseCheck != null).ToString(), (percentCheck != null).ToString() });
            HttpContext.Session.SetString(HttpSessionName, csv);

            return View(result.Item1);

            return View(query);
        }
        public string CSVStringConstructor(Tuple<bool, string>[] show, bool[] mask, List<InternationalMobilePhoneCall> data)
        {
            List<string> headers = new List<string>();

            int t = 0;

            for (int j = 0; j < show.Length; j++)
            {
                if (mask[j])
                {
                    headers.Add(SD.internationalCalls[j]);
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
                if (show[3].Item1)
                {
                    row.Add(item.CostCenterCode);
                }
                if (show[2].Item1)
                {
                    row.Add(item.CostCenterName);
                }
                if (show[4].Item1)
                {
                    row.Add(item.Addresse);
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
                    row.Add(item.Expense.ToString());
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

        public async Task<IActionResult> Export(int page, string phoneNumber, string employeeName, string costCenterName, string costCenterCode,
            string addresse, string month, string year, string minExpense, string maxExpense, string minPercent, string maxPercent,
            string phoneNumberCheck, string employeeNameCheck, string costCenterNameCheck, string costCenterCodeCheck,
            string addresseCheck, string expenseCheck, string percentCheck, string monthCheck, string yearCheck)
        {

            string webRootPath = _hostingEnviroment.WebRootPath;
            var uploads = Path.Combine(webRootPath, "ExportFiles");
            var time = System.DateTime.Now.Day.ToString() + '-' + System.DateTime.Now.Month.ToString() + '-' + System.DateTime.Now.Year.ToString() + ' ' + System.DateTime.Now.Hour.ToString() + '-' + System.DateTime.Now.ToString() + '-' + System.DateTime.Now.Second.ToString();
            var path = Path.Combine(uploads, "InternationalMobilePhoneCalls " + time + ".csv");
            using (var filesStream = new FileStream(path, FileMode.Create))
            {

            }
            StreamWriter stw = new StreamWriter(Path.Combine(uploads, "InternationalMobilePhoneCalls " + time + ".csv"));
            string HttpSessionName = SD.HttpSessionString(new List<string> { "InternationalMobilePhoneCall", page.ToString(), phoneNumber, employeeName, costCenterName, costCenterCode, addresse, month, year, minExpense, maxExpense, minPercent, maxPercent,
                                                                               phoneNumberCheck.ToString(), employeeNameCheck.ToString(), costCenterNameCheck.ToString(), costCenterCodeCheck.ToString(), addresseCheck.ToString(), monthCheck.ToString(), yearCheck.ToString(), expenseCheck.ToString(), percentCheck.ToString() });
            string csv = HttpContext.Session.GetString(HttpSessionName);
            stw.Write(csv);
            stw.Dispose();
            return RedirectToAction(nameof(Index), new
            {
                phoneNumber = phoneNumber,
                employeeName = employeeName,
                costCenterCode = costCenterCode,
                costCenterName = costCenterName,
                addresse = addresse,
                month = month,
                year = year,
                minExpense = minExpense,
                maxExpense = maxExpense,
                minPercent = minPercent,
                maxPercent = maxPercent,
                phoneNumberCheck = phoneNumberCheck == "True" ? "True" : null,
                employeeNameCheck = employeeNameCheck == "True" ? "True" : null,
                costCenterNameCheck = costCenterNameCheck == "True" ? "True" : null,
                costCenterCodeCheck = costCenterCodeCheck == "True" ? "True" : null,
                addresseCheck = addresseCheck == "True" ? "True" : null,
                expenseCheck = expenseCheck == "True" ? "True" : null,
                percentCheck = percentCheck == "True" ? "True" : null,
                monthCheck = monthCheck == "True" ? "True" : null,
                yearCheck = yearCheck == "True" ? "True" : null,
                cpage = page
            });
        }

        public bool Checking(string addresse, int _month, int _year, int month, int year)
        {
            bool a1 = !((addresse[0] == '5' && addresse.Length == 8));
            bool a2 = !ProvinceFilter(addresse);
            bool a3 = SD.DateFilter(_month, _year, new int[] { month }, new int[] { year }, false);
            return ((a1 && a2) && a3);
        }
        public bool ProvinceFilter(string s)
        {
            foreach (var code in SD.ProvinceCode)
            {
                var l = code.Length;
                if (s.Substring(0, l) == code)
                {
                    if (s.Substring(l).Length == 7)
                        return true;
                }
            }
            return false;
        }

    }
}