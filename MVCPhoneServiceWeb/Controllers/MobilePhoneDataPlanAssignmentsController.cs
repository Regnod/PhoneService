using System;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MVCPhoneServiceWeb.Controllers
{
    public class MobilePhoneDataPlanAssignmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MobilePhoneDataPlanAssignmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MobilePhoneDataPlanAssignments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MobilePhoneDataPlanAssignments.Include(m => m.DataPlan).Include(m => m.PhoneLine);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MobilePhoneDataPlanAssignments/Details/5
        public async Task<IActionResult> Details(int? phoneNumber,DateTime? dateTime,int? dataPlanId)
        {
            if (phoneNumber == null || dateTime==null || dataPlanId==null)
            {
                return NotFound();
            }

            var mobilePhoneDataPlanAssignment = await _context.MobilePhoneDataPlanAssignments
                .Include(m => m.DataPlan)
                .Include(m => m.PhoneLine)
                .FirstOrDefaultAsync(m => m.PhoneNumber == phoneNumber && m.DataPlanAssignmentDateTime==dateTime && m.DataPlanId==dataPlanId);
            if (mobilePhoneDataPlanAssignment == null)
            {
                return NotFound();
            }

            return View(mobilePhoneDataPlanAssignment);
        }

        // GET: MobilePhoneDataPlanAssignments/Create
        public IActionResult Create()
        {
            ViewData["DataPlanId"] = new SelectList(_context.DataPlans, "DataPlanId", "DataPlanId");
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber");
            return View();
        }

        // POST: MobilePhoneDataPlanAssignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhoneNumber,DataPlanAssignmentDateTime,DataPlanId,NationalDataUsage,InternationalDataUsage")] MobilePhoneDataPlanAssignment mobilePhoneDataPlanAssignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mobilePhoneDataPlanAssignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DataPlanId"] = new SelectList(_context.DataPlans, "DataPlanId", "DataPlanId", mobilePhoneDataPlanAssignment.DataPlanId);
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", mobilePhoneDataPlanAssignment.PhoneNumber);
            return View(mobilePhoneDataPlanAssignment);
        }

        // GET: MobilePhoneDataPlanAssignments/Edit/5
        public async Task<IActionResult> Edit(int? phoneNumber,DateTime? dateTime,int? dataPlanId)
        {
            if (phoneNumber == null || dateTime==null || dataPlanId==null)
            {
                return NotFound();
            }

            var mobilePhoneDataPlanAssignment = await _context.MobilePhoneDataPlanAssignments.FindAsync(phoneNumber,dateTime,dataPlanId);
            if (mobilePhoneDataPlanAssignment == null)
            {
                return NotFound();
            }
            ViewData["DataPlanId"] = new SelectList(_context.DataPlans, "DataPlanId", "DataPlanId", mobilePhoneDataPlanAssignment.DataPlanId);
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", mobilePhoneDataPlanAssignment.PhoneNumber);
            return View(mobilePhoneDataPlanAssignment);
        }

        // POST: MobilePhoneDataPlanAssignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("PhoneNumber,DataPlanAssignmentDateTime,DataPlanId,NationalDataUsage,InternationalDataUsage")] MobilePhoneDataPlanAssignment mobilePhoneDataPlanAssignment)
        {
            if (MobilePhoneDataPlanAssignmentExists(mobilePhoneDataPlanAssignment))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mobilePhoneDataPlanAssignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MobilePhoneDataPlanAssignmentExists(mobilePhoneDataPlanAssignment))
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
            ViewData["DataPlanId"] = new SelectList(_context.DataPlans, "DataPlanId", "DataPlanId", mobilePhoneDataPlanAssignment.DataPlanId);
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", mobilePhoneDataPlanAssignment.PhoneNumber);
            return View(mobilePhoneDataPlanAssignment);
        }

        // GET: MobilePhoneDataPlanAssignments/Delete/5
        public async Task<IActionResult> Delete( int? phoneNumber,DateTime? dateTime,int? dataPlanId)
        {
            if (phoneNumber == null || dateTime==null || dataPlanId==null)
            {
                return NotFound();
            }

            var mobilePhoneDataPlanAssignment = await _context.MobilePhoneDataPlanAssignments
                .Include(m => m.DataPlan)
                .Include(m => m.PhoneLine)
                .FirstOrDefaultAsync(m => m.PhoneNumber == phoneNumber && m.DataPlanAssignmentDateTime==dateTime && m.DataPlanId==dataPlanId);
            if (mobilePhoneDataPlanAssignment == null)
            {
                return NotFound();
            }

            return View(mobilePhoneDataPlanAssignment);
        }

        // POST: MobilePhoneDataPlanAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("PhoneNumber,DataPlanAssignmentDateTime,DataPlanId,NationalDataUsage,InternationalDataUsage")] MobilePhoneDataPlanAssignment mobilePhoneDataPlanAssignment)
        {
            var _mobilePhoneDataPlanAssignment = await _context.MobilePhoneDataPlanAssignments.FindAsync(mobilePhoneDataPlanAssignment.PhoneNumber,mobilePhoneDataPlanAssignment.DataPlanAssignmentDateTime,mobilePhoneDataPlanAssignment.DataPlanId);
            _context.MobilePhoneDataPlanAssignments.Remove(_mobilePhoneDataPlanAssignment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MobilePhoneDataPlanAssignmentExists(MobilePhoneDataPlanAssignment m)
        {
            return _context.MobilePhoneDataPlanAssignments.Any(e => e.PhoneNumber == m.PhoneNumber &&
                                                                    e.DataPlanId==m.DataPlanId &&
                                                                    e.DataPlanAssignmentDateTime==m.DataPlanAssignmentDateTime &&
                                                                    e.InternationalDataUsage==m.InternationalDataUsage &&
                                                                    e.NationalDataUsage==m.NationalDataUsage);
        }
    }
}
