using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RaceDay_Part2.Data;
using RaceDay_Part2.Models;
using System.Linq;
using System.Threading.Tasks;
using Gender = RaceDay_Part2.Data.Gender;

namespace RaceDay_Part2.Controllers
{
    [Authorize(Roles = "Organiser")]
    public class TblCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TblCategoriesController(ApplicationDbContext context) => _context = context;

        [AllowAnonymous]
        // GET: TblCategories/Create?eventId=11
        public IActionResult Create(int? eventId)
        {
            ViewBag.EventId = new SelectList(_context.TblEvent.OrderBy(e => e.EventName), "EventId", "EventName", eventId);

            ViewBag.RouteId = _context.TblRoute
                .Where(r => !eventId.HasValue || r.EventId == eventId.Value)
                .OrderBy(r => r.RouteName)
                .Select(r => new SelectListItem {
                    Value = r.RouteId.ToString(),
                    Text = $"{r.RouteName} ({r.DistanceKm} km)"
                })
                .ToList();

            // Offer existing category names for the given event as optional suggestions
            var categoryNames = _context.TblCategory
                .Where(c => !eventId.HasValue || c.EventId == eventId.Value)
                .Select(c => c.CategoryName)
                .Distinct()
                .OrderBy(n => n)
                .ToList();
            ViewBag.CategoryName = new SelectList(categoryNames.Select(n => new { Value = n, Text = n }), "Value", "Text");

            var model = new TblCategory();
            if (eventId.HasValue) model.EventId = eventId.Value;

            var genderItems = Enum.GetValues(typeof(Gender))
                .Cast<Gender>()
                .Select(g => new SelectListItem { Value = g.ToString(), Text = g.ToString() })
                .ToList();
            ViewBag.Genders = genderItems;

            // optional: CategoryType selector
            var categoryTypeItems = Enum.GetValues(typeof(CategoryType))
                .Cast<CategoryType>()
                .Select(t => new SelectListItem { Value = t.ToString(), Text = t.ToString() })
                .ToList();
            ViewBag.CategoryTypes = categoryTypeItems;

            return View(model); // Views/TblCategories/Create.cshtml
        }

        // POST: TblCategories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,EventId,RouteId,CategoryName,Gender,MinimumAge,MaximumAge,DistanceKm")] TblCategory category)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EventId = new SelectList(_context.TblEvent.OrderBy(e => e.EventName), "EventId", "EventName", category.EventId);

                var routesQuery = _context.TblRoute.AsQueryable();
                if (category.EventId > 0) routesQuery = routesQuery.Where(r => r.EventId == category.EventId);
                ViewBag.RouteId = new SelectList(routesQuery.OrderBy(r => r.RouteName), "RouteId", "RouteName", category.RouteId);

                var categoryNames = _context.TblCategory
                    .Where(c => c.EventId == category.EventId)
                    .Select(c => c.CategoryName)
                    .Distinct()
                    .OrderBy(n => n)
                    .ToList();
                ViewBag.CategoryName = new SelectList(categoryNames.Select(n => new { Value = n, Text = n }), "Value", "Text", category.CategoryName);

                return View(category);
            }

            _context.TblCategory.Add(category);
            await _context.SaveChangesAsync();

            // go back to categories index for that event (adjust action/controller as needed)
            return RedirectToAction("Index", new { eventId = category.EventId });
        }

        // JSON endpoint used by client JS to load routes dynamically
        [HttpGet]
        public IActionResult GetRoutes(int eventId)
        {
            var routes = _context.TblRoute
                .Where(r => r.EventId == eventId)
                .OrderBy(r => r.RouteName)
                .Select(r => new { id = r.RouteId, text = $"{r.RouteName} ({r.DistanceKm} km)" })
                .ToList();

            return Json(routes);
        }
    }
}
