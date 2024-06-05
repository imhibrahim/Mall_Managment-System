using Mall_Managment_System.Migrations;
using Mall_Managment_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Mall_Managment_System.Controllers
{
    public class FoodItemController : Controller
    {

        private readonly ApplicationDbContext FoodItem_context;
        private readonly IWebHostEnvironment env;


        public FoodItemController(ApplicationDbContext Fooditem, IWebHostEnvironment hc)
        {
            this.FoodItem_context = Fooditem;
            env = hc;
        }

        public IActionResult Index()
        {
            return View(FoodItem_context.FoodItems.ToList());
        }
        public IActionResult Addfooditem()
        {
            // Fetch the list of food courts from the database
            var foodCourts = FoodItem_context.FoodCourt.ToList();

            // Check if food courts is null or empty
            if (foodCourts == null || !foodCourts.Any())
            {
                // Handle the case where no food courts are found
                ViewBag.foodcourtid = new SelectList(Enumerable.Empty<SelectListItem>());
            }
            else
            {
                // Create a SelectList to pass to the view
                ViewBag.foodcourtid = new SelectList(foodCourts, "ID", "Name");
            }

            // Return the view with an instantiated model
            return View(new FooditemViewModel());
        }



        [HttpPost]
        public IActionResult Addfooditem(FooditemViewModel fooditem)
        {
            string filename = "";
            if (fooditem.Photo != null)
            {
                string uploadfolder = Path.Combine(env.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + "_" + fooditem.Photo.FileName;
                string filepath = Path.Combine(uploadfolder, filename);
                fooditem.Photo.CopyTo(new FileStream(filepath, FileMode.Create));
            }
            FoodItems FI = new FoodItems
            {
                FoodCourt_id = fooditem.FoodCourt_id,
                Name = fooditem.Name,
                Description = fooditem.Description,
                Price = fooditem.Price,
                Image = filename


            };

            FoodItem_context.FoodItems.Add(FI);
            FoodItem_context.SaveChanges();
            ViewBag.success = "Recoard inserted";

            return RedirectToAction("index");
        }
    }
}
