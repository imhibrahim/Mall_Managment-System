using Mall_Managment_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mall_Managment_System.Controllers
{
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


    }
}
