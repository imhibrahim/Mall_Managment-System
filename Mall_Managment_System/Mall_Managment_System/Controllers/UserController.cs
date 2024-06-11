using Mall_Managment_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mall_Managment_System.Controllers
{
	public class UserController : Controller
	{
		ApplicationDbContext User_Context;
		IWebHostEnvironment env;


		public UserController(ApplicationDbContext users, IWebHostEnvironment hc)
		{
			this.User_Context = users;
			env = hc;
		}


		public IActionResult Index()
		{
			return View(User_Context.Users.ToList());
		}


        public IActionResult details(int id)
        {

            var data = User_Context.Users.FirstOrDefault(x => x.Userid == id);
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var user = User_Context.Users.Find(id);
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            User_Context.Users.Remove(user);
            User_Context.SaveChanges(true);
            return RedirectToAction("Index");
        }

    }
}
