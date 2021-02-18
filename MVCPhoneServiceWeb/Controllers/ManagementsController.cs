using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Models;
using Repo;
using MVCPhoneServiceWeb.Utils;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace MVCPhoneServiceWeb.Controllers
{
    public class ManagementsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnviroment;

        public ManagementsController(ApplicationDbContext context, IHostingEnvironment hostingEnviroment)
        {
            _context = context;
            _hostingEnviroment = hostingEnviroment;
        }

        // GET: Managements
        public async Task<IActionResult> Index(int cpage, string name, string nameCheck, string page, string next, string previous)
        {
            //
            Tuple<bool, string>[] show = SD.Show(new List<string>() { nameCheck }, new List<string>() { name });
            ViewData["columns"] = show;
            //
            IEnumerable<Management> managements = await _context.Managements.ToListAsync();
            List<Management> _managements = managements.ToList();
            List<Management> final_result = new List<Management>();

            var nameList = (name != null) ? name.Split(", ").ToList() : new List<string>();
            _managements = DataFilter<Management>.Filter(nameList, (m) => m.Name, _managements, true).ToList();

            //Separar en paginas
            _managements = _managements.OrderBy(m => m.Name).ToList();
            var result = Paging<Management>.Pages(_managements, page, cpage, (next != null), (previous != null));

            ViewData["top"] = result.Item2;
            ViewData["mult"] = result.Item3;
            ViewData["page"] = result.Item4;
            bool[] mask = { nameCheck != null };
            string csv = CSVStringConstructor(show, mask, result.Item1);
            string HttpSessionName = SD.HttpSessionString(new List<string> { "Managements", result.Item4.ToString(), name,
                                                                               (name != null).ToString()});
            HttpContext.Session.SetString(HttpSessionName, csv);

            return View(result.Item1);
        }

        public string CSVStringConstructor(Tuple<bool, string>[] show, bool[] mask, List<Management> data)
        {
            List<string> headers = new List<string>();

            int t = 0;

            for (int j = 0; j < show.Length; j++)
            {
                if (mask[j])
                {
                    headers.Add(SD.management[j]);
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
                datas.Add(row);
            }

            string csv = SD.csvString(headers, datas);
            return csv;
        }

        public async Task<IActionResult> Export(int page, string name, string nameCheck)
        {

            string webRootPath = _hostingEnviroment.WebRootPath;
            var uploads = Path.Combine(webRootPath, "ExportFiles");
            var time = System.DateTime.Now.Day.ToString() + '-' + System.DateTime.Now.Month.ToString() + '-' + System.DateTime.Now.Year.ToString() + ' ' + System.DateTime.Now.Hour.ToString() + '-' + System.DateTime.Now.ToString() + '-' + System.DateTime.Now.Second.ToString();
            var path = Path.Combine(uploads, "Managements " + time + ".csv");
            using (var filesStream = new FileStream(path, FileMode.Create))
            {

            }
            StreamWriter stw = new StreamWriter(Path.Combine(uploads, "Managements " + time + ".csv"));
            string HttpSessionName = SD.HttpSessionString(new List<string> { "Managements", page.ToString(), name,
                                                                               name.ToString()});
            string csv = HttpContext.Session.GetString(HttpSessionName);
            stw.Write(csv);
            stw.Dispose();
            return RedirectToAction(nameof(Index), new
            {
                name = name,
                nameCheck = nameCheck == "True" ? "True" : null,
                cpage = page
            });
        }

        // GET: Managements/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var management = await _context.Managements
                .FirstOrDefaultAsync(m => m.ManagementId == id);
            if (management == null)
            {
                return NotFound();
            }

            return View(management);
        }

        // GET: Managements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Managements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ManagementId,Name")] Management management)
        {
            if (ModelState.IsValid)
            {
                _context.Add(management);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { nameCheck = "On", managementIdCheck = "On" });
            }
            return View(management);
        }

        // GET: Managements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var management = await _context.Managements
                .FirstOrDefaultAsync(m => m.ManagementId == id);
            if (management == null)
            {
                return NotFound();
            }
            return View(management);
        }

        // POST: Managements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ManagementId,Name")] Management management)
        {
            if (id != management.ManagementId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(management);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManagementExists(management.ManagementId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { nameCheck = "On", managementIdCheck = "On" });
            }
            return View(management);
        }

        // GET: Managements/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var management = await _context.Managements
                .FirstOrDefaultAsync(m => m.ManagementId == id);
            if (management == null)
            {
                return NotFound();
            }

            return View(management);
        }

        // POST: Managements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var management = await _context.Managements
                .FirstOrDefaultAsync(m => m.ManagementId == id);
            _context.Managements.Remove(management);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { nameCheck = "On", managementIdCheck = "On" });
        }

        private bool ManagementExists(int id)
        {
            return _context.Managements.Any(e => e.ManagementId == id);
        }
    }
}
