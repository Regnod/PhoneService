using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Office.Interop.Excel;



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
    public async Task<IActionResult> Index(string callingPlanId, string minutes, string messages)
    {
        var _callingPlanId = Utils.Utils.IntTryParse(callingPlanId);
        var _minutes = Utils.Utils.IntTryParse(minutes);
        var _messages = Utils.Utils.IntTryParse(messages);

        IEnumerable<CallingPlan> callingPlansFiltered = await _context.CallingPlans.ToListAsync();
        if (_callingPlanId != -1)
        {
            callingPlansFiltered = callingPlansFiltered.Where(a => a.CallingPlanId == _callingPlanId);
        }
        if (_minutes != -1)
        {
            callingPlansFiltered = callingPlansFiltered.Where(a => a.Minutes == _callingPlanId);
        }
        if (_messages != -1)
        {
            callingPlansFiltered = callingPlansFiltered.Where(a => a.Messages == _messages);
        }

        return View(callingPlansFiltered);
    }

    // GET: CallingPlans/Details/5
    public async Task<IActionResult> Details(int? id)
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
            return RedirectToAction(nameof(Index));
        }
        return View(callingPlan);
    }

    // GET: CallingPlans/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var callingPlan = await _context.CallingPlans.FindAsync(id);
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
    public async Task<IActionResult> Edit(int id, [Bind("CallingPlanId,Minutes,Messages")] CallingPlan callingPlan)
    {
        if (id != callingPlan.CallingPlanId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(callingPlan);
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
            return RedirectToAction(nameof(Index));
        }
        return View(callingPlan);
    }

    // GET: CallingPlans/Delete/5
    public async Task<IActionResult> Delete(int? id)
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
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var callingPlan = await _context.CallingPlans.FindAsync(id);
        _context.CallingPlans.Remove(callingPlan);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CallingPlanExists(int id)
    {
        return _context.CallingPlans.Any(e => e.CallingPlanId == id);
    }
}
}
