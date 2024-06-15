using Mall_Managment_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;

namespace Mall_Managment_System.Controllers
{
    [Authorize(Roles = "1")]
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext shop_context;
        private readonly IWebHostEnvironment env;

      
        public ShopController(ApplicationDbContext shops, IWebHostEnvironment hc)
        {
            this.shop_context = shops;
            env = hc;
        }

      
        public IActionResult Index()
        {
            return View(shop_context.Shops.ToList());
        }

       
        public IActionResult Addproduct()
        {
            //var shop = shop_context.Users.FirstOrDefault(x => x.Userid ==id);
            //if (shop == null)
            //{

            //    return View("Register");
            //}


           // ViewBag.shop = shop;
            return View();
        }
      
        [HttpPost]
        public IActionResult Addproduct(ShopViewModel shop)
        {
            string filename = "";
            if (shop.Photo != null)
            {
                string uploadfolder = Path.Combine(env.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + "_" + shop.Photo.FileName;
                string filepath = Path.Combine(uploadfolder, filename);
                shop.Photo.CopyTo(new FileStream(filepath, FileMode.Create));
            }
            Shops S = new Shops
            {
                Name = shop.Name,
                Description = shop.Description,
                Image = filename
            };

            shop_context.Shops.Add(S);
            shop_context.SaveChanges();
            ViewBag.success = "Recoard inserted";

            return RedirectToAction("index");
        }
 
        public IActionResult Edit(int id)
        {
            var shop = shop_context.Shops.Find(id);
            if (shop == null)
            {
                return RedirectToAction("Index");
            }

            var shopViewModel = new ShopViewModel
            {
                ID = shop.ID,
                Name = shop.Name,
                Description = shop.Description,
               
            };

            
            ViewData["Image"] = shop.Image;
              

            return View(shopViewModel);
        }

    
        [HttpPost]
        public IActionResult Edit(int id, ShopViewModel shopView)
        {
            var shop = shop_context.Shops.Find(id);
            if (shop == null)
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                return View(shopView);
            }

            // Update the image file if a new image file is uploaded
            string newFileName = shop.Image;
            if (shopView.Photo != null && shopView.Photo.Length > 0)
            {
                newFileName = Guid.NewGuid().ToString() + Path.GetExtension(shopView.Photo.FileName);
                string imageFullPath = Path.Combine(env.WebRootPath, "images", newFileName);

                // Save the new image
                using (var stream = new FileStream(imageFullPath, FileMode.Create))
                {
                    shopView.Photo.CopyTo(stream);
                }

                // Delete the old image if it exists
                if (!string.IsNullOrEmpty(shop.Image))
                {
                    string oldImageFullPath = Path.Combine(env.WebRootPath, "images", shop.Image);
                    if (System.IO.File.Exists(oldImageFullPath))
                    {
                        System.IO.File.Delete(oldImageFullPath);
                    }
                }
            }

            // Update the shop details
            shop.Name = shopView.Name;
            shop.Description = shopView.Description;
            shop.Image = newFileName;

            shop_context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "1")]
        public IActionResult Delete(int id)
        {
            var shop = shop_context.Shops.Find(id);
            if (shop == null)
            {
                return RedirectToAction("Index");
            }

            string ImageFullPath = env.WebRootPath + "/products" + shop.Image;
            System.IO.File.Delete(ImageFullPath);


            shop_context.Shops.Remove(shop);
            shop_context.SaveChanges(true);
            return RedirectToAction("Index");
        }





    }
}
