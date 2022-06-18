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
    public class WeaponsController : Controller
    {
        private readonly ApplicationDbContext context;

        public WeaponsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: Weapons
        public async Task<IActionResult> Index()
        {
              return context.Weapons != null ? 
                          View(await context.Weapons.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Weapons'  is null.");
        }

        // GET: Weapons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || context.Weapons == null)
            {
                return NotFound();
            }

            var weapon = await context.Weapons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weapon == null)
            {
                return NotFound();
            }

            return View(weapon);
        }

        // GET: Weapons/Create
        public IActionResult Create()
        {
            return View(new WeaponCreateModel());
        }

        // POST: Weapons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WeaponCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var weapon = new Weapon
                {
                    Name = model.Name,
                    Rarity = model.Rarity,
                    ImagePath = model.ImagePath,
                    WeaponType = model.WeaponType
                };
                context.Add(weapon);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Weapons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var weapon = await context.Weapons
                .SingleOrDefaultAsync(y => y.Id == id);
            if (weapon == null)
            {
                return NotFound();
            }
            var model = new WeaponEditModel
            {
                Name = weapon.Name,
                Rarity = weapon.Rarity,
                ImagePath = weapon.ImagePath,
                WeaponType = weapon.WeaponType
            };
            ViewBag.Id = weapon.Id;
            return View(model);
        }

        // POST: Weapons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, WeaponEditModel model)
        {
            if (id == null)
            {
                return NotFound();
            }
            var weapon = await context.Weapons
                .SingleOrDefaultAsync(y => y.Id == id);
            if (weapon == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                weapon.Name = model.Name;
                weapon.Rarity = model.Rarity;
                weapon.ImagePath = model.ImagePath;
                weapon.WeaponType = model.WeaponType;
            }
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Weapons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || context.Weapons == null)
            {
                return NotFound();
            }

            var weapon = await context.Weapons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weapon == null)
            {
                return NotFound();
            }

            return View(weapon);
        }

        // POST: Weapons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (context.Weapons == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Weapons'  is null.");
            }
            var weapon = await context.Weapons.FindAsync(id);
            if (weapon != null)
            {
                context.Weapons.Remove(weapon);
            }
            
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeaponExists(int id)
        {
          return (context.Weapons?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
