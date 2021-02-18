using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Models;
using Repo;
using MVCPhoneServiceWeb.Utils;

namespace MVCPhoneServiceWeb.Controllers
{
    public class SMSPlanAssignmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SMSPlanAssignmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SMSPlanAssignments
        public async Task<IActionResult> Index(int cpage, string phoneNumber, string month, string year, string smsPlan,
            string pNCheck, string monthCheck, string yearCheck, string sPCheck, 
            string page, string next, string previous)
        {

            //
            var _month = (month != null && SD.months.ContainsKey(month)) ? SD.months[month] : -1;
            if (_month == -1)
                _month = Parse.IntTryParse(month);
            var _year = Parse.IntTryParse(year);
            if (_month != -1)
                month = SD.Months[_month];
            //
            Tuple<bool, string>[] show = SD.Show(new List<string>() { pNCheck, monthCheck, yearCheck, sPCheck }, new List<string>() { phoneNumber, month, year, smsPlan });
            ViewData["columns"] = show;
            // Filters
            var spa = await _context.SmsPlanAssignments.Include(s => s.PhoneLine).Include(s => s.SmsPlan).ToListAsync();
            List<SMSPlanAssignment> _spa = spa.ToList();
            List<SMSPlanAssignment> final_result = new List<SMSPlanAssignment>();

            var phoneNumberList = (phoneNumber != null) ? phoneNumber.Split(", ").ToList() : new List<string>();
            _spa = DataFilter<SMSPlanAssignment>.Filter(phoneNumberList, (m) => m.PhoneNumber, _spa).ToList();

            var smsPlanList = (smsPlan != null) ? smsPlan.Split(", ").ToList() : new List<string>();
            _spa = DataFilter<SMSPlanAssignment>.Filter(smsPlanList, (m) => m.SMSPlanId, _spa, true).ToList();

            _spa = DataFilter<SMSPlanAssignment>.Filter(_year, (m) => m.Year, _spa).ToList();
            _spa = DataFilter<SMSPlanAssignment>.Filter(_month, (m) => m.Month, _spa).ToList();

            //Separar en paginas
            _spa = _spa.OrderBy(m => m.Year).ThenBy(m => m.Month).ToList();
            var result = Paging<SMSPlanAssignment>.Pages(_spa, page, cpage, (next != null), (previous != null));

            ViewData["top"] = result.Item2;
            ViewData["mult"] = result.Item3;
            ViewData["page"] = result.Item4;

            return View(result.Item1);
        }

        // GET: SMSPlanAssignments/Details/5 
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sMSPlanAssignment = await _context.SmsPlanAssignments
                .Include(s => s.PhoneLine)
                .Include(s => s.SmsPlan)
                .FirstOrDefaultAsync(m => m.PhoneNumber == id);
            if (sMSPlanAssignment == null)
            {
                return NotFound();
            }

            return View(sMSPlanAssignment);
        }

        // GET: SMSPlanAssignments/Create
        public IActionResult Create()
        {
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber");
            ViewData["SMSPlanId"] = new SelectList(_context.SmsPlans, "SMSPlanId", "SMSPlanId");
            List<MonthSelect> months = new List<MonthSelect>() { new MonthSelect("enero", 1), new MonthSelect("febrero", 2), new MonthSelect("marzo", 3),
                new MonthSelect("abril", 4), new MonthSelect("mayo", 5), new MonthSelect("junio", 6), new MonthSelect("julio", 7), new MonthSelect("agosto", 8),
                new MonthSelect("septiembre", 9), new MonthSelect("octubre", 10), new MonthSelect("noviembre", 11), new MonthSelect("diciembre", 12) };
            ViewData["Month"] = new SelectList(months, "MonthNumber", "Name");
            return View();
        }

        // POST: SMSPlanAssignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhoneNumber,SMSPlanId,Month,Year")] SMSPlanAssignment sMSPlanAssignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sMSPlanAssignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { pNCheck = "On", monthCheck = "On", yearCheck = "On", sPCheck = "On" });
            }
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", sMSPlanAssignment.PhoneNumber);
            ViewData["SMSPlanId"] = new SelectList(_context.SmsPlans, "SMSPlanId", "SMSPlanId", sMSPlanAssignment.SMSPlanId);
            List<MonthSelect> months = new List<MonthSelect>() { new MonthSelect("enero", 1), new MonthSelect("febrero", 2), new MonthSelect("marzo", 3),
                new MonthSelect("abril", 4), new MonthSelect("mayo", 5), new MonthSelect("junio", 6), new MonthSelect("julio", 7), new MonthSelect("agosto", 8),
                new MonthSelect("septiembre", 9), new MonthSelect("octubre", 10), new MonthSelect("noviembre", 11), new MonthSelect("diciembre", 12) };
            ViewData["Month"] = new SelectList(months, "MonthNumber", "Name");
            return View(sMSPlanAssignment);
        }

        // GET: SMSPlanAssignments/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sMSPlanAssignment = await _context.SmsPlanAssignments
                .Include(s => s.PhoneLine)
                .Include(s => s.SmsPlan)
                .FirstOrDefaultAsync(m => m.PhoneNumber == id);
            if (sMSPlanAssignment == null)
            {
                return NotFound();
            }
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", sMSPlanAssignment.PhoneNumber);
            ViewData["SMSPlanId"] = new SelectList(_context.SmsPlans, "SMSPlanId", "SMSPlanId", sMSPlanAssignment.SMSPlanId);
            List<MonthSelect> months = new List<MonthSelect>() { new MonthSelect("enero", 1), new MonthSelect("febrero", 2), new MonthSelect("marzo", 3),
                new MonthSelect("abril", 4), new MonthSelect("mayo", 5), new MonthSelect("junio", 6), new MonthSelect("julio", 7), new MonthSelect("agosto", 8),
                new MonthSelect("septiembre", 9), new MonthSelect("octubre", 10), new MonthSelect("noviembre", 11), new MonthSelect("diciembre", 12) };
            ViewData["Month"] = new SelectList(months, "MonthNumber", "Name");
            return View(sMSPlanAssignment);
        }

        // POST: SMSPlanAssignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PhoneNumber,SMSPlanId,Month,Year")] SMSPlanAssignment sMSPlanAssignment)
        {
            if (id != sMSPlanAssignment.PhoneNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sMSPlanAssignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SMSPlanAssignmentExists(sMSPlanAssignment.PhoneNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { pNCheck = "On", monthCheck = "On", yearCheck = "On", sPCheck = "On" });
            }
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", sMSPlanAssignment.PhoneNumber);
            ViewData["SMSPlanId"] = new SelectList(_context.SmsPlans, "SMSPlanId", "SMSPlanId", sMSPlanAssignment.SMSPlanId);
            List<MonthSelect> months = new List<MonthSelect>() { new MonthSelect("enero", 1), new MonthSelect("febrero", 2), new MonthSelect("marzo", 3),
                new MonthSelect("abril", 4), new MonthSelect("mayo", 5), new MonthSelect("junio", 6), new MonthSelect("julio", 7), new MonthSelect("agosto", 8),
                new MonthSelect("septiembre", 9), new MonthSelect("octubre", 10), new MonthSelect("noviembre", 11), new MonthSelect("diciembre", 12) };
            ViewData["Month"] = new SelectList(months, "MonthNumber", "Name");
            return View(sMSPlanAssignment);
        }

        // GET: SMSPlanAssignments/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sMSPlanAssignment = await _context.SmsPlanAssignments
                .Include(s => s.PhoneLine)
                .Include(s => s.SmsPlan)
                .FirstOrDefaultAsync(m => m.PhoneNumber == id);
            if (sMSPlanAssignment == null)
            {
                return NotFound();
            }

            return View(sMSPlanAssignment);
        }

        // POST: SMSPlanAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sMSPlanAssignment = await _context.SmsPlanAssignments
                .Include(s => s.PhoneLine)
                .Include(s => s.SmsPlan)
                .FirstOrDefaultAsync(m => m.PhoneNumber == id);
            _context.SmsPlanAssignments.Remove(sMSPlanAssignment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { pNCheck = "On", monthCheck = "On", yearCheck = "On", sPCheck = "On" });
        }

        private bool SMSPlanAssignmentExists(string id)
        {
            return _context.SmsPlanAssignments.Any(e => e.PhoneNumber == id);
        }
    }
}
