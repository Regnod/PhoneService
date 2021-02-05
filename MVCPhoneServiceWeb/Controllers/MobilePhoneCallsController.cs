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

namespace MVCPhoneServiceWeb.Controllers
{
    public class MobilePhoneCallsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MobilePhoneCallsController(ApplicationDbContext context)
        {
            _context = context;
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

            return View(result.Item1);
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
