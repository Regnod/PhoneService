using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MVCPhoneServiceWeb.Controllers
{
    public class PhoneLineEmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhoneLineEmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PhoneLineEmployees
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PhoneLineEmployees.Include(p => p.Employee).Include(p => p.PhoneLine);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PhoneLineEmployees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneLineEmployee = await _context.PhoneLineEmployees
                .Include(p => p.Employee)
                .Include(p => p.PhoneLine)
                .FirstOrDefaultAsync(m => m.PhoneNumber == id);
            if (phoneLineEmployee == null)
            {
                return NotFound();
            }

            return View(phoneLineEmployee);
        }

        // GET: PhoneLineEmployees/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber");
            return View();
        }

        // POST: PhoneLineEmployees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhoneNumber,EmployeeId")] PhoneLineEmployee phoneLineEmployee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phoneLineEmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", phoneLineEmployee.EmployeeId);
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", phoneLineEmployee.PhoneNumber);
            return View(phoneLineEmployee);
        }

        // GET: PhoneLineEmployees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneLineEmployee = await _context.PhoneLineEmployees.FindAsync(id);
            if (phoneLineEmployee == null)
            {
                return NotFound();
            }
            ViewData["EmployeeName"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeName");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            //ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name", phoneLineEmployee.EmployeeId);
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", phoneLineEmployee.PhoneNumber);
            return View(phoneLineEmployee);
        }

        // POST: PhoneLineEmployees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PhoneNumber,EmployeeId")] PhoneLineEmployee phoneLineEmployee)
        {
            if (id != phoneLineEmployee.PhoneNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phoneLineEmployee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhoneLineEmployeeExists(phoneLineEmployee.PhoneNumber))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", phoneLineEmployee.EmployeeId);
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", phoneLineEmployee.PhoneNumber);
            return View(phoneLineEmployee);
        }

        // GET: PhoneLineEmployees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneLineEmployee = await _context.PhoneLineEmployees
                .Include(p => p.Employee)
                .Include(p => p.PhoneLine)
                .FirstOrDefaultAsync(m => m.PhoneNumber == id);
            if (phoneLineEmployee == null)
            {
                return NotFound();
            }

            return View(phoneLineEmployee);
        }

        // POST: PhoneLineEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phoneLineEmployee = await _context.PhoneLineEmployees.FindAsync(id);
            _context.PhoneLineEmployees.Remove(phoneLineEmployee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhoneLineEmployeeExists(int id)
        {
            return _context.PhoneLineEmployees.Any(e => e.PhoneNumber == id);
        }
    }
}
