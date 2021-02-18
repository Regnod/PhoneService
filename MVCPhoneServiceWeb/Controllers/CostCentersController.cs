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
    public class CostCentersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnviroment;
        public CostCentersController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnviroment = hostingEnvironment;
        }

        // GET: CostCenters
        public async Task<IActionResult> Index(int cpage, string code, string name, string managementName,
            string codeCheck, string nameCheck, string managementNameCheck,
            string page, string next, string previous)
        {
            //int _managementId = Parse.IntTryParse(managementId);
            //
            Tuple<bool, string>[] show = SD.Show(new List<string>() { codeCheck, nameCheck, managementNameCheck }, new List<string>() { code, name, managementName });
            ViewData["columns"] = show;
            //
            IEnumerable<CostCenter> costCenters = await _context.CostCenters.Include(a => a.Management).ToListAsync();
            List<CostCenter> _costCenter = costCenters.ToList();
            List<CostCenter> final_result = new List<CostCenter>();

            var codeList = (code != null) ? code.Split(", ").ToList() : new List<string>();
            _costCenter = DataFilter<CostCenter>.Filter(codeList, (m) => m.Code, _costCenter).ToList();

            var nameList = (name != null) ? name.Split(", ").ToList() : new List<string>();
            _costCenter = DataFilter<CostCenter>.Filter(nameList, (m) => m.Name, _costCenter, true).ToList();

            var managementNameList = (managementName != null) ? managementName.Split(", ").ToList() : new List<string>();
            _costCenter = DataFilter<CostCenter>.Filter(managementNameList, (m) => m.Management.Name, _costCenter, true).ToList();

            //Separar en paginas
            _costCenter = _costCenter.OrderBy(m => m.Code).ToList();
            var result = Paging<CostCenter>.Pages(_costCenter, page, cpage, (next != null), (previous != null));

            ViewData["top"] = result.Item2;
            ViewData["mult"] = result.Item3;
            ViewData["page"] = result.Item4;
            bool[] mask = { codeCheck != null, nameCheck != null, managementNameCheck != null };
            string csv = CSVStringConstructor(show, mask, result.Item1);
            //ViewData["csv"] = ss;
            string httpSessionName = SD.HttpSessionString(new List<string> { "CostCenter", result.Item4.ToString(), code, name, managementName, (codeCheck != null).ToString(), (nameCheck != null).ToString(), (managementNameCheck != null).ToString() });

            HttpContext.Session.SetString(httpSessionName, csv);

            return View(result.Item1);
        }

        public string CSVStringConstructor(Tuple<bool, string>[] show, bool[] mask, List<CostCenter> data)
        {
            List<string> headers = new List<string>();

            int t = 0;

            for (int j = 0; j < mask.Length; j++)
            {
                if (mask[j])
                {
                    headers.Add(SD.costCenter[j]);
                }
            }

            List<List<string>> datas = new List<List<string>>();

            foreach (var item in data)
            {
                List<string> row = new List<string>();
                if (show[0].Item1)
                {
                    row.Add(item.Code);
                }
                if (show[1].Item1)
                {
                    row.Add(item.Name);
                }
                if (show[2].Item1)
                {
                    row.Add(item.Management.Name);
                }
                datas.Add(row);
            }

            string csv = SD.csvString(headers, datas);
            return csv;
        }

        public async Task<IActionResult> Export(int page, string code, string name, string managementName,
            string codeCheck, string nameCheck, string managementNameCheck)
        {

            string webRootPath = _hostingEnviroment.WebRootPath;
            var uploads = Path.Combine(webRootPath, "ExportFiles");
            string time = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second;
            var path = Path.Combine(uploads, "costCenter " + time + ".csv");
            using (var filesStream = new FileStream(path, FileMode.Create))
            {

            }
            StreamWriter stw = new StreamWriter(Path.Combine(uploads, "costCenter " + time + ".csv"));

            string httpSessionName = SD.HttpSessionString(new List<string> { "CostCenter", page.ToString(), code, name, managementName, codeCheck.ToString(), nameCheck.ToString(), managementNameCheck.ToString() });
            string csv = HttpContext.Session.GetString(httpSessionName);
            stw.Write(csv);
            stw.Dispose();
            return RedirectToAction(nameof(Index), new
            {
                codeCheck = codeCheck == "True" ? "True" : null,
                nameCheck = nameCheck == "True" ? "True" : null,
                managementNameCheck = managementNameCheck == "True" ? "True" : null,
                code = code,
                name = name,
                managementName = managementName,
                cpage = page
            });
        }
        // GET: CostCenters/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costCenter = await _context.CostCenters
                .FirstOrDefaultAsync(m => m.Code == id);
            if (costCenter == null)
            {
                return NotFound();
            }

            return View(costCenter);
        }

        // GET: CostCenters/Create
        public IActionResult Create()
        {
            ViewData["Gerency"] = new SelectList(_context.Managements, "ManagementId", "Name");
            return View();
        }

        // POST: CostCenters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Name, ManagementId")] CostCenter costCenter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(costCenter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { codeCheck = "On", nameCheck = "On", managementIdCheck = "On" });
            }
            return View(costCenter);
        }

        // GET: CostCenters/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costCenter = await _context.CostCenters
                .FirstOrDefaultAsync(m => m.Code == id);
            if (costCenter == null)
            {
                return NotFound();
            }
            return View(costCenter);
        }

        // POST: CostCenters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Code,Name, ManagementId")] CostCenter costCenter)
        {
            if (ModelState.IsValid)
            {
                var cc = await _context.CostCenters.FindAsync(id);
                try
                {
                    _context.CostCenters.Remove(cc);
                    _context.Add(costCenter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CostCenterExists(costCenter.Code))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { codeCheck = "On", nameCheck = "On", managementIdCheck = "On" });
            }
            return View(costCenter);
        }

        // GET: CostCenters/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costCenter = await _context.CostCenters
                .FirstOrDefaultAsync(m => m.Code == id);
            if (costCenter == null)
            {
                return NotFound();
            }

            return View(costCenter);
        }

        // POST: CostCenters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var costCenter = await _context.CostCenters
                .FirstOrDefaultAsync(m => m.Code == id);
            _context.CostCenters.Remove(costCenter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { codeCheck = "On", nameCheck = "On", managementIdCheck = "On" });
        }

        private bool CostCenterExists(string id)
        {
            return _context.CostCenters.Any(e => e.Code == id);
        }
    }
}
