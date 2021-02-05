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
    public class DataPlansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DataPlansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DataPlans
        public async Task<IActionResult> Index(int cpage, string dataPlanId, string data, string minCost, string maxCost,
            string dPCheck, string dataCheck, string costCheck,
            string page, string next, string previous)
        {
            int _data = Parse.IntTryParse(data);
            var _minCost = Parse.FloatTryParse(minCost);
            var _maxCost = Parse.FloatTryParse(maxCost);
            //
            Tuple<bool, string>[] show = SD.Show(new List<string>() { dPCheck, dataCheck, costCheck, costCheck }, new List<string>() { dataPlanId, data, minCost, maxCost });
            ViewData["columns"] = show;
            //
            IEnumerable<DataPlan> dataPlans = await _context.DataPlans.ToListAsync();
            List<DataPlan> _dataPlans = dataPlans.ToList();
            List<DataPlan> final_result = new List<DataPlan>();

            var dataPlanIdList = (dataPlanId != null) ? dataPlanId.Split(", ").ToList() : new List<string>();
            _dataPlans = DataFilter<DataPlan>.Filter(dataPlanIdList, (m) => m.DataPlanId, _dataPlans, true).ToList();

            _dataPlans = DataFilter<DataPlan>.Filter(_data, (m) => m.Data, _dataPlans).ToList();

            _dataPlans = DataFilter<DataPlan>.Filter(_minCost, _maxCost, (m) => m.Cost, _dataPlans).ToList();

            //Separar en paginas
            _dataPlans = _dataPlans.OrderBy(m => m.DataPlanId).ToList();
            var result = Paging<DataPlan>.Pages(_dataPlans, page, cpage, (next != null), (previous != null));

            ViewData["top"] = result.Item2;
            ViewData["mult"] = result.Item3;
            ViewData["page"] = result.Item4;

            return View(result.Item1);
        }

        // GET: DataPlans/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataPlan = await _context.DataPlans
                .FirstOrDefaultAsync(m => m.DataPlanId == id);
            if (dataPlan == null)
            {
                return NotFound();
            }

            return View(dataPlan);
        }

        // GET: DataPlans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DataPlans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DataPlanId,Data")] DataPlan dataPlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dataPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { dPCheck = "On", dataCheck = "On"});
            }
            return View(dataPlan);
        }

        // GET: DataPlans/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataPlan = await _context.DataPlans
                .FirstOrDefaultAsync(m => m.DataPlanId == id);
            if (dataPlan == null)
            {
                return NotFound();
            }
            return View(dataPlan);
        }

        // POST: DataPlans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("DataPlanId,Data")] DataPlan dataPlan)
        {
            if (ModelState.IsValid)
            {
                var dp = await _context.DataPlans.FindAsync(id);
                try
                {
                    //_context.Update(costCenter);
                    _context.DataPlans.Remove(dp);
                    _context.Add(dataPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataPlanExists(dataPlan.DataPlanId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { dPCheck = "On", dataCheck = "On" });
            }
            return View(dataPlan);
        }

        // GET: DataPlans/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataPlan = await _context.DataPlans
                .FirstOrDefaultAsync(m => m.DataPlanId == id);
            if (dataPlan == null)
            {
                return NotFound();
            }

            return View(dataPlan);
        }

        // POST: DataPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var dataPlan = await _context.DataPlans
                .FirstOrDefaultAsync(m => m.DataPlanId == id);
            _context.DataPlans.Remove(dataPlan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { dPCheck = "On", dataCheck = "On" });
        }

        private bool DataPlanExists(string id)
        {
            return _context.DataPlans.Any(e => e.DataPlanId == id);
        }
    }
}
