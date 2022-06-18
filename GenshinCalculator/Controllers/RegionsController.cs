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
    public class RegionsController : Controller
    {
        private readonly ApplicationDbContext context;

        public RegionsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: Regions
        public async Task<IActionResult> Index()
        {
              return context.Regions != null ? 
                          View(await context.Regions.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Regions'  is null.");
        }

        // GET: Regions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || context.Regions == null)
            {
                return NotFound();
            }

            var region = await context.Regions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // GET: Regions/Create
        public IActionResult Create()
        {
            return View(new RegionCreateModel());
        }

        // POST: Regions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegionCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var region = new Region
                {
                    Name = model.Name
                };

                context.Add(region);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Regions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await context.Regions
                .SingleOrDefaultAsync(y => y.Id == id);

            if (region == null)
            {
                return NotFound();
            }

            var model = new RegionEditModel
            {
                Name = region.Name
            };

            ViewBag.Id = region.Id;

            return View(model);
        }

        // POST: Regions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, RegionEditModel model)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await context.Regions
                .SingleOrDefaultAsync(y => y.Id == id);

            if (region == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                region.Name = model.Name;
            }

            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Regions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || context.Regions == null)
            {
                return NotFound();
            }

            var region = await context.Regions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // POST: Regions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (context.Regions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Regions'  is null.");
            }
            var region = await context.Regions.FindAsync(id);
            if (region != null)
            {
                context.Regions.Remove(region);
            }
            
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegionExists(int id)
        {
          return (context.Regions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
