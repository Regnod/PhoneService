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

namespace MVCPhoneServiceWeb.Controllers
{
    public class SMSController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SMSController(ApplicationDbContext context)
        {
            _context = context;
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
            Tuple<bool, string>[] show = SD.Show(new List<string>() { phoneNumberCheck, dateTimeCheck, dateTimeCheck, dateTimeCheck, erCheck, totalCostCheck, totalCostCheck, locationCheck, locationCheck, destinationCheck, roamingCheck }, 
                new List<string>() { phoneNumber, day, month, year, er, min, max, location, destination, min, max, roaming });
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

            return View(result.Item1);
            //List<List<SMS>> calls = new List<List<SMS>>();
            //List<SMS> list = new List<SMS>();
            //int i = 0;
            //int j = 0;
            //foreach (var call in _mensajes)
            //{
            //    if (i == 20)
            //    {
            //        j++;
            //        i = 0;
            //        calls.Add(list);
            //        list = new List<SMS>();
            //    }
            //    list.Add(call);
            //    i++;
            //}
            //if (i < 20)
            //{
            //    calls.Add(list);
            //    j++;
            //}
            //// elegir pagina
            //int currentPage = 0;
            //if (page != null)
            //{
            //    currentPage = (Parse.IntTryParse(page) != -1) ? Parse.IntTryParse(page) - 1 : (cpage >= j) ? 0 : cpage;
            //}
            //else if (next != null)
            //    currentPage = (cpage + 1 >= j) ? cpage : cpage + 1;
            //else if (previous != null)
            //    currentPage = (cpage - 1 < 0) ? 0 : cpage - 1;

            //int mult = currentPage / 20;

            //if (j > 20 && j - currentPage < 20)
            //    ViewData["top"] = j - currentPage;
            //else if (j < 20 && j - currentPage < 20)
            //    ViewData["top"] = j;
            //else
            //    ViewData["top"] = 20;

            //ViewData["mult"] = mult;
            //ViewData["columns"] = show;
            //ViewData["page"] = currentPage;

            //if (calls.Count != 0)
            //{
            //    if (j > currentPage)
            //    {
            //        ViewData["page"] = currentPage;
            //        return View(calls[currentPage]);
            //    }
            //    else
            //    {
            //        if (j >= cpage)
            //        {
            //            ViewData["page"] = cpage;
            //            return View(calls[cpage]);
            //        }
            //        else
            //        {
            //            cpage = 0;
            //            ViewData["page"] = cpage;
            //            return View(calls[0]);
            //        }
            //    }
            //}
            //else
            //    return View(new List<SMS>());
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
            List<e_r> er = new List<e_r>() { new e_r("Sended","Env"), new e_r("Received", "Rec") };
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