using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repo;
using MVCPhoneServiceWeb.Utils;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace MVCPhoneServiceWeb.Controllers
{
    public class CallingPlansController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnviroment;

        public CallingPlansController(ApplicationDbContext context, IHostingEnvironment hostingEnviroment)
        {
            _context = context;
            _hostingEnviroment = hostingEnviroment;
        }

        // GET: CallingPlans
        public async Task<IActionResult> Index(int cpage, string callingPlanId, string minutes, string minCost, string maxCost,
            string cPCheck, string minutesCheck, string costCheck,
            string page, string next, string previous)
        {
            // var _callingPlanId = Utils.Utils.IntTryParse(callingPlanId);
            var _minutes = Parse.IntTryParse(minutes);
            var _minCost = Parse.FloatTryParse(minCost);
            var _maxCost = Parse.FloatTryParse(maxCost);

            // para setear propiedades en la vista
            Tuple<bool, string>[] show = SD.Show(new List<string>() { cPCheck, minutesCheck, costCheck, costCheck }, new List<string>() { callingPlanId, minutes, minCost, maxCost });
            ViewData["columns"] = show;
            //
            IEnumerable<CallingPlan> callingPlans = await _context.CallingPlans.ToListAsync();
            List<CallingPlan> _callingPlans = callingPlans.ToList();
            List<CallingPlan> final_result = new List<CallingPlan>();

            var callingPlanIdList = (callingPlanId != null) ? callingPlanId.Split(", ").ToList() : new List<string>();
            _callingPlans = DataFilter<CallingPlan>.Filter(callingPlanIdList, (m) => m.CallingPlanId, _callingPlans, true).ToList();

            _callingPlans = DataFilter<CallingPlan>.Filter(_minutes, (m) => m.Minutes, _callingPlans).ToList();

            _callingPlans = DataFilter<CallingPlan>.Filter(_minCost, _maxCost, (m) => m.Cost, _callingPlans).ToList();

            //Separar en paginas
            _callingPlans = _callingPlans.OrderBy(m => m.CallingPlanId).ToList();
            var result = Paging<CallingPlan>.Pages(_callingPlans, page, cpage, (next != null), (previous != null));

            ViewData["top"] = result.Item2;
            ViewData["mult"] = result.Item3;
            ViewData["page"] = result.Item4;
            bool[] mask = { cPCheck != null, minutesCheck != null, costCheck != null, false };
            string HttpSessionName = SD.HttpSessionString(new List<string> { "CallingPlan", result.Item4.ToString(), callingPlanId, minutes, minCost, maxCost,
                                                                               (cPCheck != null).ToString(), (minutesCheck != null).ToString(), (costCheck != null).ToString()});
            string csv = CSVStringConstructor(show, mask, result.Item1);
            HttpContext.Session.SetString(HttpSessionName, csv);

            return View(result.Item1);
        }
        public string CSVStringConstructor(Tuple<bool, string>[] show, bool[] mask, List<CallingPlan> data)
        {
            List<string> headers = new List<string>();

            int t = 0;

            for (int j = 0; j < show.Length; j++)
            {
                if (mask[j])
                {
                    headers.Add(SD.callingPlan[j]);
                }
            }

            List<List<string>> datas = new List<List<string>>();

            foreach (var item in data)
            {
                List<string> row = new List<string>();
                if (show[0].Item1)
                {
                    row.Add(item.CallingPlanId);
                }
                if (show[1].Item1)
                {
                    row.Add(item.Minutes.ToString());
                }
                if (show[2].Item1)
                {
                    row.Add(item.Cost.ToString());
                }
                datas.Add(row);
            }

            string csv = SD.csvString(headers, datas);
            return csv;
        }

        public async Task<IActionResult> Export(int page, string callingPlanId, string minutes, string minCost, string maxCost,
            string cPCheck, string minutesCheck, string costCheck)
        {

            string webRootPath = _hostingEnviroment.WebRootPath;
            var uploads = Path.Combine(webRootPath, "ExportFiles");
            var time = System.DateTime.Now.Day.ToString() + '-' + System.DateTime.Now.Month.ToString() + '-' + System.DateTime.Now.Year.ToString() + ' ' + System.DateTime.Now.Hour.ToString() + '-' + System.DateTime.Now.ToString() + '-' + System.DateTime.Now.Second.ToString();
            var path = Path.Combine(uploads, "CallingPlans " + time + ".csv");
            using (var filesStream = new FileStream(path, FileMode.Create))
            {

            }
            StreamWriter stw = new StreamWriter(Path.Combine(uploads, "CallingPlans " + time + ".csv"));
            string HttpSessionName = SD.HttpSessionString(new List<string> { "CallingPlanAssignment", page.ToString(), callingPlanId, minutes, minCost, maxCost,
                                                                               cPCheck.ToString(), minutesCheck.ToString(), costCheck.ToString()});
            string csv = HttpContext.Session.GetString(HttpSessionName);
            stw.Write(csv);
            stw.Dispose();
            return RedirectToAction(nameof(Index), new
            {
                callingPlanId = callingPlanId,
                minutes = minutes,
                minCost = minCost,
                maxCost = maxCost,
                cPCheck = cPCheck == "True" ? "True" : null,
                minutesCheck = minutesCheck == "True" ? "True" : null,
                costCheck = costCheck == "True" ? "True" : null,
                cpage = page
            });
        }

        // GET: CallingPlans/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var callingPlan = await _context.CallingPlans
                .FirstOrDefaultAsync(m => m.CallingPlanId == id);
            if (callingPlan == null)
            {
                return NotFound();
            }

            return View(callingPlan);
        }

        // GET: CallingPlans/Create
        public IActionResult Create()
        {
            // new { cPCheck = "On", minutesCheck = "On", costCheck = "On"}
            return View();
        }

        // POST: CallingPlans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CallingPlanId,Minutes,Messages")] CallingPlan callingPlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(callingPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { cPCheck = "On", minutesCheck = "On", costCheck = "On" });
            }
            return View(callingPlan);
        }

        // GET: CallingPlans/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var callingPlan = await _context.CallingPlans
                .FirstOrDefaultAsync(m => m.CallingPlanId == id);
            if (callingPlan == null)
            {
                return NotFound();
            }
            return View(callingPlan);
        }

        // POST: CallingPlans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CallingPlanId,Minutes,Messages")] CallingPlan callingPlan)
        {
            //if (id != callingPlan.CallingPlanId)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                var cp = await _context.CallingPlans.FindAsync(id);
                try
                {
                    //_context.Update(costCenter);
                    _context.CallingPlans.Remove(cp);
                    _context.Add(callingPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CallingPlanExists(callingPlan.CallingPlanId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { cPCheck = "On", minutesCheck = "On", costCheck = "On" });
            }
            return View(callingPlan);
        }

        // GET: CallingPlans/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var callingPlan = await _context.CallingPlans
                .FirstOrDefaultAsync(m => m.CallingPlanId == id);
            if (callingPlan == null)
            {
                return NotFound();
            }

            return View(callingPlan);
        }

        // POST: CallingPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var callingPlan = await _context.CallingPlans
                .FirstOrDefaultAsync(m => m.CallingPlanId == id);
            _context.CallingPlans.Remove(callingPlan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { cPCheck = "On", minutesCheck = "On", costCheck = "On" });
        }

        private bool CallingPlanExists(string id)
        {
            return _context.CallingPlans.Any(e => e.CallingPlanId == id);
        }
    }
}
