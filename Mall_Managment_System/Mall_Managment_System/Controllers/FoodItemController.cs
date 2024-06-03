using Mall_Managment_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mall_Managment_System.Controllers
{
    public class FoodItemController : Controller
    {
        ApplicationDbContext FoodItem_context;
        IWebHostEnvironment env;

        public FoodItemController(ApplicationDbContext Fooditem, IWebHostEnvironment hc)
        {
            this.FoodItem_context = Fooditem;
            env = hc;
        }

        public IActionResult Index()
        {
            return View(FoodItem_context.FoodItems.ToList());
        }
        public IActionResult AddFoodItem()
        {
            // Fetch the list of shops from the database
            var fooditem = FoodItem_context.FoodItems.ToList();

            // Check if shops is null or empty
            if (fooditem == null || !fooditem.Any())
            {
                // Handle the case where no shops are found
                // For example, you might return an error view or an empty SelectList
                ViewBag.Foodcourtid = new SelectList(Enumerable.Empty<SelectListItem>());
            }
            else
            {
                // Create a SelectList to pass to the view
                ViewBag.ShopId = new SelectList(fooditem, "ID", "Name");
            }

            // Return the view
            return View(new FooditemViewModel()); // Ensure the model is instantiated
            
        }

        [HttpPost]
        public IActionResult AddFoodItem(FooditemViewModel fooditem)
        {
            string filename = "";
            if (fooditem.Photo != null)
            {
                string uploadfolder = Path.Combine(env.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + "_" + fooditem.Photo.FileName;
                string filepath = Path.Combine(uploadfolder, filename);
                fooditem.Photo.CopyTo(new FileStream(filepath, FileMode.Create));
            }
           FoodItem FI = new FoodItem
            {
             FoodCourt_id=fooditem.FoodCourt_id,
             Name = fooditem.Name,
             Description=fooditem.Description,
             Price = fooditem.Price,
             Image = filename,
           };

            FoodItem_context.FoodItems.Add(FI);
            FoodItem_context.SaveChanges();
            ViewBag.success = "Recoard inserted";

            return RedirectToAction("index");
        }



    }
}
