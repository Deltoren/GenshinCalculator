using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GenshinCalculator.Data;
using GenshinCalculator.Models;

namespace GenshinCalculator.Controllers
{
    public class FurnishingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FurnishingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Furnishings
        public async Task<IActionResult> Index()
        {
              return _context.Furnishings != null ? 
                          View(await _context.Furnishings.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Furnishings'  is null.");
        }

        // GET: Furnishings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Furnishings == null)
            {
                return NotFound();
            }

            var furnishing = await _context.Furnishings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnishing == null)
            {
                return NotFound();
            }

            return View(furnishing);
        }

        // GET: Furnishings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Furnishings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Rarity,ImagePath")] Furnishing furnishing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(furnishing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(furnishing);
        }

        // GET: Furnishings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Furnishings == null)
            {
                return NotFound();
            }

            var furnishing = await _context.Furnishings.FindAsync(id);
            if (furnishing == null)
            {
                return NotFound();
            }
            return View(furnishing);
        }

        // POST: Furnishings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Rarity,ImagePath")] Furnishing furnishing)
        {
            if (id != furnishing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(furnishing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FurnishingExists(furnishing.Id))
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
            return View(furnishing);
        }

        // GET: Furnishings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Furnishings == null)
            {
                return NotFound();
            }

            var furnishing = await _context.Furnishings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furnishing == null)
            {
                return NotFound();
            }

            return View(furnishing);
        }

        // POST: Furnishings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Furnishings == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Furnishings'  is null.");
            }
            var furnishing = await _context.Furnishings.FindAsync(id);
            if (furnishing != null)
            {
                _context.Furnishings.Remove(furnishing);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FurnishingExists(int id)
        {
          return (_context.Furnishings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
