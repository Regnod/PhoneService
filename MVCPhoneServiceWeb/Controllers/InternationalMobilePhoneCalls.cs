using System;
using System.Linq;
using System.Threading.Tasks;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Repo;
using MVCPhoneServiceWeb.Utils;
using System.Collections.Generic;

namespace MVCPhoneServiceWeb.Controllers
{
    public class InternationalMobilePhoneCalls : Controller
    {
        private ApplicationDbContext _context;

        public InternationalMobilePhoneCalls(ApplicationDbContext context)
        {
            _context = context;
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
                new List<string>() { phoneNumber, employeeName, costCenterName, costCenterCode, addresse, month, year, minExpense, maxExpense, minPercent, maxPercent, });
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

            return View(result.Item1);

            return View(query);
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