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
    public class TblWeathersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TblWeathersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TblWeathers
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblWeather.ToListAsync());
        }

        // GET: TblWeathers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblWeather = await _context.TblWeather
                .FirstOrDefaultAsync(m => m.WeatherId == id);
            if (tblWeather == null)
            {
                return NotFound();
            }

            return View(tblWeather);
        }

        // GET: TblWeathers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblWeathers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WeatherId,EventId,WeatherDate,Temperature,Conditions,WindSpeed")] TblWeather tblWeather)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblWeather);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblWeather);
        }

        // GET: TblWeathers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblWeather = await _context.TblWeather.FindAsync(id);
            if (tblWeather == null)
            {
                return NotFound();
            }
            return View(tblWeather);
        }

        // POST: TblWeathers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WeatherId,EventId,WeatherDate,Temperature,Conditions,WindSpeed")] TblWeather tblWeather)
        {
            if (id != tblWeather.WeatherId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblWeather);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblWeatherExists(tblWeather.WeatherId))
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
            return View(tblWeather);
        }

        // GET: TblWeathers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblWeather = await _context.TblWeather
                .FirstOrDefaultAsync(m => m.WeatherId == id);
            if (tblWeather == null)
            {
                return NotFound();
            }

            return View(tblWeather);
        }

        // POST: TblWeathers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblWeather = await _context.TblWeather.FindAsync(id);
            if (tblWeather != null)
            {
                _context.TblWeather.Remove(tblWeather);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblWeatherExists(int id)
        {
            return _context.TblWeather.Any(e => e.WeatherId == id);
        }
    }
}
