using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repo;
using MVCPhoneServiceWeb.Utils;
using System;

namespace MVCPhoneServiceWeb.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index(int cpage, string name, string costCenter, string personalCode, string email, string extension,
            string nameCheck, string costCenterCheck, string personalCodeCheck, string emailCheck, string extensionCheck, 
            string page, string next, string previous)
        {
            var _personalCode = Utils.Parse.IntTryParse(personalCode);
            //
            Tuple<bool, string>[] show = SD.Show(new List<string>() { nameCheck, costCenterCheck, personalCodeCheck, emailCheck, extensionCheck }, new List<string>() { name, costCenter, personalCode, email, extension});
            ViewData["columns"] = show;
            //
            IEnumerable<Employee> employees = await _context.Employees.ToListAsync();
            List<Employee> _employees = employees.ToList();
            List<Employee> final_result = new List<Employee>();

            var nameList = (name != null) ? name.Split(", ").ToList() : new List<string>();
            _employees = DataFilter<Employee>.Filter(nameList, (m) => m.Name, _employees, true).ToList();

            var costCenterList = (costCenter != null) ? costCenter.Split(", ").ToList() : new List<string>();
            _employees = DataFilter<Employee>.Filter(costCenterList, (m) => m.CostCenterCode, _employees).ToList();

            var personalCodeList = (personalCode != null) ? personalCode.Split(", ").ToList() : new List<string>();
            _employees = DataFilter<Employee>.Filter(personalCodeList, (m) => m.PersonalCode, _employees).ToList();

            _employees = DataFilter<Employee>.Filter(email, (m) => m.Email, _employees).ToList();
            _employees = DataFilter<Employee>.Filter(extension, (m) => m.Extension, _employees).ToList();

            //Separar en paginas
            _employees = _employees.OrderBy(m => m.Name).ToList();
            var result = Paging<Employee>.Pages(_employees, page, cpage, (next != null), (previous != null));

            ViewData["top"] = result.Item2;
            ViewData["mult"] = result.Item3;
            ViewData["page"] = result.Item4;

            return View(result.Item1);
        }
        //ViewData["columns"] = show;
        //return View(_employees);

        // GET: Employees/Details/5 
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [ActionName("GetName")]
        public IActionResult GetName(int id)
        {
            //Employee employee = new Employee();
            var ee = (from employee in _context.Employees
                      where employee.EmployeeId == id
                      select employee.Name);
            //new Employee() { Name = employee.Name, CostCenterCode = employee.CostCenterCode });
            
            var js = Json(ee);
            return js;
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewData["Code"] = new SelectList(_context.CostCenters, "Code", "Code");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,Name,CostCenterCode,PersonalCode")]
            Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { idCheck = "On", nameCheck = "On", costCenterCheck = "On", personalCodeCheck = "On" });
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,Name,CostCenterCode,PersonalCode")]
            Employee employee)
        {
            //if (id != employee.EmployeeId)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                var ee = await _context.Employees.FindAsync(id);
                try
                {
                    //_context.Update(costCenter);
                    _context.Employees.Remove(ee);
                    _context.Add(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index), new { idCheck = "On", nameCheck = "On", costCenterCheck = "On", personalCodeCheck = "On" });
            }

            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { idCheck = "On", nameCheck = "On", costCenterCheck = "On", personalCodeCheck = "On" });
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}