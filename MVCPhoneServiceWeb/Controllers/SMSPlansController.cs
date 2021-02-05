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
    public class SMSPlansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SMSPlansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SMSPlans
        public async Task<IActionResult> Index(int cpage, string smsPlanId, string messages, string minCost, string maxCost,
            string sPCheck, string messagesCheck, string costCheck, string page, string next, string previous)
        {
            var _messages = Parse.IntTryParse(messages);
            var _minCost = Parse.FloatTryParse(minCost);
            var _maxCost = Parse.FloatTryParse(maxCost);

            // para setear propiedades en la vista
            Tuple<bool, string>[] show = SD.Show(new List<string>() { sPCheck, messagesCheck, costCheck, costCheck }, new List<string>() { smsPlanId, messages, minCost, maxCost });
            ViewData["columns"] = show;
            //
            IEnumerable<SMSPlan> smsPlans = await _context.SmsPlans.ToListAsync();
            List<SMSPlan> _smsPlans = smsPlans.ToList();
            List<SMSPlan> final_result = new List<SMSPlan>();

            var smsPlanIdList = (smsPlanId != null) ? smsPlanId.Split(", ").ToList() : new List<string>();
            _smsPlans = DataFilter<SMSPlan>.Filter(smsPlanIdList, (m) => m.SMSPlanId, _smsPlans, true).ToList();

            _smsPlans = DataFilter<SMSPlan>.Filter(_messages, (m) => m.Messages, _smsPlans).ToList();

            _smsPlans = DataFilter<SMSPlan>.Filter(_minCost, _maxCost, (m) => m.Cost, _smsPlans).ToList();

            //Separar en paginas
            _smsPlans = _smsPlans.OrderBy(m => m.SMSPlanId).ToList();
            var result = Paging<SMSPlan>.Pages(_smsPlans, page, cpage, (next != null), (previous != null));

            ViewData["top"] = result.Item2;
            ViewData["mult"] = result.Item3;
            ViewData["page"] = result.Item4;

            return View(result.Item1);
        }

        // GET: SMSPlans/Details/5 
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sMSPlan = await _context.SmsPlans
                .FirstOrDefaultAsync(m => m.SMSPlanId == id);
            if (sMSPlan == null)
            {
                return NotFound();
            }

            return View(sMSPlan);
        }

        // GET: SMSPlans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SMSPlans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SMSPlanId,Messages,Cost")] SMSPlan sMSPlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sMSPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { sPcheck = "On", messagescheck = "On", costcheck = "On" });
            }
            return View(sMSPlan);
        }

        // GET: SMSPlans/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sMSPlan = await _context.SmsPlans
                .FirstOrDefaultAsync(m => m.SMSPlanId == id);
            if (sMSPlan == null)
            {
                return NotFound();
            }
            return View(sMSPlan);
        }

        // POST: SMSPlans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SMSPlanId,Messages,Cost")] SMSPlan sMSPlan)
        {
            if (id != sMSPlan.SMSPlanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sMSPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SMSPlanExists(sMSPlan.SMSPlanId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { sPcheck = "On", messagescheck = "On", costcheck = "On" });
            }
            return View(sMSPlan);
        }

        // GET: SMSPlans/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sMSPlan = await _context.SmsPlans
                .FirstOrDefaultAsync(m => m.SMSPlanId == id);
            if (sMSPlan == null)
            {
                return NotFound();
            }

            return View(sMSPlan);
        }

        // POST: SMSPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sMSPlan = await _context.SmsPlans
                .FirstOrDefaultAsync(m => m.SMSPlanId == id);
            _context.SmsPlans.Remove(sMSPlan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { sPcheck = "On", messagescheck = "On", costcheck = "On" });
        }

        private bool SMSPlanExists(string id)
        {
            return _context.SmsPlans.Any(e => e.SMSPlanId == id);
        }
    }
}
