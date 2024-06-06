using Mall_Managment_System.Migrations.GallaryDb;
using Mall_Managment_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mall_Managment_System.Controllers
{
    public class GalleryController : Controller
    {
        ApplicationDbContext gallery_context;
        IWebHostEnvironment env;


        public GalleryController(ApplicationDbContext gallery, IWebHostEnvironment hc)
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

        public IActionResult Edit(int id)
        {
            var Gallery = gallery_context.Gallary.Find(id);
            if (Gallery == null)
            {
                return RedirectToAction("Index");
            }

            var GallaryViewModel = new GallaryViewModel
            {
                Id= Gallery.Id,
                Name = Gallery.Name,
              
            };


            ViewData["Image"] = Gallery.Image;


            return View(GallaryViewModel);
        }


        [HttpPost]
        public IActionResult Edit(int id, GallaryViewModel galleryview)
        {
            var gallery = gallery_context.Gallary.Find(id);
            if (gallery == null)
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                return View(galleryview);
            }

            // Update the image file if a new image file is uploaded
            string newFileName = gallery.Image;
            if (galleryview.Photo != null && galleryview.Photo.Length > 0)
            {
                newFileName = Guid.NewGuid().ToString() + Path.GetExtension(galleryview.Photo.FileName);
                string imageFullPath = Path.Combine(env.WebRootPath, "images", newFileName);

                // Save the new image
                using (var stream = new FileStream(imageFullPath, FileMode.Create))
                {
                    galleryview.Photo.CopyTo(stream);
                }

                // Delete the old image if it exists
                if (!string.IsNullOrEmpty(gallery.Image))
                {
                    string oldImageFullPath = Path.Combine(env.WebRootPath, "images", gallery.Image);
                    if (System.IO.File.Exists(oldImageFullPath))
                    {
                        System.IO.File.Delete(oldImageFullPath);
                    }
                }
            }

            // Update the shop details
           gallery.Name=galleryview.Name;
            gallery.Image=newFileName;

            gallery_context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var gallery = gallery_context.Gallary.Find(id);
            if (gallery == null)
            {
                return RedirectToAction("Index");
            }

            string ImageFullPath = env.WebRootPath + "/products" + gallery.Image;
            System.IO.File.Delete(ImageFullPath);


            gallery_context.Gallary.Remove(gallery);
            gallery_context.SaveChanges(true);
            return RedirectToAction("Index");
        }



  

    }
}
