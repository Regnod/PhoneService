using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repo;
using System;
using MVCPhoneServiceWeb.Utils;

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
        public async Task<IActionResult> Index(int cpage, string iMEI, string name, 
            string iMEICheck, string employeeNameCheck, 
            string page, string next, string previous)
        {
            Tuple<bool, string>[] show = SD.Show(new List<string>() { iMEICheck, employeeNameCheck }, new List<string>() { iMEI, name });
            ViewData["columns"] = show;
            //
            IEnumerable<MobilePhoneEmployee> mobilePhoneEmployees = await _context.MobilePhoneEmployees.Include(m => m.Employee).Include(m => m.MobilePhone).ToListAsync();
            List<MobilePhoneEmployee> _mobilePhoneEmployees = mobilePhoneEmployees.ToList();
            List<MobilePhoneEmployee> final_result = new List<MobilePhoneEmployee>();

            var iMEIList = (iMEI != null) ? iMEI.Split(", ").ToList() : new List<string>();
            _mobilePhoneEmployees = DataFilter<MobilePhoneEmployee>.Filter(iMEIList, (m) => m.IMEI, _mobilePhoneEmployees).ToList();

            var employeeIdList = (name != null) ? name.Split(", ").ToList() : new List<string>();
            _mobilePhoneEmployees = DataFilter<MobilePhoneEmployee>.Filter(employeeIdList, (m) => m.Employee.Name, _mobilePhoneEmployees, true).ToList();

            //Separar en paginas
            _mobilePhoneEmployees = _mobilePhoneEmployees.OrderBy(m => m.Employee.Name).ToList();
            var result = Paging<MobilePhoneEmployee>.Pages(_mobilePhoneEmployees, page, cpage, (next != null), (previous != null));

            ViewData["top"] = result.Item2;
            ViewData["mult"] = result.Item3;
            ViewData["page"] = result.Item4;

            return View(result.Item1);
        }

        // GET: MobilePhoneEmployees/Details/5
        public async Task<IActionResult> Details(string id)
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
                return RedirectToAction(nameof(Index), new { iMEICheck = "On", employeeIdCheck = "On" });
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", mobilePhoneEmployee.EmployeeId);
            ViewData["IMEI"] = new SelectList(_context.MobilePhones, "IMEI", "IMEI", mobilePhoneEmployee.IMEI);
            return View(mobilePhoneEmployee);
        }

        // GET: MobilePhoneEmployees/Edit/5
        public async Task<IActionResult> Edit(string? id)
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
        public async Task<IActionResult> Edit(string id, [Bind("IMEI,EmployeeId")] MobilePhoneEmployee mobilePhoneEmployee)
        {
            //if (id != mobilePhoneEmployee.IMEI)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                var mpe = await _context.MobilePhoneEmployees.FindAsync(id);
                try
                {
                     _context.MobilePhoneEmployees.Remove(mpe);
                    _context.Add(mobilePhoneEmployee);
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
                return RedirectToAction(nameof(Index), new { iMEICheck = "On", employeeIdCheck = "On" });
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", mobilePhoneEmployee.EmployeeId);
            ViewData["IMEI"] = new SelectList(_context.MobilePhones, "IMEI", "IMEI", mobilePhoneEmployee.IMEI);
            return View(mobilePhoneEmployee);
        }

        // GET: MobilePhoneEmployees/Delete/5
        public async Task<IActionResult> Delete(string id)
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
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var mobilePhoneEmployee = await _context.MobilePhoneEmployees.FindAsync(id);
            _context.MobilePhoneEmployees.Remove(mobilePhoneEmployee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { iMEICheck = "On", employeeIdCheck = "On" });
        }

        private bool MobilePhoneEmployeeExists(string id)
        {
            return _context.MobilePhoneEmployees.Any(e => e.IMEI == id);
        }
    }
}
