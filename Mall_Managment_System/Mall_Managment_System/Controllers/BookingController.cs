using Mall_Managment_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mall_Managment_System.Controllers
{
    [Authorize(Roles = "1")]
    public class BookingController : Controller
	{
		private readonly ApplicationDbContext Booking_context;
		private readonly IWebHostEnvironment env;


		public BookingController(ApplicationDbContext Booking, IWebHostEnvironment hc)
		{
			this.Booking_context = Booking;
			env = hc;
		}


        [Authorize(Roles = "1")]
        public IActionResult Index()
        {
            return View(Booking_context.Booking.ToList());
        }

        public IActionResult details(int id)
        {

            var data = Booking_context.Booking.FirstOrDefault(x => x.Id == id);
            return View(data);
        }




        [Authorize(Roles = "1")]
        public IActionResult Delete(int id)
        {
            var booking = Booking_context.Booking.Find(id);
            if (booking == null)
            {
                return RedirectToAction("Index");
            }
            Booking_context.Booking.Remove(booking);
            Booking_context.SaveChanges(true);
            return RedirectToAction("Index");
        }
    }
}
