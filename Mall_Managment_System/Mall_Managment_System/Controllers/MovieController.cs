using Mall_Managment_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mall_Managment_System.Controllers
{
    [Authorize(Roles = "1")]
    public class MovieController : Controller
    {
        ApplicationDbContext Movie_context;
        IWebHostEnvironment env;

        public MovieController(ApplicationDbContext movies, IWebHostEnvironment hc)
        {
            this.Movie_context = movies;
            env = hc;
        }
        public IActionResult Index()
        {
            return View(Movie_context.Movies.ToList());
        }

        public IActionResult AddMovie()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddMovie(MovieViewModel movie)
        {
            string filename = "";
            if (movie.Photo != null)
            {
                string uploadfolder = Path.Combine(env.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + "_" + movie.Photo.FileName;
                string filepath = Path.Combine(uploadfolder, filename);
                movie.Photo.CopyTo(new FileStream(filepath, FileMode.Create));
            }
            Movies M = new Movies
            {
                MovieName = movie.MovieName,
                Description = movie.Description,
                Image = filename,
                TotalSeats = movie.TotalSeats,
                AvailableSeats = movie.AvailableSeats,
            };

            Movie_context.Movies.Add(M);
            Movie_context.SaveChanges();
            ViewBag.success = "Recoard inserted";

            return RedirectToAction("index");
        }


        public IActionResult Edit(int id)
        {
            var movie = Movie_context.Movies.Find(id);
            if (movie == null)
            {
                return RedirectToAction("Index");
            }

            var MovieViewModel = new MovieViewModel
            {
                Id= movie.Id,
              MovieName=movie.MovieName,
              Description=movie.Description,
               TotalSeats=movie.TotalSeats,
               AvailableSeats = movie.AvailableSeats
            };


            ViewData["Image"] = movie.Image;


            return View(MovieViewModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, MovieViewModel movieView)
        {
            var movie = Movie_context.Movies.Find(id);
            if (movie == null)
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                return View(movieView);
            }

            // Update the image file if a new image file is uploaded
            string newFileName = movie.Image;
            if (movieView.Photo != null && movieView.Photo.Length > 0)
            {
                newFileName = Guid.NewGuid().ToString() + Path.GetExtension(movieView.Photo.FileName);
                string imageFullPath = Path.Combine(env.WebRootPath, "images", newFileName);

                // Save the new image
                using (var stream = new FileStream(imageFullPath, FileMode.Create))
                {
                    movieView.Photo.CopyTo(stream);
                }

                // Delete the old image if it exists
                if (!string.IsNullOrEmpty(movie.Image))
                {
                    string oldImageFullPath = Path.Combine(env.WebRootPath, "images", movie.Image);
                    if (System.IO.File.Exists(oldImageFullPath))
                    {
                        System.IO.File.Delete(oldImageFullPath);
                    }
                }
            }

            // Update the shop details
            movie.MovieName = movieView.MovieName;
            movie.Description = movieView.Description;
            movie.TotalSeats = movieView.TotalSeats;
            movie.AvailableSeats= movieView.AvailableSeats;
            movie.Image = newFileName;

            Movie_context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var movie = Movie_context.Movies.Find(id);
            if (movie == null)
            {
                return RedirectToAction("Index");
            }

            string ImageFullPath = env.WebRootPath + "/products" + movie.Image;
            System.IO.File.Delete(ImageFullPath);


            Movie_context.Movies.Remove(movie);
            Movie_context.SaveChanges(true);
            return RedirectToAction("Index");
        }

    }
}
