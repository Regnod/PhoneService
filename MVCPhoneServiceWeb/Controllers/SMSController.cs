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
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace MVCPhoneServiceWeb.Controllers
{
    public class SMSController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnviroment;

        public SMSController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnviroment = hostingEnvironment;
        }

        // GET: SMS
        public async Task<IActionResult> Index(int cpage, string phoneNumber, string day, string month, string year, string er, string min, string max, string location, string destination, string roaming,
            string phoneNumberCheck, string dateTimeCheck, string erCheck, string totalCostCheck, string locationCheck, string destinationCheck, string roamingCheck, string page, string next, string previous)
        {
            // 
            var _min = Parse.FloatTryParse(min);
            var _max = Parse.FloatTryParse(max);
            var _day = Parse.IntTryParse(day);
            var _month = (month != null && SD.months.ContainsKey(month)) ? SD.months[month] : -1;
            if (_month == -1)
                _month = Parse.IntTryParse(month);
            var _year = Parse.IntTryParse(year);
            //
            Tuple<bool, string>[] show = SD.Show(new List<string>() { phoneNumberCheck, dateTimeCheck, dateTimeCheck, dateTimeCheck, erCheck, locationCheck, destinationCheck, totalCostCheck, totalCostCheck, roamingCheck },
                new List<string>() { phoneNumber, day, month, year, er, location, destination, min, max, roaming });
            ViewData["columns"] = show;
            //     
            var mensajes = await _context.SMS.Include(m => m.PhoneLine).ToListAsync();
            List<SMS> _mensajes = mensajes.ToList();
            List<SMS> final_result = new List<SMS>();

            var phoneNumberList = (phoneNumber != null) ? phoneNumber.Split(", ").ToList() : new List<string>();
            _mensajes = DataFilter<SMS>.Filter(phoneNumberList, (m) => m.PhoneNumber, _mensajes).ToList();

            _mensajes = DataFilter<SMS>.Filter(_year, (m) => m.DateTime.Year, _mensajes).ToList();
            _mensajes = DataFilter<SMS>.Filter(_month, (m) => m.DateTime.Month, _mensajes).ToList();
            _mensajes = DataFilter<SMS>.Filter(_day, (m) => m.DateTime.Day, _mensajes).ToList();

            _mensajes = DataFilter<SMS>.Filter(er, (m) => m.E_R, _mensajes).ToList();

            var locationList = (location != null) ? location.Split(", ").ToList() : new List<string>();
            _mensajes = DataFilter<SMS>.Filter(locationList, (m) => m.Location, _mensajes, true).ToList();

            var destinationList = (destination != null) ? destination.Split(", ").ToList() : new List<string>();
            _mensajes = DataFilter<SMS>.Filter(destinationList, (m) => m.Destination, _mensajes, true).ToList();

            _mensajes = DataFilter<SMS>.Filter(_min, _max, (m) => m.Total, _mensajes).ToList();

            _mensajes = DataFilter<SMS>.Filter(roaming, (m) => m.Roaming, _mensajes).ToList();

            //Separar en paginas
            _mensajes = _mensajes.OrderBy(m => m.PhoneNumber).ToList();
            var result = Paging<SMS>.Pages(_mensajes, page, cpage, (next != null), (previous != null));

            ViewData["top"] = result.Item2;
            ViewData["mult"] = result.Item3;
            ViewData["page"] = result.Item4;
            bool[] mask = { phoneNumberCheck != null, dateTimeCheck != null, false, false, erCheck != null, locationCheck != null, destinationCheck != null, totalCostCheck != null, false, roamingCheck != null };
            string csv = CSVStringConstructor(show, mask, result.Item1);
            //ViewData["csv"] = ss;
            string httpSessionName = SD.HttpSessionString(new List<string> { "SMS", result.Item4.ToString(), phoneNumber, day, month, year, er, min, max, location, destination, roaming,
                (phoneNumberCheck != null).ToString(), (dateTimeCheck != null).ToString(), (erCheck != null).ToString(), (totalCostCheck != null).ToString(), (locationCheck != null).ToString(),
                (destinationCheck != null).ToString(), (roamingCheck != null).ToString()});

            HttpContext.Session.SetString(httpSessionName, csv);

            return View(result.Item1);
        }

        public string CSVStringConstructor(Tuple<bool, string>[] show, bool[] mask, List<SMS> data)
        {
            List<string> headers = new List<string>();

            int t = 0;

            for (int j = 0; j < mask.Length; j++)
            {
                if (mask[j])
                {
                    headers.Add(SD.sms[j]);
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
                    row.Add(item.DateTime.ToString());
                }
                if (show[4].Item1)
                {
                    row.Add(item.E_R);
                }
                if (show[5].Item1)
                {
                    row.Add(item.Location);
                }
                if (show[6].Item1)
                {
                    row.Add(item.Destination);
                }
                if (show[7].Item1)
                {
                    row.Add(item.Total.ToString());
                }
                if (show[9].Item1)
                {
                    row.Add(item.Roaming.ToString());
                }
                datas.Add(row);
            }

            string csv = SD.csvString(headers, datas);
            return csv;
        }

        public async Task<IActionResult> Export(int page, string phoneNumber, string day, string month, string year, string er, string min, string max, string location, string destination, string roaming,
            string phoneNumberCheck, string dateTimeCheck, string erCheck, string totalCostCheck, string locationCheck, string destinationCheck, string roamingCheck)
        {

            string webRootPath = _hostingEnviroment.WebRootPath;
            var uploads = Path.Combine(webRootPath, "ExportFiles");
            string time = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + " " + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second;
            var path = Path.Combine(uploads, "sMS " + time + ".csv");
            using (var filesStream = new FileStream(path, FileMode.Create))
            {

            }
            StreamWriter stw = new StreamWriter(Path.Combine(uploads, "sMS " + time + ".csv"));
            string httpSessionName = SD.HttpSessionString(new List<string> { "SMS", page.ToString(), phoneNumber, day, month, year, er, min, max, location, destination, roaming,
                phoneNumberCheck.ToString(), dateTimeCheck.ToString(), erCheck.ToString(), totalCostCheck.ToString(), locationCheck.ToString(), destinationCheck.ToString(), roamingCheck.ToString()});

            string csv = HttpContext.Session.GetString(httpSessionName);
            stw.Write(csv);
            stw.Dispose();
            return RedirectToAction(nameof(Index), new
            {
                phoneNumber = phoneNumber,
                day = day,
                month = month,
                year = year,
                er = er,
                min = min,
                max = max,
                location = location,
                destination = destination,
                roaming = roaming,
                phoneNumberCheck = phoneNumberCheck == "True" ? "True" : null,
                dateTimeCheck = dateTimeCheck == "True" ? "True" : null,
                erCheck = erCheck == "True" ? "True" : null,
                totalCostCheck = totalCostCheck == "True" ? "True" : null,
                locationCheck = locationCheck == "True" ? "True" : null,
                destinationCheck = destinationCheck == "True" ? "True" : null,
                roamingCheck = roamingCheck == "True" ? "True" : null,
                cpage = page
            });
        }

        // GET: SMS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sMS = await _context.SMS
                .Include(s => s.PhoneLine)
                .FirstOrDefaultAsync(m => m.SMSId == id);
            if (sMS == null)
            {
                return NotFound();
            }

            return View(sMS);
        }

        // GET: SMS/Create
        public IActionResult Create()
        {
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber");
            List<e_r> er = new List<e_r>() { new e_r("Sended", "Env"), new e_r("Received", "Rec") };
            ViewData["E_R"] = new SelectList(er, "State", "Name");
            return View();
        }

        // POST: SMS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SMSId,PhoneNumber,DateTime,E_R,Location,Destination,Amount,LD,Discount,Charge,Total,Roaming")] SMS sMS)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sMS);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { phoneNumberCheck = "On", dateTimeCheck = "On", erCheck = "On", totalCostCheck = "On", locationCheck = "On", destinationCheck = "On" });
            }
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", sMS.PhoneNumber);
            List<e_r> er = new List<e_r>() { new e_r("Sended", "Env"), new e_r("Received", "Rec") };
            ViewData["E_R"] = new SelectList(er, "State", "Name");
            return View(sMS);
        }

        // GET: SMS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sMS = await _context.SMS
                .Include(s => s.PhoneLine)
                .FirstOrDefaultAsync(m => m.SMSId == id);
            if (sMS == null)
            {
                return NotFound();
            }
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", sMS.PhoneNumber);
            List<e_r> er = new List<e_r>() { new e_r("Sended", "Env"), new e_r("Received", "Rec") };
            ViewData["E_R"] = new SelectList(er, "State", "Name");
            return View(sMS);
        }

        // POST: SMS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SMSId,PhoneNumber,DateTime,E_R,Location,Destination,Amount,LD,Discount,Charge,Total,Roaming")] SMS sMS)
        {
            if (id != sMS.SMSId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sMS);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SMSExists(sMS.SMSId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { phoneNumberCheck = "On", dateTimeCheck = "On", erCheck = "On", totalCostCheck = "On", locationCheck = "On", destinationCheck = "On" });
            }
            ViewData["PhoneNumber"] = new SelectList(_context.PhoneLines, "PhoneNumber", "PhoneNumber", sMS.PhoneNumber);
            List<e_r> er = new List<e_r>() { new e_r("Sended", "Env"), new e_r("Received", "Rec") };
            ViewData["E_R"] = new SelectList(er, "State", "Name");
            return View(sMS);
        }

        // GET: SMS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sMS = await _context.SMS
                .Include(s => s.PhoneLine)
                .FirstOrDefaultAsync(m => m.SMSId == id);
            if (sMS == null)
            {
                return NotFound();
            }

            return View(sMS);
        }

        // POST: SMS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sMS = await _context.SMS
                .Include(s => s.PhoneLine)
                .FirstOrDefaultAsync(m => m.SMSId == id);
            _context.SMS.Remove(sMS);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { phoneNumberCheck = "On", dateTimeCheck = "On", erCheck = "On", totalCostCheck = "On", locationCheck = "On", destinationCheck = "On" });
        }

        private bool SMSExists(int id)
        {
            return _context.SMS.Any(e => e.SMSId == id);
        }
    }
}
class e_r
{
    public string State { get; set; }
    public string Name { get; set; }
    public e_r(string n, string s)
    {
        State = s;
        Name = n;
    }
}