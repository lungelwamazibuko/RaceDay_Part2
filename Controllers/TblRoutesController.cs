using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RaceDay.Models;
using RaceDay_Part2.Data;

namespace RaceDay_Part2.Controllers
{
    public class TblRoutesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TblRoutesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TblRoutes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblRoute.ToListAsync());
        }

        // GET: TblRoutes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblRoute = await _context.TblRoute
                .FirstOrDefaultAsync(m => m.RouteId == id);
            if (tblRoute == null)
            {
                return NotFound();
            }

            return View(tblRoute);
        }

        // GET: TblRoutes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblRoutes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RouteId,EventId,RouteName,DistanceKm,Description")] TblRoute tblRoute)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblRoute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblRoute);
        }

        // GET: TblRoutes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblRoute = await _context.TblRoute.FindAsync(id);
            if (tblRoute == null)
            {
                return NotFound();
            }
            return View(tblRoute);
        }

        // POST: TblRoutes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RouteId,EventId,RouteName,DistanceKm,Description")] TblRoute tblRoute)
        {
            if (id != tblRoute.RouteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblRoute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblRouteExists(tblRoute.RouteId))
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
            return View(tblRoute);
        }

        // GET: TblRoutes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblRoute = await _context.TblRoute
                .FirstOrDefaultAsync(m => m.RouteId == id);
            if (tblRoute == null)
            {
                return NotFound();
            }

            return View(tblRoute);
        }

        // POST: TblRoutes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblRoute = await _context.TblRoute.FindAsync(id);
            if (tblRoute != null)
            {
                _context.TblRoute.Remove(tblRoute);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblRouteExists(int id)
        {
            return _context.TblRoute.Any(e => e.RouteId == id);
        }
    }
}
