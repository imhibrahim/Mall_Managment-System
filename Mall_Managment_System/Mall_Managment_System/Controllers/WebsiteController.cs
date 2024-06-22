using Mall_Managment_System.Migrations.ContactDb;
using Mall_Managment_System.Migrations.ItemDb;
using Mall_Managment_System.Migrations.MovieDb;
using Mall_Managment_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;





namespace Mall_Managment_System.Controllers
{
	//[Authorize]
	public class WebsiteController : Controller
	{
		ApplicationDbContext user_context;
		IWebHostEnvironment env;
		


        public WebsiteController(ApplicationDbContext Contact, IWebHostEnvironment hc)
        {
            this.user_context = Contact;
            env = hc;
        }
       // [Authorize(Roles = "0")]
        public IActionResult Index()
		{
			return View();
		}

		public IActionResult About()
		{
			return View();
		}


      

        public IActionResult Movies()
		{
			var Movie = user_context.Movies.ToList();
			return View(Movie);
		}


		public IActionResult MovieDetails(int id)
		{

			var data = user_context.Movies.FirstOrDefault(x => x.Id == id);
			return View(data);
		}



        public IActionResult Booking(int id)
        {
            var movie = user_context.Movies.FirstOrDefault(x => x.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            // Pass the movie data using ViewBag
            ViewBag.Movie = movie;
            ViewBag.Email = HttpContext.User.Identity.Name; // Assuming you want to use the authenticated user's email

            return View();
        }

        [HttpPost]
        public IActionResult Booking(Booking booking)
        {

            Booking Booking = new Booking
            {
				Movie_Name = booking.Movie_Name,
				User_Email= booking.User_Email,
				Booking_Date=booking.Booking_Date,
				Booking_sets=booking.Booking_sets,
				Number_Tickets=1212121
            };

            user_context.Booking.Add(booking);
            user_context.SaveChanges();
            ViewBag.success = "Thank you for give the feedback";
            ModelState.Clear();
           
            return View();
        }








        public IActionResult Feedback()
		{

			return View();
		}


		[HttpPost]
        public IActionResult Feedback(Feedback feedback)
        {

            Feedback Feedback = new Feedback
            {
         //   UserId=feedback.UserId, 
			Environment=feedback.Environment,
			Rating=feedback.Rating,
			Message= feedback.Message,
			FeedbackDate= feedback.FeedbackDate

            };

            user_context.Feedback.Add(feedback);
            user_context.SaveChanges();
            ViewBag.success = "Thank you for give the feedback";
            ModelState.Clear();

            return View();
        }



		
        public IActionResult Gallary()
        {
            var galleryItems = user_context.Gallary.ToList();
            return View(galleryItems);
        }




		public IActionResult Shop(int id)
		{
        
			return View(user_context.Shops.ToList());
		}


		public IActionResult ShopDetails()
		{
		
			return View();
		}

		public IActionResult Shopitem()
		{
			var shopitem = user_context.Items.ToList();
			return View(shopitem);
		}

		public IActionResult FoodCourt()
		{
			var court = user_context.FoodCourt.ToList();
			return View(court);
		}
		public IActionResult Fooditem()
		{
			var fooditem = user_context.FoodItems.ToList();
			return View(fooditem);
		}



		public IActionResult Blog()
		{
			return View();
		}
		public IActionResult Contact()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Contact(Contact contact)
		{

			Contact Contact = new Contact
			{
				Name = contact.Name,
				Email = contact.Email,
				Number = contact.Number,
				Massage = contact.Massage


			};

			user_context.Add(Contact);
			user_context.SaveChanges();
			ViewBag.success = "Recoard inserted";

			return RedirectToAction("index","Website");

		}



		public IActionResult Register()
		{
			return View();
		}

		
		[HttpPost]
		public IActionResult Register(Users user)
		{
			Users User = new Users
			{
				FirstName = user.FirstName,
				LasttName=user.LasttName,
				Email=user.Email,
			Password=user.Password,
			PhoneNumber=user.PhoneNumber,
				UserActive = "1",
				Rolls= "0"

			};
			user_context.Users.Add(User);
			user_context.SaveChanges();
			ViewBag.success = "Register Successfully";

			return RedirectToAction("Login");



		}



		public IActionResult Login()
		{
			return View();
		}



		[HttpPost]
		public async Task<IActionResult> Login(Users user)
		{
			var dbUser = user_context.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);

			if (dbUser != null)
			{
				// Update UserLogin field to "1"
				dbUser.UserLogin = "1";
				user_context.SaveChanges();

				// Set session variables
				HttpContext.Session.SetString("UserEmail", dbUser.Email);
				HttpContext.Session.SetString("UserRole", dbUser.Rolls);
			
		

				// Create claims
				var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, dbUser.Email),
				new Claim(ClaimTypes.Role, dbUser.Rolls),
           
            };

				var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

				var authProperties = new AuthenticationProperties
				{
					IsPersistent = true
				};

				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

				// Redirect based on user role
				if (dbUser.Rolls == "1")
				{
					return RedirectToAction("Index", "Home"); // Admin Dashboard
				}
				if (dbUser.Rolls == "0")
				{
					return RedirectToAction("Index", "Website"); // User Website
				}
				else
				{
					return RedirectToAction("Register", "Website");
				}
			}
			else
			{
				ViewBag.error = "Email and Password Invalid";
			}

			return RedirectToAction("Register", "Website");
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			HttpContext.Session.Clear();
			return RedirectToAction("Login", "Website");
		}

	}
}
