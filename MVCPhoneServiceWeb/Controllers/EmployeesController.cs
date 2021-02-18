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
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace MVCPhoneServiceWeb.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnviroment;

        public EmployeesController(ApplicationDbContext context, IHostingEnvironment hostingEnviroment)
        {
            _context = context;
            _hostingEnviroment = hostingEnviroment;
        }

        // GET: Employees
        public async Task<IActionResult> Index(int cpage, string name, string costCenter, string personalCode, string email, string extension,
            string nameCheck, string costCenterCheck, string personalCodeCheck, string emailCheck, string extensionCheck,
            string page, string next, string previous)
        {
            var _personalCode = Utils.Parse.IntTryParse(personalCode);
            //
            Tuple<bool, string>[] show = SD.Show(new List<string>() { nameCheck, costCenterCheck, personalCodeCheck, emailCheck, extensionCheck }, new List<string>() { name, costCenter, personalCode, email, extension });
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
            bool[] mask = { nameCheck != null, costCenterCheck != null, personalCodeCheck != null, emailCheck != null, extensionCheck != null };
            string csv = CSVStringConstructor(show, mask, result.Item1);
            string HttpSessionName = SD.HttpSessionString(new List<string> { "Employee", result.Item4.ToString(), name, costCenter, personalCode, email, extension,
                                                                               (nameCheck != null).ToString(), (costCenterCheck != null).ToString(), (personalCodeCheck != null).ToString(), (emailCheck != null).ToString(), (extensionCheck != null).ToString()});
            HttpContext.Session.SetString(HttpSessionName, csv);

            return View(result.Item1);
        }

        public string CSVStringConstructor(Tuple<bool, string>[] show, bool[] mask, List<Employee> data)
        {
            List<string> headers = new List<string>();

            int t = 0;

            for (int j = 0; j < show.Length; j++)
            {
                if (mask[j])
                {
                    headers.Add(SD.employee[j]);
                }
            }

            List<List<string>> datas = new List<List<string>>();

            foreach (var item in data)
            {
                List<string> row = new List<string>();
                if (show[0].Item1)
                {
                    row.Add(item.Name);
                }
                if (show[3].Item1)
                {
                    row.Add(item.Email);
                }
                if (show[1].Item1)
                {
                    row.Add(item.CostCenter.ToString());
                }
                if (show[4].Item1)
                {
                    row.Add(item.Extension);
                }
                datas.Add(row);
            }

            string csv = SD.csvString(headers, datas);
            return csv;
        }

        public async Task<IActionResult> Export(int page, string name, string costCenter, string personalCode, string email, string extension,
            string nameCheck, string costCenterCheck, string personalCodeCheck, string emailCheck, string extensionCheck)
        {

            string webRootPath = _hostingEnviroment.WebRootPath;
            var uploads = Path.Combine(webRootPath, "ExportFiles");
            var time = System.DateTime.Now.Day.ToString() + '-' + System.DateTime.Now.Month.ToString() + '-' + System.DateTime.Now.Year.ToString() + ' ' + System.DateTime.Now.Hour.ToString() + '-' + System.DateTime.Now.ToString() + '-' + System.DateTime.Now.Second.ToString();
            var path = Path.Combine(uploads, "Employees " + time + ".csv");
            using (var filesStream = new FileStream(path, FileMode.Create))
            {

            }
            StreamWriter stw = new StreamWriter(Path.Combine(uploads, "Employees " + time + ".csv"));
            string HttpSessionName = SD.HttpSessionString(new List<string> { "Employee", page.ToString(), name, costCenter, personalCode, email, extension,
                                                                               (nameCheck != null).ToString(), (costCenterCheck != null).ToString(), (personalCodeCheck != null).ToString(), (emailCheck != null).ToString(), (extensionCheck != null).ToString()});
            string csv = HttpContext.Session.GetString(HttpSessionName);
            stw.Write(csv);
            stw.Dispose();
            return RedirectToAction(nameof(Index), new
            {
                name = name,
                costCenter = costCenter,
                personalCode = personalCode,
                email = email,
                extension = extension,
                nameCheck = nameCheck == "True" ? "True" : null,
                costCenterCheck = costCenterCheck == "True" ? "True" : null,
                personalCodeCheck = personalCodeCheck == "True" ? "True" : null,
                emailCheck = emailCheck == "True" ? "True" : null,
                extensionCheck = extensionCheck == "True" ? "True" : null,
                cpage = page
            });
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