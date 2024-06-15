using Mall_Managment_System.Migrations;
using Mall_Managment_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Mall_Managment_System.Controllers
{
    [Authorize(Roles = "1")]
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

        public IActionResult Edit(int id)
        {
            var fooditem = FoodItem_context.FoodItems.Find(id);
            if (fooditem == null)
            {
                return RedirectToAction("Index");
            }

            var FooditemViewModel = new FooditemViewModel
            {
                FoodCourt_id=fooditem.FoodCourt_id,
                Name = fooditem.Name,
                Description = fooditem.Description,
                Price = fooditem.Price,

                

            };


            ViewData["Image"] = fooditem.Image;


            return View(FooditemViewModel);
        }


        [HttpPost]
        public IActionResult Edit(int id, FooditemViewModel foodView)
        {
            var fooditem = FoodItem_context.FoodItems.Find(id);
            if (fooditem == null)
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                return View(foodView);
            }

            // Update the image file if a new image file is uploaded
            string newFileName = fooditem.Image;
            if (foodView.Photo != null && foodView.Photo.Length > 0)
            {
                newFileName = Guid.NewGuid().ToString() + Path.GetExtension(foodView.Photo.FileName);
                string imageFullPath = Path.Combine(env.WebRootPath, "images", newFileName);

                // Save the new image
                using (var stream = new FileStream(imageFullPath, FileMode.Create))
                {
                    foodView.Photo.CopyTo(stream);
                }

                // Delete the old image if it exists
                if (!string.IsNullOrEmpty(fooditem.Image))
                {
                    string oldImageFullPath = Path.Combine(env.WebRootPath, "images", fooditem.Image);
                    if (System.IO.File.Exists(oldImageFullPath))
                    {
                        System.IO.File.Delete(oldImageFullPath);
                    }
                }
            }

            // Update the shop details
            fooditem.foodcourt = foodView.foodcourt;
            fooditem.FoodCourt_id=fooditem.FoodCourt_id;
            fooditem.Name = foodView.Name;
            fooditem.Description = foodView.Description;
            fooditem.Price=foodView.Price;
          fooditem.Image = newFileName;

            FoodItem_context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var fooditem = FoodItem_context.FoodItems.Find(id);
            if (fooditem == null)
            {
                return RedirectToAction("Index");
            }

            string ImageFullPath = env.WebRootPath + "/products" + fooditem.Image;
            System.IO.File.Delete(ImageFullPath);


            FoodItem_context.FoodItems.Remove(fooditem);
            FoodItem_context.SaveChanges(true);
            return RedirectToAction("Index");
        }






    }
}
