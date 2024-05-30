using Mall_Managment_System.Migrations.GallaryDb;
using Mall_Managment_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mall_Managment_System.Controllers
{
    public class GalleryController : Controller
    {
        GallaryDbContext gallery_context;
        IWebHostEnvironment env;


        public GalleryController(GallaryDbContext gallery, IWebHostEnvironment hc)
        {
            this.gallery_context = gallery;
            env = hc;
        }

        public IActionResult Index()
        {
            return View(gallery_context.Gallary.ToList());
        }
        public IActionResult Addgallery()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Addgallery(GallaryViewModel gallary)
        {
            string filename = "";
            if (gallary.Photo != null)
            {
                string uploadfolder = Path.Combine(env.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + "_" + gallary.Photo.FileName;
                string filepath = Path.Combine(uploadfolder, filename);
                gallary.Photo.CopyTo(new FileStream(filepath, FileMode.Create));
            }
            Gallary G = new Gallary
            {
                Name=gallary.Name,
                Image=filename
               

            };

            gallery_context.Gallary.Add(G);
            gallery_context.SaveChanges();
            ViewBag.success = "Recoard inserted";

            return RedirectToAction("index");
        }

    }
}
