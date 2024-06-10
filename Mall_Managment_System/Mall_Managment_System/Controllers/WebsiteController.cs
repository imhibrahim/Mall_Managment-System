using Mall_Managment_System.Migrations.ContactDb;
using Mall_Managment_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Mall_Managment_System.Controllers
{
	//[Authorize(Roles = "0")]
	public class WebsiteController : Controller
	{
		ApplicationDbContext user_context;
		IWebHostEnvironment env;
		


        public WebsiteController(ApplicationDbContext Contact, IWebHostEnvironment hc)
        {
            this.user_context = Contact;
            env = hc;
        }
        public IActionResult Index()
		{
			return View();
		}

		public IActionResult About()
		{
			return View();
		}

		public IActionResult Blog()
		{
			return View();
		}
		public IActionResult Contact()
		{
			return View();
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
		public IActionResult Login(Users user)
		{
			if (ModelState.IsValid)
			{
				var dbUser = user_context.Users
					.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);


				if (dbUser != null)
				{

					// Update UserLogin field to "1"
					dbUser.UserLogin = "1";
					user_context.SaveChanges();

					// Log user role for debugging
					Console.WriteLine($"User {dbUser.Email} logged in with role {dbUser.Rolls}");

					// Redirect based on user role
					if (dbUser.Rolls == "1")
					{
						return RedirectToAction("Index", "Home"); // Admin Dashboard
					}
					else if (dbUser.Rolls == "0")
					{
						return RedirectToAction("Index", "Website"); // User Website
					}
					else
					{
						return RedirectToAction("Register", "Website"); // Default action for other roles
					}
				}
				else
				{
					ModelState.AddModelError("error", "Invalid email or password");
				}
			}
			else
			{
				// Log model state errors for debugging
				foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
				{
					Console.WriteLine($"Model Error: {error.ErrorMessage}");
				}
			}

			// If we got this far, something failed, redisplay form
			return RedirectToAction("index","Home");
		}



	}
}
