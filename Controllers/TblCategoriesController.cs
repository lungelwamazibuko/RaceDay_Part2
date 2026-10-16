using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RaceDay_Part2.Data;
using RaceDay_Part2.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RaceDay_Part2.Controllers
{
    [Authorize(Roles = "Organiser")]
    public class TblCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TblCategoriesController(ApplicationDbContext context) => _context = context;

        // GET: TblCategories/Create?eventId=11
        public IActionResult Create(int? eventId)
        {
            // populate events select list
            ViewBag.EventId = new SelectList(_context.TblEvent.OrderBy(e => e.EventName), "EventId", "EventName", eventId);

            // populate routes; if eventId provided, filter by it
            var routesQuery = _context.TblRoute.AsQueryable();
            if (eventId.HasValue)
                routesQuery = routesQuery.Where(r => r.EventId == eventId.Value);

            ViewBag.RouteId = new SelectList(routesQuery.OrderBy(r => r.RouteName), "RouteId", "RouteName");

            var model = new TblCategory();
            if (eventId.HasValue) model.EventId = eventId.Value;

            return View(model); // Views/TblCategories/Create.cshtml
        }

        // POST: TblCategories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,EventId,RouteId,CategoryName,Gender,MinimumAge,MaximumAge,DistanceKm")] TblCategory category)
        {
            if (!ModelState.IsValid)
            {
                // re-populate selects on error
                ViewBag.EventId = new SelectList(_context.TblEvent.OrderBy(e => e.EventName), "EventId", "EventName", category.EventId);
                var routesQuery = _context.TblRoute.AsQueryable();
                if (category.EventId > 0) routesQuery = routesQuery.Where(r => r.EventId == category.EventId);
                ViewBag.RouteId = new SelectList(routesQuery.OrderBy(r => r.RouteName), "RouteId", "RouteName", category.RouteId);
                return View(category);
            }

            _context.TblCategory.Add(category);
            await _context.SaveChangesAsync();

            // go back to categories index for that event (adjust action/controller as needed)
            return RedirectToAction("Index", new { eventId = category.EventId });
        }
    }
}
