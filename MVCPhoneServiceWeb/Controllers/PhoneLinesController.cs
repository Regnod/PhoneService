using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MVCPhoneServiceWeb.Controllers
{
    public class PhoneLinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhoneLinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PhoneLines
        public async Task<IActionResult> Index()
        {
            return View(await _context.PhoneLines.ToListAsync());
        }

        // GET: PhoneLines/Details/5
        public async Task<IActionResult> Details(int? id)
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
                return RedirectToAction(nameof(Index));
            }
            return View(phoneLine);
        }

        // GET: PhoneLines/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
        public async Task<IActionResult> Edit(int id, [Bind("PhoneNumber,PUK,PIN")] PhoneLine phoneLine)
        {
            if (id != phoneLine.PhoneNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phoneLine);
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
                return RedirectToAction(nameof(Index));
            }
            return View(phoneLine);
        }

        // GET: PhoneLines/Delete/5
        public async Task<IActionResult> Delete(int? id)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phoneLine = await _context.PhoneLines.FindAsync(id);
            _context.PhoneLines.Remove(phoneLine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhoneLineExists(int id)
        {
            return _context.PhoneLines.Any(e => e.PhoneNumber == id);
        }
    }
}
