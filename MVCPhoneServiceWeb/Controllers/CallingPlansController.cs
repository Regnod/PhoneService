using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repo;
using MVCPhoneServiceWeb.Utils;
using System;

namespace MVCPhoneServiceWeb.Controllers
{
    public class CallingPlansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CallingPlansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CallingPlans
        public async Task<IActionResult> Index(int cpage, string callingPlanId, string minutes, string minCost, string maxCost,
            string cPCheck, string minutesCheck, string costCheck, 
            string page, string next, string previous)
        {
            // var _callingPlanId = Utils.Utils.IntTryParse(callingPlanId);
            var _minutes = Parse.IntTryParse(minutes);
            var _minCost = Parse.FloatTryParse(minCost);
            var _maxCost = Parse.FloatTryParse(maxCost);

            // para setear propiedades en la vista
            Tuple<bool, string>[] show = SD.Show(new List<string>() { cPCheck, minutesCheck, costCheck, costCheck }, new List<string>() { callingPlanId, minutes, minCost, maxCost });
            ViewData["columns"] = show;
            //
            IEnumerable<CallingPlan> callingPlans = await _context.CallingPlans.ToListAsync();
            List<CallingPlan> _callingPlans = callingPlans.ToList();
            List<CallingPlan> final_result = new List<CallingPlan>();

            var callingPlanIdList = (callingPlanId != null) ? callingPlanId.Split(", ").ToList() : new List<string>();
            _callingPlans = DataFilter<CallingPlan>.Filter(callingPlanIdList, (m) => m.CallingPlanId, _callingPlans, true).ToList();

            _callingPlans = DataFilter<CallingPlan>.Filter(_minutes, (m) => m.Minutes, _callingPlans).ToList();

            _callingPlans = DataFilter<CallingPlan>.Filter(_minCost, _maxCost, (m) => m.Cost, _callingPlans).ToList();

            //Separar en paginas
            _callingPlans = _callingPlans.OrderBy(m => m.CallingPlanId).ToList();
            var result = Paging<CallingPlan>.Pages(_callingPlans, page, cpage, (next != null), (previous != null));

            ViewData["top"] = result.Item2;
            ViewData["mult"] = result.Item3;
            ViewData["page"] = result.Item4;

            return View(result.Item1);
        }

        // GET: CallingPlans/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var callingPlan = await _context.CallingPlans
                .FirstOrDefaultAsync(m => m.CallingPlanId == id);
            if (callingPlan == null)
            {
                return NotFound();
            }

            return View(callingPlan);
        }

        // GET: CallingPlans/Create
        public IActionResult Create()
        {
            // new { cPCheck = "On", minutesCheck = "On", costCheck = "On"}
            return View();
        }

        // POST: CallingPlans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CallingPlanId,Minutes,Messages")] CallingPlan callingPlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(callingPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { cPCheck = "On", minutesCheck = "On", costCheck = "On" });
            }
            return View(callingPlan);
        }

        // GET: CallingPlans/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var callingPlan = await _context.CallingPlans
                .FirstOrDefaultAsync(m => m.CallingPlanId == id);
            if (callingPlan == null)
            {
                return NotFound();
            }
            return View(callingPlan);
        }

        // POST: CallingPlans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CallingPlanId,Minutes,Messages")] CallingPlan callingPlan)
        {
            //if (id != callingPlan.CallingPlanId)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                var cp = await _context.CallingPlans.FindAsync(id);
                try
                {
                    //_context.Update(costCenter);
                    _context.CallingPlans.Remove(cp);
                    _context.Add(callingPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CallingPlanExists(callingPlan.CallingPlanId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { cPCheck = "On", minutesCheck = "On", costCheck = "On" });
            }
            return View(callingPlan);
        }

        // GET: CallingPlans/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var callingPlan = await _context.CallingPlans
                .FirstOrDefaultAsync(m => m.CallingPlanId == id);
            if (callingPlan == null)
            {
                return NotFound();
            }

            return View(callingPlan);
        }

        // POST: CallingPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var callingPlan = await _context.CallingPlans
                .FirstOrDefaultAsync(m => m.CallingPlanId == id);
            _context.CallingPlans.Remove(callingPlan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { cPCheck = "On", minutesCheck = "On", costCheck = "On" });
        }

        private bool CallingPlanExists(string id)
        {
            return _context.CallingPlans.Any(e => e.CallingPlanId == id);
        }
    }
}
