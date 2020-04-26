using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MVCPhoneServiceWeb.Controllers
{
    public class MobilePhoneEmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MobilePhoneEmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MobilePhoneEmployees
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MobilePhoneEmployees.Include(m => m.Employee).Include(m => m.MobilePhone);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MobilePhoneEmployees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobilePhoneEmployee = await _context.MobilePhoneEmployees
                .Include(m => m.Employee)
                .Include(m => m.MobilePhone)
                .FirstOrDefaultAsync(m => m.IMEI == id);
            if (mobilePhoneEmployee == null)
            {
                return NotFound();
            }

            return View(mobilePhoneEmployee);
        }

        // GET: MobilePhoneEmployees/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            ViewData["IMEI"] = new SelectList(_context.MobilePhones, "IMEI", "IMEI");
            return View();
        }

        // POST: MobilePhoneEmployees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IMEI,EmployeeId")] MobilePhoneEmployee mobilePhoneEmployee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mobilePhoneEmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", mobilePhoneEmployee.EmployeeId);
            ViewData["IMEI"] = new SelectList(_context.MobilePhones, "IMEI", "IMEI", mobilePhoneEmployee.IMEI);
            return View(mobilePhoneEmployee);
        }

        // GET: MobilePhoneEmployees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobilePhoneEmployee = await _context.MobilePhoneEmployees.FindAsync(id);
            if (mobilePhoneEmployee == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", mobilePhoneEmployee.EmployeeId);
            ViewData["IMEI"] = new SelectList(_context.MobilePhones, "IMEI", "IMEI", mobilePhoneEmployee.IMEI);
            return View(mobilePhoneEmployee);
        }

        // POST: MobilePhoneEmployees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IMEI,EmployeeId")] MobilePhoneEmployee mobilePhoneEmployee)
        {
            if (id != mobilePhoneEmployee.IMEI)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mobilePhoneEmployee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MobilePhoneEmployeeExists(mobilePhoneEmployee.IMEI))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", mobilePhoneEmployee.EmployeeId);
            ViewData["IMEI"] = new SelectList(_context.MobilePhones, "IMEI", "IMEI", mobilePhoneEmployee.IMEI);
            return View(mobilePhoneEmployee);
        }

        // GET: MobilePhoneEmployees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobilePhoneEmployee = await _context.MobilePhoneEmployees
                .Include(m => m.Employee)
                .Include(m => m.MobilePhone)
                .FirstOrDefaultAsync(m => m.IMEI == id);
            if (mobilePhoneEmployee == null)
            {
                return NotFound();
            }

            return View(mobilePhoneEmployee);
        }

        // POST: MobilePhoneEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mobilePhoneEmployee = await _context.MobilePhoneEmployees.FindAsync(id);
            _context.MobilePhoneEmployees.Remove(mobilePhoneEmployee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MobilePhoneEmployeeExists(int id)
        {
            return _context.MobilePhoneEmployees.Any(e => e.IMEI == id);
        }
    }
}
