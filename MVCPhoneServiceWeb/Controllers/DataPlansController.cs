using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Index(string dataPlanId,string nationalData,string internationalData)
        {
            var _dataPlanId = Utils.Utils.IntTryParse(dataPlanId);
            var _nationalData = Utils.Utils.IntTryParse(nationalData);
            var _intenationalData = Utils.Utils.IntTryParse(internationalData);

            IEnumerable<DataPlan> dataPlans = await _context.DataPlans.ToListAsync();
            if (_dataPlanId != -1)
            {
                dataPlans = dataPlans.Where(a => a.DataPlanId == _dataPlanId);
            }
            if (_nationalData != -1)
            {
                dataPlans = dataPlans.Where(a => a.NationalData == _nationalData);
            } if (_intenationalData != -1)
            {
                dataPlans = dataPlans.Where(a => a.InternationalData == _intenationalData);
            }
            return View(dataPlans);
        }

        // GET: DataPlans/Details/5
        public async Task<IActionResult> Details(int? id)
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
        public async Task<IActionResult> Create([Bind("DataPlanId,NationalData,InternationalData")] DataPlan dataPlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dataPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dataPlan);
        }

        // GET: DataPlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataPlan = await _context.DataPlans.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("DataPlanId,NationalData,InternationalData")] DataPlan dataPlan)
        {
            if (id != dataPlan.DataPlanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dataPlan);
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
                return RedirectToAction(nameof(Index));
            }
            return View(dataPlan);
        }

        // GET: DataPlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataPlan = await _context.DataPlans.FindAsync(id);
            _context.DataPlans.Remove(dataPlan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DataPlanExists(int id)
        {
            return _context.DataPlans.Any(e => e.DataPlanId == id);
        }
    }
}
