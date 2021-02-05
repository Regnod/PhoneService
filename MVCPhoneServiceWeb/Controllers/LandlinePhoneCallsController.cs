using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Data.Models;
using Repo;

namespace MVCPhoneServiceWeb.Controllers
{
    public class LandlinePhoneCallsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LandlinePhoneCallsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LandlinePhoneCalls
        public async Task<IActionResult> Index(string extension,string employeesId,string day,string month,string year,string hour,string minute,string destination,string cost,string duration,string addressee)
        {
            var _extension = Utils.Parse.IntTryParse(extension);
            var _employeeId = Utils.Parse.IntTryParse(employeesId);
            var _day = Utils.Parse.IntTryParse(day);
            var _month = Utils.Parse.IntTryParse(month);
            var _year = Utils.Parse.IntTryParse(year);
            var _hour = Utils.Parse.IntTryParse(hour);
            var _minute = Utils.Parse.IntTryParse(minute);
            var _cost = Utils.Parse.IntTryParse(cost);
            var _duration = Utils.Parse.IntTryParse(duration);
            var _addressee = Utils.Parse.IntTryParse(addressee);

            var applicationDbContext = _context.LandLinePhoneCalls.Include(l => l.Employee);
            IEnumerable<LandlinePhoneCall> landlinePhoneCalls =await applicationDbContext.ToListAsync();
            
            if (_extension != -1)
            {
                landlinePhoneCalls = landlinePhoneCalls.Where(a => a.Extension == _extension);
            }
            if (_employeeId != -1)
            {
                landlinePhoneCalls = landlinePhoneCalls.Where(a => a.EmployeeId == _employeeId);
            }
            if (_day != -1)
            {
                landlinePhoneCalls = landlinePhoneCalls.Where(a => a.DateTime.Day == _day);
            }
            if (_month != -1)
            {
                landlinePhoneCalls = landlinePhoneCalls.Where(a => a.DateTime.Month == _month);
            }
            if (_year != -1)
            {
                landlinePhoneCalls = landlinePhoneCalls.Where(a => a.DateTime.Year == _year);
            }
            if (_hour != -1)
            {
                landlinePhoneCalls = landlinePhoneCalls.Where(a => a.DateTime.Hour == _hour);
            }
            if (_minute != -1)
            {
                landlinePhoneCalls = landlinePhoneCalls.Where(a => a.DateTime.Minute == _minute);
            }
            if (destination != null)
            {
                landlinePhoneCalls = landlinePhoneCalls.Where(a => a.Destination == destination);
            }
            if (_cost != -1)
            {
                landlinePhoneCalls = landlinePhoneCalls.Where(a => Math.Abs(a.Cost - _cost) < 0.01);
            }
            if (_duration != -1)
            {
                landlinePhoneCalls = landlinePhoneCalls.Where(a => Math.Abs(a.Duration - _duration) < 0.01);
            }
            if (_addressee != -1)
            {
                landlinePhoneCalls = landlinePhoneCalls.Where(a => a.Addressee == addressee);
            }
            return View(landlinePhoneCalls);
        }

        // GET: LandlinePhoneCalls/Details/5
        public async Task<IActionResult> Details(int? extension,DateTime? dateTime,int employeeId )
        {
            if (extension == null || dateTime==null || employeeId==null)
            {
                return NotFound();
            }

            var landlinePhoneCall = await _context.LandLinePhoneCalls
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.Extension == extension && m.DateTime==dateTime && m.EmployeeId==employeeId);
            if (landlinePhoneCall == null)
            {
                return NotFound();
            }

            return View(landlinePhoneCall);
        }

        // GET: LandlinePhoneCalls/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            return View();
        }

        // POST: LandlinePhoneCalls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Extension,LandlinePhoneCallDateTime,EmployeeId,Destination,LandlinePhoneCallDuration,LandlinePhoneCallAddressee,LandlinePhoneCallCost")] LandlinePhoneCall landlinePhoneCall)
        {
            if (ModelState.IsValid)
            {
                _context.Add(landlinePhoneCall);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", landlinePhoneCall.EmployeeId);
            return View(landlinePhoneCall);
        }

        // GET: LandlinePhoneCalls/Edit/5
        public async Task<IActionResult> Edit(int? extension,DateTime? dateTime,int? employeeId)
        {
            if (extension == null || dateTime==null || employeeId==null)
            {
                return NotFound();
            }

            var landlinePhoneCall = await _context.LandLinePhoneCalls.FindAsync(extension,dateTime,employeeId);
            if (landlinePhoneCall == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", landlinePhoneCall.EmployeeId);
            return View(landlinePhoneCall);
        }

        // POST: LandlinePhoneCalls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Extension,LandlinePhoneCallDateTime,EmployeeId,Destination,LandlinePhoneCallDuration,LandlinePhoneCallAddressee")] LandlinePhoneCall landlinePhoneCall)
        {
            var landlinePhoneCallFromDb = await _context.LandLinePhoneCalls
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.Extension == landlinePhoneCall.Extension && m.DateTime == landlinePhoneCall.DateTime && m.EmployeeId == landlinePhoneCall.EmployeeId);
            landlinePhoneCallFromDb.Destination = landlinePhoneCall.Destination;
            landlinePhoneCallFromDb.Duration = landlinePhoneCall.Duration;
            landlinePhoneCallFromDb.Addressee = landlinePhoneCall.Addressee;
            if (ModelState.IsValid)
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", landlinePhoneCall.EmployeeId);
            return View(landlinePhoneCall);
        }

        // GET: LandlinePhoneCalls/Delete/5
        public async Task<IActionResult> Delete(int? extension,DateTime? dateTime,int employeeId)
        {
            if (extension == null || dateTime==null || employeeId==null)
            {
                return NotFound();
            }

            var landlinePhoneCall = await _context.LandLinePhoneCalls
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.Extension == extension && m.DateTime==dateTime && m.EmployeeId==employeeId);
            if (landlinePhoneCall == null)
            {
                return NotFound();
            }

            return View(landlinePhoneCall);
        }

        // POST: LandlinePhoneCalls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("Extension,LandlinePhoneCallDateTime,EmployeeId,Destination,LandlinePhoneCallDuration,LandlinePhoneCallAddressee")] LandlinePhoneCall landlinePhoneCall)
        {
            var _landlinePhoneCall = await _context.LandLinePhoneCalls.FindAsync(landlinePhoneCall.Extension,landlinePhoneCall.DateTime,landlinePhoneCall.EmployeeId);
            _context.LandLinePhoneCalls.Remove(_landlinePhoneCall);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LandlinePhoneCallExists(LandlinePhoneCall l)
        {
            return _context.LandLinePhoneCalls.Any(e => e.Extension == l.Extension && 
                                                        e.Destination==l.Destination && 
                                                        l.Employee==e.Employee && 
                                                        l.Addressee==e.Addressee && 
                                                        Math.Abs(l.Cost - e.Cost) < 0.01 && 
                                                        Math.Abs(l.Duration - e.Duration) < 0.01 && 
                                                        l.DateTime==e.DateTime);
        }
    }
}
