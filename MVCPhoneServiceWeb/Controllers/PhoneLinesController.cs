using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPhoneServiceWeb.Utils;
using Repo;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace MVCPhoneServiceWeb.Controllers
{
    public class PhoneLinesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnviroment;

        public PhoneLinesController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnviroment = hostingEnvironment;
        } 

        // GET: PhoneLines
        public async Task<IActionResult> Index(int cpage, string phoneNumber, string pUK, string pIN, string contract,
            string phoneNumberCheck, string pUKCheck, string pINCheck, string contractCheck,
            string page, string next, string previous)
        {
            // 
            var _PUK = Parse.IntTryParse(pUK);
            var _PIN = Parse.IntTryParse(pIN);
            //
            Tuple<bool, string>[] show = SD.Show(new List<string>() { phoneNumberCheck, pUKCheck, pINCheck, contractCheck }, new List<string>() { phoneNumber, pUK, pIN, contract });
            ViewData["columns"] = show;
            //
            IEnumerable<PhoneLine> phoneLines = await _context.PhoneLines.ToListAsync();
            List<PhoneLine> _phoneLines = phoneLines.ToList();
            List<PhoneLine> final_result = new List<PhoneLine>();

            var phoneNumberList = (phoneNumber != null) ? phoneNumber.Split(", ").ToList() : new List<string>();
            _phoneLines = DataFilter<PhoneLine>.Filter(phoneNumberList, (m) => m.PhoneNumber, _phoneLines).ToList();

            _phoneLines = DataFilter<PhoneLine>.Filter(_PIN, (m) => m.PIN, _phoneLines).ToList();

            _phoneLines = DataFilter<PhoneLine>.Filter(_PUK, (m) => m.PUK, _phoneLines).ToList();

            _phoneLines = DataFilter<PhoneLine>.Filter(contract, (m) => m.Contract, _phoneLines).ToList();

            //Separar en paginas
            _phoneLines = _phoneLines.OrderBy(m => m.PhoneNumber).ToList();
            var result = Paging<PhoneLine>.Pages(_phoneLines, page, cpage, (next != null), (previous != null));

            ViewData["top"] = result.Item2;
            ViewData["mult"] = result.Item3;
            ViewData["page"] = result.Item4;
            bool[] mask = { phoneNumberCheck == "On", pUKCheck == "On", pINCheck == "On", contractCheck == "On" };
            string csv = CSVStringConstructor(show, mask, result.Item1);
            //ViewData["csv"] = ss;
            HttpContext.Session.SetString(SD.csv, csv);

            return View(result.Item1);
        }

        public string CSVStringConstructor(Tuple<bool, string>[] show, bool[] mask, List<PhoneLine> data)
        {
            List<string> headers = new List<string>();

            int t = 0;

            for (int j = 0; j < mask.Length; j++)
            {
                if (mask[j])
                {
                    headers.Add(SD.phoneLine[j]);
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
                    row.Add(item.PUK.ToString());
                }
                if (show[2].Item1)
                {
                    row.Add(item.PIN.ToString());
                }
                if (show[3].Item1)
                {
                    row.Add(item.Contract);
                }
                datas.Add(row);
            }

            string csv = SD.csvString(headers, datas);
            return csv;
        }

        public async Task<IActionResult> Export(int page, string phoneNumber, string pUK, string pIN, string contract,
            string phoneNumberCheck, string pUKCheck, string pINCheck, string contractCheck)
        {

            string webRootPath = _hostingEnviroment.WebRootPath;
            var uploads = Path.Combine(webRootPath, "ExportFiles");
            var path = Path.Combine(uploads, "phoneLine.csv");
            using (var filesStream = new FileStream(path, FileMode.Create))
            {

            }
            StreamWriter stw = new StreamWriter(Path.Combine(uploads, "phoneLine.csv"));
            string csv = HttpContext.Session.GetString(SD.csv);
            stw.Write(csv);
            stw.Dispose();
            return RedirectToAction(nameof(Index), new
            {
                phoneNumber = phoneNumber,
                pUK = pUK,
                pIN = pIN,
                contract = contract,
                phoneNumberCheck = phoneNumberCheck,
                pUKCheck = pUKCheck,
                pINCheck = pINCheck,
                contractCheck = contractCheck,
                cpage = page
            });
        }

        // GET: PhoneLines/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneLine = await _context.PhoneLines
                .FirstOrDefaultAsync(m => m.PhoneNumber == id);
            if (phoneLine == null)
            {
                return NotFound();
            }

            return View(phoneLine);
        }

        // GET: PhoneLines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PhoneLines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhoneNumber,PUK,PIN")] PhoneLine phoneLine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phoneLine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { phoneNumberCheck = "On", pUKCheck = "On", pINCheck = "On" });
            }
            return View(phoneLine);
        }

        // GET: PhoneLines/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneLine = await _context.PhoneLines.FindAsync(id);
            if (phoneLine == null)
            {
                return NotFound();
            }
            return View(phoneLine);
        }

        // POST: PhoneLines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PhoneNumber,PUK,PIN")] PhoneLine phoneLine)
        {
            //if (id != phoneLine.PhoneNumber)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                var pl = await _context.PhoneLines
                .FirstOrDefaultAsync(m => m.PhoneNumber == id);
                try
                {
                    //_context.Update(costCenter);
                    _context.PhoneLines.Remove(pl);
                    _context.Add(phoneLine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhoneLineExists(phoneLine.PhoneNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { phoneNumberCheck = "On", pUKCheck = "On", pINCheck = "On" });
            }
            return View(phoneLine);
        }

        // GET: PhoneLines/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneLine = await _context.PhoneLines
                .FirstOrDefaultAsync(m => m.PhoneNumber == id);
            if (phoneLine == null)
            {
                return NotFound();
            }

            return View(phoneLine);
        }

        // POST: PhoneLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var phoneLine = await _context.PhoneLines
                .FirstOrDefaultAsync(m => m.PhoneNumber == id);
            _context.PhoneLines.Remove(phoneLine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { phoneNumberCheck = "On", pUKCheck = "On", pINCheck = "On" });
        }

        private bool PhoneLineExists(string id)
        {
            return _context.PhoneLines.Any(e => e.PhoneNumber == id);
        }
    }
}
