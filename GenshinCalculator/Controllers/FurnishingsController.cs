using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GenshinCalculator.Data;
using GenshinCalculator.Models;
using GenshinCalculator.Models.ViewModels;

namespace GenshinCalculator.Controllers
{
    public class FurnishingsController : Controller
    {
        private readonly ApplicationDbContext context;

        public FurnishingsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: Furnishings
        public async Task<IActionResult> Index()
        {
              return context.Furnishings != null ? 
                          View(await context.Furnishings.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Furnishings'  is null.");
        }

        // GET: Furnishings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || context.Furnishings == null)
            {
                return NotFound();
            }

            var furnishing = await context.Furnishings
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
            return View(new FurnishingCreateModel());
        }

        // POST: Furnishings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FurnishingCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var furnishing = new Furnishing
                {
                    Name = model.Name,
                    Description = model.Description,
                    Rarity = model.Rarity,
                    ImagePath = model.ImagePath
                };
                context.Add(furnishing);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Furnishings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var furnishing = await context.Furnishings
                .SingleOrDefaultAsync(y => y.Id == id);
            if (furnishing == null)
            {
                return NotFound();
            }
            var model = new FurnishingEditModel
            {
                Name = furnishing.Name,
                Description = furnishing.Description,
                Rarity = furnishing.Rarity,
                ImagePath = furnishing.ImagePath
            };
            ViewBag.Id = furnishing.Id;
            return View(model);
        }

        // POST: Furnishings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, FurnishingEditModel model)
        {
            if (id == null)
            {
                return NotFound();
            }
            var furnishing = await context.Furnishings
                .SingleOrDefaultAsync(y => y.Id == id);
            if (furnishing == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                furnishing.Name = model.Name;
                furnishing.Description = model.Description;
                furnishing.Rarity = model.Rarity;
                furnishing.ImagePath = model.ImagePath;
            }
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Furnishings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || context.Furnishings == null)
            {
                return NotFound();
            }

            var furnishing = await context.Furnishings
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
            if (context.Furnishings == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Furnishings'  is null.");
            }
            var furnishing = await context.Furnishings.FindAsync(id);
            if (furnishing != null)
            {
                context.Furnishings.Remove(furnishing);
            }
            
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FurnishingExists(int id)
        {
          return (context.Furnishings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
