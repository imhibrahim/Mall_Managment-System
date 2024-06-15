using Mall_Managment_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mall_Managment_System.Controllers
{
    [Authorize(Roles = "1")]
    public class FoodCourtController : Controller
    {
        ApplicationDbContext foodCourt_context;
        IWebHostEnvironment env;

        public FoodCourtController(ApplicationDbContext FoodCourt, IWebHostEnvironment hc)
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


        public IActionResult Edit(int id)
        {
            var foodcourt = foodCourt_context.FoodCourt.Find(id);
            if (foodcourt == null)
            {
                return RedirectToAction("Index");
            }

            var FoodCourtViewModel = new FoodCourtViewModel
            {
                ID=foodcourt.ID,
                Name=foodcourt.Name,
                Description=foodcourt.Description,
             

            };


            ViewData["Image"] = foodcourt.Image;


            return View(FoodCourtViewModel);
        }


        [HttpPost]
        public IActionResult Edit(int id, FoodCourtViewModel courtView)
        {
            var court = foodCourt_context.FoodCourt.Find(id);
            if (court == null)
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                return View(courtView);
            }

            // Update the image file if a new image file is uploaded
            string newFileName = court.Image;
            if (courtView.Photo != null && courtView.Photo.Length > 0)
            {
                newFileName = Guid.NewGuid().ToString() + Path.GetExtension(courtView.Photo.FileName);
                string imageFullPath = Path.Combine(env.WebRootPath, "images", newFileName);

                // Save the new image
                using (var stream = new FileStream(imageFullPath, FileMode.Create))
                {
                    courtView.Photo.CopyTo(stream);
                }

                // Delete the old image if it exists
                if (!string.IsNullOrEmpty(court.Image))
                {
                    string oldImageFullPath = Path.Combine(env.WebRootPath, "images",  court.Image);
                    if (System.IO.File.Exists(oldImageFullPath))
                    {
                        System.IO.File.Delete(oldImageFullPath);
                    }
                }
            }

            // Update the shop details
            court.Name = courtView.Name;
            court.Description = courtView.Description;
            court.Image = newFileName;

            foodCourt_context.SaveChanges();

            return RedirectToAction("Index");
        }




        public IActionResult Delete(int id)
        {
            var foodcourt = foodCourt_context.FoodCourt.Find(id);
            if (foodcourt == null)
            {
                return RedirectToAction("Index");
            }

            string ImageFullPath = env.WebRootPath + "/products" + foodcourt.Image;
            System.IO.File.Delete(ImageFullPath);


            foodCourt_context.FoodCourt.Remove(foodcourt);
            foodCourt_context.SaveChanges(true);
            return RedirectToAction("Index");
        }
    }
}
