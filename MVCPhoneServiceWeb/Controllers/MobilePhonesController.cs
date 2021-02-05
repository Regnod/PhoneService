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

namespace MVCPhoneServiceWeb.Controllers
{
    public class MobilePhonesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MobilePhonesController(ApplicationDbContext context)
        {
            _context = context;
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

            return View(result.Item1);
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
