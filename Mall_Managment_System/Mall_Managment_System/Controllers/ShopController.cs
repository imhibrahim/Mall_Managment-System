using Mall_Managment_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mall_Managment_System.Controllers
{
    public class ShopController : Controller
    {
        ShopDbContext shop_context;
        IWebHostEnvironment env;


        public ShopController(ShopDbContext shops, IWebHostEnvironment hc)
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
            return View();
        }
        [HttpPost]
        public IActionResult Addproduct(ShopViewModel shop)
        {
            string filename = "";
            if(shop.Photo != null)
            {
                string uploadfolder = Path.Combine(env.WebRootPath, "images");
                filename=Guid.NewGuid().ToString()+"_"+shop.Photo.FileName;
                string filepath=Path.Combine(uploadfolder, filename);   
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


    }
}
