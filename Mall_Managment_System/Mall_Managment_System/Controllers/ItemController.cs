using Mall_Managment_System.Models;
using Microsoft.AspNetCore.Mvc;

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
            return View();
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
