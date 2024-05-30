using Mall_Managment_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mall_Managment_System.Controllers
{
    public class FoodCourtController : Controller
    {
        FoodCourtDbContext foodCourt_context;
        IWebHostEnvironment env;

        public FoodCourtController(FoodCourtDbContext FoodCourt, IWebHostEnvironment hc)
        {
            this.foodCourt_context = FoodCourt;
            env = hc;
        }
        public IActionResult Index()
        {
            return View(foodCourt_context.FoodCourt.ToList());
        }
        public IActionResult AddCourt()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCourt(FoodCourtViewModel foodCourt)
        {
            string filename = "";
            if (foodCourt.Photo != null)
            {
                string uploadfolder = Path.Combine(env.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + "_" + foodCourt.Photo.FileName;
                string filepath = Path.Combine(uploadfolder, filename);
                foodCourt.Photo.CopyTo(new FileStream(filepath, FileMode.Create));
            }
            Foodcourt FC = new Foodcourt
            {
                Name=foodCourt.Name,
                Description=foodCourt.Description,
                Image=filename
            };

            foodCourt_context.FoodCourt.Add(FC);
            foodCourt_context.SaveChanges();
            ViewBag.success = "Recoard inserted";

            return RedirectToAction("index");
        }
    }
}
