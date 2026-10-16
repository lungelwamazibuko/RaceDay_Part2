using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RaceDay_Part2.Data;
using RaceDay_Part2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gender = RaceDay_Part2.Data.Gender;

namespace RaceDay_Part2.Controllers
{
    [Authorize(Roles = "Organiser")]
    public class TblCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TblCategoriesController> _logger;

        public TblCategoriesController(ApplicationDbContext context, ILogger<TblCategoriesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: TblCategories
        public async Task<IActionResult> Index(int? eventId)
        {
            try
            {
                var query = _context.TblCategory.AsQueryable();
                if (eventId.HasValue) query = query.Where(c => c.EventId == eventId.Value);

                // Try to include related data
                var list = await query
                    .Include(c => c.Route)
                    .Include(c => c.Event)
                    .OrderBy(c => c.CategoryName)
                    .ToListAsync();

                return View(list);
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                // Schema or DB error: show friendly message and return empty list
                _logger.LogWarning(sqlEx, "Database schema error when loading categories.");
                ViewBag.DbIssue = sqlEx.Message;
                return View(Enumerable.Empty<TblCategory>());
            }
            catch (InvalidOperationException invEx)
            {
                _logger.LogError(invEx, "Database connection/config error.");
                ViewBag.DbIssue = invEx.Message;
                return View(Enumerable.Empty<TblCategory>());
            }
        }

        // GET: TblCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Index));

            var tblCategory = await _context.TblCategory
                .AsNoTracking()
                .Include(c => c.Event)
                .Include(c => c.Route)
                .FirstOrDefaultAsync(c => c.CategoryId == id.Value);

            if (tblCategory == null) return NotFound();

            return View(tblCategory);
        }

        // GET: TblCategories/Create?eventId=11
        [AllowAnonymous]
        public IActionResult Create(int? eventId)
        {
            PopulateSelectLists(eventId);
            var model = new TblCategory();
            if (eventId.HasValue)
            {
                model.EventId = eventId.Value;
                var ev = _context.TblEvent.Find(eventId.Value);
                ViewBag.SelectedEventName = ev?.EventName ?? string.Empty;
            }
            return View(model);
        }

        // POST: TblCategories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,EventId,RouteId,CategoryName,Gender,MinimumAge,MaximumAge,DistanceKm")] TblCategory category)
        {
            if (category.EventId == 0 || !_context.TblEvent.Any(e => e.EventId == category.EventId))
            {
                ModelState.AddModelError("EventId", "Selected event does not exist.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState invalid when creating category.");
                PopulateSelectLists(category.EventId);
                if (category.EventId != 0)
                {
                    ViewBag.SelectedEventName = _context.TblEvent.Find(category.EventId)?.EventName;
                }
                return View(category);
            }

            _context.TblCategory.Add(category);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Category saved: Id={CategoryId}", category.CategoryId);

            return RedirectToAction(nameof(Details), new { id = category.CategoryId });
        }

        // GET: TblCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Index));

            var category = await _context.TblCategory.FindAsync(id.Value);
            if (category == null) return NotFound();

            PopulateSelectLists(category.EventId);
            return View(category);
        }

        // POST: TblCategories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,EventId,RouteId,CategoryName,Gender,MinimumAge,MaximumAge,DistanceKm")] TblCategory category)
        {
            if (id != category.CategoryId) return NotFound();

            if (category.EventId == 0 || !_context.TblEvent.Any(e => e.EventId == category.EventId))
            {
                ModelState.AddModelError("EventId", "Selected event does not exist.");
            }

            if (!ModelState.IsValid)
            {
                PopulateSelectLists(category.EventId);
                return View(category);
            }

            try
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.TblCategory.AnyAsync(e => e.CategoryId == category.CategoryId))
                {
                    return NotFound();
                }
                throw;
            }

            return RedirectToAction(nameof(Index), new { eventId = category.EventId });
        }

        // GET: TblCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Index));

            var tblCategory = await _context.TblCategory
                .Include(c => c.Event)
                .Include(c => c.Route)
                .FirstOrDefaultAsync(m => m.CategoryId == id.Value);

            if (tblCategory == null) return NotFound();

            return View(tblCategory);
        }

        // POST: TblCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.TblCategory.FindAsync(id);
            if (category != null)
            {
                _context.TblCategory.Remove(category);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index), new { eventId = category?.EventId });
        }

        // JSON endpoint returns global routes
        [HttpGet]
        public IActionResult GetRoutes(int eventId)
        {
            var routes = _context.TblRoute
                .OrderBy(r => r.RouteName)
                .Select(r => new { id = r.RouteId, text = $"{r.RouteName} ({r.DistanceKm} km)" })
                .ToList();
            return Json(routes);
        }

        // helpers
        private void PopulateSelectLists(int? selectedEventId = null)
        {
            ViewBag.EventId = new SelectList(_context.TblEvent.OrderBy(e => e.EventName), "EventId", "EventName", selectedEventId);

            ViewBag.RouteId = new SelectList(
                _context.TblRoute.OrderBy(r => r.RouteName).Select(r => new { r.RouteId, Text = r.RouteName + " (" + r.DistanceKm + " km)" }),
                "RouteId", "Text", selectedEventId == null ? null : (object?)null);

            var genderItems = Enum.GetValues(typeof(Gender))
                .Cast<Gender>()
                .Select(g => new SelectListItem { Value = g.ToString(), Text = g.ToString() })
                .ToList();
            ViewBag.Genders = genderItems;

            var categoryTypeItems = Enum.GetValues(typeof(CategoryType))
                .Cast<CategoryType>()
                .Select(t => new SelectListItem { Value = t.ToString(), Text = t.ToString() })
                .ToList();
            ViewBag.CategoryTypes = categoryTypeItems;
        }
    }
}
