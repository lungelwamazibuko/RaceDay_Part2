using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RaceDay_Part2.Models;
using RaceDay_Part2.Data;

namespace RaceDay_Part2.Controllers
{
    [Authorize(Roles = "Organiser")]
    public class TblEventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TblEventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TblEvents
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblEvent.ToListAsync());
        }

        // GET: TblEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblEvent = await _context.TblEvent
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (tblEvent == null)
            {
                return NotFound();
            }

            return View(tblEvent);
        }

        // GET: TblEvents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,EventName,EventDate,Location,Description,EventType")] TblEvent tblEvent)
        {
            if (!ModelState.IsValid) return View(tblEvent);

            _context.Add(tblEvent);
            await _context.SaveChangesAsync();

            // Redirect to the TblCategories Create action, passing eventId
            return RedirectToAction("Create", "TblCategories", new { eventId = tblEvent.EventId });
        }

        // GET: TblEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblEvent = await _context.TblEvent.FindAsync(id);
            if (tblEvent == null)
            {
                return NotFound();
            }
            return View(tblEvent);
        }

        // POST: TblEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,EventName,EventDate,Location,Description")] TblEvent tblEvent)
        {
            if (id != tblEvent.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblEventExists(tblEvent.EventId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["NewEventId"] = tblEvent.EventId;
                return View(tblEvent);
            }
            return View(tblEvent);
        }

        // GET: TblEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblEvent = await _context.TblEvent
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (tblEvent == null)
            {
                return NotFound();
            }

            return View(tblEvent);
        }

        // POST: TblEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblEvent = await _context.TblEvent.FindAsync(id);
            if (tblEvent != null)
            {
                _context.TblEvent.Remove(tblEvent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblEventExists(int id)
        {
            return _context.TblEvent.Any(e => e.EventId == id);
        }
    }
}
