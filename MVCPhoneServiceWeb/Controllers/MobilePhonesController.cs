using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repo;
using System;
using System.Collections.Generic;
using MVCPhoneServiceWeb.Utils;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace MVCPhoneServiceWeb.Controllers
{
    public class MobilePhonesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnviroment;

        public MobilePhonesController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnviroment = hostingEnvironment;
        }

        // GET: MobilePhones
        public async Task<IActionResult> Index(int cpage, string iMEI, string model,
            string iMEICheck, string modelCheck,
            string page, string next, string previous)
        {
            //
            Tuple<bool, string>[] show = SD.Show(new List<string>() { iMEICheck, modelCheck }, new List<string>() { iMEI, model });
            ViewData["columns"] = show;
            //
            IEnumerable<MobilePhone> mobilePhones = await _context.MobilePhones.ToListAsync();
            List<MobilePhone> _mobilePhones = mobilePhones.ToList();
            List<MobilePhone> final_result = new List<MobilePhone>();

            var iMEIList = (iMEI != null) ? iMEI.Split(", ").ToList() : new List<string>();
            _mobilePhones = DataFilter<MobilePhone>.Filter(iMEIList, (m) => m.IMEI, _mobilePhones).ToList();

            var modelList = (model != null) ? model.Split(", ").ToList() : new List<string>();
            _mobilePhones = DataFilter<MobilePhone>.Filter(modelList, (m) => m.Model, _mobilePhones).ToList();

            //Separar en paginas
            _mobilePhones = _mobilePhones.OrderBy(m => m.Model).ToList();
            var result = Paging<MobilePhone>.Pages(_mobilePhones, page, cpage, (next != null), (previous != null));

            ViewData["top"] = result.Item2;
            ViewData["mult"] = result.Item3;
            ViewData["page"] = result.Item4;
            bool[] mask = { iMEICheck != null, modelCheck != null };
            string csv = CSVStringConstructor(show, mask, result.Item1);
            //ViewData["csv"] = ss;
            string httpSessionName = SD.HttpSessionString(new List<string> { "MobilePhone", result.Item4.ToString(), iMEI, model, (iMEICheck != null).ToString(), (modelCheck != null).ToString() });

            HttpContext.Session.SetString(httpSessionName, csv);

            return View(result.Item1);
        }

        public string CSVStringConstructor(Tuple<bool, string>[] show, bool[] mask, List<MobilePhone> data)
        {
            List<string> headers = new List<string>();

            int t = 0;

            for (int j = 0; j < mask.Length; j++)
            {
                if (mask[j])
                {
                    headers.Add(SD.mobilePhone[j]);
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
                    row.Add(item.Model);
                }
                datas.Add(row);
            }

            string csv = SD.csvString(headers, datas);
            return csv;
        }

        public async Task<IActionResult> Export(int page, string iMEI, string model,
            string iMEICheck, string modelCheck)
        {

            string webRootPath = _hostingEnviroment.WebRootPath;
            var uploads = Path.Combine(webRootPath, "ExportFiles");
            string time = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second;
            var path = Path.Combine(uploads, "mobilePhone " + time + ".csv");
            using (var filesStream = new FileStream(path, FileMode.Create))
            {

            }
            StreamWriter stw = new StreamWriter(Path.Combine(uploads, "mobilePhone " + time + ".csv"));
            string httpSessionName = SD.HttpSessionString(new List<string> { "MobilePhone", page.ToString(), iMEI, model, iMEICheck.ToString(), modelCheck.ToString() });

            string csv = HttpContext.Session.GetString(httpSessionName);
            stw.Write(csv);
            stw.Dispose();
            return RedirectToAction(nameof(Index), new
            {
                iMEI = iMEI,
                model = model,
                iMEICheck = iMEICheck == "True" ? "True" : null,
                modelCheck = modelCheck == "True" ? "True" : null,
                cpage = page
            });
        }
        // GET: MobilePhones/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobilePhone = await _context.MobilePhones
                .FirstOrDefaultAsync(m => m.IMEI == id);
            if (mobilePhone == null)
            {
                return NotFound();
            }

            return View(mobilePhone);
        }

        // GET: MobilePhones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MobilePhones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IMEI,Model")] MobilePhone mobilePhone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mobilePhone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { iMEICheck = "On", modelCheck = "On" });
            }
            return View(mobilePhone);
        }

        // GET: MobilePhones/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobilePhone = await _context.MobilePhones
                .FirstOrDefaultAsync(m => m.IMEI == id);
            if (mobilePhone == null)
            {
                return NotFound();
            }
            return View(mobilePhone);
        }

        // POST: MobilePhones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IMEI,Model")] MobilePhone mobilePhone)
        {
            //if (id != mobilePhone.IMEI)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    var mp = await _context.MobilePhones.FindAsync(id);
                    _context.MobilePhones.Remove(mp);
                    _context.Add(mobilePhone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MobilePhoneExists(mobilePhone.IMEI))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { iMEICheck = "On", modelCheck = "On" });
            }
            return View(mobilePhone);
        }

        // GET: MobilePhones/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobilePhone = await _context.MobilePhones
                .FirstOrDefaultAsync(m => m.IMEI == id);
            if (mobilePhone == null)
            {
                return NotFound();
            }

            return View(mobilePhone);
        }

        // POST: MobilePhones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var mobilePhone = await _context.MobilePhones
                .FirstOrDefaultAsync(m => m.IMEI == id);
            _context.MobilePhones.Remove(mobilePhone);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { iMEICheck = "On", modelCheck = "On" });
        }

        private bool MobilePhoneExists(string id)
        {
            return _context.MobilePhones.Any(e => e.IMEI == id);
        }
    }
}
