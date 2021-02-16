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
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace MVCPhoneServiceWeb.Controllers
{
    public class MobilePhoneEmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnviroment;

        public MobilePhoneEmployeesController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnviroment = hostingEnvironment;
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
            bool[] mask = { iMEICheck != null, employeeNameCheck != null };
            string csv = CSVStringConstructor(show, mask, result.Item1);
            //ViewData["csv"] = ss;
            HttpContext.Session.SetString(SD.csv, csv);

            return View(result.Item1);
        }

        public string CSVStringConstructor(Tuple<bool, string>[] show, bool[] mask, List<MobilePhoneEmployee> data)
        {
            List<string> headers = new List<string>();

            int t = 0;

            for (int j = 0; j < mask.Length; j++)
            {
                if (mask[j])
                {
                    headers.Add(SD.mobilePhoneEmployee[j]);
                }
            }

            List<List<string>> datas = new List<List<string>>();

            foreach (var item in data)
            {
                List<string> row = new List<string>();
                if (show[0].Item1)
                {
                    row.Add(item.IMEI);
                }
                if (show[1].Item1)
                {
                    row.Add(item.Employee.Name);
                }
                datas.Add(row);
            }

            string csv = SD.csvString(headers, datas);
            return csv;
        }

        public async Task<IActionResult> Export(int page, string iMEI, string name,
            string iMEICheck, string employeeNameCheck)
        {

            string webRootPath = _hostingEnviroment.WebRootPath;
            var uploads = Path.Combine(webRootPath, "ExportFiles");
            var path = Path.Combine(uploads, "mobilePhoneEmployee.csv");
            using (var filesStream = new FileStream(path, FileMode.Create))
            {

            }
            StreamWriter stw = new StreamWriter(Path.Combine(uploads, "mobilePhoneEmployee.csv"));

            string csv = HttpContext.Session.GetString(SD.csv);
            stw.Write(csv);
            stw.Dispose();
            return RedirectToAction(nameof(Index), new
            {
                iMEI = iMEI,
                name = name,
                iMEICheck = iMEICheck,
                employeeNameCheck = employeeNameCheck,
                cpage = page
            });
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
