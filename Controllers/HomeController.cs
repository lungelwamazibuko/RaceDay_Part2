using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RaceDay_Part2.Models;

namespace RaceDay_Part2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult AdminShowRegistrations()
        {
            return View();
        }
        public IActionResult AdminDashboard()
        {
            return View();
        }   
        public IActionResult AdminShowResults()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
