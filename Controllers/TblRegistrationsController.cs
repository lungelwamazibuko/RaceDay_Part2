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
    public class TblRegistrationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TblRegistrationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TblRegistrations
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblRegistration.ToListAsync());
        }

        // GET: TblRegistrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblRegistration = await _context.TblRegistration
                .FirstOrDefaultAsync(m => m.RegistrationId == id);
            if (tblRegistration == null)
            {
                return NotFound();
            }

            return View(tblRegistration);
        }

        // GET: TblRegistrations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblRegistrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegistrationId,ParticipantId,CategoryId,RegistrationDate,BibNumber,RegistrationStatus")] TblRegistration tblRegistration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblRegistration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblRegistration);
        }

        // GET: TblRegistrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblRegistration = await _context.TblRegistration.FindAsync(id);
            if (tblRegistration == null)
            {
                return NotFound();
            }
            return View(tblRegistration);
        }

        // POST: TblRegistrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegistrationId,ParticipantId,CategoryId,RegistrationDate,BibNumber,RegistrationStatus")] TblRegistration tblRegistration)
        {
            if (id != tblRegistration.RegistrationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblRegistration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblRegistrationExists(tblRegistration.RegistrationId))
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
            return View(tblRegistration);
        }

        // GET: TblRegistrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblRegistration = await _context.TblRegistration
                .FirstOrDefaultAsync(m => m.RegistrationId == id);
            if (tblRegistration == null)
            {
                return NotFound();
            }

            return View(tblRegistration);
        }

        // POST: TblRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblRegistration = await _context.TblRegistration.FindAsync(id);
            if (tblRegistration != null)
            {
                _context.TblRegistration.Remove(tblRegistration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblRegistrationExists(int id)
        {
            return _context.TblRegistration.Any(e => e.RegistrationId == id);
        }
    }
}
