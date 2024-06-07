using Microsoft.AspNetCore.Mvc;

namespace Mall_Managment_System.Controllers
{
    public class WebsiteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

		public IActionResult About()
		{
			return View();
		}

		public IActionResult Blog()
		{
			return View();
		}
		public IActionResult Contact()
		{
			return View();
		}
	}
}
