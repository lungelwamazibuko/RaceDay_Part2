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
    public class TblResultsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TblResultsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TblResults
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblResult.ToListAsync());
        }

        // GET: TblResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblResult = await _context.TblResult
                .FirstOrDefaultAsync(m => m.ResultId == id);
            if (tblResult == null)
            {
                return NotFound();
            }

            return View(tblResult);
        }

        // GET: TblResults/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResultId,RegistrationId,FinishTime,Position,ResultStatus")] TblResult tblResult)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblResult);
        }

        // GET: TblResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblResult = await _context.TblResult.FindAsync(id);
            if (tblResult == null)
            {
                return NotFound();
            }
            return View(tblResult);
        }

        // POST: TblResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResultId,RegistrationId,FinishTime,Position,ResultStatus")] TblResult tblResult)
        {
            if (id != tblResult.ResultId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblResultExists(tblResult.ResultId))
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
            return View(tblResult);
        }

        // GET: TblResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblResult = await _context.TblResult
                .FirstOrDefaultAsync(m => m.ResultId == id);
            if (tblResult == null)
            {
                return NotFound();
            }

            return View(tblResult);
        }

        // POST: TblResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblResult = await _context.TblResult.FindAsync(id);
            if (tblResult != null)
            {
                _context.TblResult.Remove(tblResult);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblResultExists(int id)
        {
            return _context.TblResult.Any(e => e.ResultId == id);
        }
    }
}
