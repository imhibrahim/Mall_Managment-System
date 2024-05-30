using Mall_Managment_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mall_Managment_System.Controllers
{
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
    }
}
