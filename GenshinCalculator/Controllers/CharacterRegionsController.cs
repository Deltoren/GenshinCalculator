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

            ViewBag.Character = character;

            var characterRegions = await context.CharacterRegions
                .Include(w => w.Region)
                .Where(x => x.CharacterId == characterId)
                .ToListAsync();

            return View(characterRegions);
        }

        // GET: CharacterRegions/Create
        public async Task<IActionResult> Create(int? characterId)
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

            ViewBag.Character = character;
            ViewData["RegionId"] = new SelectList(context.Regions, "Id", "Name");

            return View(new CharacterRegionCreateModel());
        }

        // POST: CharacterRegions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? characterId, CharacterRegionCreateModel model)
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

            var item = await context.CharacterRegions
                .Include(h => h.Character)
                .Include(h => h.Region)
                .SingleOrDefaultAsync(m => m.CharacterId == characterId && m.RegionId == model.RegionId);

            if (item != null)
            {
                ViewBag.Error = "Данный персонаж уже имеет данный регион";
                ViewBag.Character = character;
                ViewData["RegionId"] = new SelectList(context.Regions, "Id", "Name");
                return View(model);
            }


            if (ModelState.IsValid)
            {
                var characterRegion = new CharacterRegion
                {
                    CharacterId = character.Id,
                    RegionId = model.RegionId
                };

                context.Add(characterRegion);
                await context.SaveChangesAsync();
                return RedirectToAction("Index", new { characterId = character.Id });
            }

            ViewData["RegionId"] = new SelectList(context.Regions, "Id", "Name");

            return View(model);
        }

        // GET: CharacterRegions/Delete/5
        public async Task<IActionResult> Delete(int? characterId, int? regionId)
        {
            if (characterId == null || regionId == null)
            {
                return NotFound();
            }

            var placement = await this.context.CharacterRegions
                .Include(h => h.Character)
                .Include(h => h.Region)
                .SingleOrDefaultAsync(m => m.CharacterId == characterId && m.RegionId == regionId);

            if (placement == null)
            {
                return NotFound();
            }

            return View(placement);
        }

        // POST: CharacterRegions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int characterId, int regionId)
        {
            var characterRegion = await context.CharacterRegions.SingleOrDefaultAsync(m => m.CharacterId == characterId && m.RegionId == regionId);
            context.CharacterRegions.Remove(characterRegion);
            await context.SaveChangesAsync();
            return RedirectToAction("Index", new { characterId = characterId });
        }
    }
}
