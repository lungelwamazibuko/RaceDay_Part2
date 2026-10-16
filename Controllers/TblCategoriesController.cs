using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RaceDay_Part2.Models;
using RaceDay_Part2.Data;

namespace RaceDay_Part2.Controllers
{
    public class TblCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TblCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TblCategories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TblCategory.Include(t => t.Event).Include(t => t.Route);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TblCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCategory = await _context.TblCategory
                .Include(t => t.Event)
                .Include(t => t.Route)
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (tblCategory == null)
            {
                return NotFound();
            }

            return View(tblCategory);
        }

        // GET: TblCategories/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Set<TblEvent>(), "EventId", "EventId");
            ViewData["RouteId"] = new SelectList(_context.TblRoute, "RouteId", "RouteId");
            return View();
        }

        // POST: TblCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,EventId,RouteId,CategoryName,Gender,MinimumAge,MaximumAge")] TblCategory tblCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Set<TblEvent>(), "EventId", "EventId", tblCategory.EventId);
            ViewData["RouteId"] = new SelectList(_context.TblRoute, "RouteId", "RouteId", tblCategory.RouteId);
            return View(tblCategory);
        }

        // GET: TblCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCategory = await _context.TblCategory.FindAsync(id);
            if (tblCategory == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Set<TblEvent>(), "EventId", "EventId", tblCategory.EventId);
            ViewData["RouteId"] = new SelectList(_context.TblRoute, "RouteId", "RouteId", tblCategory.RouteId);
            return View(tblCategory);
        }

        // POST: TblCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,EventId,RouteId,CategoryName,Gender,MinimumAge,MaximumAge")] TblCategory tblCategory)
        {
            if (id != tblCategory.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblCategoryExists(tblCategory.CategoryId))
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
            ViewData["EventId"] = new SelectList(_context.Set<TblEvent>(), "EventId", "EventId", tblCategory.EventId);
            ViewData["RouteId"] = new SelectList(_context.TblRoute, "RouteId", "RouteId", tblCategory.RouteId);
            return View(tblCategory);
        }

        // GET: TblCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCategory = await _context.TblCategory
                .Include(t => t.Event)
                .Include(t => t.Route)
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (tblCategory == null)
            {
                return NotFound();
            }

            return View(tblCategory);
        }

        // POST: TblCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblCategory = await _context.TblCategory.FindAsync(id);
            if (tblCategory != null)
            {
                _context.TblCategory.Remove(tblCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblCategoryExists(int id)
        {
            return _context.TblCategory.Any(e => e.CategoryId == id);
        }
    }
}
