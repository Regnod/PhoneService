using System;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repo;

namespace MVCPhoneServiceWeb.Controllers
{
    public class MobilePhoneCallsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MobilePhoneCallsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MobilePhoneCalls
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MobilePhoneCalls.Include(m => m.MobilePhone).Include(m => m.PhoneLine);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MobilePhoneCalls/Details/5
        public async Task<IActionResult> Details(int? phoneNumber,int? imei, DateTime? dateTime)
        {
            if (phoneNumber== null || imei == null || dateTime==null )
            {
                return NotFound();
            }

            var mobilePhoneCall = await _context.MobilePhoneCalls
                .Include(m => m.MobilePhone)
                .Include(m => m.PhoneLine)
                .FirstOrDefaultAsync(m => m.PhoneNumber == phoneNumber && m.IMEI==imei && m.DateTime==dateTime);
            if (mobilePhoneCall == null)
            {
                return NotFound();
            }

            return View(mobilePhoneCall);
        }

        // GET: MobilePhoneCalls/Create
        public IActionResult Create()
        {
            ViewData["IMEI"] = new SelectList(_context.MobilePhones, "IMEI", "IMEI");
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber");
            return View();
        }

        // POST: MobilePhoneCalls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhoneNumber,IMEI,DateTime,MobilePhoneCallAddressee,MobilePhoneCallDuration,MobilePhoneCallCost")] MobilePhoneCall mobilePhoneCall)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mobilePhoneCall);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IMEI"] = new SelectList(_context.MobilePhones, "IMEI", "IMEI", mobilePhoneCall.IMEI);
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", mobilePhoneCall.PhoneNumber);
            return View(mobilePhoneCall);
        }

        // GET: MobilePhoneCalls/Edit/5
        public async Task<IActionResult> Edit(int? phoneNumber,int? imei, DateTime? dateTime)
        {
            if (phoneNumber == null || imei == null || dateTime == null)
            {
                return NotFound();
            }

            var mobilePhoneCall = await _context.MobilePhoneCalls.FindAsync( phoneNumber, imei, dateTime);
            if (mobilePhoneCall == null)
            {
                return NotFound();
            }
            ViewData["IMEI"] = new SelectList(_context.MobilePhones, "IMEI", "IMEI", mobilePhoneCall.IMEI);
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", mobilePhoneCall.PhoneNumber);
            return View(mobilePhoneCall);
        }

        // POST: MobilePhoneCalls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("PhoneNumber,IMEI,DateTime,MobilePhoneCallAddressee,MobilePhoneCallDuration,MobilePhoneCallCost")] MobilePhoneCall mobilePhoneCall)
        {
            var mobilePhoneCall1 = await _context.MobilePhoneCalls.FindAsync(mobilePhoneCall.PhoneNumber, mobilePhoneCall.IMEI, mobilePhoneCall.DateTime);
            //if (MobilePhoneCallExists(mobilePhoneCall))
            //{
            //    return NotFound();
            //}
            mobilePhoneCall1.MobilePhoneCallAddressee = mobilePhoneCall.MobilePhoneCallAddressee;
            mobilePhoneCall1.MobilePhoneCallDuration = mobilePhoneCall.MobilePhoneCallDuration;
            mobilePhoneCall1.MobilePhoneCallCost = mobilePhoneCall.MobilePhoneCallCost;
            if (ModelState.IsValid)
            {
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["IMEI"] = new SelectList(_context.MobilePhones, "IMEI", "IMEI", mobilePhoneCall.IMEI);
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", mobilePhoneCall.PhoneNumber);
            return View(mobilePhoneCall);
        }

        // GET: MobilePhoneCalls/Delete/5
        public async Task<IActionResult> Delete(int? phoneNumber,int? imei, DateTime? dateTime)
        {
            if (phoneNumber== null || imei == null || dateTime==null )
            {
                return NotFound();
            }

            var mobilePhoneCall = await _context.MobilePhoneCalls
                .Include(m => m.MobilePhone)
                .Include(m => m.PhoneLine)
                .FirstOrDefaultAsync(m => m.PhoneNumber == phoneNumber && m.IMEI==imei && m.DateTime==dateTime);
            if (mobilePhoneCall == null)
            {
                return NotFound();
            }

            return View(mobilePhoneCall);
        }

        // POST: MobilePhoneCalls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("PhoneNumber,IMEI,DateTime,MobilePhoneCallAddressee,MobilePhoneCallDuration,MobilePhoneCallCost")] MobilePhoneCall mobilePhoneCall)
        {
            var _mobilePhoneCall = await _context.MobilePhoneCalls.FindAsync(mobilePhoneCall.PhoneNumber,mobilePhoneCall.IMEI,mobilePhoneCall.DateTime);
            _context.MobilePhoneCalls.Remove(_mobilePhoneCall);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MobilePhoneCallExists(MobilePhoneCall m)
        {
            return _context.MobilePhoneCalls.Any(e => e.PhoneNumber == m.PhoneNumber &&
                                                      e.DateTime==m.DateTime &&
                                                      e.IMEI==m.IMEI &&
                                                      e.MobilePhoneCallAddressee==m.MobilePhoneCallAddressee &&
                                                      e.MobilePhoneCallCost==m.MobilePhoneCallCost &&
                                                      e.MobilePhoneCallDuration==m.MobilePhoneCallDuration);
        }
    }
}
