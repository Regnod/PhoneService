using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repo;
using System.Collections.Generic;
using MVCPhoneServiceWeb.Utils;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace MVCPhoneServiceWeb.Controllers
{
    public class MobilePhoneCallsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnviroment;

        public MobilePhoneCallsController(ApplicationDbContext context, IHostingEnvironment hostingEnviroment)
        {
            _context = context;
            _hostingEnviroment = hostingEnviroment;
        }

        // GET: MobilePhoneCalls
        public async Task<IActionResult> Index(int cpage, string phoneNumber, string day, string month, string year, string address, string min, string max, string roaming,
            string phoneNumberCheck, string dateTimeCheck, string addressCheck, string totalCostCheck, string roamingCheck,
            string page, string next, string previous)
        {
            var _min = Parse.FloatTryParse(min);
            var _max = Parse.FloatTryParse(max);
            var _day = Parse.IntTryParse(day);
            var _month = (month != null && SD.months.ContainsKey(month)) ? SD.months[month] : -1;
            if (_month == -1)
                _month = Parse.IntTryParse(month);
            var _year = Parse.IntTryParse(year);
            //
            Tuple<bool, string>[] show = SD.Show(new List<string>() { phoneNumberCheck, dateTimeCheck, dateTimeCheck, dateTimeCheck, addressCheck, totalCostCheck, totalCostCheck, roamingCheck }, new List<string>() { phoneNumber, day, month, year, address, min, max, roaming });
            ViewData["columns"] = show;
            //     
            var mobilePhoneCalls = await _context.MobilePhoneCalls.Include(m => m.PhoneLine).ToListAsync();
            List<MobilePhoneCall> _mobilePhoneCalls = mobilePhoneCalls.ToList();
            List<MobilePhoneCall> final_result = new List<MobilePhoneCall>();

            var phoneNumberList = (phoneNumber != null) ? phoneNumber.Split(", ").ToList() : new List<string>();
            _mobilePhoneCalls = DataFilter<MobilePhoneCall>.Filter(phoneNumberList, (m) => m.PhoneNumber, _mobilePhoneCalls).ToList();

            _mobilePhoneCalls = DataFilter<MobilePhoneCall>.Filter(_year, (m) => m.DateTime.Year, _mobilePhoneCalls).ToList();
            _mobilePhoneCalls = DataFilter<MobilePhoneCall>.Filter(_month, (m) => m.DateTime.Month, _mobilePhoneCalls).ToList();
            _mobilePhoneCalls = DataFilter<MobilePhoneCall>.Filter(_day, (m) => m.DateTime.Day, _mobilePhoneCalls).ToList();

            var addressList = (address != null) ? address.Split(", ").ToList() : new List<string>();
            _mobilePhoneCalls = DataFilter<MobilePhoneCall>.Filter(addressList, (m) => m.Addressee, _mobilePhoneCalls, true).ToList();

            _mobilePhoneCalls = DataFilter<MobilePhoneCall>.Filter(_min, _max, (m) => m.TotalCost, _mobilePhoneCalls).ToList();

            _mobilePhoneCalls = DataFilter<MobilePhoneCall>.Filter(roaming, (m) => m.RoamingCall, _mobilePhoneCalls).ToList();

            //Separar en paginas
            _mobilePhoneCalls = _mobilePhoneCalls.OrderBy(m => m.DateTime.Year).ThenBy(m => m.DateTime.Month).ToList();
            var result = Paging <MobilePhoneCall >.Pages(_mobilePhoneCalls, page, cpage, (next != null), (previous != null));

            ViewData["top"] = result.Item2;
            ViewData["mult"] = result.Item3;
            ViewData["page"] = result.Item4;
            bool[] mask = { phoneNumberCheck != null, dateTimeCheck != null, false, false, addressCheck != null, totalCostCheck != null, false, roamingCheck != null };
            string csv = CSVStringConstructor(show, mask, result.Item1);
            string HttpSessionName = SD.HttpSessionString(new List<string> { "MobilePhoneCall", result.Item4.ToString(), phoneNumber, day, month, year, address, min, max, roaming,
                                                                               (phoneNumberCheck != null).ToString(), (dateTimeCheck != null).ToString(), (addressCheck != null).ToString(), (totalCostCheck != null).ToString(), (roamingCheck != null).ToString() });
            HttpContext.Session.SetString(HttpSessionName, csv);

            return View(result.Item1);
        }

        public string CSVStringConstructor(Tuple<bool, string>[] show, bool[] mask, List<MobilePhoneCall> data)
        {
            List<string> headers = new List<string>();

            int t = 0;

            for (int j = 0; j < show.Length; j++)
            {
                if (mask[j])
                {
                    headers.Add(SD.mobilePhoneCalls[j]);
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
                    row.Add(item.DateTime.ToString());
                }
                if (show[4].Item1)
                {
                    row.Add(item.Addressee);
                }
                if (show[5].Item1)
                {
                    row.Add(item.TotalCost.ToString());
                }
                if (show[7].Item1)
                {
                    row.Add(item.RoamingCall.ToString());
                }
                datas.Add(row);
            }

            string csv = SD.csvString(headers, datas);
            return csv;
        }

        public async Task<IActionResult> Export(int page, string phoneNumber, string day, string month, string year, string address, string min, string max, string roaming,
            string phoneNumberCheck, string dateTimeCheck, string addressCheck, string totalCostCheck, string roamingCheck)
        {

            string webRootPath = _hostingEnviroment.WebRootPath;
            var time = System.DateTime.Now.Day.ToString() + '-' + System.DateTime.Now.Month.ToString() + '-' + System.DateTime.Now.Year.ToString() + ' ' + System.DateTime.Now.Hour + '-' + System.DateTime.Now.Minute + '-' + System.DateTime.Now.Second;
            var uploads = Path.Combine(webRootPath, "ExportFiles");
            var path = Path.Combine(uploads, "MobilePhoneCalls " + time + ".csv");
            using (var filesStream = new FileStream(path, FileMode.Create))
            {

            }
            StreamWriter stw = new StreamWriter(Path.Combine(uploads, "MobilePhoneCalls " + time + ".csv"));
            string HttpSessionName = SD.HttpSessionString(new List<string> { "MobilePhoneCall", page.ToString(), phoneNumber, day, month, year, address, min, max, roaming,
                                                                               phoneNumberCheck.ToString(), dateTimeCheck.ToString(), addressCheck.ToString(), totalCostCheck.ToString(), roamingCheck.ToString() });
            string csv = HttpContext.Session.GetString(HttpSessionName);
            stw.Write(csv);
            stw.Dispose();
            return RedirectToAction(nameof(Index), new
            {
                phoneNumber = phoneNumber,
                day = day,
                month = month,
                year = year,
                address = address,
                min = min,
                max = max,
                roaming = roaming,
                phoneNumberCheck = phoneNumberCheck == "True" ? "True" : null,
                dateTimeCheck = dateTimeCheck == "True" ? "True" : null,
                addressCheck = addressCheck == "True" ? "True" : null,
                totalCostCheck = totalCostCheck == "True" ? "True" : null,
                roamingCheck = roamingCheck == "True" ? "True" : null,
                cpage = page
            });
        }

        // GET: MobilePhoneCalls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobilePhoneCall = await _context.MobilePhoneCalls
                .Include(m => m.PhoneLine)
                .FirstOrDefaultAsync(m => m.MobilePhoneCallId == id);
            if (mobilePhoneCall == null)
            {
                return NotFound();
            }

            return View(mobilePhoneCall);
        }

        // GET: MobilePhoneCalls/Create
        public IActionResult Create()
        {
            // ViewData["IMEI"] = new SelectList(_context.MobilePhones, "IMEI", "IMEI");
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber");
            return View();
        }

        // POST: MobilePhoneCalls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhoneNumber,IMEI,DateTime,MobilePhoneCallAddressee,Duration,Cost")] MobilePhoneCall mobilePhoneCall)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mobilePhoneCall);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { phoneNumberCheck = "On", dateTimeCheck = "On", AddressCheck = "On", totalCostCheck = "On", roamingCheck = "On" });
            }

            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", mobilePhoneCall.PhoneNumber);
            return View(mobilePhoneCall);
        }

        // GET: MobilePhoneCalls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobilePhoneCall = await _context.MobilePhoneCalls.FindAsync(id);
            if (mobilePhoneCall == null)
            {
                return NotFound();
            }

            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", mobilePhoneCall.PhoneNumber);
            return View(mobilePhoneCall);
        }

        // POST: MobilePhoneCalls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("PhoneNumber,IMEI,DateTime,MobilePhoneCallAddressee,Duration,Cost")] MobilePhoneCall mobilePhoneCall)
        {

            if (!MobilePhoneCallExists(mobilePhoneCall))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { phoneNumberCheck = "On", dateTimeCheck = "On", AddressCheck = "On", totalCostCheck = "On", roamingCheck = "On" });
            }

            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", mobilePhoneCall.PhoneNumber);
            return View(mobilePhoneCall);
        }

        // GET: MobilePhoneCalls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobilePhoneCall = await _context.MobilePhoneCalls
                .Include(m => m.PhoneLine)
                .FirstOrDefaultAsync(m => m.MobilePhoneCallId == id);
            if (mobilePhoneCall == null)
            {
                return NotFound();
            }

            return View(mobilePhoneCall);
        }

        // POST: MobilePhoneCalls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("PhoneNumber,IMEI,DateTime,MobilePhoneCallAddressee,Duration,Cost")] MobilePhoneCall mobilePhoneCall)
        {
            var _mobilePhoneCall = await _context.MobilePhoneCalls
                   .Include(m => m.PhoneLine)
                   .FirstOrDefaultAsync(m => m.PhoneNumber == mobilePhoneCall.PhoneNumber && m.DateTime == mobilePhoneCall.DateTime);
            //var _mobilePhoneCall = await _context.MobilePhoneCalls.FindAsync(mobilePhoneCall.PhoneNumber, mobilePhoneCall.DateTime);
            _context.MobilePhoneCalls.Remove(_mobilePhoneCall);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { phoneNumberCheck = "On", dateTimeCheck = "On", AddressCheck = "On", totalCostCheck = "On", roamingCheck = "On" });
        }

        private bool MobilePhoneCallExists(MobilePhoneCall m)
        {
            return _context.MobilePhoneCalls.Any(e => e.MobilePhoneCallId == m.MobilePhoneCallId);
        }
    }
}
