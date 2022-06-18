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
    public class CharacterRegionsController : Controller
    {
        private readonly ApplicationDbContext context;

        public CharacterRegionsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: CharacterRegions
        public async Task<IActionResult> Index(int? characterId)
        {
            if (characterId == null)
            {
                return NotFound();
            }

            var character = await context.Characters
                .SingleOrDefaultAsync(x => x.Id == characterId);

            if (character == null)
            {
                return NotFound();
            }

            ViewBag.CharacterId = character.Id;

            var characterRegions = await context.CharacterRegion
                .Include(w => w.Character)
                .Where(x => x.CharacterId == characterId)
                .ToListAsync();

            return View(characterRegions);
        }

        // GET: CharacterRegions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || context.CharacterRegion == null)
            {
                return NotFound();
            }

            var characterRegion = await context.CharacterRegion
                .Include(c => c.Character)
                .Include(c => c.Region)
                .FirstOrDefaultAsync(m => m.CharacterId == id);
            if (characterRegion == null)
            {
                return NotFound();
            }

            return View(characterRegion);
        }

        // GET: CharacterRegions/Create
        public IActionResult Create()
        {
            ViewData["CharacterId"] = new SelectList(context.Characters, "Id", "Id");
            ViewData["RegionId"] = new SelectList(context.Regions, "Id", "Id");
            return View();
        }

        // POST: CharacterRegions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CharacterId,RegionId")] CharacterRegion characterRegion)
        {
            if (ModelState.IsValid)
            {
                context.Add(characterRegion);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CharacterId"] = new SelectList(context.Characters, "Id", "Id", characterRegion.CharacterId);
            ViewData["RegionId"] = new SelectList(context.Regions, "Id", "Id", characterRegion.RegionId);
            return View(characterRegion);
        }

        // GET: CharacterRegions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || context.CharacterRegion == null)
            {
                return NotFound();
            }

            var characterRegion = await context.CharacterRegion.FindAsync(id);
            if (characterRegion == null)
            {
                return NotFound();
            }
            ViewData["CharacterId"] = new SelectList(context.Characters, "Id", "Id", characterRegion.CharacterId);
            ViewData["RegionId"] = new SelectList(context.Regions, "Id", "Id", characterRegion.RegionId);
            return View(characterRegion);
        }

        // POST: CharacterRegions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CharacterId,RegionId")] CharacterRegion characterRegion)
        {
            if (id != characterRegion.CharacterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(characterRegion);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharacterRegionExists(characterRegion.CharacterId))
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
            ViewData["CharacterId"] = new SelectList(context.Characters, "Id", "Id", characterRegion.CharacterId);
            ViewData["RegionId"] = new SelectList(context.Regions, "Id", "Id", characterRegion.RegionId);
            return View(characterRegion);
        }

        // GET: CharacterRegions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || context.CharacterRegion == null)
            {
                return NotFound();
            }

            var characterRegion = await context.CharacterRegion
                .Include(c => c.Character)
                .Include(c => c.Region)
                .FirstOrDefaultAsync(m => m.CharacterId == id);
            if (characterRegion == null)
            {
                return NotFound();
            }

            return View(characterRegion);
        }

        // POST: CharacterRegions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (context.CharacterRegion == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CharacterRegion'  is null.");
            }
            var characterRegion = await context.CharacterRegion.FindAsync(id);
            if (characterRegion != null)
            {
                context.CharacterRegion.Remove(characterRegion);
            }
            
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharacterRegionExists(int id)
        {
          return (context.CharacterRegion?.Any(e => e.CharacterId == id)).GetValueOrDefault();
        }
    }
}
