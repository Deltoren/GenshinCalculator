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
    public class FoodsController : Controller
    {
        private readonly ApplicationDbContext context;

        public FoodsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: Foods
        public async Task<IActionResult> Index()
        {
              return context.Foods != null ? 
                          View(await context.Foods.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Foods'  is null.");
        }

        // GET: Foods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || context.Foods == null)
            {
                return NotFound();
            }

            var food = await context.Foods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // GET: Foods/Create
        public IActionResult Create()
        {
            return View(new FoodCreateModel());
        }

        // POST: Foods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FoodCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var food = new Food
                {
                    Name = model.Name,
                    Description = model.Description,
                    Rarity = model.Rarity,
                    ImagePath = model.ImagePath
                };
                context.Add(food);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Foods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var food = await context.Foods
                .SingleOrDefaultAsync(y => y.Id == id);
            if (food == null)
            {
                return NotFound();
            }
            var model = new FoodEditModel
            {
                Name = food.Name,
                Description = food.Description,
                Rarity = food.Rarity,
                ImagePath = food.ImagePath
            };
            ViewBag.Id = food.Id;
            return View(model);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, FoodEditModel model)
        {
            if (id == null)
            {
                return NotFound();
            }
            var food = await context.Foods
                .SingleOrDefaultAsync(y => y.Id == id);
            if (food == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                food.Name = model.Name;
                food.Description = model.Description;
                food.Rarity = model.Rarity;
                food.ImagePath = model.ImagePath;
            }
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Foods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || context.Foods == null)
            {
                return NotFound();
            }

            var food = await context.Foods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (context.Foods == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Foods'  is null.");
            }
            var food = await context.Foods.FindAsync(id);
            if (food != null)
            {
                context.Foods.Remove(food);
            }
            
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodExists(int id)
        {
          return (context.Foods?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
