using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCPhoneServiceWeb.Utils;
using Repo;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace MVCPhoneServiceWeb.Controllers
{
    public class PhoneLineEmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnviroment;

        public PhoneLineEmployeesController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnviroment = hostingEnvironment;
        }

        // GET: PhoneLineEmployees
        public async Task<IActionResult> Index(int cpage, string phoneNumber, string name,
            string phoneNumberCheck, string employeeNameCheck,
            string page, string next, string previous)
        {
            // 
            Tuple<bool, string>[] show = SD.Show(new List<string>() { phoneNumberCheck, employeeNameCheck }, new List<string>() { phoneNumber, name });
            ViewData["columns"] = show;
            //
            IEnumerable<PhoneLineEmployee> phoneLineEmployees = await _context.PhoneLineEmployees.Include(p => p.Employee).Include(p => p.PhoneLine).ToListAsync();
            List<PhoneLineEmployee> _phoneLineEmployees = phoneLineEmployees.ToList();
            List<PhoneLineEmployee> final_result = new List<PhoneLineEmployee>();

            var phoneNumberList = (phoneNumber != null) ? phoneNumber.Split(", ").ToList() : new List<string>();
            _phoneLineEmployees = DataFilter<PhoneLineEmployee>.Filter(phoneNumberList, (m) => m.PhoneNumber, _phoneLineEmployees).ToList();

            var employeeIdList = (name != null) ? name.Split(", ").ToList() : new List<string>();
            _phoneLineEmployees = DataFilter<PhoneLineEmployee>.Filter(employeeIdList, (m) => m.Employee.Name, _phoneLineEmployees, true).ToList();

            //Separar en paginas
            _phoneLineEmployees = _phoneLineEmployees.OrderBy(m => m.Employee.Name).ToList();
            var result = Paging<PhoneLineEmployee>.Pages(_phoneLineEmployees, page, cpage, (next != null), (previous != null));

            ViewData["top"] = result.Item2;
            ViewData["mult"] = result.Item3;
            ViewData["page"] = result.Item4;
            bool[] mask = { phoneNumberCheck != null, employeeNameCheck != null };
            string csv = CSVStringConstructor(show, mask, result.Item1);
            //ViewData["csv"] = ss;
            string httpSessionName = SD.HttpSessionString(new List<string> { "PhoneLineEmployee", result.Item4.ToString(), phoneNumber, name,
                (phoneNumberCheck != null).ToString(), (employeeNameCheck != null).ToString() });

            HttpContext.Session.SetString(httpSessionName, csv);

            return View(result.Item1);
        }

        public string CSVStringConstructor(Tuple<bool, string>[] show, bool[] mask, List<PhoneLineEmployee> data)
        {
            List<string> headers = new List<string>();

            int t = 0;

            for (int j = 0; j < mask.Length; j++)
            {
                if (mask[j])
                {
                    headers.Add(SD.phoneLineEmployee[j]);
                }
            }

            List<List<string>> datas = new List<List<string>>();

            foreach (var item in data)
            {
                List<string> row = new List<string>();
                if (show[0].Item1)
                {
                    row.Add(item.PhoneNumber);
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

        public async Task<IActionResult> Export(int page, string phoneNumber, string name,
            string phoneNumberCheck, string employeeNameCheck)
        {

            string webRootPath = _hostingEnviroment.WebRootPath;
            var uploads = Path.Combine(webRootPath, "ExportFiles");
            string time = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second;
            var path = Path.Combine(uploads, "phoneLineEmployee " + time + ".csv");
            using (var filesStream = new FileStream(path, FileMode.Create))
            {

            }
            StreamWriter stw = new StreamWriter(Path.Combine(uploads, "phoneLineEmployee " + time + ".csv"));
            string httpSessionName = SD.HttpSessionString(new List<string> { "PhoneLineEmployee", page.ToString(), phoneNumber, name, phoneNumberCheck.ToString(), employeeNameCheck.ToString() });

            string csv = HttpContext.Session.GetString(httpSessionName);
            stw.Write(csv);
            stw.Dispose();
            return RedirectToAction(nameof(Index), new
            {
                phoneNumber = phoneNumber,
                name = name,
                phoneNumberCheck = phoneNumberCheck == "True" ? "True" : null,
                employeeNameCheck = employeeNameCheck == "True" ? "True" : null,
                cpage = page
            });
        }

        // GET: PhoneLineEmployees/Details/5
        public async Task<IActionResult> Details(string id)
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
                return RedirectToAction(nameof(Index), new { phoneNumberCheck = "On", employeeIdCheck = "On" });
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", phoneLineEmployee.EmployeeId);
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", phoneLineEmployee.PhoneNumber);
            return View(phoneLineEmployee);
        }

        // GET: PhoneLineEmployees/Edit/5
        public async Task<IActionResult> Edit(string? id)
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
            ViewData["EmployeeName"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeName");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", phoneLineEmployee.PhoneNumber);
            return View(phoneLineEmployee);
        }

        // POST: PhoneLineEmployees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PhoneNumber,EmployeeId")] PhoneLineEmployee phoneLineEmployee)
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
                return RedirectToAction(nameof(Index), new { phoneNumberCheck = "On", employeeIdCheck = "On" });
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", phoneLineEmployee.EmployeeId);
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", phoneLineEmployee.PhoneNumber);
            return View(phoneLineEmployee);
        }

        // GET: PhoneLineEmployees/Delete/5
        public async Task<IActionResult> Delete(string id)
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
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var phoneLineEmployee = await _context.PhoneLineEmployees
                .Include(p => p.Employee)
                .Include(p => p.PhoneLine)
                .FirstOrDefaultAsync(m => m.PhoneNumber == id);
            _context.PhoneLineEmployees.Remove(phoneLineEmployee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { phoneNumberCheck = "On", employeeIdCheck = "On" });
        }

        private bool PhoneLineEmployeeExists(string id)
        {
            return _context.PhoneLineEmployees.Any(e => e.PhoneNumber == id);
        }
    }
}
