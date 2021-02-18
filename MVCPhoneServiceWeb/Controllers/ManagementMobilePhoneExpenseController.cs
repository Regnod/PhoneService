using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPhoneServiceWeb.Utils;
using Repo;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace MVCPhoneServiceWeb.Controllers
{
    public class ManagementMobilePhoneExpenseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnviroment;

        public ManagementMobilePhoneExpenseController(ApplicationDbContext context, IHostingEnvironment hostingEnviroment)
        {
            _context = context;
            _hostingEnviroment = hostingEnviroment;
        }

        public async Task<IActionResult> Index(int cpage, string management,
            string minCalls, string maxCalls, string minSms, string maxSms, string minGprs, string maxGprs, string minTotal, string maxTotal, string minPercent, string maxPercent,
            string managementCheck, string callsCheck, string smsCheck, string gprsCheck, string totalCheck, string percentCheck, string monthCheck, string yearCheck, string month, string year,
            string page, string previous, string next)
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

            var models = new List<ManagementsMobilePhoneExpense>();

            if (_year == -1 && _month != -1)
            {
                await Task.Delay(TimeSpan.FromSeconds(3));
                var query1 = (from pls in _context.PhoneLineSummaries
                              join ple in _context.PhoneLineEmployees on pls.PhoneNumber equals ple.PhoneNumber
                              join e in _context.Employees on ple.EmployeeId equals e.EmployeeId
                              join cc in _context.CostCenters on e.CostCenterCode equals cc.Code
                              join m in _context.Managements on cc.ManagementId equals m.ManagementId
                              where pls.Month == _month
                              select new
                              {
                                  ManagementId = m.ManagementId,
                                  ManagementName = m.Name,
                                  Calls = pls.AirTime,
                                  SMS = pls.SmsExpenses + pls.RoamingSmsExpenses,
                                  GPRS = pls.GprsExpenses + pls.RoamingGprsExpenses,
                                  Month = pls.Month,
                                  Year = pls.Year,
                                  Total = pls.Total
                              }
                ).AsEnumerable();
                query1 = query1.ToList();
                var query2 = query1.GroupBy(a => a.ManagementId);
                float total = 0;
                foreach (var _management in query2)
                {
                    var model = new ManagementsMobilePhoneExpense()
                    {
                        Management = _management.Select(a => a.ManagementName).First(),
                        Calls = _management.Sum(a => a.Calls),
                        GPRS = _management.Sum(a => a.GPRS),
                        SMS = _management.Sum(a => a.SMS),
                        Total = _management.Sum(a => a.Total),
                        Month = _management.Select(a => a.Month).First(),
                        Year = _management.Select(a => a.Year).First(),
                        Percent = 0
                    };
                    total += model.Total;
                    models.Add(model);
                }
                foreach (var model in models)
                {
                    model.Percent = 100 * (model.Total / total);
                }
            }
            else if (_month != -1 && _year != -1)
            {
                await Task.Delay(TimeSpan.FromSeconds(3));
                var query1 = (from pls in _context.PhoneLineSummaries
                              join ple in _context.PhoneLineEmployees on pls.PhoneNumber equals ple.PhoneNumber
                              join e in _context.Employees on ple.EmployeeId equals e.EmployeeId
                              join cc in _context.CostCenters on e.CostCenterCode equals cc.Code
                              join m in _context.Managements on cc.ManagementId equals m.ManagementId
                              where pls.Month == _month && pls.Year == _year
                              select new
                              {
                                  ManagementId = m.ManagementId,
                                  ManagementName = m.Name,
                                  Calls = pls.AirTime,
                                  SMS = pls.SmsExpenses + pls.RoamingSmsExpenses,
                                  GPRS = pls.GprsExpenses + pls.RoamingGprsExpenses,
                                  Month = pls.Month,
                                  Year = pls.Year,
                                  Total = pls.Total
                              }
                ).AsEnumerable();
                query1 = query1.ToList();
                var query2 = query1.GroupBy(a => a.ManagementId);
                float total = 0;
                foreach (var _management in query2)
                {
                    var model = new ManagementsMobilePhoneExpense()
                    {
                        Management = _management.Select(a => a.ManagementName).First(),
                        Calls = _management.Sum(a => a.Calls),
                        GPRS = _management.Sum(a => a.GPRS),
                        SMS = _management.Sum(a => a.SMS),
                        Total = _management.Sum(a => a.Total),
                        Month = _management.Select(a => a.Month).First(),
                        Year = _management.Select(a => a.Year).First(),
                        Percent = 0
                    };
                    total += model.Total;
                    models.Add(model);
                }
                foreach (var model in models)
                {
                    model.Percent = 100 * (model.Total / total);
                }
            }
            else if (_month == -1 && _year != -1)
            {

                await Task.Delay(TimeSpan.FromSeconds(3));
                var query1 = (from pls in _context.PhoneLineSummaries
                              join ple in _context.PhoneLineEmployees on pls.PhoneNumber equals ple.PhoneNumber
                              join e in _context.Employees on ple.EmployeeId equals e.EmployeeId
                              join cc in _context.CostCenters on e.CostCenterCode equals cc.Code
                              join m in _context.Managements on cc.ManagementId equals m.ManagementId
                              where pls.Year == _year
                              select new
                              {
                                  ManagementId = m.ManagementId,
                                  ManagementName = m.Name,
                                  Calls = pls.AirTime,
                                  SMS = pls.SmsExpenses + pls.RoamingSmsExpenses,
                                  GPRS = pls.GprsExpenses + pls.RoamingGprsExpenses,
                                  Month = pls.Month,
                                  Year = pls.Year,
                                  Total = pls.Total
                              }
                ).AsEnumerable();
                query1 = query1.ToList();
                var query2 = query1.GroupBy(a => a.ManagementId);
                float total = 0;
                foreach (var _management in query2)
                {
                    var model = new ManagementsMobilePhoneExpense()
                    {
                        Management = _management.Select(a => a.ManagementName).First(),
                        Calls = _management.Sum(a => a.Calls),
                        GPRS = _management.Sum(a => a.GPRS),
                        SMS = _management.Sum(a => a.SMS),
                        Total = _management.Sum(a => a.Total),
                        Month = _management.Select(a => a.Month).First(),
                        Year = _management.Select(a => a.Year).First(),
                        Percent = 0
                    };
                    total += model.Total;
                    models.Add(model);
                }
                foreach (var model in models)
                {
                    model.Percent = 100 * (model.Total / total);
                }
            }
            else
            {
                _month = DateTime.Now.Month;
                _year = DateTime.Now.Year;
                await Task.Delay(TimeSpan.FromSeconds(3));
                var query1 = (from pls in _context.PhoneLineSummaries
                              join ple in _context.PhoneLineEmployees on pls.PhoneNumber equals ple.PhoneNumber
                              join e in _context.Employees on ple.EmployeeId equals e.EmployeeId
                              join cc in _context.CostCenters on e.CostCenterCode equals cc.Code
                              join m in _context.Managements on cc.ManagementId equals m.ManagementId
                              where pls.Month == _month && pls.Year == _year
                              select new
                              {
                                  ManagementId = m.ManagementId,
                                  ManagementName = m.Name,
                                  Calls = pls.AirTime,
                                  SMS = pls.SmsExpenses + pls.RoamingSmsExpenses,
                                  GPRS = pls.GprsExpenses + pls.RoamingGprsExpenses,
                                  Month = pls.Month,
                                  Year = pls.Year,
                                  Total = pls.Total
                              }
                ).AsEnumerable();
                query1 = query1.ToList();
                var query2 = query1.GroupBy(a => a.ManagementId);
                float total = 0;
                foreach (var _management in query2)
                {
                    var model = new ManagementsMobilePhoneExpense()
                    {
                        Management = _management.Select(a => a.ManagementName).First(),
                        Calls = _management.Sum(a => a.Calls),
                        GPRS = _management.Sum(a => a.GPRS),
                        SMS = _management.Sum(a => a.SMS),
                        Total = _management.Sum(a => a.Total),
                        Month = _management.Select(a => a.Month).First(),
                        Year = _management.Select(a => a.Year).First(),
                        Percent = 0
                    };
                    total += model.Total;
                    models.Add(model);
                }
                foreach (var model in models)
                {
                    model.Percent = 100 * (model.Total / total);
                }
            }
            //
            Tuple<bool, string>[] show = SD.Show(new List<string>() { managementCheck, callsCheck, callsCheck, smsCheck, smsCheck, gprsCheck, gprsCheck, totalCheck, totalCheck, percentCheck, percentCheck, monthCheck, yearCheck },
                                                    new List<string>() { management, minCalls, maxCalls, minSms, maxSms, minGprs, maxGprs, minTotal, maxTotal, minPercent, maxPercent, month, year });
                                                    
            //
            List<ManagementsMobilePhoneExpense> final_result = new List<ManagementsMobilePhoneExpense>();

            if (management != null)
            {
                foreach (var cp in models)
                {
                    if (cp.Management.ToLower().Contains(management.ToLower()))
                        final_result.Add(cp);

                }
                models = final_result;
                final_result = new List<ManagementsMobilePhoneExpense>();
            }
            #region filter Min max
            // calls
            if (_minCalls != -1 && _maxCalls != -1)
            {
                foreach (var cp in models)
                {
                    if (_minCalls <= cp.Calls && cp.Calls <= _maxCalls)
                        final_result.Add(cp);

                }
                models = final_result;
                final_result = new List<ManagementsMobilePhoneExpense>();
            }
            if (_minCalls == -1 && _maxCalls != -1)
            {
                foreach (var cp in models)
                {
                    if (cp.Calls <= _maxCalls)
                        final_result.Add(cp);

                }
                models = final_result;
                final_result = new List<ManagementsMobilePhoneExpense>();
            }
            if (_minCalls != -1 && _maxCalls == -1)
            {
                foreach (var cp in models)
                {
                    if (_minCalls <= cp.Calls)
                        final_result.Add(cp);

                }
                models = final_result;
                final_result = new List<ManagementsMobilePhoneExpense>();
            }
            // sms
            if (_minSms != -1 && _maxSms != -1)
            {
                foreach (var cp in models)
                {
                    if (_minSms <= cp.SMS && cp.SMS <= _maxSms)
                        final_result.Add(cp);

                }
                models = final_result;
                final_result = new List<ManagementsMobilePhoneExpense>();
            }
            if (_minSms == -1 && _maxSms != -1)
            {
                foreach (var cp in models)
                {
                    if (cp.SMS <= _maxSms)
                        final_result.Add(cp);

                }
                models = final_result;
                final_result = new List<ManagementsMobilePhoneExpense>();
            }
            if (_minSms != -1 && _maxSms == -1)
            {
                foreach (var cp in models)
                {
                    if (_minSms <= cp.SMS)
                        final_result.Add(cp);

                }
                models = final_result;
                final_result = new List<ManagementsMobilePhoneExpense>();
            }
            // gprs
            if (_minGprs != -1 && _maxGprs != -1)
            {
                foreach (var cp in models)
                {
                    if (_minGprs <= cp.GPRS && cp.GPRS <= _maxGprs)
                        final_result.Add(cp);

                }
                models = final_result;
                final_result = new List<ManagementsMobilePhoneExpense>();
            }
            if (_minGprs == -1 && _maxGprs != -1)
            {
                foreach (var cp in models)
                {
                    if (cp.GPRS <= _maxGprs)
                        final_result.Add(cp);

                }
                models = final_result;
                final_result = new List<ManagementsMobilePhoneExpense>();
            }
            if (_minGprs != -1 && _maxGprs == -1)
            {
                foreach (var cp in models)
                {
                    if (_minGprs <= cp.GPRS)
                        final_result.Add(cp);

                }
                models = final_result;
                final_result = new List<ManagementsMobilePhoneExpense>();
            }
            // total
            if (_minTotal != -1 && _maxTotal != -1)
            {
                foreach (var cp in models)
                {
                    if (_minTotal <= cp.Total && cp.Total <= _maxTotal)
                        final_result.Add(cp);

                }
                models = final_result;
                final_result = new List<ManagementsMobilePhoneExpense>();
            }
            if (_minTotal == -1 && _maxTotal != -1)
            {
                foreach (var cp in models)
                {
                    if (cp.Total <= _maxTotal)
                        final_result.Add(cp);

                }
                models = final_result;
                final_result = new List<ManagementsMobilePhoneExpense>();
            }
            if (_minTotal != -1 && _maxTotal == -1)
            {
                foreach (var cp in models)
                {
                    if (_minTotal <= cp.Total)
                        final_result.Add(cp);

                }
                models = final_result;
                final_result = new List<ManagementsMobilePhoneExpense>();
            }
            //
            #endregion
            //separar en paginas
            List<List<ManagementsMobilePhoneExpense>> pages = new List<List<ManagementsMobilePhoneExpense>>();
            List<ManagementsMobilePhoneExpense> list = new List<ManagementsMobilePhoneExpense>();
            int i = 0;
            int j = 0;
            foreach (var item in models)
            {
                if (i == 20)
                {
                    j++;
                    i = 0;
                    pages.Add(list);
                    list = new List<ManagementsMobilePhoneExpense>();
                }
                list.Add(item);
                i++;
            }
            if (i < 20)
            {
                pages.Add(list);
                j++;
            }
            // elegir pagina
            int currentPage = 0;
            if (page != null)
            {
                currentPage = (Parse.IntTryParse(page) != -1) ? Parse.IntTryParse(page) - 1 : (cpage >= j) ? 0 : cpage;
            }
            else if (next != null)
                currentPage = (cpage + 1 >= j) ? cpage : cpage + 1;
            else if (previous != null)
                currentPage = (cpage - 1 < 0) ? 0 : cpage - 1;

            int mult = currentPage / 20;

            if (j > 20 && j - currentPage < 20)
                ViewData["top"] = j - currentPage;
            else if (j < 20 && j - currentPage < 20)
                ViewData["top"] = j;
            else
                ViewData["top"] = 20;

            ViewData["mult"] = mult;
            ViewData["columns"] = show;
            //return View(_mobilePhoneCalls);
            
            //Arreglar la parte de paginado y cambiar el 2do parametro de csvstringconstructor x result.item1 y poner las 2 instucciones del csv aqui

            ViewData["page"] = currentPage;
            if (pages.Count != 0)
            {
                if (j > currentPage)
                {
                    string ss = CSVStringConstructor(show, pages[currentPage]);
                    ViewData["csv"] = ss;
                    ViewData["page"] = currentPage;
                    return View(pages[currentPage]);
                }
                else
                {
                    if (j >= cpage)
                    {
                        string ss = CSVStringConstructor(show, pages[cpage]);
                        ViewData["csv"] = ss;
                        ViewData["page"] = cpage;
                        return View(pages[cpage]);
                    }
                    else
                    {
                        cpage = 0;
                        string ss = CSVStringConstructor(show, pages[0]);
                        ViewData["csv"] = ss;
                        ViewData["page"] = cpage;
                        return View(pages[0]);
                    }
                }
            }
            else
                return View(new List<ManagementsMobilePhoneExpense>());
        }

        public string CSVStringConstructor(Tuple<bool, string>[] show, List<ManagementsMobilePhoneExpense> data)
        {
            List<string> headers = new List<string>();

            int t = 0;

            for (int j = 0; j < show.Length; j++)
            {
                if (show[j].Item1)
                {
                    headers.Add(SD.managementMobilePhoneExpenses[j]);
                }
            }

            List<List<string>> datas = new List<List<string>>();

            foreach (var item in data)
            {
                List<string> row = new List<string>();
                if (show[0].Item1)
                {
                    row.Add(item.Management);
                }
                if (show[1].Item1)
                {
                    row.Add(item.Calls.ToString());
                }
                if (show[3].Item1)
                {
                    row.Add(item.SMS.ToString());
                }
                if (show[5].Item1)
                {
                    row.Add(item.GPRS.ToString());
                }
                if (show[7].Item1)
                {
                    row.Add(item.Total.ToString());
                }
                if (show[9].Item1)
                {
                    row.Add(item.Percent.ToString());
                }
                if (show[11].Item1)
                {
                    row.Add(SD.Months[item.Month]);
                }
                if (show[12].Item1)
                {
                    row.Add(item.Year.ToString());
                }
                datas.Add(row);
            }

            string csv = SD.csvString(headers, datas);
            return csv;
        }

        public async Task<IActionResult> Export(int page, string management,
            string minCalls, string maxCalls, string minSms, string maxSms, string minGprs, string maxGprs, string minTotal, string maxTotal, string minPercent, string maxPercent,
            string managementCheck, string callsCheck, string smsCheck, string gprsCheck, string totalCheck, string percentCheck, string monthCheck, string yearCheck, string month, string year, string csv)
        {

            string webRootPath = _hostingEnviroment.WebRootPath;
            var uploads = Path.Combine(webRootPath, "ExportFiles");
            var path = Path.Combine(uploads, "callingPlanAssignment.csv");
            using (var filesStream = new FileStream(path, FileMode.Create))
            {

            }
            StreamWriter stw = new StreamWriter(Path.Combine(uploads, "callingPlanAssignment.csv"));
            stw.Write(csv);
            stw.Dispose();
            return RedirectToAction(nameof(Index), new
            {
                management = management,
                minCalls = minCalls,
                maxCalls = maxCalls,
                minSms = minSms,
                maxSms = maxSms,
                minGprs = minGprs,
                maxGprs = maxGprs,
                minTotal = minTotal,
                maxTotal = maxTotal,
                minPercent = minPercent,
                maxPercent = maxPercent,
                month = month,
                year = year,
                managementCheck = managementCheck,
                callsCheck = callsCheck,
                smsCheck = smsCheck,
                gprsCheck = gprsCheck,
                totalCheck = totalCheck,
                percentCheck = percentCheck,
                monthCheck = monthCheck,
                yearCheck = yearCheck,
                cpage = page
            });
        }

        Tuple<bool, string>[] Show(string management, string minCalls, string maxCalls, string minSms, string maxSms,
            string minGprs, string maxGprs, string minTotal, string maxTotal, string minPercent, string maxPercent,
            string managementCheck, string callsCheck, string smsCheck, string gprsCheck,
            string totalCheck, string percentCheck, string monthCheck, string yearCheck, string month, string year)
        {
            var _month = (month != null && SD.months.ContainsKey(month)) ? SD.months[month] : -1;
            if (_month == -1)
                _month = Parse.IntTryParse(month);
            var _year = Parse.IntTryParse(year);
            //
            Tuple<bool, string>[] show = new Tuple<bool, string>[13];
            var check1 = (management != null) ? true : false;
            var check2 = (callsCheck != null) ? true : false;
            var check21 = (callsCheck != null) ? true : false;
            var check3 = (smsCheck != null) ? true : false;
            var check31 = (smsCheck != null) ? true : false;
            var check4 = (gprsCheck != null) ? true : false;
            var check41 = (gprsCheck != null) ? true : false;
            var check5 = (totalCheck != null) ? true : false;
            var check51 = (totalCheck != null) ? true : false;
            var check6 = (percentCheck != null) ? true : false;
            var check61 = (percentCheck != null) ? true : false;
            var check7 = (monthCheck != null) ? true : false;
            var check8 = (yearCheck != null) ? true : false;

            var filter1 = (management != null) ? management : null;
            var filter2 = (minCalls != null) ? minCalls : null;
            var filter21 = (maxCalls != null) ? maxCalls : null;
            var filter3 = (minSms != null) ? minSms : null;
            var filter31 = (maxSms != null) ? maxSms : null;
            var filter4 = (minGprs != null) ? minGprs : null;
            var filter41 = (maxGprs != null) ? maxGprs : null;
            var filter5 = (minTotal != null) ? minTotal : null;
            var filter51 = (maxTotal != null) ? maxTotal : null;
            var filter6 = (minPercent != null) ? minPercent : null;
            var filter61 = (maxPercent != null) ? maxPercent : null;
            var filter7 = (_month != -1) ? month : null;
            var filter8 = (_year != -1) ? year : null;

            show[0] = new Tuple<bool, string>(check1, filter1);
            show[1] = new Tuple<bool, string>(check2, filter2);
            show[2] = new Tuple<bool, string>(check21, filter21);
            show[3] = new Tuple<bool, string>(check3, filter3);
            show[4] = new Tuple<bool, string>(check31, filter31);
            show[5] = new Tuple<bool, string>(check4, filter4);
            show[6] = new Tuple<bool, string>(check41, filter41);
            show[7] = new Tuple<bool, string>(check5, filter5);
            show[8] = new Tuple<bool, string>(check51, filter51);
            show[9] = new Tuple<bool, string>(check6, filter6);
            show[10] = new Tuple<bool, string>(check61, filter61);
            show[11] = new Tuple<bool, string>(check7, filter7);
            show[12] = new Tuple<bool, string>(check8, filter8);
            //
            return show;
        }
    }
}