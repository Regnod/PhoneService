using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Models;
using Repo;

namespace MVCPhoneServiceWeb.Controllers
{
    public class GPRSController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GPRSController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GPRS
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.GPRSs.Include(g => g.PhoneLine);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: GPRS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gPRS = await _context.GPRSs
                .Include(g => g.PhoneLine)
                .FirstOrDefaultAsync(m => m.GPRSId == id);
            if (gPRS == null)
            {
                return NotFound();
            }

            return View(gPRS);
        }

        // GET: GPRS/Create
        public IActionResult Create()
        {
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber");
            return View();
        }

        // POST: GPRS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GPRSId,PhoneNumber,DateTime,Location,APN,Volume,VolFact,Amount,Discount,Charge,Total,Roaming")] GPRS gPRS)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gPRS);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", gPRS.PhoneNumber);
            return View(gPRS);
        }

        // GET: GPRS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gPRS = await _context.GPRSs.FindAsync(id);
            if (gPRS == null)
            {
                return NotFound();
            }
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", gPRS.PhoneNumber);
            return View(gPRS);
        }

        // POST: GPRS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GPRSId,PhoneNumber,DateTime,Location,APN,Volume,VolFact,Amount,Discount,Charge,Total,Roaming")] GPRS gPRS)
        {
            //if (id != gPRS.GPRSId)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                var gprs = await _context.GPRSs.FindAsync(id);
                try
                {
                    //_context.Update(costCenter);
                    _context.GPRSs.Remove(gprs);
                    _context.Add(gPRS);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GPRSExists(gPRS.GPRSId))
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
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", gPRS.PhoneNumber);
            return View(gPRS);
        }

        // GET: GPRS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gPRS = await _context.GPRSs
                .Include(g => g.PhoneLine)
                .FirstOrDefaultAsync(m => m.GPRSId == id);
            if (gPRS == null)
            {
                return NotFound();
            }

            return View(gPRS);
        }

        // POST: GPRS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gPRS = await _context.GPRSs.FindAsync(id);
            _context.GPRSs.Remove(gPRS);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GPRSExists(int id)
        {
            return _context.GPRSs.Any(e => e.GPRSId == id);
        }
    }
}
