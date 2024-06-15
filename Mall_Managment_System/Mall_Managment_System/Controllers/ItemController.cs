using Mall_Managment_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mall_Managment_System.Controllers
{
    [Authorize(Roles = "1")]
    public class ItemController : Controller
    {
        ApplicationDbContext Item_context;
        IWebHostEnvironment env;

        public ItemController(ApplicationDbContext item, IWebHostEnvironment hc)
        {
            this.Item_context = item;
            env = hc;
        }

        public IActionResult Index()
        {
            return View(Item_context.Items.ToList());
        }

        public IActionResult AddItem()
        {
            // Fetch the list of shops from the database
            var shops = Item_context.Shops.ToList();

            // Check if shops is null or empty
            if (shops == null || !shops.Any())
            {
                // Handle the case where no shops are found
                // For example, you might return an error view or an empty SelectList
                ViewBag.ShopId = new SelectList(Enumerable.Empty<SelectListItem>());
            }
            else
            {
                // Create a SelectList to pass to the view
                ViewBag.ShopId = new SelectList(shops, "ID", "Name");
            }

            // Return the view
            return View(new ItemViewModel()); // Ensure the model is instantiated
        }

        [HttpPost]
        public IActionResult AddItem(ItemViewModel item)
        {
            string filename = "";
            if (item.Photo != null)
            {
                string uploadfolder = Path.Combine(env.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + "_" + item.Photo.FileName;
                string filepath = Path.Combine(uploadfolder, filename);
                item.Photo.CopyTo(new FileStream(filepath, FileMode.Create));
            }
            Items I = new Items
            {
                ItemName=item.ItemName,
                Description=item.Description,
                ShopId=item.ShopId,
                Image=filename,
                Price=item.Price,
                
               
            };

            Item_context.Items.Add(I);
            Item_context.SaveChanges();
            ViewBag.success = "Recoard inserted";

            return RedirectToAction("index");
        }



        public IActionResult Edit(int id)
        {
            var item = Item_context.Items.Find(id);
            if (item == null)
            {
                return RedirectToAction("Index");
            }

            var ItemViewModel = new ItemViewModel
            {
              ItemName= item.ItemName,
              Description= item.Description,
              Price = item.Price,
              Shop=item.Shop,
              ShopId= item.ShopId
              

            };


            ViewData["Image"] = item.Image;


            return View(ItemViewModel);
        }


        [HttpPost]
        public IActionResult Edit(int id, ItemViewModel itemView)
        {
            var item = Item_context.Items.Find(id);
            if (item == null)
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                return View(itemView);
            }

            // Update the image file if a new image file is uploaded
            string newFileName = item.Image;
            if (itemView.Photo != null && itemView.Photo.Length > 0)
            {
                newFileName = Guid.NewGuid().ToString() + Path.GetExtension(itemView.Photo.FileName);
                string imageFullPath = Path.Combine(env.WebRootPath, "images", newFileName);

                // Save the new image
                using (var stream = new FileStream(imageFullPath, FileMode.Create))
                {
                    itemView.Photo.CopyTo(stream);
                }

                // Delete the old image if it exists
                if (!string.IsNullOrEmpty(item.Image))
                {
                    string oldImageFullPath = Path.Combine(env.WebRootPath, "images", item.Image);
                    if (System.IO.File.Exists(oldImageFullPath))
                    {
                        System.IO.File.Delete(oldImageFullPath);
                    }
                }
            }

            // Update the shop details
           item.Shop = itemView.Shop;
            item.ShopId= itemView.ShopId;
            item.ItemName = itemView.ItemName;
            item.Description= itemView.Description;
            item.Price = itemView.Price;
            item.Image = newFileName;


            Item_context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var item = Item_context.Items.Find(id);
            if (item == null)
            {
                return RedirectToAction("Index");
            }

            string ImageFullPath = env.WebRootPath + "/products" + item.Image;
            System.IO.File.Delete(ImageFullPath);


            Item_context.Items.Remove(item);
            Item_context.SaveChanges(true);
            return RedirectToAction("Index");
        }

    }
}
