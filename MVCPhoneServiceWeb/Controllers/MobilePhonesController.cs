using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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
        public async Task<IActionResult> Index()
        {
            return View(await _context.MobilePhones.ToListAsync());
        }

        // GET: MobilePhones/Details/5
        public async Task<IActionResult> Details(int? id)
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
        public async Task<IActionResult> Create([Bind("IMEI,Modelo")] MobilePhone mobilePhone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mobilePhone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mobilePhone);
        }

        // GET: MobilePhones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mobilePhone = await _context.MobilePhones.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("IMEI,Modelo")] MobilePhone mobilePhone)
        {
            if (id != mobilePhone.IMEI)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mobilePhone);
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
                return RedirectToAction(nameof(Index));
            }
            return View(mobilePhone);
        }

        // GET: MobilePhones/Delete/5
        public async Task<IActionResult> Delete(int? id)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mobilePhone = await _context.MobilePhones.FindAsync(id);
            _context.MobilePhones.Remove(mobilePhone);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MobilePhoneExists(int id)
        {
            return _context.MobilePhones.Any(e => e.IMEI == id);
        }
    }
}
