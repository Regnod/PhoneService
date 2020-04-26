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
    public class MobilePhoneCallingPlanAssignmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MobilePhoneCallingPlanAssignmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MobilePhoneCallingPlanAssignments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MobilePhoneCallingPlanAssignments.Include(m => m.CallingPlan).Include(m => m.PhoneLine);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MobilePhoneCallingPlanAssignments/Details/5
        public async Task<IActionResult> Details(int? phoneNumber,DateTime? dateTime,int? callingPlanId)
        {
            if (phoneNumber == null || dateTime==null || callingPlanId ==null)
            {
                return NotFound();
            }

            var mobilePhoneCallingPlanAssignment = await _context.MobilePhoneCallingPlanAssignments
                .Include(m => m.CallingPlan)
                .Include(m => m.PhoneLine)
                .FirstOrDefaultAsync(m => m.PhoneNumber == phoneNumber && m.CallingPlanAssignmentDateTime==dateTime && m.CallingPlanId==callingPlanId);
            if (mobilePhoneCallingPlanAssignment == null)
            {
                return NotFound();
            }

            return View(mobilePhoneCallingPlanAssignment);
        }

        // GET: MobilePhoneCallingPlanAssignments/Create
        public IActionResult Create()
        {
            ViewData["CallingPlanId"] = new SelectList(_context.CallingPlans, "CallingPlanId", "CallingPlanId");
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber");
            return View();
        }

        // POST: MobilePhoneCallingPlanAssignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhoneNumber,CallingPlanAssignmentDateTime,CallingPlanId,MinutesConsumed,MessagesSent")] MobilePhoneCallingPlanAssignment mobilePhoneCallingPlanAssignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mobilePhoneCallingPlanAssignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CallingPlanId"] = new SelectList(_context.CallingPlans, "CallingPlanId", "CallingPlanId", mobilePhoneCallingPlanAssignment.CallingPlanId);
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", mobilePhoneCallingPlanAssignment.PhoneNumber);
            return View(mobilePhoneCallingPlanAssignment);
        }

        // GET: MobilePhoneCallingPlanAssignments/Edit/5
        public async Task<IActionResult> Edit(int? phoneNumber,DateTime? dateTime,int? callingPlanId)
        {
            if (phoneNumber == null || dateTime==null || callingPlanId ==null)
            {
                return NotFound();
            }

            var mobilePhoneCallingPlanAssignment = await _context.MobilePhoneCallingPlanAssignments.FindAsync(phoneNumber,dateTime,callingPlanId);
            if (mobilePhoneCallingPlanAssignment == null)
            {
                return NotFound();
            }
            ViewData["CallingPlanId"] = new SelectList(_context.CallingPlans, "CallingPlanId", "CallingPlanId", mobilePhoneCallingPlanAssignment.CallingPlanId);
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", mobilePhoneCallingPlanAssignment.PhoneNumber);
            return View(mobilePhoneCallingPlanAssignment);
        }

        // POST: MobilePhoneCallingPlanAssignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("PhoneNumber,CallingPlanAssignmentDateTime,CallingPlanId,MinutesConsumed,MessagesSent")] MobilePhoneCallingPlanAssignment mobilePhoneCallingPlanAssignment)
        {
            if (MobilePhoneCallingPlanAssignmentExists( mobilePhoneCallingPlanAssignment))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mobilePhoneCallingPlanAssignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MobilePhoneCallingPlanAssignmentExists(mobilePhoneCallingPlanAssignment))
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
            ViewData["CallingPlanId"] = new SelectList(_context.CallingPlans, "CallingPlanId", "CallingPlanId", mobilePhoneCallingPlanAssignment.CallingPlanId);
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", mobilePhoneCallingPlanAssignment.PhoneNumber);
            return View(mobilePhoneCallingPlanAssignment);
        }

        // GET: MobilePhoneCallingPlanAssignments/Delete/5
        public async Task<IActionResult> Delete(int? phoneNumber,DateTime? dateTime,int? callingPlanId)
        {
            if (phoneNumber == null || dateTime==null || callingPlanId ==null)
            {
                return NotFound();
            }

            var mobilePhoneCallingPlanAssignment = await _context.MobilePhoneCallingPlanAssignments
                .Include(m => m.CallingPlan)
                .Include(m => m.PhoneLine)
                .FirstOrDefaultAsync(m => m.PhoneNumber == phoneNumber && m.CallingPlanAssignmentDateTime==dateTime && m.CallingPlanId==callingPlanId);
            if (mobilePhoneCallingPlanAssignment == null)
            {
                return NotFound();
            }

            return View(mobilePhoneCallingPlanAssignment);
        }

        // POST: MobilePhoneCallingPlanAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("PhoneNumber,CallingPlanAssignmentDateTime,CallingPlanId,MinutesConsumed,MessagesSent")] MobilePhoneCallingPlanAssignment mobilePhoneCallingPlanAssignment)
        {
            var _mobilePhoneCallingPlanAssignment = await _context.MobilePhoneCallingPlanAssignments.FindAsync(mobilePhoneCallingPlanAssignment.PhoneNumber,mobilePhoneCallingPlanAssignment.CallingPlanAssignmentDateTime,mobilePhoneCallingPlanAssignment.CallingPlanId);
            _context.MobilePhoneCallingPlanAssignments.Remove(_mobilePhoneCallingPlanAssignment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MobilePhoneCallingPlanAssignmentExists(MobilePhoneCallingPlanAssignment m)
        {
            return _context.MobilePhoneCallingPlanAssignments.Any(e => e.PhoneNumber == m.PhoneNumber &&
                                                                       e.MessagesSent==m.MessagesSent &&
                                                                       e.MinutesConsumed==m.MinutesConsumed &&
                                                                       e.CallingPlanId==m.CallingPlanId &&
                                                                       e.CallingPlanAssignmentDateTime==m.CallingPlanAssignmentDateTime);
        }
    }
}
