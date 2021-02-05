using System;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repo;
using System.Collections.Generic;
using MVCPhoneServiceWeb.Utils;

namespace MVCPhoneServiceWeb.Controllers
{
    public class DataPlanAssignmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DataPlanAssignmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MobilePhoneDataPlanAssignments
        public async Task<IActionResult> Index(int cpage, string phoneNumber, string month, string year, string dataPlanId, 
            string pNCheck, string monthCheck, string yearCheck, string dPCheck, 
            string page, string next, string previous)
        {
            var _month = (month != null && SD.months.ContainsKey(month)) ? SD.months[month] : -1;
            if (_month == -1)
                _month = Parse.IntTryParse(month);
            var _year = Parse.IntTryParse(year);
            //
            Tuple<bool, string>[] show = SD.Show(new List<string>() { pNCheck, monthCheck, yearCheck, dPCheck }, new List<string>() { phoneNumber, month, year, dataPlanId });
            ViewData["columns"] = show;
            //
            var dpa = await _context.DataPlanAssignments.Include(m => m.DataPlan).Include(m => m.PhoneLine).ToListAsync();
            //IEnumerable<CallingPlan> callingPlans = await _context.CallingPlans.ToListAsync();
            List<DataPlanAssignment> _dpa = dpa.ToList();
            List<DataPlanAssignment> final_result = new List<DataPlanAssignment>();

            var phoneNumberList = (phoneNumber != null) ? phoneNumber.Split(", ").ToList() : new List<string>();
            _dpa = DataFilter<DataPlanAssignment>.Filter(phoneNumberList, (m) => m.PhoneNumber, _dpa).ToList();

            _dpa = DataFilter<DataPlanAssignment>.Filter(_month, (m) => m.Month, _dpa).ToList();
            _dpa = DataFilter<DataPlanAssignment>.Filter(_year, (m) => m.Year, _dpa).ToList();

            var dataPlanIdList = (dataPlanId != null) ? dataPlanId.Split(", ").ToList() : new List<string>();
            _dpa = DataFilter<DataPlanAssignment>.Filter(dataPlanIdList, (m) => m.DataPlanId, _dpa, true).ToList();

            //Separar en paginas
            _dpa = _dpa.OrderBy(m => m.Year).ThenBy(m => m.Month).ToList();
            var result = Paging<DataPlanAssignment>.Pages(_dpa, page, cpage, (next != null), (previous != null));

            ViewData["top"] = result.Item2;
            ViewData["mult"] = result.Item3;
            ViewData["page"] = result.Item4;

            return View(result.Item1);
        }

        // GET: MobilePhoneDataPlanAssignments/Details/5
        public async Task<IActionResult> Details(string phoneNumber, string month, string year, string dataPlanId)
        {
            if (phoneNumber == null || month == null || year == null || dataPlanId ==null)
            {
                return NotFound();
            }

            //
            var _month = (month != null && SD.months.ContainsKey(month)) ? SD.months[month] : -1;
            if (_month == -1)
                _month = Parse.IntTryParse(month);
            var _year = Parse.IntTryParse(year);
            //
            var dataPlanAssignment = await _context.DataPlanAssignments
                .Include(m => m.DataPlan)
                .Include(m => m.PhoneLine)
                .FirstOrDefaultAsync(m => m.PhoneNumber == phoneNumber && m.Month == _month && m.Year == _year && m.DataPlanId==dataPlanId);
            if (dataPlanAssignment == null)
            {
                return NotFound();
            }

            return View(dataPlanAssignment);
        }

        // GET: MobilePhoneDataPlanAssignments/Create
        public IActionResult Create()
        {
            ViewData["DataPlanId"] = new SelectList(_context.DataPlans, "DataPlanId", "DataPlanId");
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber"); 
            List<MonthSelect> months = new List<MonthSelect>() { new MonthSelect("enero", 1), new MonthSelect("febrero", 2), new MonthSelect("marzo", 3),
                new MonthSelect("abril", 4), new MonthSelect("mayo", 5), new MonthSelect("junio", 6), new MonthSelect("julio", 7), new MonthSelect("agosto", 8),
                new MonthSelect("septiembre", 9), new MonthSelect("octubre", 10), new MonthSelect("noviembre", 11), new MonthSelect("diciembre", 12) };
            ViewData["Month"] = new SelectList(months, "MonthNumber", "Name");
            return View();
        }

        // POST: MobilePhoneDataPlanAssignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhoneNumber,Month,Year,DataPlanId,NationalDataUsage,InternationalDataUsage")] DataPlanAssignment dataPlanAssignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dataPlanAssignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { pNCheck = "On", monthCheck = "On", yearCheck = "On", dPCheck = "On" });
            }
            ViewData["DataPlanId"] = new SelectList(_context.DataPlans, "DataPlanId", "DataPlanId", dataPlanAssignment.DataPlanId);
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", dataPlanAssignment.PhoneNumber);
            List<MonthSelect> months = new List<MonthSelect>() { new MonthSelect("enero", 1), new MonthSelect("febrero", 2), new MonthSelect("marzo", 3),
                new MonthSelect("abril", 4), new MonthSelect("mayo", 5), new MonthSelect("junio", 6), new MonthSelect("julio", 7), new MonthSelect("agosto", 8),
                new MonthSelect("septiembre", 9), new MonthSelect("octubre", 10), new MonthSelect("noviembre", 11), new MonthSelect("diciembre", 12) };
            ViewData["Month"] = new SelectList(months, "MonthNumber", "Name");
            return View(dataPlanAssignment);
        }

        // GET: MobilePhoneDataPlanAssignments/Edit/5
        public async Task<IActionResult> Edit(int? phoneNumber, string month, string year, string dataPlanId)
        {
            if (phoneNumber == null || month == null || year == null || dataPlanId == null)
            {
                return NotFound();
            }

            //
            var _month = (month != null && SD.months.ContainsKey(month)) ? SD.months[month] : -1;
            if (_month == -1)
                _month = Parse.IntTryParse(month);
            var _year = Parse.IntTryParse(year);
            //
            var dataPlanAssignment = await _context.DataPlanAssignments.FindAsync(phoneNumber,month,year,dataPlanId);
            if (dataPlanAssignment == null)
            {
                return NotFound();
            }
            ViewData["DataPlanId"] = new SelectList(_context.DataPlans, "DataPlanId", "DataPlanId", dataPlanAssignment.DataPlanId);
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", dataPlanAssignment.PhoneNumber);
            List<MonthSelect> months = new List<MonthSelect>() { new MonthSelect("enero", 1), new MonthSelect("febrero", 2), new MonthSelect("marzo", 3),
                new MonthSelect("abril", 4), new MonthSelect("mayo", 5), new MonthSelect("junio", 6), new MonthSelect("julio", 7), new MonthSelect("agosto", 8),
                new MonthSelect("septiembre", 9), new MonthSelect("octubre", 10), new MonthSelect("noviembre", 11), new MonthSelect("diciembre", 12) };
            ViewData["Month"] = new SelectList(months, "MonthNumber", "Name");
            return View(dataPlanAssignment);
        }

        // POST: MobilePhoneDataPlanAssignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("PhoneNumber,Month,Year,DataPlanId,NationalDataUsage,InternationalDataUsage")] DataPlanAssignment dataPlanAssignment)
        {
            if (DataPlanAssignmentExists(dataPlanAssignment))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dataPlanAssignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataPlanAssignmentExists(dataPlanAssignment))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { pNCheck = "On", monthCheck = "On", yearCheck = "On", dPCheck = "On" });
            }
            ViewData["DataPlanId"] = new SelectList(_context.DataPlans, "DataPlanId", "DataPlanId", dataPlanAssignment.DataPlanId);
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", dataPlanAssignment.PhoneNumber);
            List<MonthSelect> months = new List<MonthSelect>() { new MonthSelect("enero", 1), new MonthSelect("febrero", 2), new MonthSelect("marzo", 3),
                new MonthSelect("abril", 4), new MonthSelect("mayo", 5), new MonthSelect("junio", 6), new MonthSelect("julio", 7), new MonthSelect("agosto", 8),
                new MonthSelect("septiembre", 9), new MonthSelect("octubre", 10), new MonthSelect("noviembre", 11), new MonthSelect("diciembre", 12) };
            ViewData["Month"] = new SelectList(months, "MonthNumber", "Name");
            return View(dataPlanAssignment);
        }

        // GET: MobilePhoneDataPlanAssignments/Delete/5
        public async Task<IActionResult> Delete( string phoneNumber, string month, string year, string dataPlanId)
        {
            if (phoneNumber == null || month == null || year == null || dataPlanId == null)
            {
                return NotFound();
            }

            //
            var _month = (month != null && SD.months.ContainsKey(month)) ? SD.months[month] : -1;
            if (_month == -1)
                _month = Parse.IntTryParse(month);
            var _year = Parse.IntTryParse(year);
            //
            var dataPlanAssignment = await _context.DataPlanAssignments
                .Include(m => m.DataPlan)
                .Include(m => m.PhoneLine)
                .FirstOrDefaultAsync(m => m.PhoneNumber == phoneNumber && m.Month == _month && m.Year == _year && m.DataPlanId==dataPlanId);
            if (dataPlanAssignment == null)
            {
                return NotFound();
            }

            return View(dataPlanAssignment);
        }

        // POST: MobilePhoneDataPlanAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("PhoneNumber,Month,Year,DataPlanId,NationalDataUsage,InternationalDataUsage")] DataPlanAssignment dataPlanAssignment)
        {
            var _DataPlanAssignment = await _context.DataPlanAssignments.FindAsync(dataPlanAssignment.PhoneNumber,dataPlanAssignment.Month, dataPlanAssignment.Year,dataPlanAssignment.DataPlanId);
            _context.DataPlanAssignments.Remove(_DataPlanAssignment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { pNCheck = "On", monthCheck = "On", yearCheck = "On", dPCheck = "On" });
        }

        private bool DataPlanAssignmentExists(DataPlanAssignment m)
        {
            return _context.DataPlanAssignments.Any(e => e.PhoneNumber == m.PhoneNumber &&
                                                                    e.DataPlanId==m.DataPlanId &&
                                                                    e.Month==m.Month && e.Year == m.Year);
        }
    }
}
