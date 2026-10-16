using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RaceDay_Part2.Views.Home
{
    public class OrganiserDashboardModel : PageModel
    {
        public void OnGet()
        {
            ViewData["Title"] = "Organiser Dashboard";
        }
    }
}
