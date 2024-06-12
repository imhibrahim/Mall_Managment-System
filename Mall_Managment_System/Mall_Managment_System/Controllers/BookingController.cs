using Mall_Managment_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mall_Managment_System.Controllers
{
	public class BookingController : Controller
	{
		private readonly ApplicationDbContext Booking_context;
		private readonly IWebHostEnvironment env;


		public BookingController(ApplicationDbContext Booking, IWebHostEnvironment hc)
		{
			this.Booking_context = Booking;
			env = hc;
		}



		public IActionResult Index()
		{
			return View(Booking_context.Booking.ToList());
		}

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
