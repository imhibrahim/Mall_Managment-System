using Mall_Managment_System.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Mall_Managment_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Shops()
        {
            return View();
        }
        public IActionResult Booking()
        {
            return View();
        }

        public IActionResult Gallary()
        {
            return View();
        }
        public IActionResult Movies()
        {
            return View();
        }
        public IActionResult Food_Court()
        {
            return View();
        }
        public IActionResult Food_Item()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Feedback()
        {
            return View();
        }
        public IActionResult Users()
        {
            return View();
        }
        public IActionResult Item()
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
