using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RaceDay_Part2.Data;
using RaceDay_Part2.Models;

namespace RaceDay_Part2.Pages.Organiser.Categories
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public TblCategory TblCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TblCategory = await _context.TblCategory
                .Include(c => c.Event)
                .Include(c => c.Route)
                .FirstOrDefaultAsync(m => m.CategoryId == id.Value);

            if (TblCategory == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}