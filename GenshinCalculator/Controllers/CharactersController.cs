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
    public class CharactersController : Controller
    {
        private readonly ApplicationDbContext context;

        public CharactersController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: Characters
        public async Task<IActionResult> Index()
        {
              return context.Characters != null ? 
                          View(await context.Characters.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Characters'  is null.");
        }

        // GET: Characters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || context.Characters == null)
            {
                return NotFound();
            }

            var character = await context.Characters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // GET: Characters/Create
        public IActionResult Create()
        {
            return View(new CharacterCreateModel());
        }

        // POST: Characters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CharacterCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var character = new Character
                {
                    Name = model.Name,
                    Fullname = model.Fullname,
                    AvatarPath = model.AvatarPath,
                    Rarity = model.Rarity,
                    Description = model.Description,
                    DayOfBirth = model.DayOfBirth,
                    Vision = model.Vision,
                    WeaponType = model.WeaponType
                };

                context.Add(character);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Characters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await context.Characters
                .SingleOrDefaultAsync(y => y.Id == id);

            if (character == null)
            {
                return NotFound();
            }

            var model = new CharacterEditModel
            {
                Name = character.Name,
                Fullname = character.Fullname,
                AvatarPath = character.AvatarPath,
                Rarity = character.Rarity,
                Description = character.Description,
                DayOfBirth = character.DayOfBirth,
                Vision = character.Vision,
                WeaponType = character.WeaponType
            };

            ViewBag.Id = character.Id;

            return View(model);
        }

        // POST: Characters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CharacterEditModel model)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await context.Characters
                .SingleOrDefaultAsync(y => y.Id == id);

            if (character == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                character.Name = model.Name;
                character.Fullname = model.Fullname;
                character.AvatarPath = model.AvatarPath;
                character.Rarity = model.Rarity;
                character.Description = model.Description;
                character.DayOfBirth = model.DayOfBirth;
                character.Vision = model.Vision;
                character.WeaponType = model.WeaponType;
            }

            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Characters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || context.Characters == null)
            {
                return NotFound();
            }

            var character = await context.Characters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // POST: Characters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (context.Characters == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Characters'  is null.");
            }
            var character = await context.Characters.FindAsync(id);
            if (character != null)
            {
                context.Characters.Remove(character);
            }
            
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharacterExists(int id)
        {
          return (context.Characters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
