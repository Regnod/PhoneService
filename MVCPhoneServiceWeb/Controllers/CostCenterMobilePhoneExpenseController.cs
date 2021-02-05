using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Repo;
using MVCPhoneServiceWeb.Utils;

namespace MVCPhoneServiceWeb.Controllers
{
    public class CostCenterMobilePhoneExpenseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CostCenterMobilePhoneExpenseController(ApplicationDbContext context)
        {
            _context = context;
        }
        // TODO: Falta revisar detalles y hacer el de detais
        // GET
        public async Task<IActionResult> Index(int cpage, string costCenterCode, string costCenterName, string month, string year,
            string minCalls, string maxCalls, string minSms, string maxSms, string minGprs, string maxGprs, string minTotal, string maxTotal, string minPercent, string maxPercent,
            string costCenterCodeCheck, string costCenterNameCheck, string callsCheck, string smsCheck, string gprsCheck,
            string totalCheck, string percentCheck, string monthCheck, string yearCheck, string page, string previous, string next)
        {
            var _month = (month != null && SD.months.ContainsKey(month)) ? SD.months[month] : -1;
            if (_month == -1)
                _month = Parse.IntTryParse(month);
            var _year = Parse.IntTryParse(year);
            var _minCalls = Parse.FloatTryParse(minCalls);
            var _maxCalls = Parse.FloatTryParse(maxCalls);
            var _minSms = Parse.FloatTryParse(minSms);
            var _maxSms = Parse.FloatTryParse(maxSms);
            var _minGprs = Parse.FloatTryParse(minGprs);
            var _maxGprs = Parse.FloatTryParse(maxGprs);
            var _minTotal = Parse.FloatTryParse(minTotal);
            var _maxTotal = Parse.FloatTryParse(maxTotal);
            var _minPercent = Parse.FloatTryParse(minPercent);
            var _maxPercent = Parse.FloatTryParse(maxPercent);

            var models = new List<CostCenterMobilePhoneExpense>();
            await Task.Delay(TimeSpan.FromSeconds(3));
            var query1 = (from pls in _context.PhoneLineSummaries
                          join ple in _context.PhoneLineEmployees on pls.PhoneNumber equals ple.PhoneNumber
                          join e in _context.Employees on ple.EmployeeId equals e.EmployeeId
                          join cc in _context.CostCenters on e.CostCenterCode equals cc.Code
                          where pls.Month == _month
                          select new
                          {
                              CostCenterName = cc.Name,
                              CostCenterCode = cc.Code,
                              Calls = pls.AirTime,
                              SMS = pls.SmsExpenses + pls.RoamingSmsExpenses,
                              GPRS = pls.GprsExpenses + pls.RoamingGprsExpenses,
                              Month = pls.Month,
                              Year = pls.Year,
                              Total = pls.Total
                          }).ToList().Where(a => SD.DateFilter(_month, _year, new int[] { a.Month}, new int[]{ a.Year }, true));
            var query11 = query1.ToList();
            var query2 = query11.GroupBy(a => a.CostCenterCode);
            float total = 0;
            foreach (var costCenter in query2)
            {
                var newModel = new CostCenterMobilePhoneExpense()
                {
                    CostCenterCode = costCenter.Key,
                    CostCenterName = costCenter.Select(a => a.CostCenterName).First(),
                    Calls = costCenter.Sum(a => a.Calls),
                    SMS = costCenter.Sum(a => a.SMS),
                    GPRS = costCenter.Sum(a => a.GPRS),
                    Total = costCenter.Sum(a => a.Total),
                    Month = costCenter.Select(a => a.Month).First(),
                    Year = costCenter.Select(a => a.Year).First(),
                    Percent = 0,
                };
                total += newModel.Total;
                models.Add(newModel);
            }
            foreach (var model in models)
            {
                model.Percent = (100 * (model.Total / total));
            }
            //
            Tuple<bool, string>[] show = SD.Show(new List<string>() { costCenterCodeCheck, costCenterNameCheck, callsCheck, callsCheck, smsCheck, smsCheck, gprsCheck, gprsCheck, totalCheck, totalCheck, percentCheck, percentCheck, monthCheck, yearCheck },
                new List<string>() { costCenterCode, costCenterName, minCalls, maxCalls, minSms, maxSms, minGprs, maxGprs, minTotal, maxTotal, minPercent, maxPercent, month, year });
            ViewData["columns"] = show;
            //

            models = DataFilter<CostCenterMobilePhoneExpense>.Filter(costCenterCode, (m) => m.CostCenterCode, models).ToList();
            models = DataFilter<CostCenterMobilePhoneExpense>.Filter(costCenterName, (m) => m.CostCenterName, models).ToList();
            models = DataFilter<CostCenterMobilePhoneExpense>.Filter(_minCalls, _maxCalls, (m) => m.Calls, models).ToList();
            models = DataFilter<CostCenterMobilePhoneExpense>.Filter(_minSms, _maxSms, (m) => m.SMS, models).ToList();
            models = DataFilter<CostCenterMobilePhoneExpense>.Filter(_minGprs, _maxGprs, (m) => m.GPRS, models).ToList();
            models = DataFilter<CostCenterMobilePhoneExpense>.Filter(_minTotal, _maxTotal, (m) => m.Total, models).ToList();
            models = DataFilter<CostCenterMobilePhoneExpense>.Filter(_minPercent, _maxPercent, (m) => m.Percent, models).ToList();

            //separar en paginas
            models = models.OrderBy(m => m.CostCenterCode).ToList();
            var result = Paging<CostCenterMobilePhoneExpense>.Pages(models, page, cpage, (next != null), (previous != null));

            ViewData["top"] = result.Item2;
            ViewData["mult"] = result.Item3;
            ViewData["page"] = result.Item4;

            return View(result.Item1);
        }

        public async Task<IActionResult> Details(int cpage, string employeeName, string phoneNumber, string callingPlanId,
            string minCalls, string maxCalls, string minSms, string maxSms, string minGprs, string maxGprs, string minTotal, string maxTotal,
            string employeeNameCheck, string phoneNumberCheck, string callingPlanIdCheck, string callsCheck, string smsCheck, string gprsCheck,
            string totalCheck, string monthCheck, string yearCheck, string month, string year, string page, string previous, string next, string costCenterCode)
        {
            var _month = (month != null && SD.months.ContainsKey(month)) ? SD.months[month] : -1;
            if (_month == -1)
                _month = Parse.IntTryParse(month);
            var _year = Parse.IntTryParse(year);
            var _minCalls = Parse.FloatTryParse(minCalls);
            var _maxCalls = Parse.FloatTryParse(maxCalls);
            var _minSms = Parse.FloatTryParse(minSms);
            var _maxSms = Parse.FloatTryParse(maxSms);
            var _minGprs = Parse.FloatTryParse(minGprs);
            var _maxGprs = Parse.FloatTryParse(maxGprs);
            var _minTotal = Parse.FloatTryParse(minTotal);
            var _maxTotal = Parse.FloatTryParse(maxTotal);

            var models = new List<CostCenterMobilePhoneExpensesDetails>();
            var _costCenter = await _context.CostCenters.FindAsync(costCenterCode);
            ViewData["code"] = costCenterCode;
            ViewData["costCenterName"] = _costCenter.Name;

            await Task.Delay(TimeSpan.FromSeconds(3));
            var model = (from pls in _context.PhoneLineSummaries
                         join ple in _context.PhoneLineEmployees on pls.PhoneNumber equals ple.PhoneNumber
                         join e in _context.Employees on ple.EmployeeId equals e.EmployeeId
                         join cc in _context.CostCenters on e.CostCenterCode equals cc.Code
                         join cpa in _context.CallingPlanAssignments on pls.PhoneNumber equals cpa.PhoneNumber
                         join cp in _context.CallingPlans on cpa.CallingPlanId equals cp.CallingPlanId
                         where cc.Code == costCenterCode
                         select new 
                         {
                             EmployeeName = e.Name,
                             PhoneNumber = pls.PhoneNumber,
                             CallingPlanId = cp.CallingPlanId,
                             Gprs = pls.RoamingGprsExpenses + pls.GprsExpenses,
                             Minutes = pls.AirTime + pls.RoamingExpenses,
                             SMS = pls.SmsExpenses + pls.RoamingSmsExpenses,
                             Month = pls.Month,
                             Year = pls.Year,
                             cpaMonth = cpa.Month,
                             cpaYear = cpa.Year,
                             Total = pls.Total
                         }).ToList().Where(a => SD.DateFilter(_month, _year, new int[] { a.Month, a.cpaMonth }, new int[] { a.Year, a.cpaYear }, true));

            foreach (var item  in model)
            {
                var newModel = new CostCenterMobilePhoneExpensesDetails()
                {
                    EmployeeName = item.EmployeeName,
                    PhoneNumber = item.PhoneNumber,
                    CallingPlanId = item.CallingPlanId,
                    SMS = item.SMS,
                    Gprs = item.Gprs,
                    Minutes = item.Minutes,
                    Total = item.Total,
                    Month = item.Month,
                    Year = item.Year
                };
                models.Add(newModel);
            }
            //
            Tuple<bool, string>[] show = SD.Show(new List<string>() { employeeNameCheck, phoneNumberCheck, callingPlanIdCheck, callsCheck, callsCheck, smsCheck, smsCheck, gprsCheck, gprsCheck, totalCheck, totalCheck, monthCheck, yearCheck },
                new List<string>() { employeeName, phoneNumber, callingPlanId, minCalls, maxCalls, minSms, maxSms, minGprs, maxGprs, minTotal, maxTotal, month, year });
            ViewData["columns"] = show;
            //
            List<CostCenterMobilePhoneExpensesDetails> final_result = new List<CostCenterMobilePhoneExpensesDetails>();

            var employeeNameList = (employeeName != null) ? employeeName.Split(", ").ToList() : new List<string>();
            models = DataFilter<CostCenterMobilePhoneExpensesDetails>.Filter(employeeNameList, (m) => m.EmployeeName, models, true).ToList();

            var phoneNumberList = (phoneNumber != null) ? phoneNumber.Split(", ").ToList() : new List<string>();
            models = DataFilter<CostCenterMobilePhoneExpensesDetails>.Filter(phoneNumberList, (m) => m.PhoneNumber, models, true).ToList();

            var callingPlanIdList = (callingPlanId != null) ? callingPlanId.Split(", ").ToList() : new List<string>();
            models = DataFilter<CostCenterMobilePhoneExpensesDetails>.Filter(callingPlanIdList, (m) => m.CallingPlanId, models, true).ToList();

            #region filter Min max
            models = DataFilter<CostCenterMobilePhoneExpensesDetails>.Filter(_minCalls, _maxCalls, (m) => m.Minutes, models).ToList();
            models = DataFilter<CostCenterMobilePhoneExpensesDetails>.Filter(_minSms, _maxSms, (m) => m.SMS, models).ToList();
            models = DataFilter<CostCenterMobilePhoneExpensesDetails>.Filter(_minGprs, _maxGprs, (m) => m.Gprs, models).ToList();
            models = DataFilter<CostCenterMobilePhoneExpensesDetails>.Filter(_minTotal, _maxTotal, (m) => m.Total, models).ToList();

            #endregion
            //separar en paginas
            models = models.OrderBy(m => m.EmployeeName).ToList();
            var result = Paging<CostCenterMobilePhoneExpensesDetails>.Pages(models, page, cpage, (next != null), (previous != null));

            ViewData["top"] = result.Item2;
            ViewData["mult"] = result.Item3;
            ViewData["page"] = result.Item4;

            return View(result.Item1);
        }

        public async Task<IActionResult> General(int cpage, string employeeName, string phoneNumber, string callingPlanId, string costCenterCode, string costCenterName,
            string minCalls, string maxCalls, string minSms, string maxSms, string minGprs, string maxGprs, string minTotal, string maxTotal,
            string employeeNameCheck, string phoneNumberCheck, string callingPlanIdCheck, string callsCheck, string smsCheck, string gprsCheck,
            string totalCheck, string costCenterCodeCheck, string costCenterNameCheck, string monthCheck, string yearCheck, string month, string year, string page, string previous, string next)
        {
            var _month = (month != null && SD.months.ContainsKey(month)) ? SD.months[month] : -1;
            if (_month == -1)
                _month = Parse.IntTryParse(month);
            var _year = Parse.IntTryParse(year);
            var _minCalls = Parse.FloatTryParse(minCalls);
            var _maxCalls = Parse.FloatTryParse(maxCalls);
            var _minSms = Parse.FloatTryParse(minSms);
            var _maxSms = Parse.FloatTryParse(maxSms);
            var _minGprs = Parse.FloatTryParse(minGprs);
            var _maxGprs = Parse.FloatTryParse(maxGprs);
            var _minTotal = Parse.FloatTryParse(minTotal);
            var _maxTotal = Parse.FloatTryParse(maxTotal);

            var models = new List<CostCenterMobilePhoneGeneralExpenses>();

            await Task.Delay(TimeSpan.FromSeconds(3));
            var model = (from pls in _context.PhoneLineSummaries
                         join ple in _context.PhoneLineEmployees on pls.PhoneNumber equals ple.PhoneNumber
                         join e in _context.Employees on ple.EmployeeId equals e.EmployeeId
                         join cc in _context.CostCenters on e.CostCenterCode equals cc.Code
                         join cpa in _context.CallingPlanAssignments on pls.PhoneNumber equals cpa.PhoneNumber
                         join cp in _context.CallingPlans on cpa.CallingPlanId equals cp.CallingPlanId
                         select new
                         {
                             EmployeeName = e.Name,
                             PhoneNumber = pls.PhoneNumber,
                             CallingPlanId = cp.CallingPlanId,
                             CostCenterCode = cc.Code,
                             CostCenterName = cc.Name,
                             Gprs = pls.RoamingGprsExpenses + pls.GprsExpenses,
                             Minutes = pls.AirTime + pls.RoamingExpenses,
                             SMS = pls.SmsExpenses + pls.RoamingSmsExpenses,
                             Month = pls.Month,
                             Year = pls.Year,
                             cpaMonth = cpa.Month,
                             cpaYear = cpa.Year,
                             Total = pls.Total
                         }).ToList().Where(a => SD.DateFilter(_month, _year, new int[] { a.Month, a.cpaMonth }, new int[] { a.Year, a.cpaYear }, false, true));

            foreach (var item in model)
            {
                var newModel = new CostCenterMobilePhoneGeneralExpenses()
                {
                    EmployeeName = item.EmployeeName,
                    PhoneNumber = item.PhoneNumber,
                    CallingPlanId = item.CallingPlanId,
                    CostCenterCode = item.CostCenterCode,
                    CostCenterName = item.CostCenterName,
                    SMS = item.SMS,
                    Gprs = item.Gprs,
                    Minutes = item.Minutes,
                    Total = item.Total,
                    Month = item.Month,
                    Year = item.Year
                };
                models.Add(newModel);
            }
            //
            Tuple<bool, string>[] show = SD.Show(new List<string>() { employeeNameCheck, phoneNumberCheck, callingPlanIdCheck, callsCheck, callsCheck, smsCheck, smsCheck, gprsCheck, gprsCheck, totalCheck, totalCheck, monthCheck, yearCheck, costCenterCodeCheck, costCenterNameCheck },
                   new List<string>() { employeeName, phoneNumber, callingPlanId, minCalls, maxCalls, minSms, maxSms, minGprs, maxGprs, minTotal, maxTotal, month, year, costCenterCode, costCenterName });
            ViewData["columns"] = show;
            //
            List<CostCenterMobilePhoneGeneralExpenses> final_result = new List<CostCenterMobilePhoneGeneralExpenses>();

            var employeeNameList = (employeeName != null) ? employeeName.Split(", ").ToList() : new List<string>();
            models = DataFilter<CostCenterMobilePhoneGeneralExpenses>.Filter(employeeNameList, (m) => m.EmployeeName, models, true).ToList();

            var phoneNumberList = (phoneNumber != null) ? phoneNumber.Split(", ").ToList() : new List<string>();
            models = DataFilter<CostCenterMobilePhoneGeneralExpenses>.Filter(phoneNumberList, (m) => m.PhoneNumber, models, true).ToList();

            var callingPlanIdList = (callingPlanId != null) ? callingPlanId.Split(", ").ToList() : new List<string>();
            models = DataFilter<CostCenterMobilePhoneGeneralExpenses>.Filter(callingPlanIdList, (m) => m.CallingPlanId, models, true).ToList();

            #region filter Min max
            models = DataFilter<CostCenterMobilePhoneGeneralExpenses>.Filter(_minCalls, _maxCalls, (m) => m.Minutes, models).ToList();

            models = DataFilter<CostCenterMobilePhoneGeneralExpenses>.Filter(_minSms, _maxSms, (m) => m.SMS, models).ToList();

            models = DataFilter<CostCenterMobilePhoneGeneralExpenses>.Filter(_minGprs, _maxGprs, (m) => m.Gprs, models).ToList();

            models = DataFilter<CostCenterMobilePhoneGeneralExpenses>.Filter(_minTotal, _maxTotal, (m) => m.Total, models).ToList();

            #endregion
            //separar en paginas
            models = models.OrderBy(m => m.Year).ThenBy(m => m.Month).ToList();
            var result = Paging<CostCenterMobilePhoneGeneralExpenses>.Pages(models, page, cpage, (next != null), (previous != null));

            ViewData["top"] = result.Item2;
            ViewData["mult"] = result.Item3;
            ViewData["page"] = result.Item4;

            return View(result.Item1);
        }
    }
}
